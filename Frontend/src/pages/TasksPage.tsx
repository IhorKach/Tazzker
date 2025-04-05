import { useEffect, useState } from "react"
import axios from "axios"

interface Task {
  id: string
  title: string
  description: string
  isCompleted: boolean
}

export default function TasksPage() {
  const [tasks, setTasks] = useState<Task[]>([])

  useEffect(() => {
    const token = localStorage.getItem("token")

    axios.get("https://localhost:7164/getAllTasks", {
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(res => {
      setTasks(res.data)
    }).catch(err => {
      console.error("Ошибка получения задач:", err)
    })
  }, [])

  return (
    <div className="p-4 flex flex-col gap-2">
      <h1 className="text-2xl font-bold">Твои задачи</h1>
      <ul className="flex flex-col gap-2">
        {tasks.map(task => (
          <li key={task.id} className="border rounded p-3 bg-white shadow">
            <p className="text-lg font-semibold">{task.title}</p>
            <p className="text-sm text-gray-500">{task.description}</p>
            <p className="text-sm">{task.isCompleted ? "✅ Выполнено" : "⏳ В процессе"}</p>
          </li>
        ))}
      </ul>
    </div>
  )
}
