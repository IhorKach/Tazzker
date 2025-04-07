import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { LayoutWrapper } from "@/components/layout/LayoutWrapper";
import { taskService } from "@/lib/taskService";
import { Task } from "@/types/task";
import { TaskForm } from "@/components/tasks/TaskForm";
import { TaskCard } from "@/components/tasks/TaskCard";
import { Sublist } from "@/components/tasks/Sublist";

export function TaskListByIdPage() {
  const { listId } = useParams();
  const [tasks, setTasks] = useState<Task[]>([]);

  useEffect(() => {
    if (listId) {
      taskService.filter({ listId }).then(setTasks);
    }
  }, [listId]);

  const handleCreated = (task: Task) => {
    setTasks((prev) => [...prev, task]);
  };

  const handleDeleted = (id: string) => {
    setTasks((prev) => prev.filter((t) => t.taskId !== id));
  };

  const handleUpdated = (updated: Task) => {
    setTasks((prev) => prev.map((t) => (t.taskId === updated.taskId ? updated : t)));
  };

  const tasksWithoutSublist = tasks
    .filter((t) => !t.parentTaskId)
    .sort((a, b) => a.order - b.order);

  const sublists = tasks
    .filter((t) => tasks.some((x) => x.parentTaskId === t.taskId))
    .sort((a, b) => a.order - b.order);

  return (
    <LayoutWrapper>
      <h2 className="text-2xl font-semibold mb-4">Список задач</h2>

      <TaskForm listId={listId || ""} onCreated={handleCreated} />

      <div className="mb-6">
        <h3 className="text-lg font-semibold">Без подсписка</h3>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {tasksWithoutSublist.map((task) => (
            <TaskCard
              key={task.taskId}
              task={task}
              onDeleted={handleDeleted}
              onUpdated={handleUpdated}
            />
          ))}
        </div>
      </div>

      <div className="space-y-6">
        {sublists.map((sublist) => (
          <div key={sublist.taskId}>
            <Sublist
              parent={sublist}
              childrenTasks={tasks
                .filter((t) => t.parentTaskId === sublist.taskId)
                .sort((a, b) => a.order - b.order)}
              onDeleted={handleDeleted}
              onUpdated={handleUpdated}
            />
            <TaskForm
              listId={listId || ""}
              parentTaskId={sublist.taskId}
              onCreated={handleCreated}
            />
          </div>
        ))}
      </div>
    </LayoutWrapper>
  );
}