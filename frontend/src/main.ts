import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createVuetify } from 'vuetify'
import 'vuetify/styles'
import '@mdi/font/css/materialdesignicons.css'
import router from './router'
import './style.css'
import App from './App.vue'

const app = createApp(App)
const pinia = createPinia()
const vuetify = createVuetify()

// Bắt lỗi Vue toàn cục - không để trang bị trắng
app.config.errorHandler = (err, _instance, info) => {
  console.error('[Vue Error]', info, err);
  // Không crash app - chỉ log lỗi
};

app.use(pinia)
app.use(vuetify)
app.use(router)
app.mount('#app')

