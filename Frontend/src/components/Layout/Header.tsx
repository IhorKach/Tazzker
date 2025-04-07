import { Link, useNavigate } from "react-router-dom";

export function Header() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <header className="bg-yellow-200 py-4 px-6 flex justify-between items-center shadow-md">
      <Link to="/" className="text-xl font-bold hover:underline">
        🐝 Tazzker
      </Link>
      <nav className="flex gap-4 items-center">
        {!token ? (
          <>
            <Link to="/login" className="hover:underline">Войти</Link>
            <Link to="/register" className="hover:underline">Регистрация</Link>
          </>
        ) : (
          <>
            <Link to="/tasks" className="hover:underline">Списки</Link>
            <Link to="/calendar" className="hover:underline">Календарь</Link>
            <button onClick={handleLogout} className="text-red-600 hover:underline">Выйти</button>
          </>
        )}
      </nav>
    </header>
  );
}