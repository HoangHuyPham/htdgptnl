import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import '@styles/index.css'
import App from './App'
import { ToastContainer } from 'react-toastify'
import { UserProvider } from '@contexts'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ToastContainer />
    <UserProvider>
      <App />
    </UserProvider>
  </StrictMode>,
)
