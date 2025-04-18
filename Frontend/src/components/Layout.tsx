import { Link, Outlet } from 'react-router-dom';

export default function Layout() {
  return (
    <div className="min-h-screen p-4">
      <nav className="flex gap-4 mb-6 text-blue-600 underline">
        <Link to="/lists">Списки</Link>
        <Link to="/notes">Заметки</Link>
        <Link to="/calendar">Календарь</Link>
        <Link to="/auth">Вход</Link>
      </nav>

      <Outlet />
    </div>
  );
}
