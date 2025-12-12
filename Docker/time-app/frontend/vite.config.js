import { fileURLToPath, URL } from 'url'
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { Server } from 'http'
import { watch } from 'fs'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
   server: {
    watch:{
      usePolling: true
    }
   },
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
    },
  },
})
