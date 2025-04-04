import { Checkbox } from "@/components/ui/checkbox"
import { Input } from "@/components/ui/input"
import { Textarea } from "@/components/ui/textarea"
import { Button } from "@/components/ui/button"
import { Pencil, X, Check } from "lucide-react"
import { useState } from "react"

interface TaskCardProps {
  title: string
  description: string
  done: boolean
  onToggle: () => void
  onRemove: () => void
  onUpdate: (newTitle: string, newDescription: string) => void
}

export const TaskCard = ({
  title,
  description,
  done,
  onToggle,
  onRemove,
  onUpdate
}: TaskCardProps) => {
  const [editMode, setEditMode] = useState(false)
  const [editedTitle, setEditedTitle] = useState(title)
  const [editedDescription, setEditedDescription] = useState(description)

  const saveChanges = () => {
    onUpdate(editedTitle.trim(), editedDescription.trim())
    setEditMode(false)
  }

  const cancelEdit = () => {
    setEditedTitle(title)
    setEditedDescription(description)
    setEditMode(false)
  }

  return (
    <div className="flex flex-col gap-3 p-4 bg-white rounded-xl shadow border">
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-3">
          <Checkbox checked={done} onCheckedChange={onToggle} />
          {editMode ? (
            <Input
              value={editedTitle}
              onChange={(e) => setEditedTitle(e.target.value)}
            />
          ) : (
            <span className={`text-lg ${done ? "line-through text-gray-400" : "text-gray-800"}`}>
              {title}
            </span>
          )}
        </div>
        <div className="flex items-center gap-2">
          {editMode ? (
            <>
              <Button variant="ghost" size="icon" onClick={saveChanges}>
                <Check className="w-4 h-4" />
              </Button>
              <Button variant="ghost" size="icon" onClick={cancelEdit}>
                <X className="w-4 h-4" />
              </Button>
            </>
          ) : (
            <Button variant="ghost" size="icon" onClick={() => setEditMode(true)}>
              <Pencil className="w-4 h-4" />
            </Button>
          )}
          <Button variant="ghost" size="icon" onClick={onRemove}>
            <X className="w-4 h-4 text-red-500" />
          </Button>
        </div>
      </div>
      {editMode ? (
        <Textarea
          value={editedDescription}
          onChange={(e) => setEditedDescription(e.target.value)}
        />
      ) : (
        description && <p className="text-sm text-gray-600">{description}</p>
      )}
    </div>
  )
}
