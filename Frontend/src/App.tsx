import { useState } from "react"
import { TaskCard } from "@/components/TaskCard"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"

interface Task {
  id: number
  title: string
  description: string
  done: boolean
}

function App() {
  const [tasks, setTasks] = useState<Task[]>([])
  const [inputValue, setInputValue] = useState("")

  const addTask = () => {
    const trimmed = inputValue.trim()
    if (!trimmed) return

    const newTask: Task = {
      id: Date.now(),
      title: inputValue,
      description: "", // пока пусто
      done: false
    }
    

    setTasks(prev => [...prev, newTask])
    setInputValue("") // очищаем поле
  }

  const toggleTask = (id: number) => {
    setTasks(prev =>
      prev.map(task =>
        task.id === id ? { ...task, done: !task.done } : task
      )
    )
  }

  const updateTask = (id: number, newTitle: string, newDesc: string) => {
    setTasks(prev =>
      prev.map(task =>
        task.id === id ? { ...task, title: newTitle, description: newDesc } : task
      )
    )
  }
  


  const removeTask = (id: number) => {
    setTasks(prev => prev.filter(task => task.id !== id))
  }

  return (
    <div className="flex flex-col items-center justify-start min-h-screen bg-yellow-50 p-8 gap-6">
      <h1 className="text-3xl font-bold text-gray-800">Tazzker</h1>

      <div className="flex gap-2 w-full max-w-md">
        <Input
          placeholder="Что нужно сделать?"
          value={inputValue}
          onChange={e => setInputValue(e.target.value)}
          onKeyDown={e => {
            if (e.key === "Enter") addTask()
          }}
        />
        <Button onClick={addTask}>Добавить</Button>
      </div>

      <div className="w-full max-w-md space-y-4 mt-6">
        {tasks.map(task => (
          <TaskCard
          key={task.id}
          title={task.title}
          description={task.description}
          done={task.done}
          onToggle={() => toggleTask(task.id)}
          onRemove={() => removeTask(task.id)}
          onUpdate={(title, desc) => updateTask(task.id, title, desc)}
        />
        
        ))}
      </div>
    </div>
  )
}

export default App
