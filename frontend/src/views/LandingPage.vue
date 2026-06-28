<template>
  <div class="landing">
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
          <a href="#features" class="nav-link">Tính năng</a>
          <a href="#how" class="nav-link">Cách hoạt động</a>
          <a href="#stats" class="nav-link">Kiến trúc</a>
          <router-link to="/login" class="btn-nav-login">Đăng nhập</router-link>
          <router-link to="/register" class="btn-nav-register">
            Bắt đầu miễn phí
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
          Quản lý dự án thông minh · Microservices Architecture
        </div>
        <h1 class="hero-title">
          Quản lý dự án<br/>
          <span class="hero-gradient typing-text">{{ typedText }}<span class="cursor">|</span></span><br/>
          cùng SprintFlow
        </h1>
        <p class="hero-desc">
          Nền tảng quản lý công việc toàn diện — từ lên kế hoạch sprint, phân công task,
          theo dõi tiến độ đến thông báo thời gian thực. Tất cả trong một giao diện đẹp mắt.
        </p>
        <div class="hero-actions">
          <router-link to="/register" class="btn-primary magnetic">
            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M13 2L3 14h9l-1 8 10-12h-9l1-8z"/></svg>
            Bắt đầu miễn phí
            <span class="btn-shimmer"></span>
          </router-link>
          <router-link to="/login" class="btn-ghost">
            Đã có tài khoản →
          </router-link>
        </div>
        <div class="hero-stats">
          <div class="stat-item">
            <span class="stat-num">{{ counters.services }}</span>
            <span class="stat-label">Microservices</span>
          </div>
          <div class="stat-divider"></div>
          <div class="stat-item">
            <span class="stat-num">JWT</span>
            <span class="stat-label">Bảo mật</span>
          </div>
          <div class="stat-divider"></div>
          <div class="stat-item">
            <span class="stat-num">{{ counters.tasks }}+</span>
            <span class="stat-label">Tính năng</span>
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
    <section id="features" class="features">
      <div class="section-badge reveal-up">Tính năng</div>
      <h2 class="section-title reveal-up" style="--delay: 0.1s">Mọi thứ bạn cần để<br/><span class="text-gradient">quản lý dự án hiệu quả</span></h2>
      <p class="section-sub reveal-up" style="--delay: 0.2s">SprintFlow tích hợp đầy đủ công cụ từ lập kế hoạch đến báo cáo, giúp team làm việc đồng bộ và minh bạch.</p>
      <div class="features-grid">
        <div class="feat-card reveal-up" v-for="(feat, i) in features" :key="feat.title" :style="{ '--delay': (i * 0.1 + 0.1) + 's' }">
          <div class="feat-glow" :style="{ background: feat.glow }"></div>
          <div class="feat-icon" :style="{ background: feat.bg }">
            <span v-html="feat.icon"></span>
          </div>
          <h3 class="feat-title">{{ feat.title }}</h3>
          <p class="feat-desc">{{ feat.desc }}</p>
          <ul class="feat-list">
            <li v-for="item in feat.items" :key="item">
              <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><polyline points="20 6 9 17 4 12"/></svg>
              {{ item }}
            </li>
          </ul>
        </div>
      </div>
    </section>

    <!-- HOW IT WORKS -->
    <section id="how" class="how">
      <div class="how-inner">
        <div class="section-badge reveal-up">Cách hoạt động</div>
        <h2 class="section-title reveal-up" style="--delay: 0.1s">Bắt đầu chỉ trong<br/><span class="text-gradient">3 bước đơn giản</span></h2>
        <div class="steps">
          <div class="step reveal-up" v-for="(step, i) in steps" :key="i" :style="{ '--delay': (i * 0.15 + 0.1) + 's' }">
            <div class="step-num">{{ i + 1 }}</div>
            <div class="step-connector" v-if="i < steps.length - 1"></div>
            <div class="step-icon" :style="{ background: step.bg }">
              <span v-html="step.icon"></span>
            </div>
            <h3 class="step-title">{{ step.title }}</h3>
            <p class="step-desc">{{ step.desc }}</p>
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
        <div class="services-grid">
          <div class="service-card reveal-up" v-for="(svc, i) in services" :key="svc.name" :style="{ '--delay': (i * 0.12 + 0.1) + 's' }">
            <div class="svc-pulse" :style="{ background: svc.color }"></div>
            <div class="svc-icon" :style="{ color: svc.color }">
              <span v-html="svc.icon"></span>
            </div>
            <div class="svc-name">{{ svc.name }}</div>
            <div class="svc-port">Port {{ svc.port }}</div>
            <ul class="svc-features">
              <li v-for="f in svc.features" :key="f">{{ f }}</li>
            </ul>
          </div>
        </div>
      </div>
    </section>

    <!-- CTA -->
    <section class="cta-section">
      <div class="cta-orb cta-orb-1"></div>
      <div class="cta-orb cta-orb-2"></div>
      <div class="cta-inner reveal-up">
        <div class="cta-emoji">🚀</div>
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
import { ref, onMounted, onUnmounted } from 'vue'

// ── TYPING EFFECT ──
const words = ['hiệu quả hơn', 'chuyên nghiệp hơn', 'thông minh hơn', 'dễ dàng hơn']
let wordIdx = 0, charIdx = 0, deleting = false
const typedText = ref('')

function typeLoop() {
  const word = words[wordIdx]
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
      wordIdx = (wordIdx + 1) % words.length
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
  document.querySelectorAll('.reveal-up, .reveal-left, .reveal-right').forEach(el => observer.observe(el))
}

// ── SCROLL NAV ──
const isScrolled = ref(false)
function onScroll() { isScrolled.value = window.scrollY > 50 }

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
})

onUnmounted(() => {
  cancelAnimationFrame(animFrame)
  observer?.disconnect()
  window.removeEventListener('scroll', onScroll)
})

// ── DATA ──
const marquee = [
  'Vue 3', 'TypeScript', '.NET 8', 'Ocelot Gateway', 'JWT Auth', 'SQL Server',
  'Microservices', 'Kanban Board', 'Real-time Notify', 'REST API', 'CORS', 'Swagger'
]

const features = [
  {
    title: 'Quản lý dự án',
    desc: 'Tạo và quản lý nhiều dự án song song với đầy đủ thông tin về tiến độ, thành viên và trạng thái.',
    bg: 'linear-gradient(135deg, #667eea20, #764ba220)',
    glow: 'radial-gradient(circle at top left, rgba(99,102,241,0.15), transparent 70%)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#6366f1" stroke-width="2"><path d="M22 19a2 2 0 01-2 2H4a2 2 0 01-2-2V5a2 2 0 012-2h5l2 3h9a2 2 0 012 2z"/></svg>`,
    items: ['Tạo dự án với màu sắc & mô tả', 'Quản lý thành viên dự án', 'Theo dõi tiến độ realtime', 'Lọc & tìm kiếm nhanh']
  },
  {
    title: 'Task & Kanban Board',
    desc: 'Phân công công việc, đặt deadline, theo dõi tiến độ từng task với bảng Kanban trực quan.',
    bg: 'linear-gradient(135deg, #f59e0b20, #ef444420)',
    glow: 'radial-gradient(circle at top left, rgba(245,158,11,0.12), transparent 70%)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#f59e0b" stroke-width="2"><rect x="3" y="4" width="18" height="18" rx="2"/><line x1="16" y1="2" x2="16" y2="6"/><line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/></svg>`,
    items: ['Kanban board kéo-thả', 'Subtasks & work log', 'Độ ưu tiên & deadline', 'Phân công nhân sự']
  },
  {
    title: 'Bình luận & Thông báo',
    desc: 'Giao tiếp trực tiếp trong task, nhận thông báo tự động khi có hoạt động mới trong dự án.',
    bg: 'linear-gradient(135deg, #10b98120, #059f4620)',
    glow: 'radial-gradient(circle at top left, rgba(16,185,129,0.12), transparent 70%)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#10b981" stroke-width="2"><path d="M21 15a2 2 0 01-2 2H7l-4 4V5a2 2 0 012-2h14a2 2 0 012 2z"/></svg>`,
    items: ['Bình luận theo task', 'Thông báo realtime', 'Đánh dấu đã đọc', 'Nhật ký hoạt động']
  },
  {
    title: 'Bảo mật & Phân quyền',
    desc: 'Hệ thống JWT authentication, phân quyền theo vai trò từ Admin đến Viewer rõ ràng và an toàn.',
    bg: 'linear-gradient(135deg, #8b5cf620, #6366f120)',
    glow: 'radial-gradient(circle at top left, rgba(139,92,246,0.12), transparent 70%)',
    icon: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#8b5cf6" stroke-width="2"><path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/></svg>`,
    items: ['JWT Authentication', 'Phân quyền Admin/Manager/Member/Viewer', 'Đăng ký & quản lý tài khoản', 'Bảo mật API Gateway']
  }
]

const steps = [
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
  overflow-x: hidden;
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
.nav-logo { display: flex; align-items: center; gap: 10px; }
.logo-icon {
  width: 36px; height: 36px; border-radius: 10px;
  background: linear-gradient(135deg, #6366f1, #8b5cf6);
  display: flex; align-items: center; justify-content: center;
  animation: logo-spin 8s linear infinite;
}
@keyframes logo-spin {
  0%, 90% { transform: rotate(0deg); }
  95% { transform: rotate(180deg); }
  100% { transform: rotate(360deg); }
}
.logo-icon.small { width: 28px; height: 28px; border-radius: 8px; animation: none; }
.logo-text { font-weight: 700; font-size: 1.1rem; color: #fff; }
.nav-links { display: flex; align-items: center; gap: 1.5rem; }
.nav-link {
  color: #a1a1aa; font-size: 0.9rem; text-decoration: none;
  position: relative; transition: color 0.2s;
}
.nav-link::after {
  content: ''; position: absolute; bottom: -4px; left: 0; right: 0;
  height: 1px; background: #6366f1; transform: scaleX(0);
  transition: transform 0.3s ease;
}
.nav-link:hover { color: #fff; }
.nav-link:hover::after { transform: scaleX(1); }
.btn-nav-login {
  color: #a1a1aa; font-size: 0.9rem; text-decoration: none;
  padding: 0.4rem 1rem; border-radius: 8px;
  border: 1px solid rgba(255,255,255,0.1);
  transition: all 0.2s;
}
.btn-nav-login:hover { color: #fff; border-color: rgba(255,255,255,0.2); }
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
  bottom: -24px; left: -40px;
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
}
.features > .section-sub { margin: 0 auto 3.5rem; }
.features-grid {
  display: grid; grid-template-columns: repeat(2, 1fr); gap: 1.5rem;
  text-align: left;
}
.feat-card {
  position: relative; overflow: hidden;
  background: rgba(255,255,255,0.03);
  border: 1px solid rgba(255,255,255,0.07);
  border-radius: 16px; padding: 1.8rem;
  transition: all 0.4s cubic-bezier(0.34, 1.56, 0.64, 1);
}
.feat-glow {
  position: absolute; inset: 0; opacity: 0;
  transition: opacity 0.3s;
}
.feat-card:hover .feat-glow { opacity: 1; }
.feat-card:hover {
  background: rgba(255,255,255,0.05);
  border-color: rgba(99,102,241,0.3);
  transform: translateY(-8px) scale(1.01);
  box-shadow: 0 20px 60px rgba(0,0,0,0.3);
}
.feat-icon {
  width: 48px; height: 48px; border-radius: 12px;
  display: flex; align-items: center; justify-content: center;
  margin-bottom: 1rem; position: relative;
  transition: transform 0.3s;
}
.feat-card:hover .feat-icon { transform: scale(1.1) rotate(5deg); }
.feat-title { font-size: 1.05rem; font-weight: 700; color: #fff; margin: 0 0 0.5rem; }
.feat-desc { font-size: 0.88rem; color: #71717a; line-height: 1.6; margin: 0 0 1rem; }
.feat-list { list-style: none; padding: 0; margin: 0; display: flex; flex-direction: column; gap: 6px; }
.feat-list li {
  display: flex; align-items: center; gap: 8px;
  font-size: 0.83rem; color: #a1a1aa;
  transition: color 0.2s, transform 0.2s;
}
.feat-card:hover .feat-list li { color: #d4d4d8; }
.feat-list li:hover { transform: translateX(4px); color: #fff; }
.feat-list svg { color: #6366f1; flex-shrink: 0; }

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
</style>
