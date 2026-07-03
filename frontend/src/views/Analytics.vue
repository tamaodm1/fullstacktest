<script setup lang="ts">
import { computed } from 'vue';
import { useTaskStore } from '../stores/taskStore';
import { BarChart as BarChartIcon, CheckCircle, Clock, AlertTriangle, PieChart } from 'lucide-vue-next';

// Chart.js imports
import { Chart as ChartJS, ArcElement, Tooltip, Legend, Title, BarElement, CategoryScale, LinearScale } from 'chart.js';
import { Doughnut, Bar } from 'vue-chartjs';

ChartJS.register(ArcElement, Tooltip, Legend, Title, BarElement, CategoryScale, LinearScale);

const taskStore = useTaskStore();

// --- Computed Data for Custom UI ---

const resourceAllocation = computed(() => {
  const userMap = new Map<string, { user: any, active: number, overdue: number }>();
  taskStore.users.forEach(u => userMap.set(u.id, { user: u, active: 0, overdue: 0 }));
  const todayStr = new Date().toISOString().split('T')[0];

  taskStore.tasks.forEach(t => {
    if (t.status !== 'Done') {
      const isOverdue = t.dueDate && t.dueDate < todayStr;
      const assignees = t.assigneeId ? t.assigneeId.split(',') : [];
      assignees.forEach(uid => {
        const uData = userMap.get(uid);
        if (uData) {
          uData.active += 1;
          if (isOverdue) uData.overdue += 1;
        }
      });
    }
  });

  return Array.from(userMap.values())
    .filter(d => d.active > 0 || d.overdue > 0 || d.user.role !== 'Viewer')
    .sort((a, b) => b.active - a.active);
});

const productivity = computed(() => {
  const userMap = new Map<string, { user: any, completed: number, overdue: number, onTime: number }>();
  taskStore.users.forEach(u => userMap.set(u.id, { user: u, completed: 0, overdue: 0, onTime: 0 }));
  const todayStr = new Date().toISOString().split('T')[0];

  taskStore.tasks.forEach(t => {
    const assignees = t.assigneeId ? t.assigneeId.split(',') : [];
    assignees.forEach(uid => {
      const uData = userMap.get(uid);
      if (uData) {
        if (t.status === 'Done') {
          uData.completed += 1;
        } else if (t.dueDate && t.dueDate < todayStr) {
          uData.overdue += 1;
        } else {
          uData.onTime += 1;
        }
      }
    });
  });

  return Array.from(userMap.values())
    .filter(d => d.completed > 0 || d.overdue > 0 || d.onTime > 0)
    .sort((a, b) => b.completed - a.completed);
});

const getProgressColor = (percent: number) => {
  if (percent > 80) return 'bg-red-500';
  if (percent > 50) return 'bg-orange-500';
  return 'bg-blue-500';
};

// --- Chart.js Data ---

const doughnutData = computed(() => {
  let todo = 0, inProgress = 0, review = 0, done = 0;
  taskStore.tasks.forEach(t => {
    if (t.status === 'ToDo' || t.status === 'Backlog') todo++;
    else if (t.status === 'InProgress') inProgress++;
    else if (t.status === 'Review') review++;
    else if (t.status === 'Done') done++;
  });

  return {
    labels: ['To Do', 'In Progress', 'Review', 'Done'],
    datasets: [
      {
        backgroundColor: ['#e2e8f0', '#3b82f6', '#f59e0b', '#10b981'],
        hoverBackgroundColor: ['#cbd5e1', '#2563eb', '#d97706', '#059669'],
        borderWidth: 0,
        data: [todo, inProgress, review, done]
      }
    ]
  };
});

const doughnutOptions = {
  responsive: true,
  maintainAspectRatio: false,
  cutout: '70%',
  plugins: {
    legend: { 
      position: 'bottom' as const, 
      labels: { 
        padding: 20, 
        usePointStyle: true, 
        font: { family: "'Inter', sans-serif" } 
      } 
    }
  }
};

const barChartData = computed(() => {
  // Sort by total tasks for better visualization
  const sorted = [...productivity.value].sort((a, b) => (b.completed + b.onTime + b.overdue) - (a.completed + a.onTime + a.overdue));
  
  const labels = sorted.map(u => u.user.fullName.split(' ').pop() || u.user.fullName); // Use first/last name for brevity
  const completed = sorted.map(u => u.completed);
  const inProgress = sorted.map(u => u.onTime);
  const overdue = sorted.map(u => u.overdue);

  return {
    labels,
    datasets: [
      {
        label: 'Hoàn thành',
        backgroundColor: '#10b981',
        borderRadius: 4,
        data: completed
      },
      {
        label: 'Đang làm (Đúng hạn)',
        backgroundColor: '#3b82f6',
        borderRadius: 4,
        data: inProgress
      },
      {
        label: 'Trễ hạn',
        backgroundColor: '#f43f5e',
        borderRadius: 4,
        data: overdue
      }
    ]
  };
});

const barChartOptions = {
  responsive: true,
  maintainAspectRatio: false,
  scales: {
    x: { stacked: true, grid: { display: false } },
    y: { stacked: true, border: { display: false } }
  },
  plugins: {
    legend: { position: 'top' as const, labels: { usePointStyle: true, boxWidth: 8, font: { family: "'Inter', sans-serif" } } }
  }
};

</script>

<template>
  <div class="p-6 max-w-7xl mx-auto space-y-6 pb-24">
    <div class="flex items-center justify-between mb-8">
      <div class="flex items-center gap-3">
        <div class="p-3 bg-gradient-to-br from-indigo-500 to-purple-600 rounded-2xl shadow-lg shadow-indigo-200">
          <BarChartIcon class="w-8 h-8 text-white" />
        </div>
        <div>
          <h1 class="text-3xl font-black text-slate-900 tracking-tight">Thống kê & Nguồn lực</h1>
          <p class="text-slate-500 text-sm font-medium mt-1">Báo cáo trực quan tình trạng dự án và năng suất cá nhân</p>
        </div>
      </div>
    </div>

    <!-- Overview Cards -->
    <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
      <div class="group bg-white rounded-3xl shadow-sm border border-slate-100 p-6 flex items-center gap-6 hover:-translate-y-1.5 hover:shadow-2xl hover:shadow-blue-100/60 transition-all duration-300">
        <div class="flex items-center justify-center h-16 w-16 bg-gradient-to-br from-blue-50 to-blue-100 rounded-2xl text-blue-600 group-hover:scale-110 group-hover:rotate-6 transition-transform duration-300 shadow-inner">
          <Clock class="w-8 h-8" />
        </div>
        <div>
          <p class="text-sm font-black uppercase tracking-widest text-blue-400">Đang thực hiện</p>
          <p class="text-4xl font-black text-slate-800 mt-1">{{ taskStore.tasks.filter(t => t.status !== 'Done' && (!t.dueDate || t.dueDate >= new Date().toISOString().split('T')[0])).length }}</p>
        </div>
      </div>
      <div class="group bg-white rounded-3xl shadow-sm border border-slate-100 p-6 flex items-center gap-6 hover:-translate-y-1.5 hover:shadow-2xl hover:shadow-rose-100/60 transition-all duration-300">
        <div class="flex items-center justify-center h-16 w-16 bg-gradient-to-br from-rose-50 to-rose-100 rounded-2xl text-rose-600 group-hover:scale-110 group-hover:-rotate-6 transition-transform duration-300 shadow-inner">
          <AlertTriangle class="w-8 h-8" />
        </div>
        <div>
          <p class="text-sm font-black uppercase tracking-widest text-rose-400">Trễ hạn</p>
          <p class="text-4xl font-black text-slate-800 mt-1">{{ taskStore.tasks.filter(t => t.status !== 'Done' && t.dueDate && t.dueDate < new Date().toISOString().split('T')[0]).length }}</p>
        </div>
      </div>
      <div class="group bg-white rounded-3xl shadow-sm border border-slate-100 p-6 flex items-center gap-6 hover:-translate-y-1.5 hover:shadow-2xl hover:shadow-emerald-100/60 transition-all duration-300">
        <div class="flex items-center justify-center h-16 w-16 bg-gradient-to-br from-emerald-50 to-emerald-100 rounded-2xl text-emerald-600 group-hover:scale-110 transition-transform duration-300 shadow-inner">
          <CheckCircle class="w-8 h-8" />
        </div>
        <div>
          <p class="text-sm font-black uppercase tracking-widest text-emerald-500">Đã hoàn thành</p>
          <p class="text-4xl font-black text-slate-800 mt-1">{{ taskStore.tasks.filter(t => t.status === 'Done').length }}</p>
        </div>
      </div>
    </div>

    <!-- Charts Row -->
    <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
      
      <!-- Chart.js Doughnut: Overall Status -->
      <div class="bg-white rounded-3xl shadow-sm border border-slate-100 p-8 lg:col-span-1 flex flex-col hover:shadow-xl transition-shadow duration-300">
        <h2 class="text-xl font-black text-slate-800 mb-6 flex items-center gap-3">
          <div class="p-2 bg-indigo-50 rounded-xl"><PieChart class="w-5 h-5 text-indigo-500" /></div> Tổng quan trạng thái
        </h2>
        <div class="flex-1 relative min-h-[250px]">
          <Doughnut :data="doughnutData" :options="doughnutOptions" />
          <div class="absolute inset-0 flex flex-col items-center justify-center pointer-events-none">
            <span class="text-4xl font-black text-slate-800">{{ taskStore.tasks.length }}</span>
            <span class="text-xs font-bold text-slate-400 uppercase tracking-widest">Tasks</span>
          </div>
        </div>
      </div>

      <!-- Chart.js Bar: User Workload -->
      <div class="bg-white rounded-3xl shadow-sm border border-slate-100 p-8 lg:col-span-2 hover:shadow-xl transition-shadow duration-300">
        <h2 class="text-xl font-black text-slate-800 mb-6 flex items-center gap-3">
          <div class="p-2 bg-blue-50 rounded-xl"><BarChartIcon class="w-5 h-5 text-blue-500" /></div> Biểu đồ Khối lượng Công việc
        </h2>
        <div class="h-[280px]">
          <Bar :data="barChartData" :options="barChartOptions" />
        </div>
      </div>

    </div>

    <div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
      
      <!-- Resource Allocation Details -->
      <div class="bg-white rounded-3xl shadow-sm border border-slate-100 p-8 hover:shadow-xl transition-shadow duration-300">
        <h2 class="text-xl font-black text-slate-800 mb-6 flex items-center gap-2"><div class="w-2 h-6 bg-indigo-500 rounded-full"></div> Chi tiết Nguồn lực đang xử lý</h2>
        <div class="space-y-6">
          <div v-for="item in resourceAllocation" :key="item.user.id" class="group">
            <div class="flex items-center justify-between mb-2">
              <div class="flex items-center gap-3">
                <div class="relative">
                  <img :src="item.user.avatarUrl" class="w-10 h-10 rounded-full border-2 border-white shadow-md group-hover:scale-110 transition-transform" />
                  <div class="absolute -bottom-0.5 -right-0.5 w-3.5 h-3.5 bg-green-500 border-2 border-white rounded-full"></div>
                </div>
                <span class="text-sm font-bold text-slate-700">{{ item.user.fullName }}</span>
              </div>
              <span class="text-xs font-black px-3 py-1.5 bg-slate-100 text-slate-600 rounded-lg shadow-inner">{{ item.active }} task</span>
            </div>
            <!-- Max capacity assumption: 10 tasks is 100% -->
            <div class="w-full bg-slate-100 rounded-full h-3 flex overflow-hidden shadow-inner">
              <div :class="[getProgressColor((item.active - item.overdue)*10), 'h-3 transition-all duration-1000 ease-out bg-gradient-to-r opacity-90 group-hover:opacity-100']" :style="{ width: Math.min(((item.active - item.overdue) / 10) * 100, 100) + '%' }"></div>
              <div class="bg-gradient-to-r from-rose-400 to-rose-500 h-3 transition-all duration-1000 ease-out" v-if="item.overdue > 0" :style="{ width: Math.min((item.overdue / 10) * 100, 100) + '%' }"></div>
            </div>
            <p class="text-[11px] font-bold text-rose-500 mt-1.5 text-right opacity-0 group-hover:opacity-100 transition-opacity" v-if="item.overdue > 0">{{ item.overdue }} task trễ hạn cần xử lý gấp!</p>
          </div>
          
          <div v-if="resourceAllocation.length === 0" class="text-center text-sm font-medium text-slate-400 py-8">
            Chưa có dữ liệu nguồn lực
          </div>
        </div>
      </div>

      <!-- Productivity Leaderboard -->
      <div class="bg-white rounded-3xl shadow-sm border border-slate-100 p-8 hover:shadow-xl transition-shadow duration-300">
        <h2 class="text-xl font-black text-slate-800 mb-6 flex items-center gap-2"><div class="w-2 h-6 bg-amber-500 rounded-full"></div> Bảng vàng Năng suất</h2>
        <div class="space-y-4">
          <div v-for="(item, index) in productivity" :key="item.user.id" class="group flex items-center gap-5 p-4 rounded-2xl hover:bg-slate-50 transition-colors border border-transparent hover:border-slate-100">
            <div class="flex items-center justify-center w-12 h-12 rounded-2xl font-black text-xl shadow-sm transition-transform group-hover:scale-110" :class="index === 0 ? 'bg-gradient-to-br from-yellow-200 to-amber-300 text-amber-800' : index === 1 ? 'bg-gradient-to-br from-slate-200 to-slate-300 text-slate-700' : index === 2 ? 'bg-gradient-to-br from-orange-200 to-orange-300 text-orange-800' : 'bg-slate-100 text-slate-400'">
              #{{ index + 1 }}
            </div>
            <div class="relative">
              <img :src="item.user.avatarUrl" class="w-14 h-14 rounded-full shadow-md group-hover:rotate-6 transition-transform duration-300" />
              <div v-if="index === 0" class="absolute -top-3 -right-3 text-2xl filter drop-shadow-md animate-bounce">👑</div>
            </div>
            <div class="flex-1">
              <h3 class="text-lg font-black text-slate-800 tracking-tight">{{ item.user.fullName }}</h3>
              <div class="flex items-center gap-4 mt-2 text-xs">
                <span class="text-emerald-700 bg-emerald-100 px-2.5 py-1.5 rounded-lg flex items-center gap-1.5 font-bold shadow-sm"><CheckCircle class="w-4 h-4"/> {{ item.completed }} Hoàn thành</span>
                <span class="text-rose-600 bg-rose-100 px-2.5 py-1.5 rounded-lg flex items-center gap-1.5 font-bold shadow-sm" v-if="item.overdue > 0"><AlertTriangle class="w-4 h-4"/> {{ item.overdue }} Trễ hạn</span>
              </div>
            </div>
          </div>
          
          <div v-if="productivity.length === 0" class="text-center text-sm font-medium text-slate-400 py-8">
            Chưa có dữ liệu đánh giá
          </div>
        </div>
      </div>

    </div>
  </div>
</template>
