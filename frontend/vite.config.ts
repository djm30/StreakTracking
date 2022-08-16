import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      "/streaks": {
        target: "http://localhost:3001/api/v1",
        changeOrigin: true,
        secure: false
      }
    }
  }
})
