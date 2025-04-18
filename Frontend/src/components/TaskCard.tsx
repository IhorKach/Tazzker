import { useState } from 'react'

type TaskCardProps = {
  title: string
  isCompleted: boolean
  onChange?: (newTitle: string) => void
  onToggle?: () => void
  onDelete?: () => void
}

export default function TaskCard({
  title,
  isCompleted,
  onChange,
  onToggle,
  onDelete
}: TaskCardProps) {
  const [isEditing, setIsEditing] = useState(false)
  const [draft, setDraft] = useState(title)

  const handleEdit = () => {
    setIsEditing(true)
    setDraft(title)
  }

  const handleSave = () => {
    if (onChange) onChange(draft)
    setIsEditing(false)
  }

  return (
    <div className="border border-gray-200 rounded-lg p-3 mb-3 shadow-sm flex items-start justify-between">
      <div className="flex-1">
        {isEditing ? (
          <input
            value={draft}
            onChange={(e) => setDraft(e.target.value)}
            onBlur={handleSave}
            onKeyDown={(e) => {
              if (e.key === 'Enter') handleSave()
            }}
            autoFocus
            className="w-full border-b focus:outline-none"
          />
        ) : (
          <h3
            className={`font-medium text-base cursor-pointer ${isCompleted ? 'line-through text-gray-400' : ''}`}
            onClick={handleEdit}
          >
            {title}
          </h3>
        )}

        <p
          onClick={onToggle}
          className="text-xs text-blue-500 hover:underline mt-1 cursor-pointer"
        >
          {isCompleted
            ? '✅ Готово — кликни чтобы вернуть'
            : '🕒 Не выполнено — кликни чтобы отметить'}
        </p>
        {onDelete && (
          <button
            onClick={(e) => {
              e.stopPropagation()
              onDelete()
            }}
            className="text-sm text-red-500 ml-2"
            title="Удалить задачу"
          >
            🗑
          </button>
        )}
      </div>
    </div>
  )
}
