<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useTaskStore } from '../stores/taskStore';
import { apiService } from '../services/api';
import type { ProjectDocument } from '../services/mockData';
import { FileText, Plus, Save, Trash2, X, Edit3 } from 'lucide-vue-next';

const taskStore = useTaskStore();

const selectedProjectId = ref('');
const documents = ref<ProjectDocument[]>([]);
const loading = ref(false);

const isEditing = ref(false);
const currentDoc = ref<{ id?: string, title: string, content: string }>({ title: '', content: '' });

onMounted(async () => {
  if (taskStore.projects.length === 0) {
    await taskStore.init();
  }
  if (taskStore.projects.length > 0) {
    selectedProjectId.value = taskStore.projects[0].id;
    await fetchDocuments();
  }
});

const fetchDocuments = async () => {
  if (!selectedProjectId.value) return;
  loading.value = true;
  try {
    documents.value = await apiService.getDocuments(selectedProjectId.value);
  } catch (error) {
    console.error('Failed to fetch documents', error);
  } finally {
    loading.value = false;
  }
};

const onProjectChange = async () => {
  await fetchDocuments();
  isEditing.value = false;
};

const openCreate = () => {
  currentDoc.value = { title: '', content: '' };
  isEditing.value = true;
};

const openEdit = (doc: ProjectDocument) => {
  currentDoc.value = { id: doc.id, title: doc.title, content: doc.content };
  isEditing.value = true;
};

const cancelEdit = () => {
  isEditing.value = false;
};

const saveDocument = async () => {
  if (!currentDoc.value.title.trim()) return alert('Vui lòng nhập tiêu đề');
  
  try {
    if (currentDoc.value.id) {
      await apiService.updateDocument(selectedProjectId.value, currentDoc.value.id, currentDoc.value.title, currentDoc.value.content);
    } else {
      await apiService.createDocument(selectedProjectId.value, currentDoc.value.title, currentDoc.value.content);
    }
    await fetchDocuments();
    isEditing.value = false;
  } catch (error) {
    console.error('Failed to save document', error);
    alert('Có lỗi xảy ra khi lưu tài liệu');
  }
};

const deleteDocument = async (docId: string) => {
  if (!confirm('Bạn có chắc chắn muốn xóa tài liệu này?')) return;
  try {
    await apiService.deleteDocument(selectedProjectId.value, docId);
    await fetchDocuments();
  } catch (error) {
    console.error('Failed to delete document', error);
  }
};

const formatDate = (dateStr: string) => {
  if (!dateStr) return '';
  return new Date(dateStr).toLocaleString('vi-VN');
};

const getAuthorName = (authorId: string) => {
  const user = taskStore.users.find(u => u.id === authorId);
  return user ? user.fullName : 'Unknown';
};
</script>

<template>
  <div class="p-6 max-w-7xl mx-auto space-y-6 pb-24">
    <!-- Header -->
    <div class="flex flex-col sm:flex-row sm:items-center justify-between gap-4 mb-6">
      <div class="flex items-center gap-3">
        <div class="p-3 bg-blue-100 rounded-lg text-blue-600">
          <FileText class="w-6 h-6" />
        </div>
        <div>
          <h1 class="text-2xl font-bold text-gray-900">Tài liệu Dự án (Wiki)</h1>
          <p class="text-gray-500 text-sm">Quản lý tri thức, yêu cầu và tài liệu kỹ thuật</p>
        </div>
      </div>
      <div class="flex items-center gap-4">
        <select v-model="selectedProjectId" @change="onProjectChange" class="rounded-lg border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 bg-white">
          <option v-for="p in taskStore.projects" :key="p.id" :value="p.id">{{ p.name }}</option>
        </select>
        <button v-if="!isEditing" @click="openCreate" class="inline-flex items-center gap-2 px-4 py-2 border border-transparent rounded-lg shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700">
          <Plus class="w-4 h-4" />
          Thêm tài liệu
        </button>
      </div>
    </div>

    <!-- Editor View -->
    <div v-if="isEditing" class="bg-white shadow-sm border border-gray-200 rounded-xl overflow-hidden flex flex-col h-[700px]">
      <div class="px-6 py-4 border-b border-gray-200 flex items-center justify-between bg-gray-50">
        <div class="flex-1 mr-4">
          <input v-model="currentDoc.title" type="text" placeholder="Nhập tiêu đề tài liệu..." class="w-full text-lg font-bold bg-transparent border-none focus:ring-0 p-0 text-gray-900 placeholder-gray-400" />
        </div>
        <div class="flex items-center gap-3">
          <button @click="cancelEdit" class="inline-flex items-center gap-1.5 px-3 py-1.5 text-sm font-medium text-gray-600 hover:text-gray-900 hover:bg-gray-100 rounded-md transition-colors">
            <X class="w-4 h-4" /> Hủy
          </button>
          <button @click="saveDocument" class="inline-flex items-center gap-1.5 px-4 py-1.5 text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 rounded-md transition-colors shadow-sm">
            <Save class="w-4 h-4" /> Lưu tài liệu
          </button>
        </div>
      </div>
      <div class="flex-1 overflow-hidden flex flex-col custom-quill-container">
        <!-- Vue Quill Editor -->
        <QuillEditor v-model:content="currentDoc.content" contentType="html" toolbar="full" theme="snow" class="flex-1 overflow-y-auto" />
      </div>
    </div>

    <!-- Document List -->
    <div v-else>
      <div v-if="loading" class="flex justify-center py-12">
        <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
      </div>
      <div v-else-if="documents.length === 0" class="text-center py-20 bg-white rounded-xl border border-dashed border-gray-300">
        <FileText class="mx-auto h-12 w-12 text-gray-300" />
        <h3 class="mt-2 text-sm font-medium text-gray-900">Không có tài liệu nào</h3>
        <p class="mt-1 text-sm text-gray-500">Bắt đầu bằng cách tạo một tài liệu mới cho dự án này.</p>
        <div class="mt-6">
          <button @click="openCreate" class="inline-flex items-center gap-2 px-4 py-2 border border-transparent rounded-lg shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700">
            <Plus class="w-4 h-4" />
            Tạo tài liệu đầu tiên
          </button>
        </div>
      </div>
      <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <div v-for="doc in documents" :key="doc.id" class="bg-white rounded-xl shadow-sm border border-gray-200 p-5 hover:shadow-md transition-shadow flex flex-col h-64">
          <div class="flex items-start justify-between mb-3">
            <h3 class="text-lg font-bold text-gray-900 line-clamp-2 leading-tight flex-1 mr-2" :title="doc.title">{{ doc.title }}</h3>
            <div class="flex items-center gap-1">
              <button @click="openEdit(doc)" class="p-1.5 text-gray-400 hover:text-blue-600 hover:bg-blue-50 rounded-md transition-colors" title="Sửa">
                <Edit3 class="w-4 h-4" />
              </button>
              <button @click="deleteDocument(doc.id)" class="p-1.5 text-gray-400 hover:text-red-600 hover:bg-red-50 rounded-md transition-colors" title="Xóa">
                <Trash2 class="w-4 h-4" />
              </button>
            </div>
          </div>
          
          <div class="flex-1 overflow-hidden relative">
            <div class="text-sm text-gray-600 prose prose-sm max-w-none line-clamp-5" v-html="doc.content"></div>
            <div class="absolute bottom-0 left-0 right-0 h-12 bg-gradient-to-t from-white to-transparent"></div>
          </div>
          
          <div class="mt-4 pt-4 border-t border-gray-100 flex items-center justify-between text-xs text-gray-500">
            <div class="flex items-center gap-1.5">
              <span class="font-medium text-gray-700">{{ getAuthorName(doc.authorId) }}</span>
            </div>
            <span>{{ formatDate(doc.updatedAt) }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style>
.custom-quill-container .ql-container {
  font-size: 16px;
  font-family: 'Inter', sans-serif;
  border: none !important;
}
.custom-quill-container .ql-toolbar {
  border: none !important;
  border-bottom: 1px solid #e5e7eb !important;
  background-color: #f9fafb;
}
.custom-quill-container .ql-editor {
  padding: 1.5rem 2rem;
  min-height: 400px;
}
</style>
