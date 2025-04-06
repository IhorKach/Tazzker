import { useState } from "react"
import Sidebar from "@/components/Layout/Sidebar"
import { Outlet } from "react-router-dom"
import { Menu } from "lucide-react"
import ThemeToggle from "@/components/ThemeToggle"

export default function Layout() {
  const [sidebarOpen, setSidebarOpen] = useState(true)

  return (
    <div className="flex h-screen dark:bg-background">
      {sidebarOpen && <Sidebar />}
      <div className="flex-1 flex flex-col">
        {/* Верхняя панель */}
        <div className="flex justify-between items-center px-6 py-4 border-b dark:border-gray-700">
          <button
            className="text-gray-600 dark:text-gray-300 hover:text-black dark:hover:text-white"
            onClick={() => setSidebarOpen(!sidebarOpen)}
          >
            <Menu className="w-6 h-6" />
          </button>

          {/* Вот и она, твоя потерянная кнопка */}
          <div className="flex items-center gap-2">
            <ThemeToggle />
          </div>
        </div>

        <main className="flex-1 overflow-y-auto p-6">
          <Outlet />
        </main>
      </div>
    </div>
  )
}
