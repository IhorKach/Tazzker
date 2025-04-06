import { useEffect, useState } from 'react'
import { TaskCard } from '@/components/TaskCard'
import axios from 'axios'
import type { UpdateTaskItemDto } from '@/types/UpdateTaskItemDto'

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

  const fetchTasks = async () => {
    const token = localStorage.getItem('token')
    try {
      const res = await axios.get('https://localhost:7164/getAllTasks', {
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
        [...prev].map((task) =>
          task.taskId === taskId
            ? { ...task, isCompleted: updatedTask.isCompleted }
            : task
        )
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

  const handleRemove = (id: string) => {
    console.warn('Удалить пока не работает:', id)
  }

  useEffect(() => {
    fetchTasks()
  }, [])

  return (
    <div className="flex flex-col gap-4 p-6">
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
        <p className="text-gray-500">
          Нет задач. Всё завершено или ещё ничего не добавлено.
        </p>
      )}
    </div>
  )
}
