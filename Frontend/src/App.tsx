import { Routes, Route } from 'react-router-dom'
import Layout from '@/components/Layout/Layout'
import Tasks from '@/pages/Tasks'
import Login from '@/pages/Login'
import Logout from '@/pages/Logout'
import Settings from '@/pages/Settings'
import Calendar from '@/pages/Calendar'
import PrivateRoute from '@/components/PrivateRoute'

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route path="login" element={<Login />} />
        <Route path="logout" element={<Logout />} />
        <Route path="settings" element={<Settings />} />
        <Route
          path="calendar"
          element={
            <PrivateRoute>
              <Calendar />
            </PrivateRoute>
          }
        />

        <Route
          path="tasks"
          element={
            <PrivateRoute>
              <Tasks />
            </PrivateRoute>
          }
        />
      </Route>
    </Routes>
  )
}
