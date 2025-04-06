import { Link, useNavigate } from "react-router-dom"

export default function Sidebar() {
  const token = localStorage.getItem("token")
  const navigate = useNavigate()

  const handleLogout = () => {
    localStorage.removeItem("token")
    navigate("/logout")
  }

  return (
    <aside className="w-64 bg-white shadow h-full border-r p-4">
      <nav className="flex flex-col gap-4 text-gray-800">
        <Link to="/tasks" className="hover:text-blue-500">📝 Задачи</Link>
        <Link to="/calendar" className="hover:text-blue-500">📅 Календарь</Link>
        <Link to="/settings" className="hover:text-blue-500">⚙️ Настройки</Link>

        {token ? (
          <button
            onClick={handleLogout}
            className="mt-4 text-left text-red-500 hover:underline"
          >
            🔓 Выйти
          </button>
        ) : (
          <Link to="/login" className="mt-4 text-green-600 hover:underline">
            🔐 Войти
          </Link>
        )}
      </nav>
    </aside>
  )
}
