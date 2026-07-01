<template>
  <div class="min-h-screen bg-gray-50 p-6">
    <div class="max-w-7xl mx-auto space-y-6">
      
      <!-- Header -->
      <div class="flex items-center justify-between">
        <div>
          <h1 class="text-2xl font-bold text-gray-900">Danh sách công việc</h1>
          <p class="mt-1 text-sm text-gray-500">Xem tất cả công việc dưới dạng danh sách</p>
        </div>
        <div class="flex space-x-3">
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

      <!-- Task List -->
      <div v-else class="bg-white shadow overflow-hidden sm:rounded-md">
        <ul role="list" class="divide-y divide-gray-200">
          <li v-for="task in filteredTasks" :key="task.id">
            <a href="#" @click.prevent="openTaskDetail(task)" class="block hover:bg-gray-50 transition-colors">
              <div class="px-4 py-4 sm:px-6">
                <div class="flex items-center justify-between">
                  <p class="text-sm font-medium text-indigo-600 truncate">{{ task.title }}</p>
                  <div class="ml-2 flex-shrink-0 flex">
                    <p class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full"
                       :class="{
                         'bg-gray-100 text-gray-800': task.status === 'To Do',
                         'bg-blue-100 text-blue-800': task.status === 'In Progress',
                         'bg-yellow-100 text-yellow-800': task.status === 'Review',
                         'bg-green-100 text-green-800': task.status === 'Done'
                       }">
                      {{ task.status }}
                    </p>
                  </div>
                </div>
                <div class="mt-2 sm:flex sm:justify-between">
                  <div class="sm:flex">
                    <p class="flex items-center text-sm text-gray-500 line-clamp-1">
                      {{ task.description || 'Không có mô tả' }}
                    </p>
                  </div>
                  <div class="mt-2 flex items-center text-sm text-gray-500 sm:mt-0">
                    <div v-if="task.assigneeId" class="flex items-center mr-4" title="Người nhận">
                      <img :src="getAvatar(task.assigneeId)" alt="" class="h-5 w-5 rounded-full mr-1.5" />
                      {{ getAssigneeName(task.assigneeId) }}
                    </div>
                    <p>
                      Cập nhật: <time>{{ formatDate(task.updatedAt || task.createdAt) }}</time>
                    </p>
                  </div>
                </div>
              </div>
            </a>
          </li>
          
          <!-- Empty State -->
          <li v-if="filteredTasks.length === 0" class="px-4 py-8 text-center text-sm text-gray-500">
            Không tìm thấy công việc nào.
          </li>
        </ul>
      </div>

    </div>
  </div>

  <TaskDetailModal 
    :isOpen="isTaskDetailOpen" 
    :taskId="selectedTask?.id"
    @close="isTaskDetailOpen = false"
  />
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useTaskStore } from '../stores/taskStore';
import TaskDetailModal from '../components/TaskDetailModal.vue';
import type { Task } from '../services/mockData';

const taskStore = useTaskStore();

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

const fetchTasks = async () => {
  // taskStore automatically handles updates, no explicit fetchTasks needed here
};

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

onMounted(async () => {
  // Store is initialized in App.vue, so data should be ready
});
</script>
