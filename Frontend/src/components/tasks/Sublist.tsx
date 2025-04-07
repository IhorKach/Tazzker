import { Task } from "@/types/task";
import { TaskCard } from "./TaskCard";

interface Props {
  parent: Task;
  childrenTasks: Task[];
  onDeleted?: (id: string) => void;
  onUpdated?: (task: Task) => void;
}

export const Sublist = ({ parent, childrenTasks, onDeleted, onUpdated }: Props) => {
  return (
    <div className="bg-yellow-50 border rounded p-4">
      <h3 className="text-xl font-bold mb-2">{parent.title || "Без названия подсписка"}</h3>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {childrenTasks.map((task) => (
          <TaskCard
            key={task.taskId}
            task={task}
            onDeleted={onDeleted}
            onUpdated={onUpdated}
          />
        ))}
      </div>
    </div>
  );
};