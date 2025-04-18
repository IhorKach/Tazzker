import { useParams } from 'react-router-dom'
import { useState } from 'react'

type Note = {
  id: string
  title: string
  content: string
}

const mockNotes: Note[] = [
  {
    id: '1',
    title: 'Мысли о проекте',
    content: 'Нужно сделать календарь и IndexedDB...'
  },
  {
    id: '2',
    title: 'Покупки',
    content: 'Купить пельмени, зубную пасту и пиво.'
  }
]

export default function NoteDetailsPage() {
  const { id } = useParams()

  const originalNote = mockNotes.find((n) => n.id === id)

  const [note, setNote] = useState<Note | null>(originalNote || null)

  if (!note) {
    return (
      <div className="p-4 max-w-2xl mx-auto space-y-4">
        <input
          type="text"
          defaultValue={`Заметка #${id}`}
          className="w-full text-xl font-bold border-b border-gray-300 focus:outline-none focus:border-blue-500"
        />

        <textarea
          defaultValue=""
          placeholder="Текст заметки..."
          className="w-full h-64 resize-none border border-gray-300 rounded p-2 focus:outline-none focus:border-blue-500"
        />
      </div>
    )
  }

  const handleChange =
    (field: keyof Note) =>
    (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
      setNote((prev) => (prev ? { ...prev, [field]: e.target.value } : null))
    }

  return (
    <div className="p-4 max-w-2xl mx-auto space-y-4">
      <input
        type="text"
        value={note.title}
        onChange={handleChange('title')}
        className="w-full text-xl font-bold border-b border-gray-300 focus:outline-none focus:border-blue-500"
      />

      <textarea
        value={note.content}
        onChange={handleChange('content')}
        placeholder="Текст заметки..."
        className="w-full h-64 resize-none border border-gray-300 rounded p-2 focus:outline-none focus:border-blue-500"
      />
    </div>
  )
}
