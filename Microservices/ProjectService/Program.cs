using System.Data;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var jwtSecret = builder.Configuration["Jwt:Secret"] ?? "ProjectHub.Shared.Secret.Key.For.Student.Microservices.2026!";
var issuer = builder.Configuration["Jwt:Issuer"] ?? "ProjectHub";
var audience = builder.Configuration["Jwt:Audience"] ?? "ProjectHub.Client";

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = TokenValidation(jwtSecret, issuer, audience));
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();

var db = new SqlDb(app.Configuration.GetConnectionString("ProjectDb")!);
await db.EnsureDatabaseAsync("ProjectDB");
await EnsureSchemaAsync(db);

app.MapGet("/health", () => Results.Ok(new { service = "ProjectService", status = "ok" }));

app.MapGet("/api/projects", async () =>
{
    await using var conn = await db.OpenAsync();
    var projects = await QueryAsync<ProjectRow>(conn,
        "SELECT id, name, description, status, statusText, progress, color, createdAt FROM Projects ORDER BY createdAt DESC");
    var members = await QueryAsync<ProjectMemberRow>(conn,
        "SELECT projectId, userId, role FROM ProjectMembers");
    var result = projects.Select(project => new ProjectDto(
        project.Id,
        project.Name,
        project.Description,
        project.Status,
        project.StatusText,
        project.Progress,
        project.Color,
        project.CreatedAt,
        members.Where(m => m.ProjectId == project.Id)
            .Select(m => new MemberDto(m.UserId, m.UserId, "", m.Role, true, ""))
            .ToList()));
    return Results.Ok(result);
}).RequireAuthorization();

app.MapPost("/api/projects", async (ProjectCreateRequest request, ClaimsPrincipal principal) =>
{
    if (!IsManager(principal)) return Results.Forbid();
    var id = "p_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    var createdAt = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd");
    await using var conn = await db.OpenAsync();
    await ExecuteAsync(conn,
        """
        INSERT INTO Projects(id, name, description, status, statusText, progress, color, createdAt)
        VALUES(@id,@name,@description,@status,@statusText,0,@color,@createdAt)
        """,
        P("@id", id), P("@name", request.Name), P("@description", request.Description),
        P("@status", request.Status), P("@statusText", request.StatusText),
        P("@color", request.Color), P("@createdAt", createdAt));

    foreach (var member in request.Members ?? [])
    {
        await ExecuteAsync(conn,
            "INSERT INTO ProjectMembers(projectId, userId, role) VALUES(@projectId,@userId,@role)",
            P("@projectId", id), P("@userId", member.Id), P("@role", string.IsNullOrWhiteSpace(member.Role) ? "Member" : member.Role));
    }

    var dto = new ProjectDto(id, request.Name, request.Description, request.Status, request.StatusText, 0, request.Color, createdAt, request.Members ?? []);
    return Results.Created($"/api/projects/{id}", dto);
}).RequireAuthorization();

app.MapPut("/api/projects/{id}/progress", async (string id, ProjectProgressRequest request, ClaimsPrincipal principal) =>
{
    if (!IsManager(principal)) return Results.Forbid();
    await using var conn = await db.OpenAsync();
    await ExecuteAsync(conn, "UPDATE Projects SET progress=@progress WHERE id=@id", P("@progress", request.Progress), P("@id", id));
    return Results.Ok(new { message = "Cập nhật tiến độ thành công" });
}).RequireAuthorization();

app.MapPut("/api/projects/{id}/members", async (string id, ProjectMembersRequest request, ClaimsPrincipal principal) =>
{
    if (!IsManager(principal)) return Results.Forbid();
    await using var conn = await db.OpenAsync();
    await ExecuteAsync(conn, "DELETE FROM ProjectMembers WHERE projectId=@projectId", P("@projectId", id));
    foreach (var userId in request.Members.Distinct())
    {
        await ExecuteAsync(conn,
            "INSERT INTO ProjectMembers(projectId, userId, role) VALUES(@projectId,@userId,'Member')",
            P("@projectId", id), P("@userId", userId));
    }
    return Results.Ok(new { message = "Cập nhật thành viên thành công" });
}).RequireAuthorization();

app.MapPost("/api/projects/{id}/sprints", async (string id, SprintCreateRequest request, ClaimsPrincipal principal) =>
{
    if (!IsManager(principal)) return Results.Forbid();
    var sprintId = "sp_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    await using var conn = await db.OpenAsync();
    await ExecuteAsync(conn,
        "INSERT INTO Sprints(id, projectId, name, goal, startDate, endDate) VALUES(@id,@projectId,@name,@goal,@startDate,@endDate)",
        P("@id", sprintId), P("@projectId", id), P("@name", request.Name), P("@goal", request.Goal),
        P("@startDate", request.StartDate), P("@endDate", request.EndDate));
    return Results.Created($"/api/projects/{id}/sprints/{sprintId}", new { id = sprintId, projectId = id, request.Name, request.Goal, request.StartDate, request.EndDate });
}).RequireAuthorization();

app.MapGet("/api/projects/{id}/documents", async (string id, ClaimsPrincipal principal) =>
{
    await using var conn = await db.OpenAsync();
    var docs = await QueryAsync<ProjectDocumentRow>(conn, "SELECT * FROM ProjectDocuments WHERE projectId=@projectId ORDER BY updatedAt DESC", P("@projectId", id));
    return Results.Ok(docs);
}).RequireAuthorization();

app.MapPost("/api/projects/{id}/documents", async (string id, ProjectDocumentCreateRequest request, ClaimsPrincipal principal) =>
{
    var docId = "doc_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    var authorId = principal.FindFirstValue(ClaimTypes.NameIdentifier) ?? "unknown";
    var now = DateTimeOffset.UtcNow.ToString("O");
    await using var conn = await db.OpenAsync();
    await ExecuteAsync(conn,
        "INSERT INTO ProjectDocuments(id, projectId, title, content, authorId, createdAt, updatedAt) VALUES(@id,@projectId,@title,@content,@authorId,@createdAt,@updatedAt)",
        P("@id", docId), P("@projectId", id), P("@title", request.Title), P("@content", request.Content),
        P("@authorId", authorId), P("@createdAt", now), P("@updatedAt", now));
    return Results.Created($"/api/projects/{id}/documents/{docId}", new { id = docId, projectId = id, request.Title, request.Content, authorId, createdAt = now, updatedAt = now });
}).RequireAuthorization();

app.MapPut("/api/projects/{id}/documents/{docId}", async (string id, string docId, ProjectDocumentCreateRequest request, ClaimsPrincipal principal) =>
{
    var now = DateTimeOffset.UtcNow.ToString("O");
    await using var conn = await db.OpenAsync();
    await ExecuteAsync(conn,
        "UPDATE ProjectDocuments SET title=@title, content=@content, updatedAt=@updatedAt WHERE id=@id AND projectId=@projectId",
        P("@title", request.Title), P("@content", request.Content), P("@updatedAt", now), P("@id", docId), P("@projectId", id));
    return Results.Ok(new { message = "Cập nhật tài liệu thành công" });
}).RequireAuthorization();

app.MapDelete("/api/projects/{id}/documents/{docId}", async (string id, string docId, ClaimsPrincipal principal) =>
{
    await using var conn = await db.OpenAsync();
    await ExecuteAsync(conn, "DELETE FROM ProjectDocuments WHERE id=@id AND projectId=@projectId", P("@id", docId), P("@projectId", id));
    return Results.Ok(new { message = "Xóa tài liệu thành công" });
}).RequireAuthorization();

app.Run();

static TokenValidationParameters TokenValidation(string secret, string issuer, string audience) => new()
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = issuer,
    ValidAudience = audience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
    ClockSkew = TimeSpan.Zero
};

static bool IsManager(ClaimsPrincipal user)
{
    var role = user.FindFirstValue(ClaimTypes.Role);
    return role is "Admin" or "Project Manager";
}

static async Task EnsureSchemaAsync(SqlDb db)
{
    await using var conn = await db.OpenAsync();
    await ExecuteAsync(conn,
        """
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Projects' AND xtype='U')
        CREATE TABLE Projects(
            id NVARCHAR(50) PRIMARY KEY,
            name NVARCHAR(255) NOT NULL,
            description NVARCHAR(MAX),
            status NVARCHAR(50),
            statusText NVARCHAR(100),
            progress INT DEFAULT 0,
            color NVARCHAR(50),
            createdAt NVARCHAR(50)
        );
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ProjectMembers' AND xtype='U')
        CREATE TABLE ProjectMembers(
            projectId NVARCHAR(50) NOT NULL,
            userId NVARCHAR(50) NOT NULL,
            role NVARCHAR(50) DEFAULT 'Member',
            PRIMARY KEY(projectId, userId)
        );
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Sprints' AND xtype='U')
        CREATE TABLE Sprints(
            id NVARCHAR(50) PRIMARY KEY,
            projectId NVARCHAR(50) NOT NULL,
            name NVARCHAR(255) NOT NULL,
            goal NVARCHAR(MAX),
            startDate NVARCHAR(50),
            endDate NVARCHAR(50)
        );
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ProjectDocuments' AND xtype='U')
        CREATE TABLE ProjectDocuments(
            id NVARCHAR(50) PRIMARY KEY,
            projectId NVARCHAR(50) NOT NULL,
            title NVARCHAR(255) NOT NULL,
            content NVARCHAR(MAX),
            authorId NVARCHAR(50) NOT NULL,
            createdAt NVARCHAR(50),
            updatedAt NVARCHAR(50)
        );
        """);
}

static async Task<List<T>> QueryAsync<T>(SqlConnection conn, string sql, params SqlParameter[] parameters)
{
    await using var cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddRange(parameters);
    await using var reader = await cmd.ExecuteReaderAsync();
    var list = new List<T>();
    while (await reader.ReadAsync()) list.Add(Map<T>(reader));
    return list;
}

static async Task ExecuteAsync(SqlConnection conn, string sql, params SqlParameter[] parameters)
{
    await using var cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddRange(parameters);
    await cmd.ExecuteNonQueryAsync();
}

static SqlParameter P(string name, object? value) => new(name, value ?? DBNull.Value);

static T Map<T>(IDataRecord row)
{
    object? Get(string name) => row[name] == DBNull.Value ? null : row[name];
    if (typeof(T) == typeof(ProjectRow))
        return (T)(object)new ProjectRow(Get("id")!.ToString()!, Get("name")!.ToString()!, Get("description")?.ToString() ?? "", Get("status")?.ToString() ?? "New", Get("statusText")?.ToString() ?? "", Convert.ToInt32(Get("progress") ?? 0), Get("color")?.ToString() ?? "indigo", Get("createdAt")?.ToString() ?? "");
    if (typeof(T) == typeof(ProjectMemberRow))
        return (T)(object)new ProjectMemberRow(Get("projectId")!.ToString()!, Get("userId")!.ToString()!, Get("role")?.ToString() ?? "Member");
    if (typeof(T) == typeof(ProjectDocumentRow))
        return (T)(object)new ProjectDocumentRow(Get("id")!.ToString()!, Get("projectId")!.ToString()!, Get("title")!.ToString()!, Get("content")?.ToString() ?? "", Get("authorId")!.ToString()!, Get("createdAt")?.ToString() ?? "", Get("updatedAt")?.ToString() ?? "");
    throw new NotSupportedException(typeof(T).Name);
}

sealed class SqlDb(string connectionString)
{
    public async Task EnsureDatabaseAsync(string databaseName)
    {
        var master = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master" }.ConnectionString;
        await using var conn = new SqlConnection(master);
        await conn.OpenAsync();
        await using var cmd = new SqlCommand($"IF DB_ID(N'{databaseName.Replace("'", "''")}') IS NULL CREATE DATABASE [{databaseName}]", conn);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<SqlConnection> OpenAsync()
    {
        var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        return conn;
    }
}

record ProjectCreateRequest(string Name, string Description, string Status, string StatusText, string Color, List<MemberDto>? Members);
record ProjectProgressRequest(int Progress);
record ProjectMembersRequest(List<string> Members);
record SprintCreateRequest(string Name, string Goal, string StartDate, string EndDate);
record ProjectDocumentCreateRequest(string Title, string Content);
record ProjectRow(string Id, string Name, string Description, string Status, string StatusText, int Progress, string Color, string CreatedAt);
record ProjectMemberRow(string ProjectId, string UserId, string Role);
record ProjectDocumentRow(string Id, string ProjectId, string Title, string Content, string AuthorId, string CreatedAt, string UpdatedAt);
record ProjectDto(string Id, string Name, string Description, string Status, string StatusText, int Progress, string Color, string CreatedAt, List<MemberDto> Members);
record MemberDto(string Id, string FullName, string AvatarUrl, string Role, bool IsOnline, string Email);
