import { useState } from 'react'
import TaskCard from './TaskCard'

type Task = {
  id: string
  title: string
  isCompleted: boolean
}

type SublistBlockProps = {
  title: string
  tasks: Task[]
  onRename?: (newTitle: string) => void
  onTaskChange?: (taskId: string, newTitle: string) => void
  onTaskToggle?: (taskId: string) => void
  onAddTask?: () => void
  onDeleteSublist?: () => void
  onDeleteTask?: (taskId: string) => void;
}

export default function SublistBlock({
  title,
  tasks,
  onRename,
  onTaskChange,
  onTaskToggle,
  onAddTask,
  onDeleteSublist,
  onDeleteTask
}: SublistBlockProps) {
  const [isEditing, setIsEditing] = useState(false)
  const [draftTitle, setDraftTitle] = useState(title)

  

  const handleRename = () => {
    if (onRename) onRename(draftTitle)
    setIsEditing(false)
  }

  return (
    <div className="mb-6">
      <div className="flex justify-between items-center mb-2">
        {isEditing ? (
          <input
            value={draftTitle}
            onChange={(e) => setDraftTitle(e.target.value)}
            onBlur={handleRename}
            onKeyDown={(e) => {
              if (e.key === 'Enter') handleRename()
            }}
            autoFocus
            className="text-lg font-semibold border-b focus:outline-none w-full"
          />
        ) : (
          <h2
            className="text-lg font-semibold cursor-pointer"
            onClick={() => setIsEditing(true)}
          >
            {title}
          </h2>
        )}

        {onAddTask && (
          <button
            className="text-sm bg-green-500 text-white px-3 py-1 rounded hover:bg-green-600"
            onClick={onAddTask}
          >
            + Задача
          </button>
        )}

        {onDeleteSublist && (
          <button
            onClick={onDeleteSublist}
            className="text-sm text-red-500 ml-4"
            title="Удалить подсписок"
            
          >
            🗑
          </button>
        )}
      </div>

      {tasks.map((task) => (
        <TaskCard
          key={task.id}
          title={task.title}
          isCompleted={task.isCompleted}
          onChange={(newTitle) => onTaskChange?.(task.id, newTitle)}
          onToggle={() => onTaskToggle?.(task.id)}
          onDelete={() => onDeleteTask?.(task.id)}
        />
      ))}
    </div>
  )
}
