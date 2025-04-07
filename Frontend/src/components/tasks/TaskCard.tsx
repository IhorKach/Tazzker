import { useState } from "react";
import { Task, UpdateTaskDto } from "@/types/task";
import { taskService } from "@/lib/taskService";

interface Props {
  task: Task;
  onDeleted?: (id: string) => void;
  onUpdated?: (task: Task) => void;
}

export const TaskCard = ({ task, onDeleted, onUpdated }: Props) => {
  const [isEditing, setIsEditing] = useState(false);
  const [title, setTitle] = useState(task.title || "");
  const [description, setDescription] = useState(task.description || "");

  const handleDelete = async () => {
    if (!confirm("Удалить задачу?")) return;
    await taskService.delete(task.taskId);
    if (onDeleted) onDeleted(task.taskId);
  };

  const handleUpdate = async () => {
    const updateDto: UpdateTaskDto = {
      taskId: task.taskId,
      title,
      description,
      updatedAt: new Date().toISOString(),
    };
    const updated = await taskService.update(updateDto);
    setIsEditing(false);
    if (onUpdated) onUpdated(updated);
  };

  const toggleCompleted = async () => {
    const updated = await taskService.update({
      taskId: task.taskId,
      isCompleted: !task.isCompleted,
    });
    if (onUpdated) onUpdated(updated);
  };

  if (isEditing) {
    return (
      <div className="border p-4 rounded shadow-sm bg-white">
        <input
          className="border mb-2 p-2 w-full rounded"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <textarea
          className="border mb-2 p-2 w-full rounded"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <div className="flex gap-2">
          <button
            onClick={handleUpdate}
            className="bg-green-500 text-white px-3 py-1 rounded"
          >
            Сохранить
          </button>
          <button
            onClick={() => setIsEditing(false)}
            className="bg-gray-300 px-3 py-1 rounded"
          >
            Отмена
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="relative border p-4 rounded shadow-sm bg-white">
      <div className="flex items-center gap-2">
        <input
          type="checkbox"
          checked={task.isCompleted}
          onChange={toggleCompleted}
        />
        <div>
          <h3 className="font-bold text-lg line-clamp-1">
            {task.title}
          </h3>
          {task.description && <p className="text-sm text-gray-600">{task.description}</p>}
        </div>
      </div>
      <div className="absolute top-2 right-2 flex gap-2">
        <button
          onClick={() => setIsEditing(true)}
          className="text-blue-500 hover:text-blue-700"
          title="Редактировать"
        >
          ✎
        </button>
        <button
          onClick={handleDelete}
          className="text-red-500 hover:text-red-700"
          title="Удалить"
        >
          ✕
        </button>
      </div>
    </div>
  );
};
