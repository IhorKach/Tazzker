import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { authService } from "@/lib/authService";

export function LoginForm() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const token = await authService.login({ username, password });
      localStorage.setItem("token", token);
      navigate("/tasks");
    } catch {
      setError("Неверный логин или пароль");
    }
  };

  return (
    <form onSubmit={handleLogin} className="max-w-md mx-auto mt-10 space-y-4">
      <h2 className="text-2xl font-bold">Вход</h2>
      {error && <p className="text-red-500">{error}</p>}
      <input
        className="border p-2 w-full rounded"
        placeholder="Имя пользователя"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <input
        type="password"
        className="border p-2 w-full rounded"
        placeholder="Пароль"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button type="submit" className="bg-yellow-300 px-4 py-2 rounded w-full">
        Войти
      </button>
    </form>
  );
}