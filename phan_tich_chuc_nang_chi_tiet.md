# Phân tích Kỹ thuật Chi tiết 4 Phân hệ Cốt lõi

Dựa trên việc đánh giá trực tiếp mã nguồn (Source Code) của hệ thống, dưới đây là bản phân tích kỹ thuật chuyên sâu về 4 phân hệ cốt lõi. Tài liệu này đặc biệt hữu ích khi viết báo cáo phần **"Phân tích thiết kế và Công nghệ áp dụng"**.

---

## 1. Phân hệ Danh sách Công việc (TasksList.vue & TaskDetailModal.vue)

Đây là module quản lý vi mô với khối lượng logic đồ sộ nhất, kết hợp giữa hiển thị danh sách đa dạng và thao tác chi tiết mượt mà.

**Điểm nhấn Kỹ thuật trong Source Code:**
* **Kiến trúc State Management:** Dữ liệu được quản lý tập trung bằng `Pinia` (`taskStore`), đảm bảo dữ liệu luôn đồng bộ (Reactive) giữa màn hình danh sách, màn hình lịch và màn hình Kanban.
* **Chuyển đổi View Mode động:** Cung cấp 2 chế độ hiển thị: Dạng thẻ (List View) và Dạng lịch (Calendar View) thông qua directive `v-if/v-else-if`, giúp tái sử dụng state mà không cần load lại trang. 
* **Thuật toán Render Lịch (Calendar View):** Thuật toán tự động tính toán số ngày trong tháng (`daysInMonth`), ngày bắt đầu của tháng (`firstDay.getDay()`), và điền đầy đủ các ô ngày trống để tạo thành một ma trận lưới 7x6 hoàn hảo. Hệ thống cũng xử lý triệt để vấn đề Timezone (Múi giờ) bằng cách trừ đi `getTimezoneOffset()` để so sánh ngày chính xác.
* **Export Dữ liệu (Báo cáo):** Tích hợp xử lý Blob Object để xuất file CSV và thư viện `jspdf` kết hợp `jspdf-autotable` để tự động render file PDF báo cáo danh sách công việc. 
* **UI/UX:** Thiết kế theo xu hướng Glassmorphism (Kính mờ) với các lớp `bg-white/60`, `backdrop-blur`, cùng các hiệu ứng phản quang (Deco Glow) chạy bằng CSS thuần (Tailwind).

---

## 2. Phân hệ Tiến độ Dự án (GanttChart.vue)

Thay vì phụ thuộc vào các thư viện biểu đồ cồng kềnh (như DhtmlxGantt), biểu đồ này được code thuần túy bằng Vue 3 kết hợp Tailwind CSS.

**Điểm nhấn Kỹ thuật trong Source Code:**
* **Thuật toán định tuyến thời gian (Timeline Engine):** 
  * Tự động quét toàn bộ `tasks` để tìm ra `minDate` (ngày sớm nhất) và `maxDate` (ngày xa nhất), sau đó cộng/trừ thêm biên độ (padding) để biểu đồ luôn có không gian thở.
  * Tính toán vị trí tương đối tuyệt đối (Absolute Positioning): Hàm `getTaskPosition()` nội suy khoảng cách của thanh công việc dựa vào chênh lệch (diffTime) giữa ngày bắt đầu task và ngày bắt đầu dự án, sau đó nhân với `dayWidth` (40px).
* **Xử lý Ngoại lệ (Edge Cases):**
  * **Công việc chưa có Due Date:** Hệ thống tự động gán độ dài ảo là 3 ngày.
  * **Công việc siêu ngắn (1 Ngày):** Khối màu (width < 100px) tự động giấu chữ vào bên trong và đẩy tên công việc ra bên phải CSS (`left: width + 8px`) để không bị vỡ giao diện.
* **Chống lỗi Hình ảnh (Avatar Fallback):** Code xử lý `@error` trên thẻ `<img>`. Nếu avatar nhân sự bị lỗi 404, lập tức thay thế bằng ảnh tạo từ `ui-avatars.com` dựa trên tên nhân sự (`fullName`).
* **Đồng bộ thanh cuộn (Sync Scroll):** Đồng bộ hóa trục Y giữa danh sách tên công việc bên trái và lưới sơ đồ bên phải thông qua `e.target.scrollTop`.

---

## 3. Phân hệ Thống kê & Báo cáo (Analytics.vue & Dashboard.vue)

Module phục vụ việc ra quyết định cho Ban Quản trị thông qua các biểu đồ phân tích dữ liệu trực quan.

**Điểm nhấn Kỹ thuật trong Source Code:**
* **Tính toán Năng lực (Resource Allocation):** 
  * Sử dụng cấu trúc dữ liệu `Map<string, Object>` để gom nhóm (Group-by) các công việc đang làm (Active) và trễ hạn (Overdue) theo từng `assigneeId`.
  * Hiển thị cảnh báo trực quan: Tính toán phần trăm khối lượng công việc (giả định 1 người gánh tối đa 10 tasks = 100%). Nếu có task trễ hạn, thanh Progress sẽ xuất hiện dải màu Đỏ cảnh báo.
* **Bảng vàng Năng suất (Leaderboard):** Thuật toán sắp xếp mảng dữ liệu (Array sort) động dựa trên biểu thức `completed - a.completed` để tự động vinh danh 3 cá nhân có hiệu suất tốt nhất dự án.
* **Tích hợp Chart.js:** 
  * Ứng dụng `vue-chartjs` để nhúng các biểu đồ Doughnut (Tổng quan trạng thái) và Bar Chart (Stacked Bar cho Khối lượng công việc).
  * Biểu đồ Bar Chart được cấu hình `stacked: true` để cộng dồn các cột Hoàn thành, Đang làm và Trễ hạn trên cùng một cột biểu đồ của mỗi cá nhân, tối ưu hóa không gian.

---

## 4. Phân hệ Quản lý Tài liệu (Wiki.vue)

Phân hệ đóng vai trò là một Wiki / Knowledge Base nội bộ của toàn dự án.

**Điểm nhấn Kỹ thuật trong Source Code:**
* **Soạn thảo Rich-text:** Tích hợp thành công `Vue Quill Editor`. Toàn bộ nội dung tài liệu được mã hóa và lưu trữ dưới dạng mã HTML.
* **Hiển thị HTML An toàn:** Sử dụng directive `v-html` của Vue để dịch ngược nội dung HTML. Kết hợp với class `prose` (Typography plugin của Tailwind) để văn bản được tự động định dạng chuẩn SEO (Heading, List, Blockquote) cực kỳ đẹp mắt mà không cần viết CSS thủ công.
* **Thiết kế Thẻ tài liệu (Document Cards):** Thay vì bảng danh sách nhàm chán, tài liệu được hiển thị theo dạng Thẻ (Grid). Áp dụng kỹ thuật CSS Gradient Mask (`bg-gradient-to-t from-white to-transparent`) ở phần chân thẻ để làm mờ dần các đoạn văn bản quá dài, kết hợp với `line-clamp-5` để cố định chiều cao.
* **Quản lý State tập trung:** Các thao tác Thêm/Sửa/Xóa tài liệu gọi trực tiếp qua `apiService` và được xử lý theo luồng `async/await`, cập nhật tức thì biến `documents` để Vue tự động render lại (Re-activity).
