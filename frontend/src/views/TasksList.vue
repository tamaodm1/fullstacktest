<template>
  <div class="min-h-screen p-8 font-inter">
    <div class="max-w-7xl mx-auto space-y-8">
      
      <!-- Top Navigation -->
      <div class="flex items-center justify-between">
        <div class="flex items-center space-x-2 text-sm">
          <span class="text-slate-400 font-medium">Synchro</span>
          <span class="text-slate-400">/</span>
          <span class="text-slate-900 font-bold">My Task</span>
        </div>
        <div class="flex items-center space-x-4">
          <div class="relative cursor-pointer" @click="alert('Tính năng tìm kiếm đang phát triển')">
            <Search class="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-400" />
            <input type="text" placeholder="Search task..." class="pl-9 pr-4 py-2 glass-panel rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-slate-900 w-64 cursor-pointer" readonly />
          </div>
          <div class="flex items-center text-slate-400 text-sm">
            <Clock class="w-4 h-4 mr-1.5" />
            <span>3 min ago</span>
          </div>
          <div class="flex -space-x-2">
            <img class="w-8 h-8 rounded-full border-2 border-[#F8F9FB]" src="https://ui-avatars.com/api/?name=K&background=0D8ABC&color=fff" />
            <img class="w-8 h-8 rounded-full border-2 border-[#F8F9FB]" src="https://ui-avatars.com/api/?name=L&background=F59E0B&color=fff" />
            <img class="w-8 h-8 rounded-full border-2 border-[#F8F9FB]" src="https://ui-avatars.com/api/?name=A&background=10B981&color=fff" />
            <div class="w-8 h-8 rounded-full border-2 border-[#F8F9FB] bg-slate-100 flex items-center justify-center text-xs font-bold text-slate-600">+2</div>
          </div>
          <button @click="alert('Tính năng mời thành viên đang phát triển')" class="bg-slate-900 text-white px-4 py-2 rounded-lg text-sm font-bold hover:bg-slate-800 transition-colors flex items-center space-x-2">
            <Plus class="w-4 h-4" />
            <span>Invite</span>
          </button>
          <button @click="alert('Tính năng tùy chọn đang phát triển')" class="w-9 h-9 border border-slate-200 rounded-xl flex items-center justify-center text-slate-400 hover:text-slate-900 transition-all"><MoreHorizontal class="w-5 h-5" /></button>
        </div>
      </div>

      <h1 class="text-2xl font-extrabold text-slate-900">My Task</h1>

      <!-- Interactive Task Calendar -->
      <div class="glass-panel rounded-2xl p-6 shadow-sm">
        <div class="flex justify-between items-center mb-6">
          <h2 class="text-sm font-bold text-slate-900">Task Calendar</h2>
          <div class="flex space-x-2">
            <button @click="calendarOffset -= 1" class="w-8 h-8 border border-slate-200 rounded-lg flex items-center justify-center text-slate-500 bg-white/50 hover:text-slate-900 hover:bg-white transition-all"><ChevronLeft class="w-4 h-4" /></button>
            <button @click="calendarOffset = 0" class="px-3 text-xs font-bold text-slate-600 bg-white/50 border border-slate-200 rounded-lg hover:text-slate-900 hover:bg-white transition-all">Today</button>
            <button @click="calendarOffset += 1" class="w-8 h-8 border border-slate-200 rounded-lg flex items-center justify-center text-slate-500 bg-white/50 hover:text-slate-900 hover:bg-white transition-all"><ChevronRight class="w-4 h-4" /></button>
          </div>
        </div>
        
        <!-- Headers -->
        <div class="flex text-xs font-bold text-slate-400 mb-2 relative">
          <div v-for="day in timelineDays" :key="day.dateStr" class="flex-1 text-center" :class="{ 'text-slate-900': day.isToday }">
            {{ day.label }}
          </div>
        </div>
        
        <!-- Timeline grid -->
        <div class="relative h-28 border-t border-slate-100 mt-2 overflow-hidden">
           <!-- Day dividers -->
           <div class="absolute inset-0 flex pointer-events-none">
             <div v-for="day in timelineDays" :key="'grid-'+day.dateStr" class="flex-1 border-r border-slate-100 last:border-r-0"></div>
           </div>
           
           <!-- Tasks placed on grid -->
           <div class="relative w-full h-full pt-4">
              <!-- Dropzones for each day -->
              <div class="absolute inset-0 flex">
                 <div v-for="day in timelineDays" :key="'drop-'+day.dateStr" class="flex-1 h-full transition-colors border border-transparent group relative" 
                      @dragover.prevent
                      @dragenter.prevent="dragHoverDate = day.dateStr"
                      @dragleave="dragHoverDate = null"
                      @drop="onDropTimeline($event, day.dateStr)"
                      :class="{ 'bg-slate-50 border-slate-200 border-dashed rounded-lg': dragHoverDate === day.dateStr }">
                      
                      <!-- Hover Line -->
                      <div class="absolute top-0 bottom-0 left-0 w-px bg-slate-900 opacity-0 group-hover:opacity-100 transition-opacity pointer-events-none z-20">
                         <!-- Inverted Triangle -->
                         <div class="absolute top-0 left-1/2 -translate-x-1/2 w-0 h-0 border-l-[6px] border-r-[6px] border-t-[8px] border-l-transparent border-r-transparent border-t-slate-900"></div>
                      </div>
                 </div>
              </div>
              
              <!-- Placed tasks -->
              <div v-for="task in timelineTasks" :key="'tl-'+task.id"
                   draggable="true"
                   @dragstart="onDragStart($event, task)"
                   @click="openTaskDetail(task)"
                   class="absolute h-6 rounded-full flex items-center px-3 text-[10px] font-bold cursor-pointer transition-all z-10 shadow-sm border hover:shadow-md"
                   :style="getTimelineStyle(task)"
                   :class="getTimelineClasses(task)">
                 <span class="mr-1.5 opacity-80 whitespace-nowrap">{{ getShortDate(task) }}</span>
                 <span class="truncate w-full">{{ task.title }}</span>
              </div>
           </div>
        </div>
      </div>

      <!-- Kanban Board -->
      <div class="space-y-4">
        <div class="flex items-center justify-between">
          <h2 class="text-lg font-bold text-slate-900">All Task (Drag & Drop to update status)</h2>
          <div class="flex space-x-2 glass-panel rounded-lg p-1">
            <button class="px-3 py-1.5 text-xs font-bold text-slate-500 rounded-md hover:bg-slate-50 flex items-center space-x-1.5 transition-colors">
              <LayoutGrid class="w-3.5 h-3.5" /> <span>Spreadsheet</span>
            </button>
            <button class="px-3 py-1.5 text-xs font-bold text-slate-500 rounded-md hover:bg-slate-50 flex items-center space-x-1.5 transition-colors">
              <CalendarIcon class="w-3.5 h-3.5" /> <span>Timeline</span>
            </button>
            <button class="px-3 py-1.5 text-xs font-bold text-slate-900 bg-white rounded-md shadow-sm border border-slate-200 flex items-center space-x-1.5 transition-colors">
              <Kanban class="w-3.5 h-3.5" /> <span>Kanban</span>
            </button>
          </div>
        </div>

        <div class="flex gap-6 overflow-x-auto pb-4">
          <!-- Columns -->
          <div v-for="(col, index) in columns" :key="index" class="flex-1 min-w-[280px]"
               @dragover.prevent
               @dragenter.prevent="dragHoverStatus = col.status"
               @dragleave="dragHoverStatus = null"
               @drop="onDropKanban($event, col.status)">
            
            <div class="flex items-center justify-between mb-4">
              <div class="flex items-center space-x-2 glass-panel px-3 py-1.5 rounded-lg shadow-sm">
                <div class="w-2 h-2 rounded-full" :class="col.colorClass"></div>
                <span class="text-xs font-bold text-slate-700">{{ col.title }}</span>
                <span class="text-xs font-bold text-slate-400 ml-1">{{ getTasksByStatus(col.status).length }}</span>
              </div>
              <div class="flex space-x-1 text-slate-400">
                <button class="hover:text-slate-900" title="More options" @click="alert('Tính năng đang phát triển')"><MoreHorizontal class="w-4 h-4" /></button>
                <button class="hover:text-slate-900" title="Add task" @click="openQuickTask(col.status)"><Plus class="w-4 h-4" /></button>
              </div>
            </div>

            <!-- Task Cards Container (Dropzone) -->
            <div class="space-y-3 min-h-[150px] p-2 -mx-2 rounded-xl transition-colors border-2 border-transparent"
                 :class="{ 'bg-slate-100/50 border-slate-300 border-dashed': dragHoverStatus === col.status }">
              <div v-for="task in getTasksByStatus(col.status)" :key="task.id" 
                   draggable="true"
                   @dragstart="onDragStart($event, task)"
                   @click="openTaskDetail(task)"
                   class="glass-card rounded-xl p-4 premium-hover cursor-pointer transition-all">
                <div class="flex justify-between items-start mb-2">
                  <h3 class="text-sm font-bold text-slate-900">{{ task.title }}</h3>
                  <button class="text-slate-400 hover:text-slate-900"><MoreHorizontal class="w-4 h-4" /></button>
                </div>
                
                <div class="flex items-center space-x-1.5 mb-4 text-[10px] font-bold text-rose-600 bg-rose-50/80 w-max px-2 py-1 rounded-md border border-rose-100">
                  <CalendarIcon class="w-3 h-3 text-rose-500" />
                  <span>{{ formatDate(task.dueDate) }}</span>
                </div>

                <div class="mb-4">
                  <p class="text-xs font-medium text-slate-600 truncate">{{ task.description || 'Không có mô tả' }}</p>
                </div>

                <div v-if="task.status !== 'Done'" class="mb-4">
                  <div class="flex justify-between text-[10px] font-bold text-slate-500 mb-1.5">
                    <span>Progress</span>
                    <span>{{ col.progress }}%</span>
                  </div>
                  <div class="h-1.5 w-full bg-slate-100/50 rounded-full overflow-hidden">
                    <div class="h-full bg-emerald-500 rounded-full" :style="`width: ${col.progress}%`"></div>
                  </div>
                </div>

                <div class="flex justify-between items-center border-t border-slate-200/50 pt-3">
                  <div class="flex -space-x-1.5">
                    <img v-if="task.assigneeId" :src="getAvatar(task.assigneeId)" class="w-6 h-6 rounded-full border-2 border-white object-cover shadow-sm" />
                    <div v-else class="w-6 h-6 rounded-full border-2 border-white bg-slate-100 flex items-center justify-center text-[8px] text-slate-400"><User class="w-3 h-3"/></div>
                  </div>
                  <div class="flex items-center space-x-3 text-slate-400 text-xs font-bold">
                    <div class="flex items-center space-x-1"><MessageSquare class="w-3.5 h-3.5"/><span>3</span></div>
                    <div class="flex items-center space-x-1"><Paperclip class="w-3.5 h-3.5"/><span>2</span></div>
                  </div>
                </div>
              </div>
              
              <!-- Empty state placeholder -->
              <div v-if="getTasksByStatus(col.status).length === 0" class="h-24 rounded-xl border-2 border-slate-200/50 border-dashed flex items-center justify-center text-xs font-bold text-slate-400">
                Kéo thả task vào đây
              </div>
            </div>
            
          </div>
        </div>
      </div>

    </div>

    <!-- Modal -->
    <TaskDetailModal 
      :isOpen="isTaskDetailOpen" 
      :taskId="selectedTask?.id"
      @close="isTaskDetailOpen = false"
    />
    <!-- Quick Task Modal -->
    <QuickTaskModal 
      :isOpen="isQuickTaskOpen" 
      :defaultStatus="quickTaskStatus"
      @close="isQuickTaskOpen = false" 
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import { useTaskStore } from '../stores/taskStore';
import TaskDetailModal from '../components/TaskDetailModal.vue';
import QuickTaskModal from '../components/QuickTaskModal.vue';
import type { Task } from '../services/mockData';
import { 
  Search, Clock, Plus, MoreHorizontal, 
  ChevronLeft, ChevronRight,
  LayoutGrid, Calendar as CalendarIcon, Kanban, 
  MessageSquare, Paperclip, User 
} from 'lucide-vue-next';

const taskStore = useTaskStore();

const isTaskDetailOpen = ref(false);
const selectedTask = ref<Task | null>(null);

const isQuickTaskOpen = ref(false);
const quickTaskStatus = ref<string>('ToDo');

const tasks = computed(() => taskStore.tasks);
const users = computed(() => taskStore.users);

// ===================
// KANBAN LOGIC
// ===================
const columns = [
  { title: 'To-do', status: 'ToDo', colorClass: 'bg-slate-400', progress: 0 },
  { title: 'In Progress', status: 'InProgress', colorClass: 'bg-blue-400', progress: 45 },
  { title: 'In Review', status: 'Review', colorClass: 'bg-amber-400', progress: 85 },
  { title: 'Completed', status: 'Done', colorClass: 'bg-emerald-400', progress: 100 }
];

const getTasksByStatus = (status: string) => {
  return tasks.value.filter(t => t.status === status);
};

const dragHoverStatus = ref<string | null>(null);

const onDropKanban = async (e: DragEvent, status: string) => {
  dragHoverStatus.value = null;
  const taskId = e.dataTransfer?.getData('taskId');
  if (taskId) {
    const task = tasks.value.find(t => t.id === taskId);
    if (task && task.status !== status) {
      await taskStore.updateTaskStatus(taskId, status as any);
    }
  }
};

// ===================
// TIMELINE LOGIC
// ===================
const calendarOffset = ref(0);
const dragHoverDate = ref<string | null>(null);

const timelineDays = computed(() => {
  const days = [];
  const baseDate = new Date();
  baseDate.setDate(baseDate.getDate() + calendarOffset.value - 2); // Show from 2 days ago

  for (let i = 0; i < 7; i++) {
    const d = new Date(baseDate);
    d.setDate(d.getDate() + i);
    // Correct local ISO string equivalent
    const offset = d.getTimezoneOffset() * 60000;
    const dateStr = (new Date(d.getTime() - offset)).toISOString().split('T')[0];
    
    const todayOffset = new Date().getTimezoneOffset() * 60000;
    const todayStr = (new Date(Date.now() - todayOffset)).toISOString().split('T')[0];
    const isToday = dateStr === todayStr;
    
    const label = d.toLocaleDateString('en-US', { day: 'numeric', month: 'short' });
    days.push({ date: d, dateStr, label, isToday, index: i });
  }
  return days;
});

const timelineTasks = computed(() => {
  if (timelineDays.value.length === 0) return [];
  const startStr = timelineDays.value[0].dateStr;
  const endStr = timelineDays.value[timelineDays.value.length - 1].dateStr;
  
  return tasks.value.filter(t => {
    const due = t.dueDate?.split('T')[0] || t.createdAt.split('T')[0];
    return due >= startStr && due <= endStr;
  });
});

const getTimelineStyle = (task: Task) => {
  const due = task.dueDate?.split('T')[0] || task.createdAt.split('T')[0];
  const dayIndex = timelineDays.value.findIndex(d => d.dateStr === due);
  if (dayIndex === -1) return { display: 'none' };
  
  // Deterministic row assignment based on ID to avoid overlaps
  let tIndex = 0;
  for(let i=0; i<task.id.length; i++){
    tIndex += task.id.charCodeAt(i);
  }
  const row = tIndex % 3; 
  
  const left = (dayIndex * (100 / 7)) + 1; 
  const width = (100 / 7) - 2; 
  const top = row * 28; 
  
  return {
    left: `${left}%`,
    width: `${width}%`,
    top: `${top}px`
  };
};

const getTimelineClasses = (task: Task) => {
  if (task.status === 'InProgress' || task.status === 'Review') return 'bg-slate-900 text-white border-slate-900';
  return 'bg-slate-50 text-slate-600 border-slate-200';
};

const onDragStart = (e: DragEvent, task: Task) => {
  if (e.dataTransfer) {
    e.dataTransfer.dropEffect = 'move';
    e.dataTransfer.effectAllowed = 'move';
    e.dataTransfer.setData('taskId', task.id);
  }
};

const onDropTimeline = async (e: DragEvent, dateStr: string) => {
  dragHoverDate.value = null;
  const taskId = e.dataTransfer?.getData('taskId');
  if (taskId) {
    const task = tasks.value.find(t => t.id === taskId);
    if (task) {
      // Create copy of task with new dueDate
      const updated = { ...task, dueDate: dateStr + 'T00:00:00Z' }; // Append time for proper DB format
      await taskStore.updateTask(updated);
    }
  }
};


// ===================
// UTILS
// ===================
const openTaskDetail = (task: Task) => {
  selectedTask.value = task;
  isTaskDetailOpen.value = true;
};

const openQuickTask = (status: string) => {
  quickTaskStatus.value = status;
  isQuickTaskOpen.value = true;
};

const alert = (msg: string) => {
  window.alert(msg);
};

const getAvatar = (userId: string) => {
  return users.value.find(u => u.id === userId)?.avatarUrl || 'https://ui-avatars.com/api/?name=U';
};

const formatDate = (dateString: string) => {
  if (!dateString) return 'No Date';
  return new Date(dateString).toLocaleDateString('en-US', {
    month: 'short', day: 'numeric', year: 'numeric'
  });
};

const getShortDate = (task: Task) => {
  const due = task.dueDate?.split('T')[0] || task.createdAt.split('T')[0];
  return new Date(due).toLocaleDateString('en-US', { day: 'numeric', month: 'short' });
};
</script>
