<template>
  <div class="landing" :class="{ 'light-theme': isLightMode }">
    <!-- PARTICLES BACKGROUND -->
    <canvas ref="particleCanvas" class="particle-canvas"></canvas>

    <!-- NAV -->
    <nav class="nav" :class="{ scrolled: isScrolled }">
      <div class="nav-inner">
        <div class="nav-logo">
          <div class="logo-icon">
            <svg width="22" height="22" viewBox="0 0 24 24" fill="none">
              <rect x="3" y="3" width="8" height="8" rx="2" fill="white" opacity="0.9"/>
              <rect x="13" y="3" width="8" height="8" rx="2" fill="white" opacity="0.6"/>
              <rect x="3" y="13" width="8" height="8" rx="2" fill="white" opacity="0.6"/>
              <rect x="13" y="13" width="8" height="8" rx="2" fill="white" opacity="0.9"/>
            </svg>
          </div>
          <span class="logo-text">SprintFlow</span>
        </div>
        <div class="nav-links">
          <a href="#features" class="nav-link">{{ tNav.features }}</a>
          <a href="#how" class="nav-link">{{ tNav.how }}</a>
          <a href="#stats" class="nav-link">{{ tNav.stats }}</a>
          
          <div class="nav-lang-dropdown" @click="langMenuOpen = !langMenuOpen" @blur="langMenuOpen = false" tabindex="0">
            <span class="current-lang">
              {{ currentLang === 'vi' ? '🇻🇳 VN' : '🇺🇸 EN' }}
            </span>
            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M6 9l6 6 6-6"/></svg>
            <transition name="fade">
              <div class="lang-menu" v-show="langMenuOpen">
                <div class="lang-item" :class="{active: currentLang === 'vi'}" @click.stop="setLang('vi')">🇻🇳 Tiếng Việt</div>
                <div class="lang-item" :class="{active: currentLang === 'en'}" @click.stop="setLang('en')">🇺🇸 English</div>
              </div>
            </transition>
          </div>

          
          <div class="theme-toggle" @click="toggleTheme" style="cursor:pointer; display:flex; align-items:center; justify-content:center; width:36px; height:36px; border-radius:50%; background:rgba(255,255,255,0.1); margin-right:10px;">
            <svg v-if="!isLightMode" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M12 3a6 6 0 0 0 9 9 9 9 0 1 1-9-9Z"/></svg>
            <svg v-else width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="12" cy="12" r="4"/><path d="M12 2v2"/><path d="M12 20v2"/><path d="m4.93 4.93 1.41 1.41"/><path d="m17.66 17.66 1.41 1.41"/><path d="M2 12h2"/><path d="M20 12h2"/><path d="m6.34 17.66-1.41 1.41"/><path d="m19.07 4.93-1.41 1.41"/></svg>
          </div>
          <router-link to="/login" class="btn-nav-login">{{ tNav.login }}</router-link>

          <router-link to="/register" class="btn-nav-register">
            {{ tNav.register }}
            <span class="btn-shimmer"></span>
          </router-link>
        </div>
      </div>
    </nav>

    <!-- HERO -->
    <section class="hero">
      <div class="hero-bg">
        <div class="hero-orb orb-1"></div>
        <div class="hero-orb orb-2"></div>
        <div class="hero-orb orb-3"></div>
        <div class="hero-grid"></div>
      </div>
      <div class="hero-content reveal-left">
        <div class="hero-badge">
          <span class="badge-dot"></span>
          {{ currentLang === 'vi' ? 'Quản lý dự án thông minh · Microservices Architecture' : 'Smart Project Management · Microservices Architecture' }}
        </div>
        <h1 class="hero-title" v-if="currentLang === 'vi'">
          Quản lý dự án<br/>
          <span class="hero-gradient typing-text">{{ typedText }}<span class="cursor">|</span></span><br/>
          cùng SprintFlow
        </h1>
        <h1 class="hero-title" v-else>
          Project Management<br/>
          <span class="hero-gradient typing-text">{{ typedText }}<span class="cursor">|</span></span><br/>
          with SprintFlow
        </h1>
        <p class="hero-desc">
          {{ currentLang === 'vi' ? 'Nền tảng quản lý công việc toàn diện — từ lên kế hoạch sprint, phân công task, theo dõi tiến độ đến thông báo thời gian thực. Tất cả trong một giao diện đẹp mắt.' : 'A comprehensive project management platform — from sprint planning, task assignment, progress tracking to real-time notifications. All in a beautiful interface.' }}
        </p>
        <div class="hero-actions">
          <router-link to="/register" class="btn-primary magnetic">
            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M13 2L3 14h9l-1 8 10-12h-9l1-8z"/></svg>
            {{ tNav.register }}
            <span class="btn-shimmer"></span>
          </router-link>
          <router-link to="/login" class="btn-ghost">
            Đã có tài khoản →
          </router-link>
        </div>
        <div class="hero-stats-new reveal-up" style="--delay: 0.3s">
          <div class="stat-pill">
            <div class="stat-icon" style="color: #6366f1"><svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M22 19a2 2 0 01-2 2H4a2 2 0 01-2-2V5a2 2 0 012-2h5l2 3h9a2 2 0 012 2z"/></svg></div>
            <div>
              <div class="stat-val">{{ counters.services }}</div>
              <div class="stat-desc">Microservices</div>
            </div>
          </div>
          <div class="stat-pill">
            <div class="stat-icon" style="color: #ec4899"><svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="3" y="11" width="18" height="11" rx="2"/><path d="M7 11V7a5 5 0 0110 0v4"/></svg></div>
            <div>
              <div class="stat-val">JWT</div>
              <div class="stat-desc">Bảo mật</div>
            </div>
          </div>
          <div class="stat-pill">
            <div class="stat-icon" style="color: #10b981"><svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polyline points="22 12 18 12 15 21 9 3 6 12 2 12"/></svg></div>
            <div>
              <div class="stat-val">{{ counters.tasks }}+</div>
              <div class="stat-desc">Tính năng</div>
            </div>
          </div>
        </div>
      </div>

      <!-- Dashboard preview -->
      <div class="hero-preview reveal-right">
        <div class="preview-card">
          <div class="preview-header">
            <div class="preview-dots">
              <span class="dot red"></span>
              <span class="dot yellow"></span>
              <span class="dot green"></span>
            </div>
            <span class="preview-title-bar">SprintFlow — Dashboard</span>
          </div>
          <div class="preview-body">
            <div class="preview-sidebar">
              <div class="ps-item active">
                <svg width="14" height="14" viewBox="0 0 24 24" fill="currentColor"><rect x="3" y="3" width="7" height="7" rx="1"/><rect x="14" y="3" width="7" height="7" rx="1"/><rect x="3" y="14" width="7" height="7" rx="1"/><rect x="14" y="14" width="7" height="7" rx="1"/></svg>
                Dashboard
              </div>
              <div class="ps-item">
                <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M22 19a2 2 0 01-2 2H4a2 2 0 01-2-2V5a2 2 0 012-2h5l2 3h9a2 2 0 012 2z"/></svg>
                Dự án
              </div>
              <div class="ps-item">
                <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="3" y="4" width="18" height="18" rx="2"/><line x1="16" y1="2" x2="16" y2="6"/><line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/></svg>
                Kanban
              </div>
              <div class="ps-item notify-item">
                <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M18 8A6 6 0 006 8c0 7-3 9-3 9h18s-3-2-3-9M13.73 21a2 2 0 01-3.46 0"/></svg>
                Thông báo
                <span class="badge-count">3</span>
              </div>
            </div>
            <div class="preview-main">
              <div class="pm-title">Tổng quan</div>
              <div class="pm-cards">
                <div class="pm-card blue">
                  <div class="pm-card-num">12</div>
                  <div class="pm-card-label">Dự án</div>
                </div>
                <div class="pm-card purple">
                  <div class="pm-card-num">48</div>
                  <div class="pm-card-label">Task</div>
                </div>
                <div class="pm-card green">
                  <div class="pm-card-num">85%</div>
                  <div class="pm-card-label">Hoàn thành</div>
                </div>
              </div>
              <div class="pm-tasks">
                <div class="pm-task-row">
                  <span class="task-dot done"></span>
                  <span class="task-text">Thiết kế UI Dashboard</span>
                  <span class="task-badge done-badge">Xong</span>
                </div>
                <div class="pm-task-row">
                  <span class="task-dot progress"></span>
                  <span class="task-text">API Gateway integration</span>
                  <span class="task-badge prog-badge">Đang làm</span>
                </div>
                <div class="pm-task-row">
                  <span class="task-dot todo"></span>
                  <span class="task-text">Viết unit tests</span>
                  <span class="task-badge todo-badge">Chờ</span>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="float-card float-1">
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="#6366f1" stroke-width="2"><path d="M18 8A6 6 0 006 8c0 7-3 9-3 9h18s-3-2-3-9"/></svg>
          <div>
            <div class="fc-title">Thông báo mới</div>
            <div class="fc-sub">Minh đã comment vào task của bạn</div>
          </div>
        </div>
        <div class="float-card float-2">
          <div class="fc-progress-label">Sprint progress</div>
          <div class="fc-progress-bar">
            <div class="fc-progress-fill"></div>
          </div>
          <div class="fc-progress-pct">72%</div>
        </div>
      </div>
    </section>

    <!-- MARQUEE STRIP -->
    <div class="marquee-wrap">
      <div class="marquee-track">
        <span v-for="tag in [...marquee, ...marquee]" :key="Math.random()" class="marquee-item">
          <svg width="14" height="14" viewBox="0 0 24 24" fill="#6366f1"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
          {{ tag }}
        </span>
      </div>
    </div>

    <!-- FEATURES -->
    <section id="features" class="features features-stack">
      <div class="section-badge reveal-up">{{ tNav.features }}</div>
      <h2 class="section-title reveal-up" style="--delay: 0.1s" v-if="currentLang === 'vi'">Mọi thứ bạn cần để<br/><span class="text-gradient">quản lý dự án hiệu quả</span></h2>
      <h2 class="section-title reveal-up" style="--delay: 0.1s" v-else>Everything you need for<br/><span class="text-gradient">efficient project management</span></h2>
      <p class="section-sub reveal-up" style="--delay: 0.2s">{{ currentLang === 'vi' ? 'Trải nghiệm mượt mà với các tính năng được xếp chồng thông minh.' : 'Seamless experience with smart stacked features.' }}</p>

      <div class="stack-container">
        <div v-for="(feat, i) in features" :key="i" class="stack-card" :style="{ top: `calc(15vh + ${i * 40}px)` }" :data-index="i">
          <div class="card-inner" :style="{ background: feat.bg, boxShadow: `0 -10px 40px ${feat.glow}` }">
            <div class="card-content">
              <div class="feat-icon" :style="{ background: feat.glow }">
                <span v-html="feat.icon"></span>
              </div>
              <h3 class="feat-title">{{ feat.title }}</h3>
              <p class="feat-desc">{{ feat.desc }}</p>
              <ul class="feat-list">
                <li v-for="item in feat.items" :key="item">{{ item }}</li>
              </ul>
            </div>
            <div class="card-image-wrap">
              <div class="feat-preview-html" v-html="feat.mockupHtml" style="width:100%; height:100%; display:flex; align-items:center; justify-content:center;"></div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- HOW IT WORKS -->
    <section id="how" class="how">
      <div class="how-inner">
        <div class="section-badge reveal-up">{{ tNav.how }}</div>
        <h2 class="section-title reveal-up" style="--delay: 0.1s" v-if="currentLang === 'vi'">Bắt đầu chỉ trong<br/><span class="text-gradient">3 bước đơn giản</span></h2>
        <h2 class="section-title reveal-up" style="--delay: 0.1s" v-else>Get started in<br/><span class="text-gradient">3 simple steps</span></h2>
        <div class="steps-vertical">
          <div class="step-v reveal-slide-right" v-for="(step, i) in steps" :key="i" :style="{ '--delay': (i * 0.2 + 0.1) + 's' }">
            <div class="step-v-icon-wrap">
              <div class="step-v-num">{{ i + 1 }}</div>
              <div class="step-v-icon" :style="{ background: step.bg, boxShadow: `0 0 30px ${step.glow || 'rgba(99,102,241,0.3)'}` }">
                <span v-html="step.icon"></span>
              </div>
            </div>
            <div class="step-v-content">
              <h3 class="step-v-title">{{ step.title }}</h3>
              <p class="step-v-desc">{{ step.desc }}</p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- STATS/SERVICES -->
    <section id="stats" class="stats-section">
      <div class="stats-bg">
        <div class="stats-orb"></div>
      </div>
      <div class="stats-inner">
        <div class="section-badge light reveal-up">Kiến trúc hệ thống</div>
        <h2 class="section-title white reveal-up" style="--delay: 0.1s">Xây dựng trên nền tảng<br/><span class="text-gradient-light">Microservices hiện đại</span></h2>
        <div class="services-tabs-container reveal-up">
          <div class="svc-tabs-list">
            <div v-for="(svc, i) in services" :key="svc.name" 
                 class="svc-tab-item" 
                 :class="{ active: activeServiceTab === i }"
                 @click="activeServiceTab = i"
                 :style="{ '--theme-color': svc.color }">
              <span class="svc-tab-icon" v-html="svc.icon"></span>
              <span class="svc-tab-name">{{ svc.name }}</span>
            </div>
          </div>
          <div class="svc-tabs-content">
            <Transition name="fade-up" mode="out-in">
              <div :key="activeServiceTab" class="svc-tab-pane" :style="{ '--theme-color': services[activeServiceTab].color }">
                <div class="svc-pane-top">
                  <div class="svc-pane-header">
                    <h3 class="svc-pane-title">{{ services[activeServiceTab].name }}</h3>
                    <div class="svc-port-badge">Port {{ services[activeServiceTab].port }}</div>
                  </div>
                  <ul class="svc-pane-features">
                    <li v-for="f in services[activeServiceTab].features" :key="f">
                      <svg width="18" height="18" viewBox="0 0 24 24" fill="none" :stroke="services[activeServiceTab].color" stroke-width="3" style="flex-shrink:0"><polyline points="20 6 9 17 4 12"/></svg>
                      <span>{{ f }}</span>
                    </li>
                  </ul>
                </div>
                
                <!-- CSS Mockups -->
                <div class="svc-mockup-area">
                  <!-- Tab 0: API Gateway -->
                  <div v-if="activeServiceTab === 0" class="mockup-api">
                    <div class="api-node client">Client</div>
                    <div class="api-line line-1"><div class="api-dot"></div></div>
                    <div class="api-node gateway">API Gateway</div>
                    <div class="api-lines-out">
                      <div class="api-line-out line-out-1"><div class="api-dot"></div></div>
                      <div class="api-line-out line-out-2"><div class="api-dot" style="animation-delay: 0.5s"></div></div>
                      <div class="api-line-out line-out-3"><div class="api-dot" style="animation-delay: 1s"></div></div>
                    </div>
                    <div class="api-services">
                      <div class="api-node micro proj">Project</div>
                      <div class="api-node micro task">Task</div>
                      <div class="api-node micro noti">Notify</div>
                    </div>
                  </div>
                  
                  <!-- Tab 1: Project Service -->
                  <div v-if="activeServiceTab === 1" class="mockup-project">
                    <div class="pj-header">Project Dashboard</div>
                    <div class="pj-list">
                      <div class="pj-item">
                        <div class="pj-info"><div class="pj-avatar" style="background:#6366f1"></div><div class="pj-name">Website Redesign</div></div>
                        <div class="pj-progress"><div class="pj-fill" style="width: 75%; background:#f59e0b"></div></div>
                      </div>
                      <div class="pj-item" style="animation-delay: 0.2s">
                        <div class="pj-info"><div class="pj-avatar" style="background:#ec4899"></div><div class="pj-name">Mobile App</div></div>
                        <div class="pj-progress"><div class="pj-fill" style="width: 40%; background:#f59e0b"></div></div>
                      </div>
                    </div>
                  </div>
                  
                  <!-- Tab 2: Task Service -->
                  <div v-if="activeServiceTab === 2" class="mockup-task">
                    <div class="kb-col">
                      <div class="kb-title">TODO</div>
                      <div class="kb-card"></div>
                      <div class="kb-card" style="opacity:0.5; height: 30px;"></div>
                    </div>
                    <div class="kb-col">
                      <div class="kb-title">DOING</div>
                      <div class="kb-card kb-drag"></div>
                    </div>
                    <div class="kb-col">
                      <div class="kb-title">DONE</div>
                      <div class="kb-card" style="opacity:0.3"></div>
                    </div>
                  </div>
                  
                  <!-- Tab 3: Notify Service -->
                  <div v-if="activeServiceTab === 3" class="mockup-notify">
                    <div class="nf-bell">
                      <svg width="60" height="60" viewBox="0 0 24 24" fill="none" stroke="var(--theme-color)" stroke-width="2"><path d="M18 8A6 6 0 006 8c0 7-3 9-3 9h18s-3-2-3-9"/><path d="M13.73 21a2 2 0 01-3.46 0"/></svg>
                      <div class="nf-badge">3</div>
                      <div class="nf-ring"></div>
                    </div>
                    <div class="nf-toast">
                      <div class="nf-toast-icon">✓</div>
                      <div class="nf-toast-text">Task completed successfully</div>
                    </div>
                  </div>
                </div>
              </div>
            </Transition>
          </div>
        </div>
      </div>
    </section>

    
    <!-- TESTIMONIALS -->
    <section class="testimonials-section py-24 relative overflow-hidden" style="border-top: 1px solid rgba(255,255,255,0.05);">
      <div class="text-center max-w-3xl mx-auto mb-16 px-6">
        <h2 class="text-4xl font-black mb-4 title-text">Loved by Teams Worldwide</h2>
        <p class="text-lg sub-text">Thousands of forward-thinking teams use SprintFlow.</p>
      </div>
      <div class="marquee-container" style="display:flex; gap:24px; overflow:hidden; width:100%;">
        <div class="marquee-content" style="display:flex; gap:24px; animation: marquee 30s linear infinite;">
          <div class="testimonial-card" style="min-width:350px; background:rgba(255,255,255,0.02); border:1px solid rgba(255,255,255,0.05); border-radius:20px; padding:24px;">
            <div class="flex items-center gap-3 mb-4">
              <img src="https://i.pravatar.cc/150?u=1" class="w-10 h-10 rounded-full" />
              <div><div class="font-bold title-text">Sarah Jenkins</div><div class="text-xs sub-text">Product Manager @ Acme</div></div>
            </div>
            <p class="text-sm title-text opacity-80 leading-relaxed">"SprintFlow completely transformed how we ship features. The real-time chat integrated right into tasks is a game changer."</p>
          </div>
          <div class="testimonial-card" style="min-width:350px; background:rgba(255,255,255,0.02); border:1px solid rgba(255,255,255,0.05); border-radius:20px; padding:24px;">
            <div class="flex items-center gap-3 mb-4">
              <img src="https://i.pravatar.cc/150?u=2" class="w-10 h-10 rounded-full" />
              <div><div class="font-bold title-text">David Chen</div><div class="text-xs sub-text">CTO @ StartupX</div></div>
            </div>
            <p class="text-sm title-text opacity-80 leading-relaxed">"The role-based access control and JWT security give us peace of mind when working with enterprise clients."</p>
          </div>
          <div class="testimonial-card" style="min-width:350px; background:rgba(255,255,255,0.02); border:1px solid rgba(255,255,255,0.05); border-radius:20px; padding:24px;">
            <div class="flex items-center gap-3 mb-4">
              <img src="https://i.pravatar.cc/150?u=3" class="w-10 h-10 rounded-full" />
              <div><div class="font-bold title-text">Emily Rodriguez</div><div class="text-xs sub-text">Lead Designer</div></div>
            </div>
            <p class="text-sm title-text opacity-80 leading-relaxed">"Absolutely gorgeous UI. Working in SprintFlow doesn't feel like a chore; it feels like stepping into the future."</p>
          </div>
          <!-- Repeat for smooth scroll -->
          <div class="testimonial-card" style="min-width:350px; background:rgba(255,255,255,0.02); border:1px solid rgba(255,255,255,0.05); border-radius:20px; padding:24px;">
            <div class="flex items-center gap-3 mb-4">
              <img src="https://i.pravatar.cc/150?u=1" class="w-10 h-10 rounded-full" />
              <div><div class="font-bold title-text">Sarah Jenkins</div><div class="text-xs sub-text">Product Manager @ Acme</div></div>
            </div>
            <p class="text-sm title-text opacity-80 leading-relaxed">"SprintFlow completely transformed how we ship features. The real-time chat integrated right into tasks is a game changer."</p>
          </div>
          <div class="testimonial-card" style="min-width:350px; background:rgba(255,255,255,0.02); border:1px solid rgba(255,255,255,0.05); border-radius:20px; padding:24px;">
            <div class="flex items-center gap-3 mb-4">
              <img src="https://i.pravatar.cc/150?u=2" class="w-10 h-10 rounded-full" />
              <div><div class="font-bold title-text">David Chen</div><div class="text-xs sub-text">CTO @ StartupX</div></div>
            </div>
            <p class="text-sm title-text opacity-80 leading-relaxed">"The role-based access control and JWT security give us peace of mind when working with enterprise clients."</p>
          </div>
        </div>
      </div>
    </section>

    <!-- PRICING -->
    <section class="pricing-section py-24" style="background:rgba(255,255,255,0.01); border-top: 1px solid rgba(255,255,255,0.05);">
      <div class="max-w-7xl mx-auto px-6">
        <div class="text-center max-w-3xl mx-auto mb-20">
          <h2 class="text-4xl md:text-5xl font-black mb-6 title-text">Simple, transparent pricing</h2>
          <p class="text-lg sub-text">Start for free, upgrade when your team grows.</p>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8 items-center">
          <div class="p-card" style="background:var(--card-inner-bg, #0c0c0c); border:1px solid rgba(255,255,255,0.1); border-radius:24px; padding:40px;">
            <h3 class="text-xl font-bold mb-2 title-text">Hobby</h3>
            <p class="text-sm sub-text mb-6">For individuals & small teams</p>
            <div class="text-4xl font-black mb-6 title-text">Free<span class="text-lg font-normal sub-text">/forever</span></div>
            <ul class="flex flex-col gap-3 mb-8 title-text text-sm">
              <li class="flex items-center gap-2"><svg width="16" height="16" stroke="#10b981" stroke-width="2" fill="none"><polyline points="20 6 9 17 4 12"/></svg> Up to 3 projects</li>
              <li class="flex items-center gap-2"><svg width="16" height="16" stroke="#10b981" stroke-width="2" fill="none"><polyline points="20 6 9 17 4 12"/></svg> Basic Kanban board</li>
            </ul>
            <a href="#" class="btn-outline" style="display:block; text-align:center; padding:12px; border-radius:8px; border:1px solid rgba(255,255,255,0.1);">Get Started</a>
          </div>
          <div class="p-card p-pro" style="background:var(--card-inner-bg, #0c0c0c); border:1px solid #8b5cf6; box-shadow:0 10px 40px rgba(139,92,246,0.15); border-radius:24px; padding:40px; position:relative; transform:translateY(-16px);">
            <div style="position:absolute; top:-12px; left:50%; transform:translateX(-50%); background:linear-gradient(90deg,#6366f1,#8b5cf6); color:white; font-size:10px; font-weight:bold; padding:4px 12px; border-radius:12px;">RECOMMENDED</div>
            <h3 class="text-xl font-bold mb-2 title-text">Pro Team</h3>
            <p class="text-sm sub-text mb-6">For professional teams</p>
            <div class="text-4xl font-black mb-6 title-text">$12<span class="text-lg font-normal sub-text">/user/mo</span></div>
            <ul class="flex flex-col gap-3 mb-8 title-text text-sm">
              <li class="flex items-center gap-2"><svg width="16" height="16" stroke="#10b981" stroke-width="2" fill="none"><polyline points="20 6 9 17 4 12"/></svg> Unlimited projects</li>
              <li class="flex items-center gap-2"><svg width="16" height="16" stroke="#10b981" stroke-width="2" fill="none"><polyline points="20 6 9 17 4 12"/></svg> Real-time Chat</li>
              <li class="flex items-center gap-2"><svg width="16" height="16" stroke="#10b981" stroke-width="2" fill="none"><polyline points="20 6 9 17 4 12"/></svg> Role-based access</li>
            </ul>
            <a href="#" class="btn-primary" style="display:block; text-align:center; padding:12px; border-radius:8px; background:linear-gradient(90deg,#6366f1,#8b5cf6); color:white;">Try for 14 days</a>
          </div>
          <div class="p-card" style="background:var(--card-inner-bg, #0c0c0c); border:1px solid rgba(255,255,255,0.1); border-radius:24px; padding:40px;">
            <h3 class="text-xl font-bold mb-2 title-text">Enterprise</h3>
            <p class="text-sm sub-text mb-6">For large organizations</p>
            <div class="text-4xl font-black mb-6 title-text">Custom</div>
            <ul class="flex flex-col gap-3 mb-8 title-text text-sm">
              <li class="flex items-center gap-2"><svg width="16" height="16" stroke="#10b981" stroke-width="2" fill="none"><polyline points="20 6 9 17 4 12"/></svg> All Pro features</li>
              <li class="flex items-center gap-2"><svg width="16" height="16" stroke="#10b981" stroke-width="2" fill="none"><polyline points="20 6 9 17 4 12"/></svg> SSO Integration</li>
              <li class="flex items-center gap-2"><svg width="16" height="16" stroke="#10b981" stroke-width="2" fill="none"><polyline points="20 6 9 17 4 12"/></svg> 24/7 Support</li>
            </ul>
            <a href="#" class="btn-outline" style="display:block; text-align:center; padding:12px; border-radius:8px; border:1px solid rgba(255,255,255,0.1);">Contact Sales</a>
          </div>
        </div>
      </div>
    </section>

    <!-- CTA -->
    <section class="cta-section">
      <div class="cta-orb cta-orb-1"></div>
      <div class="cta-orb cta-orb-2"></div>
      <div class="cta-inner reveal-up">
        
        <h2 class="cta-title">Sẵn sàng bắt đầu?</h2>
        <p class="cta-sub">Đăng ký ngay hôm nay và trải nghiệm cách quản lý dự án thông minh hơn cùng SprintFlow.</p>
        <div class="cta-actions">
          <router-link to="/register" class="btn-primary large">
            Tạo tài khoản miễn phí
            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M5 12h14M12 5l7 7-7 7"/></svg>
            <span class="btn-shimmer"></span>
          </router-link>
          <router-link to="/login" class="btn-outline-white">
            Đăng nhập
          </router-link>
        </div>
      </div>
    </section>

    <!-- FOOTER -->
    <footer class="footer">
      <div class="footer-inner">
        <div class="footer-logo">
          <div class="logo-icon small">
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none">
              <rect x="3" y="3" width="8" height="8" rx="2" fill="white" opacity="0.9"/>
              <rect x="13" y="3" width="8" height="8" rx="2" fill="white" opacity="0.5"/>
              <rect x="3" y="13" width="8" height="8" rx="2" fill="white" opacity="0.5"/>
              <rect x="13" y="13" width="8" height="8" rx="2" fill="white" opacity="0.9"/>
            </svg>
          </div>
          <span>SprintFlow</span>
        </div>
        <div class="footer-links">
          <router-link to="/login">Đăng nhập</router-link>
          <router-link to="/register">Đăng ký</router-link>
        </div>
        <div class="footer-copy">© 2026 SprintFlow · Hệ thống quản lý dự án</div>
      </div>
    </footer>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted, computed, watch } from 'vue'

const currentLang = ref('vi');
const langMenuOpen = ref(false);

const activeFeature = ref(0);
const activeServiceTab = ref(0);

const setLang = (lang: string) => {
  currentLang.value = lang;
  langMenuOpen.value = false;
};

const tNav = computed(() => currentLang.value === 'vi' ? {
  features: 'Tính năng', how: 'Cách hoạt động', stats: 'Kiến trúc',
  login: 'Đăng nhập', register: 'Bắt đầu miễn phí'
} : {
  features: 'Features', how: 'How it works', stats: 'Architecture',
  login: 'Login', register: 'Get Started'
});

// ── TYPING EFFECT ──
const wordsVi = ['hiệu quả hơn', 'chuyên nghiệp hơn', 'thông minh hơn', 'dễ dàng hơn']
const wordsEn = ['more efficiently', 'more professionally', 'smarter', 'easier']
const words = computed(() => currentLang.value === 'vi' ? wordsVi : wordsEn)

let wordIdx = 0, charIdx = 0, deleting = false
const typedText = ref('')

watch(currentLang, () => {
  wordIdx = 0; charIdx = 0; deleting = false; typedText.value = '';
});

function typeLoop() {
  const word = words.value[wordIdx]
  if (!word) return;
  if (!deleting) {
    typedText.value = word.slice(0, ++charIdx)
    if (charIdx === word.length) {
      deleting = true
      setTimeout(typeLoop, 1800)
      return
    }
  } else {
    typedText.value = word.slice(0, --charIdx)
    if (charIdx === 0) {
      deleting = false
      wordIdx = (wordIdx + 1) % words.value.length
    }
  }
  setTimeout(typeLoop, deleting ? 60 : 90)
}

// ── COUNTER ANIMATION ──
const counters = ref({ services: 0, tasks: 0 })
function animateCounter(key: 'services' | 'tasks', target: number, duration = 1500) {
  const start = performance.now()
  function step(now: number) {
    const progress = Math.min((now - start) / duration, 1)
    const ease = 1 - Math.pow(1 - progress, 3)
    counters.value[key] = Math.round(ease * target)
    if (progress < 1) requestAnimationFrame(step)
  }
  requestAnimationFrame(step)
}

// ── PARTICLES ──
const particleCanvas = ref<HTMLCanvasElement | null>(null)
let animFrame = 0
function initParticles() {
  if (!particleCanvas.value) return
  const canvas: HTMLCanvasElement = particleCanvas.value
  const ctx = canvas.getContext('2d')!
  canvas.width = window.innerWidth
  canvas.height = window.innerHeight

  const particles: { x: number; y: number; vx: number; vy: number; r: number; alpha: number }[] = []
  for (let i = 0; i < 60; i++) {
    particles.push({
      x: Math.random() * canvas.width,
      y: Math.random() * canvas.height,
      vx: (Math.random() - 0.5) * 0.4,
      vy: (Math.random() - 0.5) * 0.4,
      r: Math.random() * 1.5 + 0.5,
      alpha: Math.random() * 0.4 + 0.1
    })
  }

  function draw() {
    ctx.clearRect(0, 0, canvas.width, canvas.height)
    particles.forEach(p => {
      p.x += p.vx; p.y += p.vy
      if (p.x < 0 || p.x > canvas.width) p.vx *= -1
      if (p.y < 0 || p.y > canvas.height) p.vy *= -1
      ctx.beginPath()
      ctx.arc(p.x, p.y, p.r, 0, Math.PI * 2)
      ctx.fillStyle = `rgba(99,102,241,${p.alpha})`
      ctx.fill()
    })
    // Draw connecting lines
    particles.forEach((a, i) => {
      particles.slice(i + 1).forEach(b => {
        const d = Math.hypot(a.x - b.x, a.y - b.y)
        if (d < 120) {
          ctx.beginPath()
          ctx.moveTo(a.x, a.y)
          ctx.lineTo(b.x, b.y)
          ctx.strokeStyle = `rgba(99,102,241,${0.06 * (1 - d / 120)})`
          ctx.stroke()
        }
      })
    })
    animFrame = requestAnimationFrame(draw)
  }
  draw()
}

// ── SCROLL REVEAL ──
let observer: IntersectionObserver
function initScrollReveal() {
  observer = new IntersectionObserver((entries) => {
    entries.forEach(el => {
      if (el.isIntersecting) {
        const target = el.target as HTMLElement
        const delay = target.style.getPropertyValue('--delay') || '0s'
        target.style.transitionDelay = delay
        target.classList.add('revealed')
        observer.unobserve(el.target)
      }
    })
  }, { threshold: 0.1 })
  document.querySelectorAll('.reveal-up, .reveal-left, .reveal-right, .reveal-slide-right').forEach(el => observer.observe(el))
}

// ── SCROLL NAV ──

const isLightMode = ref(false)
const toggleTheme = () => {
  isLightMode.value = !isLightMode.value
}

const isScrolled = ref(false)
function onScroll() { isScrolled.value = window.scrollY > 50

  // Sticky Stacking Cards Scale Logic
  const stackCards = document.querySelectorAll('.stack-card')
  if (stackCards.length > 0) {
    stackCards.forEach((card, index) => {
      const rect = card.getBoundingClientRect()
      // If card is sticky at its top position (which is 15vh + index*40px)
      const stickyTop = (window.innerHeight * 0.15) + (index * 40)
      if (rect.top <= stickyTop + 5) {
        // Calculate how far the page has scrolled past this card
        const distancePast = stickyTop - rect.top
        const distance = Math.max(0, -rect.top + stickyTop);
        let scale = 1 - (distance * 0.0004);
        scale = Math.max(0.85, scale);
        let rotateX = Math.min(10, distance * 0.015);
        let yOffset = distance * -0.05;
        let opacity = Math.max(0.3, 1 - (distance * 0.0015));
        
        const inner = card.querySelector('.card-inner') as HTMLElement;
        if (inner) {
          inner.style.transform = `scale(${scale}) perspective(1200px) rotateX(${rotateX}deg) translateY(${yOffset}px)`;
          inner.style.opacity = opacity.toString();
          inner.style.transformOrigin = 'top center';
          inner.style.transition = 'transform 0.1s cubic-bezier(0.16, 1, 0.3, 1), opacity 0.1s ease-out';
        }
      } else {
        const inner = card.querySelector('.card-inner') as HTMLElement
        if (inner) {
          inner.style.transform = `scale(1)`
        }
      }
    })
  }
 }

onMounted(() => {
  typeLoop()
  setTimeout(() => animateCounter('services', 4, 1200), 500)
  setTimeout(() => animateCounter('tasks', 20, 1500), 700)
  initParticles()
  setTimeout(initScrollReveal, 100)
  window.addEventListener('scroll', onScroll)
  window.addEventListener('resize', () => {
    if (particleCanvas.value) {
      particleCanvas.value.width = window.innerWidth
      particleCanvas.value.height = window.innerHeight
    }
  })

  // Feature observer for background transitions
  featObserver = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        activeFeature.value = parseInt(entry.target.getAttribute('data-index') || '0')
      }
    })
  }, { threshold: 0.5 })

  setTimeout(() => {
    document.querySelectorAll('.feat-row').forEach((el, index) => {
      el.setAttribute('data-index', index.toString())
      featObserver?.observe(el)
    })
  }, 500)
})

let featObserver: IntersectionObserver | null = null

onUnmounted(() => {
  cancelAnimationFrame(animFrame)
  observer?.disconnect()
  featObserver?.disconnect()
  window.removeEventListener('scroll', onScroll)
})

// ── DATA ──
const marquee = [
  'Vue 3', 'TypeScript', '.NET 8', 'Ocelot Gateway', 'JWT Auth', 'SQL Server',
  'Microservices', 'Kanban Board', 'Real-time Notify', 'REST API', 'CORS', 'Swagger'
]

const featuresVi = [
  {
    title: 'Quản lý dự án',
    desc: 'Tạo và quản lý nhiều dự án song song với đầy đủ thông tin về tiến độ, thành viên và trạng thái.',
    bg: '#09090b',
    glow: 'rgba(99,102,241,0.5)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#6366f1" stroke-width="2"><path d="M22 19a2 2 0 01-2 2H4a2 2 0 01-2-2V5a2 2 0 012-2h5l2 3h9a2 2 0 012 2z"/></svg>`,
    items: ['Tạo dự án với màu sắc & mô tả', 'Quản lý thành viên dự án', 'Theo dõi tiến độ realtime', 'Lọc & tìm kiếm nhanh'],
    mockupHtml: `

      <div class="mk-hud reveal-up">
        <div class="mk-card mk-glass float-anim" style="margin-bottom: 24px; padding: 24px; display:flex; justify-content:space-between; align-items:center; width: 320px; border-radius: 24px; box-shadow: 0 30px 60px rgba(0,0,0,0.4);">
          <div>
            <div class="mk-text-sm" style="color:#a1a1aa; letter-spacing: 2px; text-transform: uppercase;">Tổng dự án</div>
            <div class="mk-text-xl mk-gradient-text" style="font-weight:900; font-size:3rem; line-height:1">12</div>
          </div>
          <div style="width:60px; height:60px; border-radius:50%; background:linear-gradient(135deg, #6366f1, #8b5cf6); box-shadow: 0 10px 30px rgba(99,102,241,0.5);"></div>
        </div>
        <div class="mk-card mk-glass float-anim-delayed" style="padding: 24px; width: 360px; border-radius: 24px; box-shadow: 0 30px 60px rgba(0,0,0,0.4); margin-left: 40px;">
          <div class="mk-text-sm" style="color:#a1a1aa; margin-bottom:12px; letter-spacing: 2px; text-transform: uppercase;">Tiến độ Sprint</div>
          <div class="mk-progress-bar" style="height: 8px; border-radius: 8px; background: rgba(255,255,255,0.05);"><div class="mk-progress-fill" style="width:85%; border-radius: 8px; background: linear-gradient(90deg, #6366f1, #8b5cf6); box-shadow: 0 0 20px rgba(99,102,241,0.5);"></div></div>
        </div>
      </div>

    `
  },
  {
    title: 'Bảng Kanban',
    desc: 'Hệ thống quản lý công việc trực quan theo phong cách Kanban. Nắm bắt tức thì ai đang làm gì, ở đâu.',
    bg: '#09090b',
    glow: 'rgba(139,92,246,0.5)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#8b5cf6" stroke-width="2"><rect x="3" y="3" width="18" height="18" rx="2"/><path d="M8 7v7m8-7v4"/></svg>`,
    items: ['Kéo thả linh hoạt', 'Tùy chỉnh cột trạng thái', 'Gán thành viên, deadline', 'Đính kèm file & comment'],
    mockupHtml: `

      <div class="mk-hud reveal-up" style="display: flex; gap: 20px;">
        <div class="mk-col float-anim" style="width: 200px;">
          <div class="mk-col-title" style="color:#a1a1aa; letter-spacing: 2px; margin-bottom: 16px;">TO DO</div>
          <div class="mk-task-card mk-glass" style="border-radius: 16px; padding: 16px; margin-bottom: 16px; box-shadow: 0 20px 40px rgba(0,0,0,0.3);"><div class="mk-badge mk-badge-blue">Design</div><div class="mk-task-line" style="width:80%; height:6px; border-radius:3px;"></div><div class="mk-task-line" style="width:50%; height:6px; border-radius:3px;"></div></div>
          <div class="mk-task-card mk-glass" style="border-radius: 16px; padding: 16px; box-shadow: 0 20px 40px rgba(0,0,0,0.3);"><div class="mk-badge mk-badge-red">Bug</div><div class="mk-task-line" style="width:60%; height:6px; border-radius:3px;"></div></div>
        </div>
        <div class="mk-col float-anim-delayed" style="width: 200px; margin-top: 40px;">
          <div class="mk-col-title" style="color:#a1a1aa; letter-spacing: 2px; margin-bottom: 16px;">DOING</div>
          <div class="mk-task-card mk-glass mk-drag" style="border-radius: 16px; padding: 16px; box-shadow: 0 30px 60px rgba(139,92,246,0.3); transform: rotate(3deg); border: 1px solid rgba(139,92,246,0.4);"><div class="mk-badge mk-badge-purple">Feature</div><div class="mk-task-line" style="width:90%; height:6px; border-radius:3px;"></div><div class="mk-task-line" style="width:40%; height:6px; border-radius:3px;"></div></div>
        </div>
      </div>

    `
  },
  {
    title: 'Thông báo Realtime',
    desc: 'Mọi thay đổi trong dự án đều được đồng bộ và thông báo ngay lập tức tới tất cả các thành viên liên quan.',
    bg: '#09090b',
    glow: 'rgba(16,185,129,0.5)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#10b981" stroke-width="2"><path d="M18 8A6 6 0 006 8c0 7-3 9-3 9h18s-3-2-3-9"/><path d="M13.73 21a2 2 0 01-3.46 0"/></svg>`,
    items: ['Cập nhật trạng thái tức thì', 'Giao tiếp qua chat bong bóng', 'Nhận thông báo khi được gán', 'Lịch sử hoạt động'],
    mockupHtml: `

      <div class="mk-hud reveal-up" style="display: flex; flex-direction: column; gap: 16px; width: 340px;">
        <div class="mk-bubble mk-left mk-glass float-anim" style="padding: 16px 20px; border-radius: 24px 24px 24px 4px; box-shadow: 0 20px 40px rgba(0,0,0,0.3); color: #fff;">API integration is done!</div>
        <div class="mk-bubble mk-right mk-gradient-bg float-anim-delayed" style="padding: 16px 20px; border-radius: 24px 24px 4px 24px; box-shadow: 0 20px 40px rgba(16,185,129,0.3); margin-left: auto;">Great, I will review the PR.</div>
        <div class="mk-toast mk-glass float-anim" style="padding: 16px 20px; border-radius: 16px; box-shadow: 0 20px 40px rgba(0,0,0,0.3); color: #fff; margin-top: 16px; border: 1px solid rgba(16,185,129,0.2);">
          <span class="mk-highlight" style="color:#10b981">Minh</span> resolved a task in <span class="mk-highlight" style="color:#10b981">Backend</span>
        </div>
      </div>

    `
  },
  {
    title: 'Bảo mật & Phân quyền',
    desc: 'Bảo vệ dữ liệu tuyệt đối với hệ thống mã hóa JWT và cơ chế phân quyền RBAC chi tiết.',
    bg: '#09090b',
    glow: 'rgba(236,72,153,0.5)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#ec4899" stroke-width="2"><rect x="3" y="11" width="18" height="11" rx="2"/><path d="M7 11V7a5 5 0 0110 0v4"/></svg>`,
    items: ['Xác thực JWT an toàn', 'Quản lý Role-Based Access', 'Phân quyền Admin / Member', 'Bật/tắt 2FA'],
    mockupHtml: `

      <div class="mk-hud reveal-up" style="width: 360px;">
        <div class="mk-setting-row mk-glass float-anim" style="padding: 20px; border-radius: 20px; margin-bottom: 20px; box-shadow: 0 20px 40px rgba(0,0,0,0.3); border: 1px solid rgba(236,72,153,0.3);">
          <div class="mk-setting-info">
            <div class="mk-setting-title" style="color:#fff; font-size:1.1rem; margin-bottom:4px;">2FA Authentication</div>
            <div class="mk-setting-desc" style="color:#a1a1aa">Require 2FA for all members</div>
          </div>
          <div class="mk-toggle mk-toggle-on" style="background:#ec4899; box-shadow: 0 0 15px rgba(236,72,153,0.5);"><div class="mk-toggle-circle"></div></div>
        </div>
        <div class="mk-setting-row mk-glass float-anim-delayed" style="padding: 20px; border-radius: 20px; box-shadow: 0 20px 40px rgba(0,0,0,0.3); margin-left: 30px;">
          <div class="mk-setting-info">
            <div class="mk-setting-title" style="color:#fff; font-size:1.1rem; margin-bottom:4px;">API Gateway Access</div>
            <div class="mk-setting-desc" style="color:#a1a1aa">Allow external integrations</div>
          </div>
          <div class="mk-toggle"><div class="mk-toggle-circle"></div></div>
        </div>
      </div>

    `
  }
]

const featuresEn = [
  {
    title: 'Quản lý dự án',
    desc: 'Tạo và quản lý nhiều dự án song song với đầy đủ thông tin về tiến độ, thành viên và trạng thái.',
    bg: '#09090b',
    glow: 'rgba(99,102,241,0.5)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#6366f1" stroke-width="2"><path d="M22 19a2 2 0 01-2 2H4a2 2 0 01-2-2V5a2 2 0 012-2h5l2 3h9a2 2 0 012 2z"/></svg>`,
    items: ['Tạo dự án với màu sắc & mô tả', 'Quản lý thành viên dự án', 'Theo dõi tiến độ realtime', 'Lọc & tìm kiếm nhanh'],
    mockupHtml: `

      <div class="mk-hud reveal-up">
        <div class="mk-card mk-glass float-anim" style="margin-bottom: 24px; padding: 24px; display:flex; justify-content:space-between; align-items:center; width: 320px; border-radius: 24px; box-shadow: 0 30px 60px rgba(0,0,0,0.4);">
          <div>
            <div class="mk-text-sm" style="color:#a1a1aa; letter-spacing: 2px; text-transform: uppercase;">Tổng dự án</div>
            <div class="mk-text-xl mk-gradient-text" style="font-weight:900; font-size:3rem; line-height:1">12</div>
          </div>
          <div style="width:60px; height:60px; border-radius:50%; background:linear-gradient(135deg, #6366f1, #8b5cf6); box-shadow: 0 10px 30px rgba(99,102,241,0.5);"></div>
        </div>
        <div class="mk-card mk-glass float-anim-delayed" style="padding: 24px; width: 360px; border-radius: 24px; box-shadow: 0 30px 60px rgba(0,0,0,0.4); margin-left: 40px;">
          <div class="mk-text-sm" style="color:#a1a1aa; margin-bottom:12px; letter-spacing: 2px; text-transform: uppercase;">Tiến độ Sprint</div>
          <div class="mk-progress-bar" style="height: 8px; border-radius: 8px; background: rgba(255,255,255,0.05);"><div class="mk-progress-fill" style="width:85%; border-radius: 8px; background: linear-gradient(90deg, #6366f1, #8b5cf6); box-shadow: 0 0 20px rgba(99,102,241,0.5);"></div></div>
        </div>
      </div>

    `
  },
  {
    title: 'Bảng Kanban',
    desc: 'Hệ thống quản lý công việc trực quan theo phong cách Kanban. Nắm bắt tức thì ai đang làm gì, ở đâu.',
    bg: '#09090b',
    glow: 'rgba(139,92,246,0.5)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#8b5cf6" stroke-width="2"><rect x="3" y="3" width="18" height="18" rx="2"/><path d="M8 7v7m8-7v4"/></svg>`,
    items: ['Kéo thả linh hoạt', 'Tùy chỉnh cột trạng thái', 'Gán thành viên, deadline', 'Đính kèm file & comment'],
    mockupHtml: `

      <div class="mk-hud reveal-up" style="display: flex; gap: 20px;">
        <div class="mk-col float-anim" style="width: 200px;">
          <div class="mk-col-title" style="color:#a1a1aa; letter-spacing: 2px; margin-bottom: 16px;">TO DO</div>
          <div class="mk-task-card mk-glass" style="border-radius: 16px; padding: 16px; margin-bottom: 16px; box-shadow: 0 20px 40px rgba(0,0,0,0.3);"><div class="mk-badge mk-badge-blue">Design</div><div class="mk-task-line" style="width:80%; height:6px; border-radius:3px;"></div><div class="mk-task-line" style="width:50%; height:6px; border-radius:3px;"></div></div>
          <div class="mk-task-card mk-glass" style="border-radius: 16px; padding: 16px; box-shadow: 0 20px 40px rgba(0,0,0,0.3);"><div class="mk-badge mk-badge-red">Bug</div><div class="mk-task-line" style="width:60%; height:6px; border-radius:3px;"></div></div>
        </div>
        <div class="mk-col float-anim-delayed" style="width: 200px; margin-top: 40px;">
          <div class="mk-col-title" style="color:#a1a1aa; letter-spacing: 2px; margin-bottom: 16px;">DOING</div>
          <div class="mk-task-card mk-glass mk-drag" style="border-radius: 16px; padding: 16px; box-shadow: 0 30px 60px rgba(139,92,246,0.3); transform: rotate(3deg); border: 1px solid rgba(139,92,246,0.4);"><div class="mk-badge mk-badge-purple">Feature</div><div class="mk-task-line" style="width:90%; height:6px; border-radius:3px;"></div><div class="mk-task-line" style="width:40%; height:6px; border-radius:3px;"></div></div>
        </div>
      </div>

    `
  },
  {
    title: 'Thông báo Realtime',
    desc: 'Mọi thay đổi trong dự án đều được đồng bộ và thông báo ngay lập tức tới tất cả các thành viên liên quan.',
    bg: '#09090b',
    glow: 'rgba(16,185,129,0.5)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#10b981" stroke-width="2"><path d="M18 8A6 6 0 006 8c0 7-3 9-3 9h18s-3-2-3-9"/><path d="M13.73 21a2 2 0 01-3.46 0"/></svg>`,
    items: ['Cập nhật trạng thái tức thì', 'Giao tiếp qua chat bong bóng', 'Nhận thông báo khi được gán', 'Lịch sử hoạt động'],
    mockupHtml: `

      <div class="mk-hud reveal-up" style="display: flex; flex-direction: column; gap: 16px; width: 340px;">
        <div class="mk-bubble mk-left mk-glass float-anim" style="padding: 16px 20px; border-radius: 24px 24px 24px 4px; box-shadow: 0 20px 40px rgba(0,0,0,0.3); color: #fff;">API integration is done!</div>
        <div class="mk-bubble mk-right mk-gradient-bg float-anim-delayed" style="padding: 16px 20px; border-radius: 24px 24px 4px 24px; box-shadow: 0 20px 40px rgba(16,185,129,0.3); margin-left: auto;">Great, I will review the PR.</div>
        <div class="mk-toast mk-glass float-anim" style="padding: 16px 20px; border-radius: 16px; box-shadow: 0 20px 40px rgba(0,0,0,0.3); color: #fff; margin-top: 16px; border: 1px solid rgba(16,185,129,0.2);">
          <span class="mk-highlight" style="color:#10b981">Minh</span> resolved a task in <span class="mk-highlight" style="color:#10b981">Backend</span>
        </div>
      </div>

    `
  },
  {
    title: 'Bảo mật & Phân quyền',
    desc: 'Bảo vệ dữ liệu tuyệt đối với hệ thống mã hóa JWT và cơ chế phân quyền RBAC chi tiết.',
    bg: '#09090b',
    glow: 'rgba(236,72,153,0.5)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#ec4899" stroke-width="2"><rect x="3" y="11" width="18" height="11" rx="2"/><path d="M7 11V7a5 5 0 0110 0v4"/></svg>`,
    items: ['Xác thực JWT an toàn', 'Quản lý Role-Based Access', 'Phân quyền Admin / Member', 'Bật/tắt 2FA'],
    mockupHtml: `

      <div class="mk-hud reveal-up" style="width: 360px;">
        <div class="mk-setting-row mk-glass float-anim" style="padding: 20px; border-radius: 20px; margin-bottom: 20px; box-shadow: 0 20px 40px rgba(0,0,0,0.3); border: 1px solid rgba(236,72,153,0.3);">
          <div class="mk-setting-info">
            <div class="mk-setting-title" style="color:#fff; font-size:1.1rem; margin-bottom:4px;">2FA Authentication</div>
            <div class="mk-setting-desc" style="color:#a1a1aa">Require 2FA for all members</div>
          </div>
          <div class="mk-toggle mk-toggle-on" style="background:#ec4899; box-shadow: 0 0 15px rgba(236,72,153,0.5);"><div class="mk-toggle-circle"></div></div>
        </div>
        <div class="mk-setting-row mk-glass float-anim-delayed" style="padding: 20px; border-radius: 20px; box-shadow: 0 20px 40px rgba(0,0,0,0.3); margin-left: 30px;">
          <div class="mk-setting-info">
            <div class="mk-setting-title" style="color:#fff; font-size:1.1rem; margin-bottom:4px;">API Gateway Access</div>
            <div class="mk-setting-desc" style="color:#a1a1aa">Allow external integrations</div>
          </div>
          <div class="mk-toggle"><div class="mk-toggle-circle"></div></div>
        </div>
      </div>

    `
  }
]

const features = computed(() => currentLang.value === 'vi' ? featuresVi : featuresEn)

const stepsVi = [
  {
    title: 'Tạo tài khoản',
    desc: 'Đăng ký miễn phí với email và mật khẩu. Được cấp quyền Member ngay lập tức.',
    bg: 'linear-gradient(135deg, #6366f1, #8b5cf6)',
    icon: `<svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><path d="M20 21v-2a4 4 0 00-4-4H8a4 4 0 00-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>`
  },
  {
    title: 'Tạo dự án & mời thành viên',
    desc: 'Tạo dự án mới, mời đồng nghiệp tham gia và phân công công việc ngay trong hệ thống.',
    bg: 'linear-gradient(135deg, #f59e0b, #ef4444)',
    icon: `<svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><path d="M17 21v-2a4 4 0 00-4-4H5a4 4 0 00-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 00-3-3.87M16 3.13a4 4 0 010 7.75"/></svg>`
  },
  {
    title: 'Làm việc & theo dõi',
    desc: 'Sử dụng Kanban board, bình luận, nhận thông báo và xem báo cáo tiến độ theo thời gian thực.',
    bg: 'linear-gradient(135deg, #10b981, #0ea5e9)',
    icon: `<svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><polyline points="22 12 18 12 15 21 9 3 6 12 2 12"/></svg>`
  }
]

const stepsEn = [
  {
    title: 'Create Account',
    desc: 'Register for free with email and password. Get Member access instantly.',
    bg: 'linear-gradient(135deg, #6366f1, #8b5cf6)',
    icon: `<svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><path d="M20 21v-2a4 4 0 00-4-4H8a4 4 0 00-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>`
  },
  {
    title: 'Create Project & Invite',
    desc: 'Create a new project, invite colleagues, and start assigning tasks in the system.',
    bg: 'linear-gradient(135deg, #f59e0b, #ef4444)',
    icon: `<svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><path d="M17 21v-2a4 4 0 00-4-4H5a4 4 0 00-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 00-3-3.87M16 3.13a4 4 0 010 7.75"/></svg>`
  },
  {
    title: 'Work & Track',
    desc: 'Use the Kanban board, comment, receive notifications, and view progress in real-time.',
    bg: 'linear-gradient(135deg, #10b981, #0ea5e9)',
    icon: `<svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><polyline points="22 12 18 12 15 21 9 3 6 12 2 12"/></svg>`
  }
]

const steps = computed(() => currentLang.value === 'vi' ? stepsVi : stepsEn)

const services = [
  {
    name: 'API Gateway',
    port: '7000',
    color: '#6366f1',
    icon: `<svg width="28" height="28" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><circle cx="12" cy="12" r="10"/><line x1="2" y1="12" x2="22" y2="12"/><path d="M12 2a15.3 15.3 0 014 10 15.3 15.3 0 01-4 10 15.3 15.3 0 01-4-10 15.3 15.3 0 014-10z"/></svg>`,
    features: ['Routing (Ocelot)', 'Load balancing', 'Auth forwarding']
  },
  {
    name: 'Project Service',
    port: '5001',
    color: '#f59e0b',
    icon: `<svg width="28" height="28" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><path d="M22 19a2 2 0 01-2 2H4a2 2 0 01-2-2V5a2 2 0 012-2h5l2 3h9a2 2 0 012 2z"/></svg>`,
    features: ['CRUD dự án', 'Quản lý thành viên', 'Tiến độ & trạng thái']
  },
  {
    name: 'Task Service',
    port: '5002',
    color: '#ef4444',
    icon: `<svg width="28" height="28" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><rect x="3" y="4" width="18" height="18" rx="2"/><line x1="16" y1="2" x2="16" y2="6"/><line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/></svg>`,
    features: ['CRUD task & subtask', 'Kanban status', 'Work log & giờ làm']
  },
  {
    name: 'Notify Service',
    port: '5003',
    color: '#10b981',
    icon: `<svg width="28" height="28" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><path d="M18 8A6 6 0 006 8c0 7-3 9-3 9h18s-3-2-3-9M13.73 21a2 2 0 01-3.46 0"/></svg>`,
    features: ['JWT Auth & Users', 'Comment & Activity log', 'Notifications & đã đọc']
  }
]
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700;800;900&display=swap');
* { box-sizing: border-box; }

.landing {
  font-family: 'Inter', sans-serif;
  background: #09090b;
  color: #e4e4e7;
  overflow-x: clip;
}

/* ── PARTICLES ── */
.particle-canvas {
  position: fixed; inset: 0; z-index: 0;
  pointer-events: none;
}

/* ── SCROLL REVEAL ── */
.reveal-up {
  opacity: 0;
  transform: translateY(40px);
  transition: opacity 0.7s ease, transform 0.7s ease;
}
.reveal-left {
  opacity: 0;
  transform: translateX(-50px);
  transition: opacity 0.8s ease, transform 0.8s ease;
}
.reveal-right {
  opacity: 0;
  transform: translateX(50px);
  transition: opacity 0.8s ease, transform 0.8s ease;
}
.reveal-up.revealed,
.reveal-left.revealed,
.reveal-right.revealed {
  opacity: 1;
  transform: none;
}

/* ── NAV ── */
.nav {
  position: fixed; top: 0; left: 0; right: 0; z-index: 100;
  transition: background 0.3s, border-color 0.3s, backdrop-filter 0.3s;
  border-bottom: 1px solid transparent;
}
.nav.scrolled {
  background: rgba(9,9,11,0.9);
  backdrop-filter: blur(20px);
  border-color: rgba(255,255,255,0.06);
}
.nav-inner {
  max-width: 1200px; margin: 0 auto; padding: 0 2rem;
  display: flex; align-items: center; justify-content: space-between;
  height: 64px;
}
.nav-logo { display: flex; align-items: center; gap: 10px; text-decoration: none; }
.logo-icon {
  width: 34px; height: 34px; border-radius: 10px;
  background: linear-gradient(135deg, #6366f1, #3b82f6);
  display: flex; align-items: center; justify-content: center;
  box-shadow: 0 4px 12px rgba(99,102,241,0.3);
  animation: logo-spin 8s linear infinite;
}
@keyframes logo-spin {
  0%, 90% { transform: rotate(0deg); }
  95% { transform: rotate(180deg); }
  100% { transform: rotate(360deg); }
}
.logo-icon.small { width: 28px; height: 28px; border-radius: 8px; animation: none; }
.logo-text { font-size: 1.25rem; font-weight: 700; color: #fff; letter-spacing: -0.5px; }

.nav-links { display: flex; align-items: center; gap: 2rem; }
.nav-link { color: #a1a1aa; font-size: 0.95rem; font-weight: 500; text-decoration: none; transition: color 0.2s; }
.nav-link:hover { color: #fff; }

.nav-lang-dropdown {
  position: relative; display: flex; align-items: center; gap: 6px;
  cursor: pointer; padding: 6px 12px; border-radius: 8px;
  background: rgba(255,255,255,0.05); color: #fff; font-size: 0.85rem;
  font-weight: 500; transition: background 0.2s; outline: none;
  border: 1px solid rgba(255,255,255,0.05);
}
.nav-lang-dropdown:hover { background: rgba(255,255,255,0.08); }
.lang-menu {
  position: absolute; top: 130%; right: 0; background: #18181b;
  border: 1px solid rgba(255,255,255,0.1); border-radius: 10px;
  padding: 6px; min-width: 140px; box-shadow: 0 10px 40px rgba(0,0,0,0.5);
  display: flex; flex-direction: column; gap: 4px;
  z-index: 100; transform-origin: top right;
}
.lang-item {
  padding: 8px 12px; border-radius: 6px; display: flex; align-items: center; gap: 8px;
  transition: all 0.2s; color: #a1a1aa; font-size: 0.85rem;
}
.lang-item:hover, .lang-item.active { background: rgba(99,102,241,0.15); color: #fff; }

.btn-nav-login { color: #fff; font-size: 0.95rem; font-weight: 500; text-decoration: none; }
.btn-nav-register {
  position: relative; overflow: hidden;
  background: linear-gradient(135deg, #6366f1, #8b5cf6);
  color: #fff; font-size: 0.9rem; font-weight: 500;
  text-decoration: none; padding: 0.4rem 1.1rem;
  border-radius: 8px; transition: opacity 0.2s, transform 0.2s;
}
.btn-nav-register:hover { opacity: 0.85; transform: translateY(-1px); }

/* ── SHIMMER EFFECT ── */
.btn-shimmer {
  position: absolute; inset: 0;
  background: linear-gradient(105deg, transparent 40%, rgba(255,255,255,0.2) 50%, transparent 60%);
  background-size: 200% 100%;
  animation: shimmer 2.5s infinite;
}
@keyframes shimmer {
  0% { background-position: 200% 0; }
  100% { background-position: -200% 0; }
}

/* ── HERO ── */
.hero {
  min-height: 100vh; position: relative;
  display: flex; align-items: center;
  padding: 100px 2rem 60px;
  max-width: 1200px; margin: 0 auto;
  gap: 4rem; z-index: 1;
}
.hero-bg {
  position: fixed; inset: 0; pointer-events: none; z-index: 0;
}
.hero-orb {
  position: absolute; border-radius: 50%;
  filter: blur(80px);
}
.orb-1 {
  width: 600px; height: 600px; background: #6366f1;
  top: -100px; left: -200px;
  opacity: 0.1;
  animation: orb-float 8s ease-in-out infinite;
}
.orb-2 {
  width: 400px; height: 400px; background: #8b5cf6;
  top: 200px; right: -100px;
  opacity: 0.08;
  animation: orb-float 10s ease-in-out infinite reverse;
}
.orb-3 {
  width: 300px; height: 300px; background: #10b981;
  bottom: 0; left: 40%;
  opacity: 0.06;
  animation: orb-float 12s ease-in-out infinite 2s;
}
@keyframes orb-float {
  0%, 100% { transform: translateY(0) scale(1); }
  50% { transform: translateY(-30px) scale(1.05); }
}
.hero-grid {
  position: absolute; inset: 0;
  background-image: linear-gradient(rgba(255,255,255,0.02) 1px, transparent 1px),
                    linear-gradient(90deg, rgba(255,255,255,0.02) 1px, transparent 1px);
  background-size: 60px 60px;
  animation: grid-move 20s linear infinite;
}
@keyframes grid-move {
  0% { background-position: 0 0; }
  100% { background-position: 60px 60px; }
}
.hero-content { flex: 1; position: relative; z-index: 1; }
.hero-badge {
  display: inline-flex; align-items: center; gap: 8px;
  background: rgba(99,102,241,0.12); border: 1px solid rgba(99,102,241,0.3);
  color: #a5b4fc; font-size: 0.8rem; font-weight: 500;
  padding: 6px 14px; border-radius: 20px; margin-bottom: 1.5rem;
  animation: badge-slide 0.8s ease both;
}
@keyframes badge-slide {
  from { opacity: 0; transform: translateY(-10px); }
  to { opacity: 1; transform: translateY(0); }
}
.badge-dot {
  width: 6px; height: 6px; border-radius: 50%;
  background: #6366f1; box-shadow: 0 0 8px #6366f1;
  animation: pulse-dot 2s infinite;
}
@keyframes pulse-dot {
  0%, 100% { opacity: 1; box-shadow: 0 0 8px #6366f1; }
  50% { opacity: 0.4; box-shadow: 0 0 2px #6366f1; }
}
.hero-title {
  font-size: clamp(2.5rem, 5vw, 3.8rem);
  font-weight: 900; line-height: 1.1;
  color: #fff; margin: 0 0 1.2rem;
}
.hero-gradient {
  background: linear-gradient(135deg, #6366f1, #a78bfa, #38bdf8);
  -webkit-background-clip: text; -webkit-text-fill-color: transparent;
  background-size: 200% auto;
  animation: gradient-shift 4s ease infinite;
}
@keyframes gradient-shift {
  0%, 100% { background-position: 0% center; }
  50% { background-position: 100% center; }
}
.typing-text { display: inline; }
.cursor {
  display: inline-block;
  -webkit-text-fill-color: #6366f1;
  animation: blink 0.8s step-end infinite;
  font-weight: 300;
}
@keyframes blink {
  0%, 100% { opacity: 1; }
  50% { opacity: 0; }
}
.hero-desc {
  color: #71717a; font-size: 1.05rem; line-height: 1.7;
  max-width: 520px; margin-bottom: 2rem;
}
.hero-actions { display: flex; gap: 1rem; align-items: center; margin-bottom: 2.5rem; }

.btn-primary {
  position: relative; overflow: hidden;
  display: inline-flex; align-items: center; gap: 8px;
  background: linear-gradient(135deg, #6366f1, #8b5cf6);
  color: #fff; font-weight: 600; font-size: 0.95rem;
  text-decoration: none; padding: 0.75rem 1.5rem;
  border-radius: 12px; transition: all 0.3s;
  box-shadow: 0 4px 20px rgba(99,102,241,0.4);
}
.btn-primary:hover {
  transform: translateY(-3px) scale(1.02);
  box-shadow: 0 12px 40px rgba(99,102,241,0.6);
}
.btn-primary.large { padding: 1rem 2rem; font-size: 1rem; }
.btn-ghost {
  color: #a1a1aa; font-size: 0.95rem; text-decoration: none;
  transition: color 0.2s, letter-spacing 0.2s;
}
.btn-ghost:hover { color: #fff; letter-spacing: 0.5px; }

.hero-stats { display: flex; align-items: center; gap: 1.5rem; }
.stat-item { text-align: center; }
.stat-num {
  display: block; font-size: 1.6rem; font-weight: 800; color: #fff;
  transition: color 0.3s;
}
.stat-label { font-size: 0.75rem; color: #52525b; text-transform: uppercase; letter-spacing: 1px; }
.stat-divider { width: 1px; height: 40px; background: rgba(255,255,255,0.08); }

/* ── HERO PREVIEW ── */
.hero-preview { flex: 1; position: relative; z-index: 1; min-width: 0; }
.preview-card {
  background: #141416;
  border: 1px solid rgba(255,255,255,0.08);
  border-radius: 16px; overflow: hidden;
  box-shadow: 0 24px 80px rgba(0,0,0,0.6);
  transition: transform 0.3s, box-shadow 0.3s;
}
.preview-card:hover {
  transform: translateY(-6px) rotateX(2deg);
  box-shadow: 0 40px 100px rgba(0,0,0,0.7), 0 0 40px rgba(99,102,241,0.1);
}
.preview-header {
  background: #1c1c1e; padding: 12px 16px;
  display: flex; align-items: center; gap: 12px;
  border-bottom: 1px solid rgba(255,255,255,0.06);
}
.preview-dots { display: flex; gap: 6px; }
.dot { width: 10px; height: 10px; border-radius: 50%; }
.dot.red { background: #ff5f57; }
.dot.yellow { background: #febc2e; }
.dot.green { background: #28c840; }
.preview-title-bar { font-size: 0.75rem; color: #52525b; flex: 1; text-align: center; }
.preview-body { display: flex; }
.preview-sidebar {
  width: 130px; background: #101012;
  padding: 12px 8px; border-right: 1px solid rgba(255,255,255,0.05);
  display: flex; flex-direction: column; gap: 4px;
}
.ps-item {
  display: flex; align-items: center; gap: 8px;
  padding: 7px 10px; border-radius: 8px;
  font-size: 0.72rem; color: #52525b; cursor: pointer;
  transition: all 0.2s; position: relative;
}
.ps-item.active { background: rgba(99,102,241,0.15); color: #818cf8; }
.ps-item:hover:not(.active) { background: rgba(255,255,255,0.03); color: #a1a1aa; }
.badge-count {
  position: absolute; right: 8px; top: 50%; transform: translateY(-50%);
  background: #ef4444; color: white; font-size: 0.6rem;
  width: 16px; height: 16px; border-radius: 50%;
  display: flex; align-items: center; justify-content: center;
  animation: bounce-badge 2s ease infinite;
}
@keyframes bounce-badge {
  0%, 100% { transform: translateY(-50%) scale(1); }
  50% { transform: translateY(-50%) scale(1.2); }
}
.preview-img-wrap {
  position: relative; overflow: hidden;
  border-radius: 0 0 12px 12px;
  max-height: 280px;
}
.preview-img {
  width: 100%; display: block;
  object-fit: cover; object-position: top;
  transition: transform 6s ease;
  animation: slow-pan 12s ease-in-out infinite alternate;
}
@keyframes slow-pan {
  from { transform: translateY(0); }
  to   { transform: translateY(-15%); }
}
.preview-img-overlay {
  position: absolute; inset: 0;
  background: linear-gradient(to bottom, transparent 60%, #141416);
  pointer-events: none;
}
.preview-main { flex: 1; padding: 14px; }
.pm-title { font-size: 0.8rem; font-weight: 600; color: #e4e4e7; margin-bottom: 10px; }
.pm-cards { display: flex; gap: 8px; margin-bottom: 12px; }
.pm-card { flex: 1; border-radius: 8px; padding: 10px 8px; text-align: center; transition: transform 0.2s; }
.pm-card:hover { transform: scale(1.05); }
.pm-card.blue { background: rgba(99,102,241,0.15); }
.pm-card.purple { background: rgba(139,92,246,0.15); }
.pm-card.green { background: rgba(16,185,129,0.15); }
.pm-card-num { font-size: 1.1rem; font-weight: 700; color: #fff; }
.pm-card-label { font-size: 0.6rem; color: #71717a; }
.pm-tasks { display: flex; flex-direction: column; gap: 6px; }
.pm-task-row {
  display: flex; align-items: center; gap: 8px;
  padding: 6px 8px; background: rgba(255,255,255,0.02);
  border-radius: 6px; transition: background 0.2s;
}
.pm-task-row:hover { background: rgba(255,255,255,0.04); }
.task-dot { width: 6px; height: 6px; border-radius: 50%; flex-shrink: 0; }
.task-dot.done { background: #10b981; }
.task-dot.progress { background: #f59e0b; animation: pulse-dot 1.5s infinite; }
.task-dot.todo { background: #52525b; }
.task-text { flex: 1; font-size: 0.68rem; color: #a1a1aa; }
.task-badge { font-size: 0.6rem; padding: 2px 6px; border-radius: 4px; }
.done-badge { background: rgba(16,185,129,0.15); color: #10b981; }
.prog-badge { background: rgba(245,158,11,0.15); color: #f59e0b; }
.todo-badge { background: rgba(82,82,91,0.15); color: #71717a; }

.float-card {
  position: absolute;
  background: rgba(20,20,22,0.95);
  border: 1px solid rgba(255,255,255,0.1);
  border-radius: 12px; padding: 12px 16px;
  backdrop-filter: blur(10px);
  box-shadow: 0 8px 32px rgba(0,0,0,0.5);
}
.float-1 {
  bottom: -40px; left: -80px;
  display: flex; align-items: center; gap: 10px;
  animation: float 4s ease-in-out infinite;
}
.float-2 {
  top: -20px; right: -30px; min-width: 160px;
  animation: float 4s ease-in-out infinite 2s;
}
@keyframes float {
  0%, 100% { transform: translateY(0) rotate(0deg); }
  33% { transform: translateY(-10px) rotate(0.5deg); }
  66% { transform: translateY(-5px) rotate(-0.5deg); }
}
.fc-title { font-size: 0.75rem; font-weight: 600; color: #e4e4e7; }
.fc-sub { font-size: 0.65rem; color: #71717a; white-space: nowrap; }
.fc-progress-label { font-size: 0.7rem; color: #71717a; margin-bottom: 8px; }
.fc-progress-bar { background: rgba(255,255,255,0.08); border-radius: 4px; height: 6px; margin-bottom: 6px; }
.fc-progress-fill {
  width: 0%; height: 100%;
  background: linear-gradient(90deg, #6366f1, #8b5cf6);
  border-radius: 4px;
  animation: fill-bar 2s ease 1s forwards;
}
@keyframes fill-bar { to { width: 72%; } }
.fc-progress-pct { font-size: 0.8rem; font-weight: 700; color: #a5b4fc; text-align: right; }

/* ── MARQUEE ── */
.marquee-wrap {
  overflow: hidden; padding: 1.2rem 0;
  border-top: 1px solid rgba(255,255,255,0.04);
  border-bottom: 1px solid rgba(255,255,255,0.04);
  background: rgba(255,255,255,0.01);
  position: relative; z-index: 1;
}
.marquee-track {
  display: flex; gap: 0;
  animation: marquee 30s linear infinite;
  width: max-content;
}
.marquee-track:hover { animation-play-state: paused; }
@keyframes marquee {
  0% { transform: translateX(0); }
  100% { transform: translateX(-50%); }
}
.marquee-item {
  display: flex; align-items: center; gap: 8px;
  padding: 0 2rem;
  font-size: 0.8rem; font-weight: 500; color: #52525b;
  white-space: nowrap; transition: color 0.2s;
}
.marquee-item:hover { color: #a5b4fc; }

/* ── SECTIONS COMMON ── */
.section-badge {
  display: inline-block;
  background: rgba(99,102,241,0.12); border: 1px solid rgba(99,102,241,0.25);
  color: #a5b4fc; font-size: 0.78rem; font-weight: 600;
  padding: 5px 14px; border-radius: 20px; margin-bottom: 1rem;
  text-transform: uppercase; letter-spacing: 1px;
}
.section-badge.light { background: rgba(255,255,255,0.08); border-color: rgba(255,255,255,0.15); color: #a1a1aa; }
.section-title {
  font-size: clamp(1.8rem, 3.5vw, 2.8rem);
  font-weight: 800; color: #fff; line-height: 1.2;
  margin: 0 0 1rem;
}
.section-sub { color: #71717a; font-size: 1rem; line-height: 1.7; max-width: 540px; }
.text-gradient {
  background: linear-gradient(135deg, #6366f1, #a78bfa, #38bdf8);
  -webkit-background-clip: text; -webkit-text-fill-color: transparent;
  background-size: 200% auto;
  animation: gradient-shift 4s ease infinite;
}
.text-gradient-light {
  background: linear-gradient(135deg, #a5b4fc, #7dd3fc);
  -webkit-background-clip: text; -webkit-text-fill-color: transparent;
}


/* ── FEATURES ── */
.features {
  padding: 100px 2rem;
  max-width: 1200px; margin: 0 auto;
  text-align: center; position: relative; z-index: 1;
  transition: background-color 0.8s ease;
}
.features > .section-sub { margin: 0 auto 2rem; }

.features-zigzag {
  display: flex; flex-direction: column; gap: 8rem;
  max-width: 1200px; margin: 0 auto; position: relative; z-index: 1;
}
.feat-row {
  display: flex; align-items: center; gap: 6rem;
}
.feat-row.row-reverse {
  flex-direction: row-reverse;
}
.feat-text {
  flex: 1; text-align: left;
}
.feat-image-wrap {
  flex: 1.2; position: relative;
  border-radius: 24px; overflow: hidden;
  box-shadow: 0 40px 80px -20px rgba(0,0,0,0.8);
  border: 1px solid rgba(255,255,255,0.06);
  background: rgba(20,20,22,0.8);
  transform-style: preserve-3d;
  transform: perspective(1000px) rotateY(-8deg) rotateX(4deg);
  transition: transform 0.6s cubic-bezier(0.2, 0.8, 0.2, 1), border-color 0.4s, box-shadow 0.4s;
}
.feat-row.row-reverse .feat-image-wrap {
  transform: perspective(1000px) rotateY(8deg) rotateX(4deg);
}
.feat-image-wrap:hover { 
  transform: perspective(1000px) rotateY(0deg) rotateX(0deg) scale(1.04) translateY(-10px); 
  border-color: rgba(99,102,241,0.5); 
  box-shadow: 0 50px 100px -20px rgba(99,102,241,0.2), 0 0 40px rgba(99,102,241,0.1); 
}

@keyframes float-img {
  0% { transform: translateY(0px); }
  50% { transform: translateY(-12px); }
  100% { transform: translateY(0px); }
}

.feat-mockup-container {
  width: 100%; height: 100%; min-height: 400px;
  display: flex; align-items: center; justify-content: center;
  padding: 2rem;
  animation: float-img 6s ease-in-out infinite;
  opacity: 0.9; transition: opacity 0.5s;
}
.feat-image-wrap:hover .feat-mockup-container { opacity: 1; animation-play-state: paused; }



.feat-glow {
  position: absolute; inset: 0; opacity: 0; pointer-events: none;
  transition: opacity 0.5s; z-index: 2; mix-blend-mode: color-dodge;
}
.feat-image-wrap:hover .feat-glow { opacity: 0.8; }

.feat-icon {
  width: 54px; height: 54px; border-radius: 14px;
  display: flex; align-items: center; justify-content: center;
  margin-bottom: 1.5rem; position: relative; z-index: 2;
  border: 1px solid rgba(255,255,255,0.05);
  box-shadow: 0 8px 16px rgba(0,0,0,0.2);
  transition: transform 0.3s cubic-bezier(0.34, 1.56, 0.64, 1);
}
.feat-row:hover .feat-icon { transform: scale(1.1) translateY(-4px) rotate(3deg); }
.feat-title {
  font-size: 1.8rem; font-weight: 800; color: #fff;
  margin-bottom: 1rem; position: relative; z-index: 2;
  letter-spacing: -0.5px;
}
.feat-desc {
  font-size: 1.05rem; color: #a1a1aa; line-height: 1.7;
  margin-bottom: 2rem; position: relative; z-index: 2;
}
.feat-list {
  list-style: none; padding: 0; margin: 0;
  display: flex; flex-direction: column; gap: 1rem;
  position: relative; z-index: 2;
}
.feat-list li {
  font-size: 0.95rem; color: #d4d4d8;
  display: flex; align-items: center; gap: 12px;
  transition: color 0.2s, transform 0.2s;
}
.feat-row:hover .feat-list li { color: #fff; }
.feat-list li:hover { transform: translateX(6px); color: #818cf8; }
.feat-list svg { color: #6366f1; flex-shrink: 0; }

@media (max-width: 900px) {
  .feat-row, .feat-row.row-reverse { flex-direction: column; gap: 3rem; min-height: auto; padding: 4rem 0; }
  .features-zigzag { gap: 0; }
}

/* ── HOW IT WORKS ── */
.how {
  padding: 100px 2rem;
  background: linear-gradient(180deg, transparent, rgba(99,102,241,0.04), transparent);
  position: relative; z-index: 1;
}
.how-inner { max-width: 900px; margin: 0 auto; text-align: center; }
.how-inner .section-sub { margin: 0 auto 3.5rem; }
.steps { display: flex; gap: 2rem; align-items: flex-start; }
.step { flex: 1; text-align: center; position: relative; transition: transform 0.3s; }
.step:hover { transform: translateY(-6px); }
.step-num {
  position: absolute; top: -12px; left: 50%; transform: translateX(-50%);
  width: 24px; height: 24px; border-radius: 50%;
  background: #6366f1; color: white;
  font-size: 0.7rem; font-weight: 700;
  display: flex; align-items: center; justify-content: center;
  z-index: 2; box-shadow: 0 0 16px rgba(99,102,241,0.5);
}
.step-connector {
  position: absolute; top: 32px; left: calc(50% + 40px);
  width: calc(100% - 80px); height: 1px;
  border-top: 1px dashed rgba(99,102,241,0.3);
  animation: connector-glow 2s ease infinite alternate;
}
@keyframes connector-glow {
  from { border-color: rgba(99,102,241,0.2); }
  to { border-color: rgba(99,102,241,0.5); }
}
.step-icon {
  width: 64px; height: 64px; border-radius: 16px;
  display: flex; align-items: center; justify-content: center;
  margin: 0 auto 1rem;
  box-shadow: 0 8px 24px rgba(0,0,0,0.3);
  transition: transform 0.3s, box-shadow 0.3s;
}
.step:hover .step-icon {
  transform: scale(1.1);
  box-shadow: 0 16px 40px rgba(0,0,0,0.4);
}
.step-title { font-size: 1rem; font-weight: 700; color: #fff; margin: 0 0 0.5rem; }
.step-desc { font-size: 0.85rem; color: #71717a; line-height: 1.6; }

/* ── STATS/SERVICES ── */
.stats-section {
  padding: 100px 2rem; position: relative;
  background: linear-gradient(180deg, #09090b, #0d0d14, #09090b);
  z-index: 1;
}
.stats-bg { position: absolute; inset: 0; overflow: hidden; pointer-events: none; }
.stats-orb {
  position: absolute; width: 600px; height: 600px; border-radius: 50%;
  background: radial-gradient(circle, rgba(99,102,241,0.08), transparent);
  left: 50%; top: 50%; transform: translate(-50%, -50%);
  animation: orb-float 10s ease-in-out infinite;
}
.stats-inner { max-width: 1200px; margin: 0 auto; text-align: center; position: relative; }
.services-grid {
  display: grid; grid-template-columns: repeat(4, 1fr); gap: 1.5rem;
  text-align: left; margin-top: 3rem;
}
.service-card {
  position: relative; overflow: hidden;
  background: rgba(255,255,255,0.03);
  border: 1px solid rgba(255,255,255,0.07);
  border-radius: 16px; padding: 1.5rem;
  transition: all 0.4s cubic-bezier(0.34, 1.56, 0.64, 1);
}
.svc-pulse {
  position: absolute; top: 12px; right: 12px;
  width: 8px; height: 8px; border-radius: 50%;
  opacity: 0.7;
  animation: svc-pulse 2s ease infinite;
}
@keyframes svc-pulse {
  0%, 100% { transform: scale(1); box-shadow: 0 0 0 0 currentColor; }
  50% { transform: scale(1.2); box-shadow: 0 0 0 4px transparent; }
}
.service-card:hover {
  border-color: rgba(255,255,255,0.15);
  transform: translateY(-8px);
  box-shadow: 0 20px 60px rgba(0,0,0,0.4);
}
.svc-icon { margin-bottom: 0.8rem; transition: transform 0.3s; }
.service-card:hover .svc-icon { transform: scale(1.15) rotate(-5deg); }
.svc-name { font-size: 0.95rem; font-weight: 700; color: #fff; margin-bottom: 2px; }
.svc-port { font-size: 0.75rem; color: #52525b; margin-bottom: 1rem; }
.svc-features { list-style: none; padding: 0; margin: 0; display: flex; flex-direction: column; gap: 5px; }
.svc-features li { font-size: 0.78rem; color: #71717a; padding-left: 12px; position: relative; transition: color 0.2s; }
.svc-features li::before { content: '·'; position: absolute; left: 0; color: #52525b; }
.service-card:hover .svc-features li { color: #a1a1aa; }

/* ── CTA ── */
.cta-section {
  padding: 100px 2rem; text-align: center; position: relative;
  overflow: hidden; z-index: 1;
}
.cta-orb {
  position: absolute; border-radius: 50%;
  filter: blur(80px); pointer-events: none;
}
.cta-orb-1 {
  width: 400px; height: 400px; background: rgba(99,102,241,0.15);
  left: -100px; top: -100px;
  animation: orb-float 8s ease-in-out infinite;
}
.cta-orb-2 {
  width: 300px; height: 300px; background: rgba(139,92,246,0.1);
  right: -50px; bottom: -50px;
  animation: orb-float 10s ease-in-out infinite reverse;
}
.cta-inner { position: relative; max-width: 600px; margin: 0 auto; }
.cta-emoji {
  font-size: 3rem; margin-bottom: 1rem;
  display: block;
  animation: emoji-bounce 2s ease infinite;
}
@keyframes emoji-bounce {
  0%, 100% { transform: translateY(0) rotate(0deg); }
  25% { transform: translateY(-10px) rotate(5deg); }
  75% { transform: translateY(-5px) rotate(-3deg); }
}
.cta-title { font-size: clamp(2rem, 4vw, 3rem); font-weight: 900; color: #fff; margin: 0 0 1rem; }
.cta-sub { color: #71717a; font-size: 1rem; line-height: 1.7; margin-bottom: 2.5rem; }
.cta-actions { display: flex; justify-content: center; gap: 1rem; flex-wrap: wrap; }
.btn-outline-white {
  display: inline-flex; align-items: center; gap: 8px;
  border: 1px solid rgba(255,255,255,0.15);
  color: #a1a1aa; font-size: 1rem; font-weight: 500;
  text-decoration: none; padding: 1rem 2rem; border-radius: 12px;
  transition: all 0.3s;
}
.btn-outline-white:hover {
  border-color: rgba(255,255,255,0.4);
  color: #fff;
  transform: translateY(-2px);
  background: rgba(255,255,255,0.05);
}

/* ── FOOTER ── */
.footer {
  border-top: 1px solid rgba(255,255,255,0.06);
  padding: 2rem; position: relative; z-index: 1;
}
.footer-inner {
  max-width: 1200px; margin: 0 auto;
  display: flex; align-items: center; justify-content: space-between;
  flex-wrap: wrap; gap: 1rem;
}
.footer-logo { display: flex; align-items: center; gap: 10px; font-weight: 700; color: #fff; }
.footer-links { display: flex; gap: 1.5rem; }
.footer-links a { color: #52525b; font-size: 0.85rem; text-decoration: none; transition: color 0.2s; }
.footer-links a:hover { color: #a1a1aa; }
.footer-copy { font-size: 0.8rem; color: #3f3f46; }

/* ── RESPONSIVE ── */
@media (max-width: 900px) {
  .hero { flex-direction: column; padding-top: 100px; }
  .hero-preview { display: none; }
  .features-grid { grid-template-columns: 1fr; }
  .services-grid { grid-template-columns: repeat(2, 1fr); }
  .steps { flex-direction: column; }
  .step-connector { display: none; }
  .nav-links .btn-nav-login,
  .nav-links .nav-link { display: none; }
}
@media (max-width: 600px) {
  .services-grid { grid-template-columns: 1fr; }
  .footer-inner { flex-direction: column; text-align: center; }
}

/* STICKY STACKING CARDS */
.features-stack {
  padding-bottom: 20vh;
}
.stack-container {
  display: flex;
  flex-direction: column;
  gap: 20px;
  width: 100%;
  max-width: 1100px;
  margin: 40px auto 0;
  position: relative;
}
.stack-card {
  position: sticky;
  height: 75vh;
  display: flex;
  align-items: center;
  justify-content: center;
  transform-origin: top center;
  transition: transform 0.1s ease-out;
}
.card-inner {
  width: 100%;
  height: 100%;
  max-height: 600px;
  border-radius: 40px;
  border: 1px solid rgba(255, 255, 255, 0.1);
  display: flex;
  flex-direction: row;
  overflow: hidden;
  box-shadow: 0 -10px 40px rgba(0,0,0,0.5);
}
.card-content {
  width: 45%;
  padding: 50px;
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.card-image-wrap {
  width: 55%;
  height: 100%;
  padding: 20px;
  display: flex;
  align-items: center;
  justify-content: center;
  background: rgba(0,0,0,0.2);
}
.feat-preview-img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  border-radius: 24px;
  box-shadow: 0 20px 40px rgba(0,0,0,0.4);
}
@media (max-width: 768px) {
  .card-inner { flex-direction: column; }
  .card-content { width: 100%; padding: 30px; height: 50%; }
  .card-image-wrap { width: 100%; height: 50%; }
  .stack-card { height: 85vh; }
}


/* SAAS EXTRAS */
@keyframes marquee { 0% { transform: translateX(0); } 100% { transform: translateX(-100%); } }
.title-text { color: #e4e4e7; }
.sub-text { color: #a1a1aa; }


/* LIGHT THEME OVERRIDES */
.landing.light-theme {
  background: #f8fafc;
  color: #0f172a;
}
.landing.light-theme .nav {
  background: rgba(248, 250, 252, 0.8);
  border-bottom: 1px solid rgba(0, 0, 0, 0.05);
}
.landing.light-theme .logo-text,
.landing.light-theme .nav-link,
.landing.light-theme .btn-nav-login,
.landing.light-theme .hero-title,
.landing.light-theme .title-text,
.landing.light-theme .feat-title,
.landing.light-theme .step-title,
.landing.light-theme .section-title,
.landing.light-theme .svc-name,
.landing.light-theme .stat-val {
  color: #0f172a !important;
}

.landing.light-theme .hero-desc,
.landing.light-theme .sub-text,
.landing.light-theme .feat-desc,
.landing.light-theme .feat-list li,
.landing.light-theme .step-desc,
.landing.light-theme .section-sub,
.landing.light-theme .svc-port,
.landing.light-theme .svc-features li,
.landing.light-theme .marquee-item,
.landing.light-theme .stat-label {
  color: #475569 !important;
}

.landing.light-theme .section-badge {
  background: rgba(0,0,0,0.05) !important;
  color: #475569 !important;
  border-color: rgba(0,0,0,0.1) !important;
}

.landing.light-theme .step-num {
  color: rgba(0,0,0,0.05) !important;
}

.landing.light-theme .theme-toggle {
  background: rgba(0,0,0,0.05) !important;
  color: #0f172a !important;
}
.landing.light-theme .hero-mockup {
  background: #ffffff;
  border-color: rgba(0, 0, 0, 0.1);
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.05);
}
.landing.light-theme .hm-sidebar { background: #f1f5f9; border-right-color: rgba(0,0,0,0.05); }
.landing.light-theme .card-inner {
  background: #ffffff !important;
  border-color: rgba(0, 0, 0, 0.1) !important;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.05) !important;
}
.landing.light-theme .card-image-wrap {
  background: #f8fafc !important;
}
.landing.light-theme .mk-premium {
  background: #ffffff;
  border-color: rgba(0, 0, 0, 0.1);
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.05);
}
.landing.light-theme .mk-header { background: #f1f5f9; border-bottom-color: rgba(0,0,0,0.05); }
.landing.light-theme .mk-glass {
  background: #ffffff !important;
  border-color: rgba(0, 0, 0, 0.1) !important;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.05) !important;
}
.landing.light-theme .mk-title,
.landing.light-theme .mk-text-sm,
.landing.light-theme .mk-col-title,
.landing.light-theme .mk-setting-desc {
  color: #64748b !important;
}
.landing.light-theme .mk-bubble.mk-left { color: #0f172a !important; }
.landing.light-theme .mk-bubble.mk-right { color: white !important; }
.landing.light-theme .mk-toast {
  background: #ffffff !important;
  color: #0f172a !important;
  border-color: rgba(0, 0, 0, 0.1) !important;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1) !important;
}
.landing.light-theme .mk-setting-title { color: #0f172a !important; }
.landing.light-theme .mk-toggle { border-color: rgba(0,0,0,0.1) !important; background: rgba(0,0,0,0.05) !important; }
.landing.light-theme .mk-task-card { background: #ffffff !important; border: 1px solid rgba(0,0,0,0.1) !important; box-shadow: 0 4px 10px rgba(0,0,0,0.02) !important; }

.landing.light-theme .testimonial-card,
.landing.light-theme .p-card {
  background: #ffffff !important;
  border-color: rgba(0, 0, 0, 0.1) !important;
}
.landing.light-theme .p-card .btn-outline {
  border-color: rgba(0, 0, 0, 0.2) !important;
  color: #0f172a !important;
}
.landing.light-theme .pricing-section { background: #f8fafc !important; border-top-color: rgba(0,0,0,0.05) !important; }
.landing.light-theme .testimonials-section { border-top-color: rgba(0,0,0,0.05) !important; }
.landing.light-theme .cta-section { background: #f8fafc !important; }
/* Removed ugly cta-inner box */
.landing.light-theme .cta-title { color: #0f172a !important; }
.landing.light-theme .cta-sub { color: #475569 !important; }
.landing.light-theme .footer { border-top-color: rgba(0,0,0,0.05) !important; }
.landing.light-theme .footer-text { color: #64748b !important; }
.landing.light-theme .stats-section { background: #f8fafc !important; }
.landing.light-theme .service-card { background: #ffffff !important; border: 1px solid rgba(0,0,0,0.1) !important; box-shadow: 0 10px 30px rgba(0,0,0,0.05) !important; }

.landing.light-theme .btn-outline-white {
  color: #0f172a !important;
  border-color: rgba(0, 0, 0, 0.2) !important;
}
.landing.light-theme .btn-outline-white:hover {
  background: rgba(0, 0, 0, 0.05) !important;
}

/* BETTER ANIMATIONS */
.cta-inner {
  transition: transform 0.5s cubic-bezier(0.4, 0, 0.2, 1), box-shadow 0.5s ease;
}
.cta-inner:hover {
  transform: translateY(-5px) scale(1.01);
  box-shadow: 0 30px 60px rgba(0, 0, 0, 0.1) !important;
}

.btn-primary.large {
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}
.btn-primary.large:hover {
  transform: translateY(-2px);
  box-shadow: 0 15px 30px rgba(99, 102, 241, 0.4);
}

.btn-outline-white {
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}
.btn-outline-white:hover {
  transform: translateY(-2px);
  background: rgba(255, 255, 255, 0.1);
}

/* Floating animation for mockups */
@keyframes float-soft {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-10px); }
}
.hero-mockup {
  animation: float-soft 6s ease-in-out infinite;
}


/* FOOTER AND NAV FIXES */
.landing.light-theme .footer-logo span { color: #0f172a !important; }
.landing.light-theme .footer-links a { color: #64748b !important; }
.landing.light-theme .footer-links a:hover { color: #6366f1 !important; }
.landing.light-theme .footer-copy { color: #94a3b8 !important; }

.landing.light-theme .nav-lang-dropdown { border-color: rgba(0,0,0,0.1) !important; background: rgba(0,0,0,0.03) !important; }
.landing.light-theme .nav-lang-dropdown .current-lang { color: #0f172a !important; }
.landing.light-theme .nav-lang-dropdown svg { stroke: #0f172a !important; }
.landing.light-theme .lang-menu { background: #ffffff !important; border: 1px solid rgba(0,0,0,0.1) !important; box-shadow: 0 10px 30px rgba(0,0,0,0.1) !important; }
.landing.light-theme .lang-item { color: #475569 !important; }
.landing.light-theme .lang-item:hover { background: #f1f5f9 !important; color: #0f172a !important; }
.landing.light-theme .lang-item.active { background: rgba(99, 102, 241, 0.1) !important; color: #6366f1 !important; }


/* ENHANCED CONTINUOUS ANIMATIONS */
@keyframes continuous-float {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-12px); }
}

@keyframes continuous-pulse {
  0%, 100% { transform: scale(1); opacity: 0.8; }
  50% { transform: scale(1.05); opacity: 1; }
}

.cta-inner {
  animation: continuous-float 6s ease-in-out infinite;
}
.cta-inner:hover {
  animation-play-state: paused;
}

.cta-orb-1, .cta-orb-2 {
  animation: continuous-pulse 8s ease-in-out infinite alternate !important;
}

.hero-mockup {
  animation: continuous-float 7s ease-in-out infinite;
}
.hero-mockup:hover {
  animation-play-state: paused;
}


/* HUD & ADVANCED ANIMATIONS (2026) */
.mk-hud {
  display: flex; justify-content: center; align-items: center; width: 100%; height: 100%;
}
.float-anim {
  animation: hud-float 6s cubic-bezier(0.16, 1, 0.3, 1) infinite alternate;
}
.float-anim-delayed {
  animation: hud-float 6s cubic-bezier(0.16, 1, 0.3, 1) infinite alternate;
  animation-delay: -3s;
}
@keyframes hud-float {
  0% { transform: translateY(0px); }
  100% { transform: translateY(-15px); }
}

/* Fix sticky card inner clipping */
.card-inner {
  max-height: 85vh !important;
  overflow: visible !important;
}

/* Light theme overrides for HUD */
.landing.light-theme .mk-hud .mk-glass {
  background: rgba(255,255,255,0.7) !important;
  border: 1px solid rgba(0,0,0,0.05) !important;
  box-shadow: 0 30px 60px rgba(0,0,0,0.05) !important;
}
.landing.light-theme .mk-hud .mk-setting-title,
.landing.light-theme .mk-hud .mk-bubble.mk-left,
.landing.light-theme .mk-hud .mk-toast {
  color: #0f172a !important;
}
.landing.light-theme .mk-hud .mk-bubble.mk-right {
  color: #ffffff !important; /* Keep gradient text white */
}


/* VERTICAL TIMELINE DESIGN */
.steps-vertical {
  display: flex;
  flex-direction: column;
  gap: 4rem;
  max-width: 700px;
  margin: 40px auto 0;
  position: relative;
  text-align: left;
}
.steps-vertical::before {
  content: '';
  position: absolute;
  top: 40px; bottom: 0;
  left: 40px;
  width: 2px;
  background: linear-gradient(180deg, rgba(99,102,241,0.5) 0%, rgba(236,72,153,0.1) 100%);
  z-index: 0;
}

.step-v {
  display: flex;
  align-items: flex-start;
  gap: 3rem;
  position: relative;
  z-index: 1;
}

.step-v-icon-wrap {
  position: relative;
  flex-shrink: 0;
}

.step-v-icon {
  width: 80px; height: 80px;
  border-radius: 24px;
  display: flex; align-items: center; justify-content: center;
  position: relative;
  z-index: 2;
}

.step-v-num {
  position: absolute;
  top: -12px; right: -12px;
  width: 28px; height: 28px;
  border-radius: 50%;
  background: #ffffff; color: #0f172a;
  font-size: 0.8rem; font-weight: 800;
  display: flex; align-items: center; justify-content: center;
  z-index: 3;
  box-shadow: 0 4px 10px rgba(0,0,0,0.2);
}

.step-v-content {
  padding-top: 10px;
}
.step-v-title {
  font-size: 1.8rem; font-weight: 800; color: white; margin-bottom: 0.5rem;
}
.step-v-desc {
  font-size: 1.1rem; color: rgba(255,255,255,0.7); line-height: 1.6;
}

/* NEW ADVANCED SLIDE-RIGHT ANIMATION */
.reveal-slide-right {
  opacity: 0;
  transform: translateX(-60px);
  transition: all 1s cubic-bezier(0.16, 1, 0.3, 1);
}
.reveal-slide-right.revealed {
  opacity: 1;
  transform: translateX(0);
}

/* Light theme overrides for vertical steps */
.landing.light-theme .step-v-title { color: #0f172a !important; }
.landing.light-theme .step-v-desc { color: #475569 !important; }
.landing.light-theme .step-v-num { background: #0f172a; color: #ffffff; }

/* Responsive */
@media (max-width: 768px) {
  .steps-vertical { gap: 3rem; }
  .steps-vertical::before { left: 30px; }
  .step-v { gap: 1.5rem; }
  .step-v-icon { width: 60px; height: 60px; border-radius: 16px; }
  .step-v-title { font-size: 1.4rem; }
  .step-v-desc { font-size: 0.95rem; }
}


/* SERVICES 2x2 GRID UPGRADE */
.services-grid {
  display: grid; 
  grid-template-columns: repeat(2, 1fr) !important; 
  gap: 2.5rem !important;
  text-align: left; 
  margin-top: 4rem;
}

.service-card {
  background: rgba(255,255,255,0.02) !important;
  border: 1px solid rgba(255,255,255,0.05) !important;
  border-radius: 32px !important;
  padding: 2.5rem !important;
  transition: all 0.5s cubic-bezier(0.16, 1, 0.3, 1) !important;
}

.service-card:hover {
  transform: translateY(-8px) scale(1.02);
  background: rgba(255,255,255,0.04) !important;
  border-color: var(--theme-color) !important;
  box-shadow: 0 40px 80px rgba(0,0,0,0.6), inset 0 1px 0 rgba(255,255,255,0.1) !important;
}

.svc-name-large {
  font-size: 1.6rem;
  font-weight: 800;
  color: white;
  margin-bottom: 6px;
  letter-spacing: -0.5px;
}
.svc-port-badge {
  display: inline-block;
  padding: 4px 10px;
  border-radius: 8px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 0.8rem;
  font-weight: 600;
}

.svc-features-grid {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 16px;
  list-style: none;
  padding: 0;
  margin: 0;
}
.svc-features-grid li {
  display: flex;
  align-items: center;
  gap: 10px;
  color: rgba(255,255,255,0.7);
  font-size: 1.05rem;
  transition: color 0.3s;
}
.service-card:hover .svc-features-grid li {
  color: rgba(255,255,255,0.95);
}

/* Light Theme overrides for wide cards */
.landing.light-theme .svc-name-large { color: #0f172a !important; }
.landing.light-theme .svc-features-grid li { color: #475569 !important; }
.landing.light-theme .service-card {
  background: #ffffff !important;
  border-color: rgba(0,0,0,0.1) !important;
  box-shadow: 0 20px 40px rgba(0,0,0,0.03) !important;
}
.landing.light-theme .service-card:hover {
  box-shadow: 0 40px 80px rgba(0,0,0,0.1) !important;
}
.landing.light-theme .svc-icon-wrap {
  background: #f8fafc !important;
  border-color: rgba(0,0,0,0.05) !important;
}

@media (max-width: 900px) {
  .services-grid { grid-template-columns: 1fr !important; }
  .svc-features-grid { grid-template-columns: 1fr; }
}


/* SERVICES TABS & CSS MOCKUPS */
.services-tabs-container {
  display: flex; gap: 2rem;
  margin-top: 4rem;
  text-align: left;
  max-width: 1100px;
  margin-left: auto; margin-right: auto;
}

.svc-tabs-list {
  display: flex; flex-direction: column; gap: 12px;
  width: 300px; flex-shrink: 0;
}

.svc-tab-item {
  display: flex; align-items: center; gap: 16px;
  padding: 20px 24px;
  border-radius: 16px;
  background: rgba(255,255,255,0.02);
  border: 1px solid rgba(255,255,255,0.05);
  cursor: pointer;
  transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1);
  color: rgba(255,255,255,0.6);
}

.svc-tab-item:hover {
  background: rgba(255,255,255,0.05);
  color: white;
}

.svc-tab-item.active {
  background: rgba(255,255,255,0.08);
  border-color: var(--theme-color);
  color: white;
  box-shadow: 0 10px 30px rgba(0,0,0,0.3);
}

.svc-tab-icon {
  width: 24px; height: 24px;
  display: flex; align-items: center; justify-content: center;
  transition: color 0.3s;
}

.svc-tab-item.active .svc-tab-icon {
  color: var(--theme-color);
  filter: drop-shadow(0 0 8px var(--theme-color));
}

.svc-tab-name {
  font-size: 1.1rem; font-weight: 600;
}

.svc-tabs-content {
  flex-grow: 1;
  background: rgba(255,255,255,0.02);
  border: 1px solid rgba(255,255,255,0.05);
  border-radius: 24px;
  padding: 3rem;
  position: relative;
  overflow: hidden;
  display: flex; flex-direction: column;
}

.svc-pane-top {
  margin-bottom: 3rem;
}

.svc-pane-header {
  display: flex; align-items: center; justify-content: space-between;
  margin-bottom: 2rem;
}

.svc-pane-title {
  font-size: 2.2rem; font-weight: 800; color: white; margin: 0;
}

.svc-port-badge {
  display: inline-block; padding: 6px 16px;
  border-radius: 12px;
  background: rgba(255,255,255,0.05);
  color: var(--theme-color);
  font-family: 'JetBrains Mono', monospace; font-size: 0.9rem; font-weight: 700;
  border: 1px solid var(--theme-color);
}

.svc-pane-features {
  display: grid; grid-template-columns: repeat(2, 1fr); gap: 16px;
  list-style: none; padding: 0; margin: 0;
}
.svc-pane-features li {
  display: flex; align-items: center; gap: 12px;
  color: rgba(255,255,255,0.8); font-size: 1.1rem;
}

/* Transitions */
.fade-up-enter-active, .fade-up-leave-active {
  transition: all 0.4s cubic-bezier(0.16, 1, 0.3, 1);
}
.fade-up-enter-from { opacity: 0; transform: translateY(20px); }
.fade-up-leave-to { opacity: 0; transform: translateY(-20px); }

/* --- CSS MOCKUPS --- */
.svc-mockup-area {
  height: 250px;
  background: rgba(0,0,0,0.2);
  border-radius: 16px;
  border: 1px solid rgba(255,255,255,0.05);
  display: flex; align-items: center; justify-content: center;
  position: relative; overflow: hidden;
}

/* Tab 0: API Mockup */
.mockup-api { display: flex; align-items: center; gap: 20px; }
.api-node {
  padding: 12px 24px; border-radius: 8px; font-weight: 600; font-size: 0.9rem;
  background: #1e1e24; border: 2px solid rgba(255,255,255,0.1); color: white;
  position: relative; z-index: 2;
}
.api-node.gateway {
  border-color: #6366f1; box-shadow: 0 0 20px rgba(99,102,241,0.4);
  padding: 20px 30px; font-size: 1.1rem;
}
.api-node.proj { border-color: #f59e0b; }
.api-node.task { border-color: #ef4444; }
.api-node.noti { border-color: #10b981; }

.api-line, .api-line-out {
  height: 2px; background: rgba(255,255,255,0.1); position: relative;
}
.api-line { width: 50px; }
.api-lines-out { display: flex; flex-direction: column; gap: 28px; justify-content: center; height: 120px; }
.api-line-out { width: 40px; }

.api-dot {
  width: 8px; height: 8px; border-radius: 50%; background: white;
  position: absolute; top: -3px; left: 0;
  box-shadow: 0 0 10px white;
  animation: api-flow 1.5s infinite linear;
}
@keyframes api-flow {
  0% { left: 0; opacity: 1; }
  100% { left: 100%; opacity: 0; }
}
.api-services { display: flex; flex-direction: column; gap: 12px; }

/* Tab 1: Project Mockup */
.mockup-project {
  width: 350px; background: #18181b; border-radius: 16px; border: 1px solid rgba(255,255,255,0.05);
  padding: 20px; box-shadow: 0 20px 40px rgba(0,0,0,0.4);
  animation: float 4s ease-in-out infinite alternate;
}
.pj-header { font-size: 1.1rem; font-weight: 700; color: white; margin-bottom: 16px; }
.pj-list { display: flex; flex-direction: column; gap: 12px; }
.pj-item {
  background: rgba(255,255,255,0.03); padding: 12px; border-radius: 8px;
  animation: slide-right 0.5s cubic-bezier(0.16, 1, 0.3, 1) backwards;
}
.pj-info { display: flex; align-items: center; gap: 12px; margin-bottom: 12px; }
.pj-avatar { width: 24px; height: 24px; border-radius: 6px; }
.pj-name { font-size: 0.9rem; color: #e4e4e7; font-weight: 600; }
.pj-progress { height: 6px; background: rgba(255,255,255,0.1); border-radius: 3px; overflow: hidden; }
.pj-fill { height: 100%; animation: fill-bar 1.5s cubic-bezier(0.16, 1, 0.3, 1) forwards; transform-origin: left; transform: scaleX(0); }
@keyframes fill-bar { to { transform: scaleX(1); } }
@keyframes slide-right { from { opacity: 0; transform: translateX(-20px); } to { opacity: 1; transform: translateX(0); } }

/* Tab 2: Task Mockup */
.mockup-task {
  display: flex; gap: 16px; transform: rotateX(10deg) rotateZ(-5deg); perspective: 1000px;
  transform-style: preserve-3d;
}
.kb-col {
  width: 120px; background: rgba(255,255,255,0.02); border-radius: 12px; padding: 12px;
  border: 1px solid rgba(255,255,255,0.05);
}
.kb-title { font-size: 0.7rem; font-weight: 700; color: #a1a1aa; margin-bottom: 12px; letter-spacing: 1px; }
.kb-card {
  height: 40px; background: #27272a; border-radius: 6px; margin-bottom: 8px;
  border: 1px solid rgba(255,255,255,0.05);
}
.kb-drag {
  border-color: #ef4444; box-shadow: 0 10px 20px rgba(239,68,68,0.3);
  transform: translateZ(20px) rotate(-3deg);
  animation: kb-float 3s infinite alternate;
}
@keyframes kb-float { to { transform: translateZ(30px) rotate(-3deg) translateY(-10px); } }

/* Tab 3: Notify Mockup */
.mockup-notify {
  display: flex; flex-direction: column; align-items: center; gap: 24px;
}
.nf-bell {
  position: relative; animation: bell-shake 4s infinite;
}
.nf-badge {
  position: absolute; top: -5px; right: -5px; width: 20px; height: 20px; border-radius: 50%;
  background: #ef4444; color: white; font-size: 0.7rem; font-weight: bold;
  display: flex; align-items: center; justify-content: center;
  box-shadow: 0 0 10px rgba(239,68,68,0.5);
}
.nf-ring {
  position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);
  width: 60px; height: 60px; border-radius: 50%;
  border: 2px solid var(--theme-color); opacity: 0;
  animation: ring-pulse 2s infinite;
}
.nf-toast {
  background: #18181b; border: 1px solid rgba(16,185,129,0.3); border-radius: 12px;
  padding: 12px 20px; display: flex; align-items: center; gap: 12px;
  box-shadow: 0 20px 40px rgba(0,0,0,0.5);
  animation: toast-up 4s infinite cubic-bezier(0.16, 1, 0.3, 1);
}
.nf-toast-icon {
  width: 24px; height: 24px; border-radius: 50%; display: flex; align-items: center; justify-content: center;
  font-size: 0.8rem; font-weight: bold;
}
.nf-toast-text { color: #e4e4e7; font-size: 0.95rem; font-weight: 500; }

@keyframes bell-shake {
  0%, 80%, 100% { transform: rotate(0); }
  85% { transform: rotate(15deg); }
  90% { transform: rotate(-15deg); }
  95% { transform: rotate(10deg); }
}
@keyframes ring-pulse {
  0% { transform: translate(-50%, -50%) scale(0.8); opacity: 0.8; }
  100% { transform: translate(-50%, -50%) scale(1.5); opacity: 0; }
}
@keyframes toast-up {
  0%, 10% { opacity: 0; transform: translateY(30px); }
  20%, 80% { opacity: 1; transform: translateY(0); }
  90%, 100% { opacity: 0; transform: translateY(-30px); }
}

/* Light Theme overrides */
.landing.light-theme .svc-tab-item { color: #475569; border-color: rgba(0,0,0,0.05); }
.landing.light-theme .svc-tab-item.active { background: #ffffff; color: #0f172a; box-shadow: 0 10px 30px rgba(0,0,0,0.05); }
.landing.light-theme .svc-tabs-content { background: #ffffff; border-color: rgba(0,0,0,0.05); box-shadow: 0 20px 40px rgba(0,0,0,0.02); }
.landing.light-theme .svc-pane-title { color: #0f172a; }
.landing.light-theme .svc-pane-features li { color: #475569; }
.landing.light-theme .svc-mockup-area { background: #f8fafc; border-color: rgba(0,0,0,0.05); }

/* Responsive */
@media (max-width: 900px) {
  .services-tabs-container { flex-direction: column; }
  .svc-tabs-list { width: 100%; flex-direction: row; flex-wrap: wrap; }
  .svc-tab-item { flex: 1 1 45%; }
  .svc-tabs-content { padding: 2rem; }
  .svc-pane-features { grid-template-columns: 1fr; }
  .mockup-task { transform: scale(0.8); }
}


/* NEW HERO STATS REDESIGN */
.hero-stats-new {
  display: flex; align-items: center; gap: 16px; flex-wrap: wrap; margin-top: 1rem;
}
.stat-pill {
  display: flex; align-items: center; gap: 12px;
  background: rgba(255,255,255,0.03);
  border: 1px solid rgba(255,255,255,0.08);
  border-radius: 100px;
  padding: 8px 24px 8px 8px;
  backdrop-filter: blur(10px);
  transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1);
  text-align: left;
}
.stat-pill:hover {
  transform: translateY(-4px);
  background: rgba(255,255,255,0.06);
  border-color: rgba(99,102,241,0.3);
  box-shadow: 0 10px 30px rgba(99,102,241,0.1);
}
.stat-icon {
  width: 36px; height: 36px; border-radius: 50%;
  background: rgba(255,255,255,0.05);
  display: flex; align-items: center; justify-content: center;
}
.stat-val { font-size: 1.1rem; font-weight: 800; color: white; line-height: 1; margin-bottom: 2px; }
.stat-desc { font-size: 0.7rem; color: #a1a1aa; text-transform: uppercase; letter-spacing: 1px; }

/* Light Theme overrides for Hero Stats */
.landing.light-theme .stat-pill {
  background: #ffffff !important;
  border-color: rgba(0,0,0,0.08) !important;
  box-shadow: 0 10px 20px rgba(0,0,0,0.03) !important;
}
.landing.light-theme .stat-val { color: #0f172a !important; background: transparent !important; }
.landing.light-theme .stat-desc { color: #64748b !important; }
.landing.light-theme .stat-icon { background: #f8fafc !important; }

</style>

<style>
/* ── MOCKUP UI CSS ── */

/* PREMIUM CSS MOCKUPS 2026 */
.mk-premium {
  width: 100%; max-width: 420px; background: rgba(20,20,25,0.4);
  border-radius: 16px; border: 1px solid rgba(255,255,255,0.08);
  overflow: hidden; display: flex; flex-direction: column;
  box-shadow: 0 24px 60px rgba(0,0,0,0.6), inset 0 1px 0 rgba(255,255,255,0.1);
  backdrop-filter: blur(20px);
  -webkit-backdrop-filter: blur(20px);
}
.mk-header {
  padding: 12px 16px; background: rgba(0,0,0,0.3);
  display: flex; align-items: center; justify-content: space-between;
  border-bottom: 1px solid rgba(255,255,255,0.05);
}
.mk-dots { display: flex; gap: 6px; }
.mk-dots span { width: 10px; height: 10px; border-radius: 50%; box-shadow: inset 0 1px 2px rgba(255,255,255,0.3); }
.mk-dot-r { background: #ff5f56; } .mk-dot-y { background: #ffbd2e; } .mk-dot-g { background: #27c93f; }
.mk-title { font-size: 0.75rem; color: #a1a1aa; font-weight: 500; font-family: 'Inter', sans-serif; letter-spacing: 0.5px; }

.mk-body { padding: 16px; }
.mk-flex { display: flex; gap: 16px; }
.mk-glass {
  background: rgba(255,255,255,0.03);
  border: 1px solid rgba(255,255,255,0.05);
  border-radius: 12px;
  box-shadow: 0 4px 20px rgba(0,0,0,0.2);
}
.mk-sidebar { width: 30%; padding: 12px; display: flex; flex-direction: column; gap: 10px; }
.mk-sb-item { height: 8px; border-radius: 4px; background: rgba(255,255,255,0.1); width: 100%; transition: 0.3s; }
.mk-sb-item.mk-active { background: linear-gradient(90deg, #6366f1, #8b5cf6); box-shadow: 0 0 10px rgba(99,102,241,0.5); }
.mk-content { width: 70%; display: flex; flex-direction: column; }
.mk-card { padding: 16px; }
.mk-gradient-text { background: linear-gradient(135deg, #6366f1, #8b5cf6); -webkit-background-clip: text; -webkit-text-fill-color: transparent; }
.mk-progress-bar { width: 100%; height: 6px; background: rgba(255,255,255,0.1); border-radius: 4px; overflow: hidden; }
.mk-progress-fill { height: 100%; border-radius: 4px; box-shadow: 0 0 10px rgba(99,102,241,0.5); }

.mk-kanban { display: flex; gap: 12px; }
.mk-col { flex: 1; display: flex; flex-direction: column; gap: 10px; }
.mk-col-title { font-size: 0.75rem; color: #a1a1aa; font-weight: 600; text-transform: uppercase; letter-spacing: 1px; margin-bottom: 4px; }
.mk-task-card { padding: 12px; display: flex; flex-direction: column; gap: 8px; cursor: grab; transition: transform 0.2s, box-shadow 0.2s; }
.mk-task-card:hover { transform: translateY(-2px); box-shadow: 0 10px 20px rgba(0,0,0,0.4); }
.mk-drag { transform: scale(1.05) rotate(2deg); box-shadow: 0 15px 30px rgba(139,92,246,0.3); border-color: rgba(139,92,246,0.5); }
.mk-badge { font-size: 0.6rem; padding: 2px 6px; border-radius: 4px; font-weight: 600; display: inline-block; width: fit-content; text-transform: uppercase; }
.mk-badge-blue { background: rgba(59,130,246,0.2); color: #60a5fa; border: 1px solid rgba(59,130,246,0.4); }
.mk-badge-red { background: rgba(239,68,68,0.2); color: #f87171; border: 1px solid rgba(239,68,68,0.4); }
.mk-badge-purple { background: rgba(139,92,246,0.2); color: #a78bfa; border: 1px solid rgba(139,92,246,0.4); }
.mk-task-line { height: 6px; background: rgba(255,255,255,0.1); border-radius: 4px; }

.mk-chat { display: flex; flex-direction: column; gap: 12px; position: relative; }
.mk-bubble { padding: 10px 14px; border-radius: 18px; font-size: 0.8rem; max-width: 80%; line-height: 1.4; color: #e4e4e7; font-family: 'Inter', sans-serif; }
.mk-left { align-self: flex-start; border-bottom-left-radius: 4px; }
.mk-right { align-self: flex-end; border-bottom-right-radius: 4px; }
.mk-gradient-bg { background: linear-gradient(135deg, #6366f1, #8b5cf6); box-shadow: 0 4px 15px rgba(99,102,241,0.4); border: none; }
.mk-toast { position: absolute; bottom: 0; left: 50%; transform: translateX(-50%) translateY(50%); padding: 10px 16px; border-radius: 30px; font-size: 0.75rem; white-space: nowrap; box-shadow: 0 10px 30px rgba(0,0,0,0.5); border: 1px solid rgba(255,189,46,0.3); background: rgba(30,25,10,0.8); }
.mk-highlight { color: #ffbd2e; font-weight: 600; }

.mk-settings { display: flex; flex-direction: column; gap: 12px; }
.mk-setting-row { display: flex; justify-content: space-between; align-items: center; padding: 14px 16px; }
.mk-setting-title { font-size: 0.85rem; color: #fff; font-weight: 500; margin-bottom: 2px; font-family: 'Inter', sans-serif; }
.mk-setting-desc { font-size: 0.7rem; color: #a1a1aa; font-family: 'Inter', sans-serif; }
.mk-toggle { width: 44px; height: 24px; background: rgba(255,255,255,0.1); border-radius: 12px; position: relative; cursor: pointer; transition: 0.3s; border: 1px solid rgba(255,255,255,0.05); }
.mk-toggle-circle { width: 18px; height: 18px; background: #fff; border-radius: 50%; position: absolute; top: 2px; left: 3px; transition: 0.3s; box-shadow: 0 2px 5px rgba(0,0,0,0.3); }
.mk-toggle-on { background: #10b981; border-color: #10b981; box-shadow: 0 0 10px rgba(16,185,129,0.4); }
.mk-toggle-on .mk-toggle-circle { left: 21px; }

/* RE-APPLY STACKING FIX FOR PREVIOUS OLD CSS */
.features-stack { padding-bottom: 20vh; }
.stack-container { display: flex; flex-direction: column; gap: 20px; width: 100%; max-width: 1100px; margin: 40px auto 0; position: relative; }
.stack-card { position: sticky; height: 75vh; display: flex; align-items: center; justify-content: center; transform-origin: top center; transition: transform 0.1s ease-out; }
.card-inner { width: 100%; height: 100%; max-height: 600px; border-radius: 40px; border: 1px solid rgba(255, 255, 255, 0.1); display: flex; flex-direction: row; overflow: hidden; box-shadow: 0 -10px 40px rgba(0,0,0,0.5); }
.card-content { width: 45%; padding: 50px; display: flex; flex-direction: column; justify-content: center; }
.card-image-wrap { width: 55%; height: 100%; padding: 20px; display: flex; align-items: center; justify-content: center; background: rgba(0,0,0,0.2); }

/* SAAS EXTRAS */
@keyframes marquee { 0% { transform: translateX(0); } 100% { transform: translateX(-100%); } }
.title-text { color: #e4e4e7; }
.sub-text { color: #a1a1aa; }


/* LIGHT THEME OVERRIDES */
.landing.light-theme {
  background: #f8fafc;
  color: #0f172a;
}
.landing.light-theme .nav {
  background: rgba(248, 250, 252, 0.8);
  border-bottom: 1px solid rgba(0, 0, 0, 0.05);
}
.landing.light-theme .logo-text,
.landing.light-theme .nav-link,
.landing.light-theme .btn-nav-login,
.landing.light-theme .hero-title,
.landing.light-theme .title-text,
.landing.light-theme .feat-title,
.landing.light-theme .step-title,
.landing.light-theme .section-title,
.landing.light-theme .svc-name,
.landing.light-theme .stat-val {
  color: #0f172a !important;
}

.landing.light-theme .hero-desc,
.landing.light-theme .sub-text,
.landing.light-theme .feat-desc,
.landing.light-theme .feat-list li,
.landing.light-theme .step-desc,
.landing.light-theme .section-sub,
.landing.light-theme .svc-port,
.landing.light-theme .svc-features li,
.landing.light-theme .marquee-item,
.landing.light-theme .stat-label {
  color: #475569 !important;
}

.landing.light-theme .section-badge {
  background: rgba(0,0,0,0.05) !important;
  color: #475569 !important;
  border-color: rgba(0,0,0,0.1) !important;
}

.landing.light-theme .step-num {
  color: rgba(0,0,0,0.05) !important;
}

.landing.light-theme .theme-toggle {
  background: rgba(0,0,0,0.05) !important;
  color: #0f172a !important;
}
.landing.light-theme .hero-mockup {
  background: #ffffff;
  border-color: rgba(0, 0, 0, 0.1);
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.05);
}
.landing.light-theme .hm-sidebar { background: #f1f5f9; border-right-color: rgba(0,0,0,0.05); }
.landing.light-theme .card-inner {
  background: #ffffff !important;
  border-color: rgba(0, 0, 0, 0.1) !important;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.05) !important;
}
.landing.light-theme .card-image-wrap {
  background: #f8fafc !important;
}
.landing.light-theme .mk-premium {
  background: #ffffff;
  border-color: rgba(0, 0, 0, 0.1);
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.05);
}
.landing.light-theme .mk-header { background: #f1f5f9; border-bottom-color: rgba(0,0,0,0.05); }
.landing.light-theme .mk-glass {
  background: #ffffff !important;
  border-color: rgba(0, 0, 0, 0.1) !important;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.05) !important;
}
.landing.light-theme .mk-title,
.landing.light-theme .mk-text-sm,
.landing.light-theme .mk-col-title,
.landing.light-theme .mk-setting-desc {
  color: #64748b !important;
}
.landing.light-theme .mk-bubble.mk-left { color: #0f172a !important; }
.landing.light-theme .mk-bubble.mk-right { color: white !important; }
.landing.light-theme .mk-toast {
  background: #ffffff !important;
  color: #0f172a !important;
  border-color: rgba(0, 0, 0, 0.1) !important;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1) !important;
}
.landing.light-theme .mk-setting-title { color: #0f172a !important; }
.landing.light-theme .mk-toggle { border-color: rgba(0,0,0,0.1) !important; background: rgba(0,0,0,0.05) !important; }
.landing.light-theme .mk-task-card { background: #ffffff !important; border: 1px solid rgba(0,0,0,0.1) !important; box-shadow: 0 4px 10px rgba(0,0,0,0.02) !important; }

.landing.light-theme .testimonial-card,
.landing.light-theme .p-card {
  background: #ffffff !important;
  border-color: rgba(0, 0, 0, 0.1) !important;
}
.landing.light-theme .p-card .btn-outline {
  border-color: rgba(0, 0, 0, 0.2) !important;
  color: #0f172a !important;
}
.landing.light-theme .pricing-section { background: #f8fafc !important; border-top-color: rgba(0,0,0,0.05) !important; }
.landing.light-theme .testimonials-section { border-top-color: rgba(0,0,0,0.05) !important; }
.landing.light-theme .cta-section { background: #f8fafc !important; }
/* Removed ugly cta-inner box */
.landing.light-theme .cta-title { color: #0f172a !important; }
.landing.light-theme .cta-sub { color: #475569 !important; }
.landing.light-theme .footer { border-top-color: rgba(0,0,0,0.05) !important; }
.landing.light-theme .footer-text { color: #64748b !important; }
.landing.light-theme .stats-section { background: #f8fafc !important; }
.landing.light-theme .service-card { background: #ffffff !important; border: 1px solid rgba(0,0,0,0.1) !important; box-shadow: 0 10px 30px rgba(0,0,0,0.05) !important; }

.landing.light-theme .btn-outline-white {
  color: #0f172a !important;
  border-color: rgba(0, 0, 0, 0.2) !important;
}
.landing.light-theme .btn-outline-white:hover {
  background: rgba(0, 0, 0, 0.05) !important;
}

/* BETTER ANIMATIONS */
.cta-inner {
  transition: transform 0.5s cubic-bezier(0.4, 0, 0.2, 1), box-shadow 0.5s ease;
}
.cta-inner:hover {
  transform: translateY(-5px) scale(1.01);
  box-shadow: 0 30px 60px rgba(0, 0, 0, 0.1) !important;
}

.btn-primary.large {
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}
.btn-primary.large:hover {
  transform: translateY(-2px);
  box-shadow: 0 15px 30px rgba(99, 102, 241, 0.4);
}

.btn-outline-white {
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}
.btn-outline-white:hover {
  transform: translateY(-2px);
  background: rgba(255, 255, 255, 0.1);
}

/* Floating animation for mockups */
@keyframes float-soft {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-10px); }
}
.hero-mockup {
  animation: float-soft 6s ease-in-out infinite;
}


/* FOOTER AND NAV FIXES */
.landing.light-theme .footer-logo span { color: #0f172a !important; }
.landing.light-theme .footer-links a { color: #64748b !important; }
.landing.light-theme .footer-links a:hover { color: #6366f1 !important; }
.landing.light-theme .footer-copy { color: #94a3b8 !important; }

.landing.light-theme .nav-lang-dropdown { border-color: rgba(0,0,0,0.1) !important; background: rgba(0,0,0,0.03) !important; }
.landing.light-theme .nav-lang-dropdown .current-lang { color: #0f172a !important; }
.landing.light-theme .nav-lang-dropdown svg { stroke: #0f172a !important; }
.landing.light-theme .lang-menu { background: #ffffff !important; border: 1px solid rgba(0,0,0,0.1) !important; box-shadow: 0 10px 30px rgba(0,0,0,0.1) !important; }
.landing.light-theme .lang-item { color: #475569 !important; }
.landing.light-theme .lang-item:hover { background: #f1f5f9 !important; color: #0f172a !important; }
.landing.light-theme .lang-item.active { background: rgba(99, 102, 241, 0.1) !important; color: #6366f1 !important; }


/* ENHANCED CONTINUOUS ANIMATIONS */
@keyframes continuous-float {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-12px); }
}

@keyframes continuous-pulse {
  0%, 100% { transform: scale(1); opacity: 0.8; }
  50% { transform: scale(1.05); opacity: 1; }
}

.cta-inner {
  animation: continuous-float 6s ease-in-out infinite;
}
.cta-inner:hover {
  animation-play-state: paused;
}

.cta-orb-1, .cta-orb-2 {
  animation: continuous-pulse 8s ease-in-out infinite alternate !important;
}

.hero-mockup {
  animation: continuous-float 7s ease-in-out infinite;
}
.hero-mockup:hover {
  animation-play-state: paused;
}


/* HUD & ADVANCED ANIMATIONS (2026) */
.mk-hud {
  display: flex; justify-content: center; align-items: center; width: 100%; height: 100%;
}
.float-anim {
  animation: hud-float 6s cubic-bezier(0.16, 1, 0.3, 1) infinite alternate;
}
.float-anim-delayed {
  animation: hud-float 6s cubic-bezier(0.16, 1, 0.3, 1) infinite alternate;
  animation-delay: -3s;
}
@keyframes hud-float {
  0% { transform: translateY(0px); }
  100% { transform: translateY(-15px); }
}

/* Fix sticky card inner clipping */
.card-inner {
  max-height: 85vh !important;
  overflow: visible !important;
}

/* Light theme overrides for HUD */
.landing.light-theme .mk-hud .mk-glass {
  background: rgba(255,255,255,0.7) !important;
  border: 1px solid rgba(0,0,0,0.05) !important;
  box-shadow: 0 30px 60px rgba(0,0,0,0.05) !important;
}
.landing.light-theme .mk-hud .mk-setting-title,
.landing.light-theme .mk-hud .mk-bubble.mk-left,
.landing.light-theme .mk-hud .mk-toast {
  color: #0f172a !important;
}
.landing.light-theme .mk-hud .mk-bubble.mk-right {
  color: #ffffff !important; /* Keep gradient text white */
}


/* VERTICAL TIMELINE DESIGN */
.steps-vertical {
  display: flex;
  flex-direction: column;
  gap: 4rem;
  max-width: 700px;
  margin: 40px auto 0;
  position: relative;
  text-align: left;
}
.steps-vertical::before {
  content: '';
  position: absolute;
  top: 40px; bottom: 0;
  left: 40px;
  width: 2px;
  background: linear-gradient(180deg, rgba(99,102,241,0.5) 0%, rgba(236,72,153,0.1) 100%);
  z-index: 0;
}

.step-v {
  display: flex;
  align-items: flex-start;
  gap: 3rem;
  position: relative;
  z-index: 1;
}

.step-v-icon-wrap {
  position: relative;
  flex-shrink: 0;
}

.step-v-icon {
  width: 80px; height: 80px;
  border-radius: 24px;
  display: flex; align-items: center; justify-content: center;
  position: relative;
  z-index: 2;
}

.step-v-num {
  position: absolute;
  top: -12px; right: -12px;
  width: 28px; height: 28px;
  border-radius: 50%;
  background: #ffffff; color: #0f172a;
  font-size: 0.8rem; font-weight: 800;
  display: flex; align-items: center; justify-content: center;
  z-index: 3;
  box-shadow: 0 4px 10px rgba(0,0,0,0.2);
}

.step-v-content {
  padding-top: 10px;
}
.step-v-title {
  font-size: 1.8rem; font-weight: 800; color: white; margin-bottom: 0.5rem;
}
.step-v-desc {
  font-size: 1.1rem; color: rgba(255,255,255,0.7); line-height: 1.6;
}

/* NEW ADVANCED SLIDE-RIGHT ANIMATION */
.reveal-slide-right {
  opacity: 0;
  transform: translateX(-60px);
  transition: all 1s cubic-bezier(0.16, 1, 0.3, 1);
}
.reveal-slide-right.revealed {
  opacity: 1;
  transform: translateX(0);
}

/* Light theme overrides for vertical steps */
.landing.light-theme .step-v-title { color: #0f172a !important; }
.landing.light-theme .step-v-desc { color: #475569 !important; }
.landing.light-theme .step-v-num { background: #0f172a; color: #ffffff; }

/* Responsive */
@media (max-width: 768px) {
  .steps-vertical { gap: 3rem; }
  .steps-vertical::before { left: 30px; }
  .step-v { gap: 1.5rem; }
  .step-v-icon { width: 60px; height: 60px; border-radius: 16px; }
  .step-v-title { font-size: 1.4rem; }
  .step-v-desc { font-size: 0.95rem; }
}


/* SERVICES 2x2 GRID UPGRADE */
.services-grid {
  display: grid; 
  grid-template-columns: repeat(2, 1fr) !important; 
  gap: 2.5rem !important;
  text-align: left; 
  margin-top: 4rem;
}

.service-card {
  background: rgba(255,255,255,0.02) !important;
  border: 1px solid rgba(255,255,255,0.05) !important;
  border-radius: 32px !important;
  padding: 2.5rem !important;
  transition: all 0.5s cubic-bezier(0.16, 1, 0.3, 1) !important;
}

.service-card:hover {
  transform: translateY(-8px) scale(1.02);
  background: rgba(255,255,255,0.04) !important;
  border-color: var(--theme-color) !important;
  box-shadow: 0 40px 80px rgba(0,0,0,0.6), inset 0 1px 0 rgba(255,255,255,0.1) !important;
}

.svc-name-large {
  font-size: 1.6rem;
  font-weight: 800;
  color: white;
  margin-bottom: 6px;
  letter-spacing: -0.5px;
}
.svc-port-badge {
  display: inline-block;
  padding: 4px 10px;
  border-radius: 8px;
  font-family: 'JetBrains Mono', monospace;
  font-size: 0.8rem;
  font-weight: 600;
}

.svc-features-grid {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 16px;
  list-style: none;
  padding: 0;
  margin: 0;
}
.svc-features-grid li {
  display: flex;
  align-items: center;
  gap: 10px;
  color: rgba(255,255,255,0.7);
  font-size: 1.05rem;
  transition: color 0.3s;
}
.service-card:hover .svc-features-grid li {
  color: rgba(255,255,255,0.95);
}

/* Light Theme overrides for wide cards */
.landing.light-theme .svc-name-large { color: #0f172a !important; }
.landing.light-theme .svc-features-grid li { color: #475569 !important; }
.landing.light-theme .service-card {
  background: #ffffff !important;
  border-color: rgba(0,0,0,0.1) !important;
  box-shadow: 0 20px 40px rgba(0,0,0,0.03) !important;
}
.landing.light-theme .service-card:hover {
  box-shadow: 0 40px 80px rgba(0,0,0,0.1) !important;
}
.landing.light-theme .svc-icon-wrap {
  background: #f8fafc !important;
  border-color: rgba(0,0,0,0.05) !important;
}

@media (max-width: 900px) {
  .services-grid { grid-template-columns: 1fr !important; }
  .svc-features-grid { grid-template-columns: 1fr; }
}


/* SERVICES TABS & CSS MOCKUPS */
.services-tabs-container {
  display: flex; gap: 2rem;
  margin-top: 4rem;
  text-align: left;
  max-width: 1100px;
  margin-left: auto; margin-right: auto;
}

.svc-tabs-list {
  display: flex; flex-direction: column; gap: 12px;
  width: 300px; flex-shrink: 0;
}

.svc-tab-item {
  display: flex; align-items: center; gap: 16px;
  padding: 20px 24px;
  border-radius: 16px;
  background: rgba(255,255,255,0.02);
  border: 1px solid rgba(255,255,255,0.05);
  cursor: pointer;
  transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1);
  color: rgba(255,255,255,0.6);
}

.svc-tab-item:hover {
  background: rgba(255,255,255,0.05);
  color: white;
}

.svc-tab-item.active {
  background: rgba(255,255,255,0.08);
  border-color: var(--theme-color);
  color: white;
  box-shadow: 0 10px 30px rgba(0,0,0,0.3);
}

.svc-tab-icon {
  width: 24px; height: 24px;
  display: flex; align-items: center; justify-content: center;
  transition: color 0.3s;
}

.svc-tab-item.active .svc-tab-icon {
  color: var(--theme-color);
  filter: drop-shadow(0 0 8px var(--theme-color));
}

.svc-tab-name {
  font-size: 1.1rem; font-weight: 600;
}

.svc-tabs-content {
  flex-grow: 1;
  background: rgba(255,255,255,0.02);
  border: 1px solid rgba(255,255,255,0.05);
  border-radius: 24px;
  padding: 3rem;
  position: relative;
  overflow: hidden;
  display: flex; flex-direction: column;
}

.svc-pane-top {
  margin-bottom: 3rem;
}

.svc-pane-header {
  display: flex; align-items: center; justify-content: space-between;
  margin-bottom: 2rem;
}

.svc-pane-title {
  font-size: 2.2rem; font-weight: 800; color: white; margin: 0;
}

.svc-port-badge {
  display: inline-block; padding: 6px 16px;
  border-radius: 12px;
  background: rgba(255,255,255,0.05);
  color: var(--theme-color);
  font-family: 'JetBrains Mono', monospace; font-size: 0.9rem; font-weight: 700;
  border: 1px solid var(--theme-color);
}

.svc-pane-features {
  display: grid; grid-template-columns: repeat(2, 1fr); gap: 16px;
  list-style: none; padding: 0; margin: 0;
}
.svc-pane-features li {
  display: flex; align-items: center; gap: 12px;
  color: rgba(255,255,255,0.8); font-size: 1.1rem;
}

/* Transitions */
.fade-up-enter-active, .fade-up-leave-active {
  transition: all 0.4s cubic-bezier(0.16, 1, 0.3, 1);
}
.fade-up-enter-from { opacity: 0; transform: translateY(20px); }
.fade-up-leave-to { opacity: 0; transform: translateY(-20px); }

/* --- CSS MOCKUPS --- */
.svc-mockup-area {
  height: 250px;
  background: rgba(0,0,0,0.2);
  border-radius: 16px;
  border: 1px solid rgba(255,255,255,0.05);
  display: flex; align-items: center; justify-content: center;
  position: relative; overflow: hidden;
}

/* Tab 0: API Mockup */
.mockup-api { display: flex; align-items: center; gap: 20px; }
.api-node {
  padding: 12px 24px; border-radius: 8px; font-weight: 600; font-size: 0.9rem;
  background: #1e1e24; border: 2px solid rgba(255,255,255,0.1); color: white;
  position: relative; z-index: 2;
}
.api-node.gateway {
  border-color: #6366f1; box-shadow: 0 0 20px rgba(99,102,241,0.4);
  padding: 20px 30px; font-size: 1.1rem;
}
.api-node.proj { border-color: #f59e0b; }
.api-node.task { border-color: #ef4444; }
.api-node.noti { border-color: #10b981; }

.api-line, .api-line-out {
  height: 2px; background: rgba(255,255,255,0.1); position: relative;
}
.api-line { width: 50px; }
.api-lines-out { display: flex; flex-direction: column; gap: 28px; justify-content: center; height: 120px; }
.api-line-out { width: 40px; }

.api-dot {
  width: 8px; height: 8px; border-radius: 50%; background: white;
  position: absolute; top: -3px; left: 0;
  box-shadow: 0 0 10px white;
  animation: api-flow 1.5s infinite linear;
}
@keyframes api-flow {
  0% { left: 0; opacity: 1; }
  100% { left: 100%; opacity: 0; }
}
.api-services { display: flex; flex-direction: column; gap: 12px; }

/* Tab 1: Project Mockup */
.mockup-project {
  width: 350px; background: #18181b; border-radius: 16px; border: 1px solid rgba(255,255,255,0.05);
  padding: 20px; box-shadow: 0 20px 40px rgba(0,0,0,0.4);
  animation: float 4s ease-in-out infinite alternate;
}
.pj-header { font-size: 1.1rem; font-weight: 700; color: white; margin-bottom: 16px; }
.pj-list { display: flex; flex-direction: column; gap: 12px; }
.pj-item {
  background: rgba(255,255,255,0.03); padding: 12px; border-radius: 8px;
  animation: slide-right 0.5s cubic-bezier(0.16, 1, 0.3, 1) backwards;
}
.pj-info { display: flex; align-items: center; gap: 12px; margin-bottom: 12px; }
.pj-avatar { width: 24px; height: 24px; border-radius: 6px; }
.pj-name { font-size: 0.9rem; color: #e4e4e7; font-weight: 600; }
.pj-progress { height: 6px; background: rgba(255,255,255,0.1); border-radius: 3px; overflow: hidden; }
.pj-fill { height: 100%; animation: fill-bar 1.5s cubic-bezier(0.16, 1, 0.3, 1) forwards; transform-origin: left; transform: scaleX(0); }
@keyframes fill-bar { to { transform: scaleX(1); } }
@keyframes slide-right { from { opacity: 0; transform: translateX(-20px); } to { opacity: 1; transform: translateX(0); } }

/* Tab 2: Task Mockup */
.mockup-task {
  display: flex; gap: 16px; transform: rotateX(10deg) rotateZ(-5deg); perspective: 1000px;
  transform-style: preserve-3d;
}
.kb-col {
  width: 120px; background: rgba(255,255,255,0.02); border-radius: 12px; padding: 12px;
  border: 1px solid rgba(255,255,255,0.05);
}
.kb-title { font-size: 0.7rem; font-weight: 700; color: #a1a1aa; margin-bottom: 12px; letter-spacing: 1px; }
.kb-card {
  height: 40px; background: #27272a; border-radius: 6px; margin-bottom: 8px;
  border: 1px solid rgba(255,255,255,0.05);
}
.kb-drag {
  border-color: #ef4444; box-shadow: 0 10px 20px rgba(239,68,68,0.3);
  transform: translateZ(20px) rotate(-3deg);
  animation: kb-float 3s infinite alternate;
}
@keyframes kb-float { to { transform: translateZ(30px) rotate(-3deg) translateY(-10px); } }

/* Tab 3: Notify Mockup */
.mockup-notify {
  display: flex; flex-direction: column; align-items: center; gap: 24px;
}
.nf-bell {
  position: relative; animation: bell-shake 4s infinite;
}
.nf-badge {
  position: absolute; top: -5px; right: -5px; width: 20px; height: 20px; border-radius: 50%;
  background: #ef4444; color: white; font-size: 0.7rem; font-weight: bold;
  display: flex; align-items: center; justify-content: center;
  box-shadow: 0 0 10px rgba(239,68,68,0.5);
}
.nf-ring {
  position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);
  width: 60px; height: 60px; border-radius: 50%;
  border: 2px solid var(--theme-color); opacity: 0;
  animation: ring-pulse 2s infinite;
}
.nf-toast {
  background: #18181b; border: 1px solid rgba(16,185,129,0.3); border-radius: 12px;
  padding: 12px 20px; display: flex; align-items: center; gap: 12px;
  box-shadow: 0 20px 40px rgba(0,0,0,0.5);
  animation: toast-up 4s infinite cubic-bezier(0.16, 1, 0.3, 1);
}
.nf-toast-icon {
  width: 24px; height: 24px; border-radius: 50%; display: flex; align-items: center; justify-content: center;
  font-size: 0.8rem; font-weight: bold;
}
.nf-toast-text { color: #e4e4e7; font-size: 0.95rem; font-weight: 500; }

@keyframes bell-shake {
  0%, 80%, 100% { transform: rotate(0); }
  85% { transform: rotate(15deg); }
  90% { transform: rotate(-15deg); }
  95% { transform: rotate(10deg); }
}
@keyframes ring-pulse {
  0% { transform: translate(-50%, -50%) scale(0.8); opacity: 0.8; }
  100% { transform: translate(-50%, -50%) scale(1.5); opacity: 0; }
}
@keyframes toast-up {
  0%, 10% { opacity: 0; transform: translateY(30px); }
  20%, 80% { opacity: 1; transform: translateY(0); }
  90%, 100% { opacity: 0; transform: translateY(-30px); }
}

/* Light Theme overrides */
.landing.light-theme .svc-tab-item { color: #475569; border-color: rgba(0,0,0,0.05); }
.landing.light-theme .svc-tab-item.active { background: #ffffff; color: #0f172a; box-shadow: 0 10px 30px rgba(0,0,0,0.05); }
.landing.light-theme .svc-tabs-content { background: #ffffff; border-color: rgba(0,0,0,0.05); box-shadow: 0 20px 40px rgba(0,0,0,0.02); }
.landing.light-theme .svc-pane-title { color: #0f172a; }
.landing.light-theme .svc-pane-features li { color: #475569; }
.landing.light-theme .svc-mockup-area { background: #f8fafc; border-color: rgba(0,0,0,0.05); }

/* Responsive */
@media (max-width: 900px) {
  .services-tabs-container { flex-direction: column; }
  .svc-tabs-list { width: 100%; flex-direction: row; flex-wrap: wrap; }
  .svc-tab-item { flex: 1 1 45%; }
  .svc-tabs-content { padding: 2rem; }
  .svc-pane-features { grid-template-columns: 1fr; }
  .mockup-task { transform: scale(0.8); }
}


/* NEW HERO STATS REDESIGN */
.hero-stats-new {
  display: flex; align-items: center; gap: 16px; flex-wrap: wrap; margin-top: 1rem;
}
.stat-pill {
  display: flex; align-items: center; gap: 12px;
  background: rgba(255,255,255,0.03);
  border: 1px solid rgba(255,255,255,0.08);
  border-radius: 100px;
  padding: 8px 24px 8px 8px;
  backdrop-filter: blur(10px);
  transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1);
  text-align: left;
}
.stat-pill:hover {
  transform: translateY(-4px);
  background: rgba(255,255,255,0.06);
  border-color: rgba(99,102,241,0.3);
  box-shadow: 0 10px 30px rgba(99,102,241,0.1);
}
.stat-icon {
  width: 36px; height: 36px; border-radius: 50%;
  background: rgba(255,255,255,0.05);
  display: flex; align-items: center; justify-content: center;
}
.stat-val { font-size: 1.1rem; font-weight: 800; color: white; line-height: 1; margin-bottom: 2px; }
.stat-desc { font-size: 0.7rem; color: #a1a1aa; text-transform: uppercase; letter-spacing: 1px; }

/* Light Theme overrides for Hero Stats */
.landing.light-theme .stat-pill {
  background: #ffffff !important;
  border-color: rgba(0,0,0,0.08) !important;
  box-shadow: 0 10px 20px rgba(0,0,0,0.03) !important;
}
.landing.light-theme .stat-val { color: #0f172a !important; background: transparent !important; }
.landing.light-theme .stat-desc { color: #64748b !important; }
.landing.light-theme .stat-icon { background: #f8fafc !important; }

</style>
