import { useState } from "react"
import { Input } from "@/components/ui/input"
import { Textarea } from "@/components/ui/textarea"
import { Button } from "@/components/ui/button"
import axios from "axios"

interface Props {
  listId: string
  parentTaskId: string
  onCreated: () => void
}

export const TaskCreateForm = ({ listId, parentTaskId, onCreated }: Props) => {
  const [title, setTitle] = useState("")
  const [description, setDescription] = useState("")
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState("")

  const handleSubmit = async () => {
    if (!title.trim()) {
      setError("Введите заголовок задачи.")
      return
    }

    try {
      setLoading(true)
      setError("")

      const token = localStorage.getItem("token")

      await axios.post("https://localhost:7164/CreateTask", {
        listId,
        parentTaskId,
        title,
        description,
        updatedAt: new Date().toISOString()
      }, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json"
        }
      })

      setTitle("")
      setDescription("")
      onCreated()
    } catch (err) {
      console.error("Ошибка при создании задачи:", err)
      setError("Не удалось создать задачу.")
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="flex flex-col gap-2 p-4 bg-gray-50 border rounded-xl shadow">
      <Input
        placeholder="Заголовок задачи"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
      />
      <Textarea
        placeholder="Описание (необязательно)"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
      />
      {error && <p className="text-red-500 text-sm">{error}</p>}
      <Button onClick={handleSubmit} disabled={loading}>
        {loading ? "Создание..." : "Создать задачу"}
      </Button>
    </div>
  )
}
