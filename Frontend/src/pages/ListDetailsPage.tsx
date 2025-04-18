import { useParams } from 'react-router-dom'
import { useState } from 'react'
import SublistBlock from '../components/SublistBlock'

type Sublist = {
  id: string
  title: string
  listId: string
}

type Task = {
  id: string
  title: string
  isCompleted: boolean
  sublistId: string
}

export default function ListDetailsPage() {
  const { id: listId } = useParams()

  const [sublists, setSublists] = useState<Sublist[]>([
    { id: 's1', title: '📚 Теория', listId: '1' },
    { id: 's2', title: '🎓 Практика', listId: '1' }
  ])

  const [tasks, setTasks] = useState<Task[]>([
    {
      id: 't1',
      title: 'Понять useEffect',
      isCompleted: false,
      sublistId: 's1'
    },
    { id: 't2', title: 'Сделать TODO', isCompleted: true, sublistId: 's2' }
  ])

  const handleAddSublist = () => {
    const newSublist: Sublist = {
      id: crypto.randomUUID(),
      title: 'Новый подсписок',
      listId: listId!
    }

    setSublists((prev) => [...prev, newSublist])
  }

  const filteredSublists = sublists.filter((s) => s.listId === listId)

  return (
    <div className="p-4">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Список #{listId}</h1>
        <button
          className="bg-blue-600 text-white px-4 py-2 rounded-lg shadow hover:bg-blue-700"
          onClick={handleAddSublist}
        >
          + Подсписок
        </button>
      </div>

      {filteredSublists.map((sublist) => {
        const relatedTasks = tasks.filter((t) => t.sublistId === sublist.id)

        return (
          <SublistBlock
            key={sublist.id}
            title={sublist.title}
            tasks={relatedTasks}
            onRename={(newTitle) =>
              setSublists((prev) =>
                prev.map((s) =>
                  s.id === sublist.id ? { ...s, title: newTitle } : s
                )
              )
            }
            onTaskChange={(taskId, newTitle) =>
              setTasks((prev) =>
                prev.map((t) =>
                  t.id === taskId ? { ...t, title: newTitle } : t
                )
              )
            }
            onTaskToggle={(taskId) =>
              setTasks((prev) =>
                prev.map((t) =>
                  t.id === taskId ? { ...t, isCompleted: !t.isCompleted } : t
                )
              )
            }
            onAddTask={() =>
              setTasks((prev) => [
                ...prev,
                {
                  id: crypto.randomUUID(),
                  title: 'Новая задача',
                  isCompleted: false,
                  sublistId: sublist.id
                }
              ])
            }
            onDeleteSublist={() =>
              setSublists((prev) => prev.filter((s) => s.id !== sublist.id))
            }
            onDeleteTask={(taskId) =>
                setTasks((prev) => prev.filter((t) => t.id !== taskId))
              }              
          />
        )
      })}
    </div>
  )
}
