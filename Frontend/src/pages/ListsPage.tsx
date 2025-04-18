import { useNavigate } from 'react-router-dom'
import { useState } from 'react'
import ListCard from '../components/ListCard'

type List = {
  id: string
  title: string
}

export default function ListsPage() {
  const navigate = useNavigate()

  const [lists, setLists] = useState<List[]>([
    { id: '1', title: 'Учёба' },
    { id: '2', title: 'Дом' }
  ])

  const handleAddList = () => {
    const newList: List = {
      id: crypto.randomUUID(),
      title: 'Новый список'
    }

    setLists((prev) => [...prev, newList])
    navigate(`/lists/${newList.id}`)
  }

  return (
    <div className="p-4">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Списки</h1>
        <button
          className="bg-blue-600 text-white px-4 py-2 rounded-lg shadow hover:bg-blue-700"
          onClick={handleAddList}
        >
          + Добавить
        </button>
      </div>

      {lists.map((list) => (
        <ListCard
          key={list.id}
          id={list.id}
          title={list.title}
          taskCount={0}
          onRename={(newTitle) =>
            setLists((prev) =>
              prev.map((l) =>
                l.id === list.id ? { ...l, title: newTitle } : l
              )
            )
          }
          onDelete={() =>
            setLists((prev) => prev.filter((l) => l.id !== list.id))
          }
        />
      ))}
    </div>
  )
}
