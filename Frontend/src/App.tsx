import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate
} from 'react-router-dom'
import { HomePage } from '@/pages/HomePage'
import { LoginPage } from '@/pages/LoginPage'
import { RegisterPage } from '@/pages/RegisterPage'
import { TaskListOverview } from '@/pages/tasks/TaskListOverview'
import { TaskListByIdPage } from '@/pages/tasks/TaskListByIdPage'
import { CalendarPage } from '@/pages/CalendarPage'

const isAuth = () => !!localStorage.getItem('token')

export default function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route
          path="/tasks"
          element={isAuth() ? <TaskListOverview /> : <Navigate to="/login" />}
        />
        <Route
          path="/tasks/:listId"
          element={isAuth() ? <TaskListByIdPage /> : <Navigate to="/login" />}
        />
        <Route
          path="/calendar"
          element={isAuth() ? <CalendarPage /> : <Navigate to="/login" />}
        />
      </Routes>
    </Router>
  )
}
