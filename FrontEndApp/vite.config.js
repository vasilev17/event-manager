import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': { // The path you want to proxy
        target: 'https://localhost:7223', // Your backend URL
        changeOrigin: true, // Required for CORS issues
        secure: false, // Set to true if your backend uses HTTPS with a valid certificate
        rewrite: (path) => path.replace(/^\/api/, ''), // Optional: remove the /api prefix
      },
      // You can add more proxy configurations here for other paths
    },
  }
})
