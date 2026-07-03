<template>
  <div class="min-h-screen bg-gray-50 p-6">
    <div class="max-w-7xl mx-auto space-y-6">
      
      <!-- Header -->
      <div class="flex items-center justify-between">
        <div>
          <h1 class="text-2xl font-bold text-gray-900">Danh sách công việc</h1>
          <p class="mt-1 text-sm text-gray-500">Xem tất cả công việc dưới dạng danh sách hoặc lịch</p>
        </div>
        <div class="flex space-x-3 items-center">
          <div class="flex bg-gray-200 p-1 rounded-lg">
            <button @click="viewMode = 'list'" :class="{'bg-white shadow-sm': viewMode === 'list', 'text-gray-500 hover:text-gray-700': viewMode !== 'list'}" class="px-3 py-1.5 text-sm font-medium rounded-md flex items-center gap-2 transition-all">
              <List class="w-4 h-4"/> Danh sách
            </button>
            <button @click="viewMode = 'calendar'" :class="{'bg-white shadow-sm': viewMode === 'calendar', 'text-gray-500 hover:text-gray-700': viewMode !== 'calendar'}" class="px-3 py-1.5 text-sm font-medium rounded-md flex items-center gap-2 transition-all">
              <CalendarIcon class="w-4 h-4"/> Lịch
            </button>
          </div>
          <div class="relative group">
            <button class="inline-flex items-center gap-2 px-4 py-2 border border-gray-300 rounded-md shadow-sm text-sm font-medium text-gray-700 bg-white hover:bg-gray-50">
              <Download class="w-4 h-4" /> Xuất dữ liệu
            </button>
            <div class="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg py-1 z-10 hidden group-hover:block border border-gray-200">
              <button @click="exportCSV" class="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">Xuất file CSV (Excel)</button>
              <button @click="exportPDF" class="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">Xuất file PDF</button>
            </div>
          </div>
          <select v-model="filterStatus" class="rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
            <option value="all">Tất cả trạng thái</option>
            <option value="To Do">To Do</option>
            <option value="In Progress">In Progress</option>
            <option value="Review">Review</option>
            <option value="Done">Done</option>
          </select>
        </div>
      </div>

      <!-- Loading State -->
      <div v-if="loading" class="flex justify-center py-12">
        <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-indigo-600"></div>
      </div>

      <!-- Task Grid -->
      <div v-else-if="viewMode === 'list'" class="p-4 sm:p-8">
        <div class="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6">
          <a v-for="task in filteredTasks" :key="task.id" href="#" @click.prevent="openTaskDetail(task)" 
             class="glass-card rounded-3xl p-6 premium-hover flex flex-col h-full bg-white/60 hover:bg-white/80 group border border-white/60 shadow-lg shadow-slate-200/40 relative overflow-hidden">
            
            <!-- Deco Glow -->
            <div class="absolute -top-10 -right-10 w-32 h-32 bg-gradient-to-br from-indigo-500/10 to-purple-500/10 rounded-full blur-2xl group-hover:scale-150 transition-transform duration-700"></div>

            <div class="flex justify-between items-start mb-5 relative z-10">
              <span class="px-3 py-1.5 text-[10px] font-black uppercase tracking-wider rounded-xl border shadow-sm flex items-center gap-1.5"
                    :class="{
                      'bg-slate-100 text-slate-500 border-slate-200': task.status === 'ToDo' || task.status === 'Backlog',
                      'bg-blue-100 text-blue-700 border-blue-200': task.status === 'InProgress',
                      'bg-yellow-100 text-yellow-700 border-yellow-200': task.status === 'Review',
                      'bg-emerald-100 text-emerald-700 border-emerald-200': task.status === 'Done'
                    }">
                <span class="w-1.5 h-1.5 rounded-full" 
                      :class="{
                        'bg-slate-400': task.status === 'ToDo' || task.status === 'Backlog',
                        'bg-blue-500': task.status === 'InProgress',
                        'bg-yellow-500': task.status === 'Review',
                        'bg-emerald-500': task.status === 'Done'
                      }"></span>
                {{ task.status }}
              </span>
              <span v-if="task.dueDate" class="text-xs font-bold text-slate-500 group-hover:text-indigo-600 transition-colors flex items-center bg-white/50 px-2.5 py-1.5 rounded-lg shadow-sm border border-white">
                <CalendarIcon class="w-3.5 h-3.5 mr-1.5" />
                {{ formatDate(task.dueDate) }}
              </span>
            </div>
            
            <h3 class="text-lg font-black text-slate-800 mb-3 leading-snug group-hover:text-indigo-600 transition-colors line-clamp-2 relative z-10">
              {{ task.title }}
            </h3>
            
            <p class="text-[13px] text-slate-500 line-clamp-2 mb-6 flex-grow font-medium leading-relaxed relative z-10">
              {{ task.description || 'Không có mô tả chi tiết cho công việc này.' }}
            </p>
            
            <div class="pt-5 border-t border-slate-200/50 flex justify-between items-center mt-auto relative z-10">
              <div class="flex items-center gap-3">
                <div v-if="task.assigneeId" class="relative">
                  <img :src="getAvatar(task.assigneeId)" alt="" class="h-9 w-9 rounded-full ring-2 ring-white shadow-sm object-cover" />
                  <div class="absolute bottom-0 right-0 w-2.5 h-2.5 bg-emerald-400 rounded-full ring-2 ring-white"></div>
                </div>
                <div v-if="task.assigneeId" class="flex flex-col">
                  <span class="text-sm font-bold text-slate-700">{{ getAssigneeName(task.assigneeId) }}</span>
                  <span class="text-[9px] font-black text-slate-400 uppercase tracking-wider">Người thực hiện</span>
                </div>
                <div v-else class="flex items-center gap-2">
                  <div class="h-9 w-9 rounded-full bg-slate-100 border border-slate-200 border-dashed flex items-center justify-center">
                    <User class="w-4 h-4 text-slate-400" />
                  </div>
                  <span class="text-xs font-bold text-slate-400 italic">Chưa giao việc</span>
                </div>
              </div>
              <div class="h-9 w-9 rounded-full bg-white border border-slate-200 flex items-center justify-center group-hover:bg-indigo-600 group-hover:border-indigo-600 transition-colors shadow-sm">
                <ArrowRight class="w-4 h-4 text-slate-400 group-hover:text-white transition-colors" />
              </div>
            </div>
          </a>
        </div>
        
        <!-- Empty State -->
        <div v-if="filteredTasks.length === 0" class="glass-card rounded-3xl p-12 text-center text-slate-500 font-bold mt-8">
          Không tìm thấy công việc nào phù hợp.
        </div>
      </div>

      <!-- Calendar View -->
      <div v-else-if="viewMode === 'calendar'" class="glass-card sm:rounded-3xl overflow-hidden">
        <div class="px-6 py-4 border-b border-white/20 flex items-center justify-between bg-white/40">
          <div class="flex items-center gap-4">
            <button @click="prevMonth" class="p-1 rounded-full hover:bg-white/60 transition-colors"><ChevronLeft class="w-5 h-5"/></button>
            <h2 class="text-lg font-bold text-gray-900">{{ currentMonthName }}</h2>
            <button @click="nextMonth" class="p-1 rounded-full hover:bg-white/60 transition-colors"><ChevronRight class="w-5 h-5"/></button>
          </div>
          <button @click="currentDate = new Date()" class="px-3 py-1 text-sm font-bold border border-slate-300 rounded-xl hover:bg-white/60 transition-colors">Hôm nay</button>
        </div>
        
        <div class="grid grid-cols-7 border-b border-white/20 bg-white/20 text-center">
          <div v-for="day in ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7']" :key="day" class="py-2 text-xs font-black text-slate-500 uppercase tracking-wider border-r border-white/20 last:border-r-0">
            {{ day }}
          </div>
        </div>
        
        <div class="grid grid-cols-7 gap-px bg-slate-200 border border-slate-200 rounded-b-3xl">
          <div v-for="(day, idx) in calendarDays" :key="idx" 
               class="min-h-[140px] p-2 flex flex-col transition-colors bg-white group/cell"
               :class="{
                 'bg-indigo-50/30': day.isToday,
                 'opacity-60 bg-slate-50': !day.isCurrentMonth
               }">
            <div class="flex justify-between items-start mb-1.5">
              <span class="text-xs font-bold w-6 h-6 flex items-center justify-center rounded-full"
                    :class="{'bg-indigo-600 text-white shadow-sm shadow-indigo-200': day.isToday, 'text-slate-700': day.isCurrentMonth && !day.isToday, 'text-slate-400': !day.isCurrentMonth}">
                {{ day.date.getDate() }}
              </span>
            </div>
            <div class="flex-1 overflow-y-auto space-y-1.5 pr-1 custom-scrollbar">
              <div v-for="t in day.tasks" :key="t.id" 
                   @click.prevent="openTaskDetail(t)"
                   class="flex flex-col px-2 py-1.5 rounded-md cursor-pointer transition-all border border-transparent hover:shadow-sm relative overflow-hidden group/task"
                   :class="{
                     'bg-emerald-50 text-emerald-700 hover:border-emerald-200': t.status === 'Done',
                     'bg-blue-50 text-blue-700 hover:border-blue-200': t.status === 'InProgress',
                     'bg-amber-50 text-amber-700 hover:border-amber-200': t.status === 'Review',
                     'bg-rose-50 text-rose-700 hover:border-rose-200': isOverdue(t),
                     'bg-slate-50 text-slate-700 hover:border-slate-200': t.status === 'ToDo' && !isOverdue(t)
                   }"
                   :title="t.title">
                
                <!-- Left Accent Border -->
                <div class="absolute left-0 top-0 bottom-0 w-0.5"
                     :class="{
                       'bg-emerald-400': t.status === 'Done',
                       'bg-blue-400': t.status === 'InProgress',
                       'bg-amber-400': t.status === 'Review',
                       'bg-rose-400': isOverdue(t),
                       'bg-slate-300': t.status === 'ToDo' && !isOverdue(t)
                     }"></div>
                
                <div class="flex items-start justify-between gap-1 w-full pl-0.5">
                  <span class="text-[10px] font-bold truncate flex-1 leading-tight">{{ t.title }}</span>
                  <img v-if="t.assigneeId" :src="getAvatar(t.assigneeId)" class="w-3.5 h-3.5 rounded-full shrink-0 mt-0.5 opacity-80 group-hover/task:opacity-100" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

    </div>

    <!-- Modal nằm trong cùng root div -->
    <TaskDetailModal 
      :isOpen="isTaskDetailOpen" 
      :taskId="selectedTask?.id"
      @close="isTaskDetailOpen = false"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useTaskStore } from '../stores/taskStore';
import TaskDetailModal from '../components/TaskDetailModal.vue';
import type { Task } from '../services/mockData';
import { Download, List, Calendar as CalendarIcon, ChevronLeft, ChevronRight, ArrowRight, User } from 'lucide-vue-next';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

const taskStore = useTaskStore();

const viewMode = ref<'list' | 'calendar'>('list');
const filterStatus = ref('all');
const isTaskDetailOpen = ref(false);
const selectedTask = ref<Task | null>(null);

const loading = computed(() => false);
const tasks = computed(() => taskStore.tasks);
const users = computed(() => taskStore.users);

const filteredTasks = computed(() => {
  if (filterStatus.value === 'all') return tasks.value;
  return tasks.value.filter(t => t.status === filterStatus.value);
});



const openTaskDetail = (task: Task) => {
  selectedTask.value = task;
  isTaskDetailOpen.value = true;
};

const getAvatar = (userId: string) => {
  return users.value.find(u => u.id === userId)?.avatarUrl || 'https://ui-avatars.com/api/?name=U';
};

const getAssigneeName = (userId: string) => {
  return users.value.find(u => u.id === userId)?.fullName || 'Không rõ';
};

const formatDate = (dateString: string) => {
  return new Date(dateString).toLocaleDateString('vi-VN', {
    year: 'numeric', month: 'short', day: 'numeric'
  });
};

const isOverdue = (task: Task) => {
  if (task.status === 'Done') return false;
  const due = task.dueDate || task.createdAt;
  return new Date(due).getTime() < new Date().getTime() - 86400000;
};

// --- Calendar Logic ---
const currentDate = ref(new Date());

const currentMonthName = computed(() => {
  return currentDate.value.toLocaleDateString('vi-VN', { month: 'long', year: 'numeric' });
});

const calendarDays = computed(() => {
  const year = currentDate.value.getFullYear();
  const month = currentDate.value.getMonth();
  
  const firstDay = new Date(year, month, 1);
  const lastDay = new Date(year, month + 1, 0);
  
  const daysInMonth = lastDay.getDate();
  
  const days = [];
  
  for (let i = 0; i < firstDay.getDay(); i++) {
    const d = new Date(year, month, -firstDay.getDay() + i + 1);
    days.push({ date: d, isCurrentMonth: false, tasks: getTasksForDate(d) });
  }
  
  for (let i = 1; i <= daysInMonth; i++) {
    const d = new Date(year, month, i);
    days.push({ date: d, isCurrentMonth: true, isToday: isToday(d), tasks: getTasksForDate(d) });
  }
  
  const remainingCells = 42 - days.length; 
  for (let i = 1; i <= remainingCells; i++) {
    const d = new Date(year, month + 1, i);
    days.push({ date: d, isCurrentMonth: false, tasks: getTasksForDate(d) });
  }
  
  return days;
});

const getTasksForDate = (d: Date) => {
  // Use local time zone offset to get correct YYYY-MM-DD
  const offset = d.getTimezoneOffset() * 60000;
  const localISOTime = (new Date(d.getTime() - offset)).toISOString().split('T')[0];
  return tasks.value.filter(t => (t.dueDate || t.createdAt).startsWith(localISOTime));
};

const isToday = (d: Date) => {
  const today = new Date();
  return d.getDate() === today.getDate() && d.getMonth() === today.getMonth() && d.getFullYear() === today.getFullYear();
};

const nextMonth = () => {
  currentDate.value = new Date(currentDate.value.getFullYear(), currentDate.value.getMonth() + 1, 1);
};

const prevMonth = () => {
  currentDate.value = new Date(currentDate.value.getFullYear(), currentDate.value.getMonth() - 1, 1);
};
// ----------------------

const exportCSV = () => {
  const headers = ['ID', 'Tên công việc', 'Trạng thái', 'Người nhận', 'Ngày cập nhật'];
  const rows = filteredTasks.value.map(t => [
    t.id, 
    `"${t.title.replace(/"/g, '""')}"`, 
    t.status, 
    `"${t.assigneeId ? getAssigneeName(t.assigneeId) : 'Không có'}"`, 
    formatDate(t.updatedAt || t.createdAt)
  ]);
  const csvContent = [headers.join(','), ...rows.map(r => r.join(','))].join('\n');
  const blob = new Blob([new Uint8Array([0xEF, 0xBB, 0xBF]), csvContent], { type: 'text/csv;charset=utf-8;' });
  const url = URL.createObjectURL(blob);
  const link = document.createElement('a');
  link.setAttribute('href', url);
  link.setAttribute('download', 'danh-sach-cong-viec.csv');
  link.style.visibility = 'hidden';
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
};

const removeAccents = (str: string) => {
  return str.normalize('NFD').replace(/[\u0300-\u036f]/g, '').replace(/đ/g, 'd').replace(/Đ/g, 'D');
};

const exportPDF = () => {
  const doc = new jsPDF();
  doc.text("Danh sach cong viec (Task List)", 14, 15);
  
  const headers = [['ID', 'Ten cong viec', 'Trang thai', 'Nguoi nhan', 'Ngay cap nhat']];
  const data = filteredTasks.value.map(t => [
    t.id.substring(0, 8), 
    removeAccents(t.title), 
    t.status, 
    removeAccents(t.assigneeId ? getAssigneeName(t.assigneeId) : 'Khong co'), 
    formatDate(t.updatedAt || t.createdAt)
  ]);
  
  autoTable(doc, {
    head: headers,
    body: data,
    startY: 20
  });
  
  doc.save('danh-sach-cong-viec.pdf');
};

onMounted(async () => {
  // Store is initialized in App.vue, so data should be ready
});
</script>
