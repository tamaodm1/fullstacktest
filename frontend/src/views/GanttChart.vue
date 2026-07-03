<template>
  <div class="h-[calc(100vh-64px)] flex flex-col p-4 sm:p-8 overflow-hidden bg-transparent">
    <!-- Header -->
    <div class="flex flex-col sm:flex-row justify-between items-start sm:items-center mb-6 shrink-0 gap-4">
      <div>
        <h1 class="text-3xl font-black text-slate-800 tracking-tight flex items-center gap-3">
          <CalendarIcon class="w-8 h-8 text-indigo-600" />
          Tiến độ dự án (Gantt)
        </h1>
        <p class="text-sm text-slate-500 font-medium mt-1">Theo dõi thời gian và quản lý nguồn lực hiệu quả</p>
      </div>
      
      <div class="flex items-center space-x-3 bg-white/70 backdrop-blur-xl p-1.5 rounded-2xl shadow-sm border border-slate-200/60">
        <div class="relative">
          <select 
            v-model="selectedProject"
            class="pl-4 pr-10 py-2 bg-transparent text-sm font-bold text-slate-700 focus:outline-none appearance-none cursor-pointer"
          >
            <option value="all">Tất cả dự án</option>
            <option v-for="project in taskStore.projects" :key="project.id" :value="project.id">
              {{ project.name }}
            </option>
          </select>
          <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-3 text-slate-400">
            <svg class="h-4 w-4 fill-current" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
              <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z"/>
            </svg>
          </div>
        </div>
        
        <div class="h-6 w-px bg-slate-200"></div>
        
        <div class="flex">
          <button @click="zoomLevel = 'day'" :class="zoomLevel === 'day' ? 'bg-indigo-600 text-white shadow-md' : 'text-slate-600 hover:bg-slate-100'" class="px-4 py-1.5 text-xs font-bold rounded-xl transition-all">Ngày</button>
          <button @click="zoomLevel = 'week'" :class="zoomLevel === 'week' ? 'bg-indigo-600 text-white shadow-md' : 'text-slate-600 hover:bg-slate-100'" class="px-4 py-1.5 text-xs font-bold rounded-xl transition-all">Tuần</button>
        </div>
      </div>
    </div>

    <!-- Gantt Container -->
    <div class="flex-1 glass-card rounded-3xl border border-white/80 shadow-2xl overflow-hidden flex flex-col bg-white/60 backdrop-blur-2xl relative">
      
      <!-- Empty state -->
      <div v-if="filteredTasks.length === 0" class="flex-1 flex flex-col items-center justify-center text-slate-400">
        <CalendarIcon class="w-16 h-16 mb-4 text-slate-300" />
        <p class="font-bold text-xl text-slate-500">Chưa có công việc nào</p>
        <p class="text-sm mt-2">Vui lòng tạo công việc mới để xem trên biểu đồ Gantt.</p>
      </div>

      <div v-else class="flex flex-1 overflow-hidden relative">
        
        <!-- Left Panel: Task List (Fixed width) -->
        <div class="w-96 shrink-0 border-r border-slate-200/60 bg-white/95 z-20 flex flex-col shadow-[10px_0_20px_rgba(0,0,0,0.03)] relative">
          <!-- Left Header -->
          <div class="h-16 border-b border-slate-200/80 flex items-center px-6 font-black text-xs text-slate-400 uppercase tracking-widest shrink-0 bg-slate-50/80">
            <div class="flex-1">Tên công việc</div>
            <div class="w-24 text-right">Người làm</div>
          </div>
          
          <!-- Left Rows -->
          <div class="flex-1 overflow-y-hidden" ref="leftScrollRef">
            <div 
              v-for="task in filteredTasks" 
              :key="task.id"
              class="h-16 border-b border-slate-100/80 flex items-center px-6 hover:bg-indigo-50/40 transition-colors group relative cursor-pointer"
              @click="openTaskDetail(task)"
            >
              <!-- Status Indicator Line -->
              <div class="absolute left-0 top-0 bottom-0 w-1" :class="getStatusColorDot(task.status).replace('bg-', 'bg-')"></div>
              
              <div class="flex-1 truncate pr-4">
                <p class="text-[13px] font-bold text-slate-800 truncate group-hover:text-indigo-600 transition-colors">{{ task.title }}</p>
                <div class="flex items-center gap-2 mt-1">
                  <span class="text-[10px] font-black uppercase tracking-wider px-2 py-0.5 rounded-full" :class="getStatusBadgeClass(task.status)">
                    {{ task.status }}
                  </span>
                  <span v-if="!task.dueDate" class="text-[10px] font-semibold text-amber-600 bg-amber-50 px-2 py-0.5 rounded-md">Dự kiến</span>
                </div>
              </div>
              <div class="w-24 flex justify-end items-center">
                <img v-if="task.assigneeId" :src="getAvatar(task.assigneeId)" @error="(e) => { (e.target as HTMLImageElement).src = `https://ui-avatars.com/api/?name=User&background=6366f1&color=fff` }" class="w-7 h-7 rounded-full ring-2 ring-white shadow-sm" :title="getAssigneeName(task.assigneeId)" />
                <div v-else class="w-7 h-7 rounded-full bg-slate-100 flex items-center justify-center border border-slate-200 border-dashed" title="Chưa giao"><UserIcon class="w-3.5 h-3.5 text-slate-400" /></div>
              </div>
            </div>
          </div>
        </div>

        <!-- Right Panel: Timeline (Scrollable) -->
        <div class="flex-1 overflow-x-auto overflow-y-auto custom-scrollbar relative bg-slate-50/30" ref="rightScrollRef" @scroll="syncScroll">
          <div class="min-w-max relative flex flex-col">
            
            <!-- Timeline Header (Months & Days) -->
            <div class="sticky top-0 z-30 bg-white/95 backdrop-blur-xl border-b border-slate-200/80 shrink-0 shadow-sm">
              <!-- Months Row -->
              <div class="flex border-b border-slate-200/50 h-8">
                <div 
                  v-for="month in timelineMonths" 
                  :key="month.key"
                  class="flex items-center justify-center text-[11px] font-black text-slate-700 uppercase tracking-[0.2em] border-r border-slate-200/50"
                  :style="{ width: `${month.days * dayWidth}px` }"
                >
                  {{ month.label }}
                </div>
              </div>
              <!-- Days Row -->
              <div class="flex h-8">
                <div 
                  v-for="day in timelineDays" 
                  :key="day.date.toISOString()"
                  class="flex items-center justify-center text-[11px] font-bold border-r border-slate-200/50 transition-colors"
                  :class="[
                    isToday(day.date) ? 'bg-indigo-600 text-white shadow-inner' : 
                    isWeekend(day.date) ? 'bg-slate-100/80 text-slate-400' : 'text-slate-500 hover:bg-slate-50'
                  ]"
                  :style="{ width: `${dayWidth}px` }"
                >
                  {{ zoomLevel === 'day' ? day.date.getDate() : (day.date.getDay() === 1 ? day.date.getDate() : '') }}
                </div>
              </div>
            </div>

            <!-- Timeline Grid Area -->
            <div class="relative">
              
              <!-- Perfect Vertical Grid Lines -->
              <div class="absolute inset-0 flex pointer-events-none z-0">
                <div 
                  v-for="day in timelineDays" 
                  :key="'grid-'+day.date.toISOString()" 
                  class="border-r border-slate-200/60 shrink-0 h-full"
                  :class="{ 'bg-slate-100/30': isWeekend(day.date) }"
                  :style="{ width: `${dayWidth}px` }"
                ></div>
              </div>

              <!-- Today Column Highlight -->
              <div v-if="todayIndex >= 0" class="absolute top-0 bottom-0 pointer-events-none z-0 bg-indigo-500/5 border-x border-indigo-500/20 shadow-[0_0_15px_rgba(99,102,241,0.1)]" :style="{ left: `${todayIndex * dayWidth}px`, width: `${dayWidth}px` }"></div>

              <!-- Task Rows -->
              <div class="relative z-10 pt-2">
                <div 
                  v-for="task in filteredTasks" 
                  :key="`bar-${task.id}`"
                  class="h-16 border-b border-transparent relative flex items-center group"
                >
                  <div 
                    class="absolute h-10 rounded-xl shadow-md flex items-center cursor-pointer transition-all duration-300 hover:shadow-xl hover:-translate-y-0.5 group/bar border border-white/20"
                    :class="[getStatusColorBg(task.status)]"
                    :style="{
                      left: `${getTaskPosition(task).left}px`,
                      width: `${getTaskPosition(task).width}px`
                    }"
                    @click="openTaskDetail(task)"
                  >
                    <!-- Glossy overlay -->
                    <div class="absolute inset-0 bg-gradient-to-b from-white/30 to-transparent rounded-xl pointer-events-none"></div>
                    
                    <!-- Content inside bar (if width >= 100) -->
                    <div v-if="getTaskPosition(task).width >= 100" class="px-3 flex items-center justify-between w-full relative z-10">
                      <span class="text-xs font-black text-white truncate drop-shadow-md pr-2">
                        {{ task.title }}
                      </span>
                      <img v-if="task.assigneeId" :src="getAvatar(task.assigneeId)" @error="(e) => { (e.target as HTMLImageElement).src = `https://ui-avatars.com/api/?name=User&background=6366f1&color=fff` }" class="w-6 h-6 rounded-full ring-2 ring-white/50 shadow-sm shrink-0" />
                    </div>
                    
                    <!-- Content inside short bar -->
                    <div v-else class="w-full h-full flex items-center justify-center relative z-10">
                      <img v-if="task.assigneeId" :src="getAvatar(task.assigneeId)" @error="(e) => { (e.target as HTMLImageElement).src = `https://ui-avatars.com/api/?name=User&background=6366f1&color=fff` }" class="w-6 h-6 rounded-full ring-1 ring-white/50 shadow-sm shrink-0" />
                    </div>
                  </div>
                  
                  <!-- Title outside bar if too short -->
                  <div v-if="getTaskPosition(task).width < 100" class="absolute flex items-center gap-2 pointer-events-none" :style="{ left: `${getTaskPosition(task).left + getTaskPosition(task).width + 8}px` }">
                    <span class="text-xs font-bold text-slate-700 whitespace-nowrap drop-shadow-sm">{{ task.title }}</span>
                  </div>
                </div>
              </div>

            </div>

          </div>
        </div>
      </div>
    </div>
    
    <!-- Task Detail Modal -->
    <TaskDetailModal 
      :isOpen="isTaskDetailOpen" 
      :taskId="selectedTask?.id"
      @close="isTaskDetailOpen = false"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, nextTick } from 'vue';
import { useTaskStore } from '../stores/taskStore';
import TaskDetailModal from '../components/TaskDetailModal.vue';
import type { Task } from '../services/mockData';
import { Calendar as CalendarIcon, User as UserIcon } from 'lucide-vue-next';

const taskStore = useTaskStore();
const selectedProject = ref('all');
const zoomLevel = ref<'day' | 'week'>('day');

// Modal state
const isTaskDetailOpen = ref(false);
const selectedTask = ref<Task | null>(null);

const dayWidth = computed(() => zoomLevel.value === 'day' ? 40 : 12); // Width of one day block in pixels

// Sync scroll between left list and right timeline vertically
const leftScrollRef = ref<HTMLElement | null>(null);
const rightScrollRef = ref<HTMLElement | null>(null);

const syncScroll = (e: Event) => {
  if (leftScrollRef.value && rightScrollRef.value) {
    const target = e.target as HTMLElement;
    leftScrollRef.value.scrollTop = target.scrollTop;
  }
};

const filteredTasks = computed(() => {
  let tasks = taskStore.tasks;
  if (selectedProject.value !== 'all') {
    tasks = tasks.filter(t => t.projectId === selectedProject.value);
  }
  // Sort tasks by start date (createdAt)
  return tasks.sort((a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime());
});

// Calculate timeline range
const timelineRange = computed(() => {
  if (filteredTasks.value.length === 0) return { start: new Date(), end: new Date() };

  let minDate = new Date();
  let maxDate = new Date();
  
  // Look back 7 days from today as absolute minimum start
  minDate.setDate(minDate.getDate() - 7);
  // Look forward 30 days as minimum end
  maxDate.setDate(maxDate.getDate() + 30);

  filteredTasks.value.forEach(t => {
    const start = new Date(t.createdAt);
    if (start < minDate) minDate = new Date(start.getTime() - 7 * 24 * 60 * 60 * 1000); // 7 days padding

    if (t.dueDate) {
      const end = new Date(t.dueDate);
      if (end > maxDate) maxDate = new Date(end.getTime() + 14 * 24 * 60 * 60 * 1000); // 14 days padding
    }
  });

  // Ensure start is beginning of a month for cleaner UI
  minDate = new Date(minDate.getFullYear(), minDate.getMonth(), 1);
  // Ensure end is end of a month
  maxDate = new Date(maxDate.getFullYear(), maxDate.getMonth() + 1, 0);

  return { start: minDate, end: maxDate };
});

const timelineDays = computed(() => {
  const days = [];
  let current = new Date(timelineRange.value.start);
  const end = timelineRange.value.end;

  while (current <= end) {
    days.push({ date: new Date(current) });
    current.setDate(current.getDate() + 1);
  }
  return days;
});

const timelineMonths = computed(() => {
  const months: { key: string; label: string; days: number }[] = [];
  timelineDays.value.forEach(d => {
    const m = d.date.getMonth();
    const y = d.date.getFullYear();
    const key = `${y}-${m}`;
    const existing = months.find(x => x.key === key);
    if (existing) {
      existing.days++;
    } else {
      months.push({
        key,
        label: `Tháng ${m + 1}, ${y}`,
        days: 1
      });
    }
  });
  return months;
});

const todayIndex = computed(() => {
  const today = new Date();
  today.setHours(0, 0, 0, 0);
  
  if (today < timelineRange.value.start || today > timelineRange.value.end) return -1;
  
  const diffTime = Math.abs(today.getTime() - timelineRange.value.start.getTime());
  return Math.ceil(diffTime / (1000 * 60 * 60 * 24)); 
});

// Helper for Task positioning
const getTaskPosition = (task: Task) => {
  const startDate = new Date(task.createdAt || new Date());
  startDate.setHours(0,0,0,0);
  
  // If no due date, default to 3 days from start
  const isEstimated = !task.dueDate;
  const endDate = task.dueDate ? new Date(task.dueDate) : new Date(startDate.getTime() + 3 * 24 * 60 * 60 * 1000);
  endDate.setHours(23,59,59,999);

  if (endDate < startDate) {
    endDate.setTime(startDate.getTime() + 24 * 60 * 60 * 1000); // Minimum 1 day if invalid
  }

  const startDiff = (startDate.getTime() - timelineRange.value.start.getTime()) / (1000 * 60 * 60 * 24);
  const durationDays = ((endDate.getTime() - startDate.getTime()) / (1000 * 60 * 60 * 24)) || 1;

  // Minimum width is 1 day block
  const width = Math.max(durationDays * dayWidth.value, dayWidth.value);

  return {
    left: startDiff * dayWidth.value,
    width: width,
    isEstimated
  };
};

const isWeekend = (d: Date) => {
  const day = d.getDay();
  return day === 0 || day === 6;
};

const isToday = (d: Date) => {
  const today = new Date();
  return d.getDate() === today.getDate() && d.getMonth() === today.getMonth() && d.getFullYear() === today.getFullYear();
};

const openTaskDetail = (task: Task) => {
  selectedTask.value = task;
  isTaskDetailOpen.value = true;
};

// User / Assignee helpers
const getAssigneeName = (id: string) => {
  const user = taskStore.users.find(u => u.id === id);
  return user ? user.fullName : 'Unknown';
};

const getAvatar = (id: string) => {
  const user = taskStore.users.find(u => u.id === id);
  if (user && user.avatarUrl) {
    return user.avatarUrl;
  }
  const name = user ? user.fullName : 'U';
  return `https://ui-avatars.com/api/?name=${encodeURIComponent(name)}&background=6366f1&color=fff`;
};

const getStatusColorBg = (status: string) => {
  switch (status) {
    case 'ToDo': return 'bg-gradient-to-r from-slate-500 to-slate-400';
    case 'InProgress': return 'bg-gradient-to-r from-indigo-600 to-blue-500';
    case 'Review': return 'bg-gradient-to-r from-amber-500 to-orange-400';
    case 'Done': return 'bg-gradient-to-r from-emerald-500 to-teal-400';
    default: return 'bg-gradient-to-r from-slate-500 to-slate-400';
  }
};

const getStatusColorDot = (status: string) => {
  switch (status) {
    case 'ToDo': return 'bg-slate-400';
    case 'InProgress': return 'bg-indigo-500';
    case 'Review': return 'bg-amber-400';
    case 'Done': return 'bg-emerald-500';
    default: return 'bg-slate-400';
  }
};

const getStatusBadgeClass = (status: string) => {
  switch (status) {
    case 'ToDo': return 'bg-slate-100 text-slate-600';
    case 'InProgress': return 'bg-indigo-100 text-indigo-700';
    case 'Review': return 'bg-amber-100 text-amber-700';
    case 'Done': return 'bg-emerald-100 text-emerald-700';
    default: return 'bg-slate-100 text-slate-600';
  }
};

onMounted(() => {
  // Auto-scroll to today after render
  nextTick(() => {
    if (rightScrollRef.value && todayIndex.value > 0) {
      // Center today column in view
      rightScrollRef.value.scrollLeft = (todayIndex.value * dayWidth.value) - (rightScrollRef.value.clientWidth / 2);
    }
  });
});
</script>

<style scoped>
/* Hidden scrollbar for left pane */
.overflow-y-hidden::-webkit-scrollbar {
  display: none;
}
.overflow-y-hidden {
  -ms-overflow-style: none;
  scrollbar-width: none;
}
</style>
