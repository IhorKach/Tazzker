import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import NoteCard from '../components/NoteCard'

type Note = {
  id: string
  title: string
  content: string
}

export default function NotesPage() {
  const navigate = useNavigate()

  const [notes, setNotes] = useState<Note[]>([
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
  ])

  const handleAddNote = () => {
    const newNote: Note = {
      id: crypto.randomUUID(), // генерируем уникальный id
      title: 'Новая заметка',
      content: ''
    }

    setNotes((prev) => [...prev, newNote])
    navigate(`/notes/${newNote.id}`)
  }

  return (
    <div className="p-4">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Заметки</h1>
        <button
          className="bg-blue-600 text-white px-4 py-2 rounded-lg shadow hover:bg-blue-700"
          onClick={handleAddNote}
        >
          + Добавить
        </button>
      </div>

      {notes.map((note) => (
        <NoteCard
        key={note.id}
        id={note.id}
        title={note.title}
        preview={note.content.slice(0, 50) + '...'}
        onDelete={() =>
          setNotes((prev) => prev.filter((n) => n.id !== note.id))
        }
      />      
      ))}
    </div>
  )
}
