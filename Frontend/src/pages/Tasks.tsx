import { useEffect, useState } from 'react'
import { TaskCard } from '@/components/TaskCard'
import axios from 'axios'
import type { UpdateTaskItemDto } from '@/types/UpdateTaskItemDto'
import { TaskCreateForm } from "@/components/TaskCreateForm"

type Task = {
  taskId: string
  listId: string
  parentTaskId: string
  title: string
  description: string
  updatedAt: string
  reminderAt?: string
  dueTime?: string
  isCompleted: boolean
  isDeleted: boolean
  order?: number
}

export default function Tasks() {
  const [tasks, setTasks] = useState<Task[]>([])
  const listId = "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  const parentTaskId = "3fa85f64-5717-4562-b3fc-2c963f66afa6"

  const fetchTasks = async () => {
    const token = localStorage.getItem('token')
    try {
      const res = await axios.get<Task[]>('https://localhost:7164/getAllTasks', {
        headers: { Authorization: `Bearer ${token}` }
      })
      setTasks([...res.data].sort((a, b) => (a.order ?? 0) - (b.order ?? 0)))
    } catch (err) {
      console.error('Ошибка при получении задач', err)
    }
  }

  const handleToggle = async (taskId: string, isCompleted: boolean) => {
    const token = localStorage.getItem('token')
    try {
      const res = await axios.patch(
        'https://localhost:7164/updateTask',
        {
          taskId,
          isCompleted: !isCompleted,
          updatedAt: new Date().toISOString()
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        }
      )

      const updatedTask = res.data
      setTasks((prev) =>
        [...prev]
          .map((task) =>
            task.taskId === taskId
              ? { ...task, isCompleted: updatedTask.isCompleted }
              : task
          )
          .sort((a, b) => (a.order ?? 0) - (b.order ?? 0))
      )
    } catch (err) {
      console.error('Ошибка при переключении задачи:', err)
    }
  }

  const handleUpdate = async (dto: UpdateTaskItemDto) => {
    const token = localStorage.getItem('token')
    try {
      const res = await axios.patch('https://localhost:7164/updateTask', dto, {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      })
      console.log('Обновлено:', res.data)
      fetchTasks()
    } catch (err) {
      console.error('Ошибка при обновлении:', err)
    }
  }

  const handleRemove = async (taskId: string) => {
    const token = localStorage.getItem('token')
    try {
      await axios.delete(`https://localhost:7164/DeleteTask/${taskId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        }
      })
      setTasks(prev => prev.filter(task => task.taskId !== taskId))
    } catch (err) {
      console.error('Ошибка при удалении задачи:', err)
    }
  }

  useEffect(() => {
    fetchTasks()
  }, [])

  return (
    <div className="flex flex-col gap-6 p-6">
      <div className="flex items-center justify-between">
        <h2 className="text-2xl font-bold text-gray-800 dark:text-white">Мои задачи</h2>
        <span className="text-sm text-muted-foreground">{tasks.length} задач</span>
      </div>

      <TaskCreateForm
        listId={listId}
        parentTaskId={parentTaskId}
        onCreated={fetchTasks}
      />

      <div className="flex flex-col gap-4">
        {tasks.length > 0 ? (
          tasks.map((task) => (
            <TaskCard
              key={task.taskId}
              title={task.title}
              description={task.description}
              done={task.isCompleted}
              onToggle={() => handleToggle(task.taskId, task.isCompleted)}
              onRemove={() => handleRemove(task.taskId)}
              onUpdate={(title, description) =>
                handleUpdate({ taskId: task.taskId, title, description })
              }
            />
          ))
        ) : (
          <p className="text-center text-muted-foreground">Нет задач. Можно расслабиться... или создать новую.</p>
        )}
      </div>
    </div>
  )
}
