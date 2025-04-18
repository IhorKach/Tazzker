import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'

import NotFoundPage from './pages/NotFoundPage'
import ListsPage from './pages/ListsPage'
import CalendarPage from './pages/CalendarPage'
import NoteDetailsPage from './pages/NoteDetailsPage'
import NotesPage from './pages/NotesPage'
import ListDetailsPage from './pages/ListDetailsPage'
import AuthPage from './pages/AuthPage'
import Layout from './components/Layout'

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Layout />} />
        <Route path="auth" element={<AuthPage />} />
        <Route path="lists" element={<ListsPage />} />
        <Route path="lists/:id" element={<ListDetailsPage />} />
        <Route path="notes" element={<NotesPage />} />
        <Route path="notes/:id" element={<NoteDetailsPage />} />
        <Route path="calendar" element={<CalendarPage />} />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </Router>
  )
}

export default App
