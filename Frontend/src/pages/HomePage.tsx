import { LayoutWrapper } from "@/components/layout/LayoutWrapper";
import { Link } from "react-router-dom";

export function HomePage() {
  return (
    <LayoutWrapper>
      <div className="text-center py-20">
        <h1 className="text-4xl font-bold mb-4">Добро пожаловать в Tazzker 🐝</h1>
        <p className="text-gray-600 mb-6">Ваш умный таск-менеджер. Планируй умно. Жужжи сильно.</p>
        <div className="flex justify-center gap-4">
          <Link to="/login" className="bg-yellow-300 px-6 py-2 rounded shadow hover:bg-yellow-400">
            Войти
          </Link>
          <Link to="/register" className="bg-white border px-6 py-2 rounded shadow hover:bg-gray-100">
            Зарегистрироваться
          </Link>
        </div>
      </div>
    </LayoutWrapper>
  );
}