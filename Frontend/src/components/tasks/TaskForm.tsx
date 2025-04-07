import { useState } from "react";
import { taskService } from "@/lib/taskService";
import { Task } from "@/types/task";

interface Props {
  listId: string;
  parentTaskId?: string;
  onCreated?: (task: Task) => void;
}

export function TaskForm({ listId, parentTaskId, onCreated }: Props) {
  const [title, setTitle] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!title.trim()) return;
    const newTask = await taskService.create({
      listId,
      title,
      parentTaskId,
      updatedAt: new Date().toISOString(),
      order: 0,
    });
    if (onCreated) onCreated(newTask);
    setTitle("");
  };

  return (
    <form onSubmit={handleSubmit} className="flex gap-2 my-4">
      <input
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        placeholder="Новая задача"
        className="border p-2 rounded w-full"
      />
      <button type="submit" className="bg-green-500 text-white px-4 py-2 rounded">
        +
      </button>
    </form>
  );
}
