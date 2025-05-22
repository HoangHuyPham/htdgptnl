import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import '@styles/index.css'
import App from './App'
import { Slide, ToastContainer } from 'react-toastify'
import { UserProvider } from '@contexts'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ToastContainer
      position="bottom-right"
      autoClose={5000}
      hideProgressBar={false}
      newestOnTop
      closeOnClick
      rtl={false}
      pauseOnFocusLoss
      draggable
      pauseOnHover
      theme="light"
      transition={Slide}
    />
    <UserProvider>
      <App />
    </UserProvider>
  </StrictMode>,
)
