import { useEffect, useState } from "react";
import { Task } from "@/types/task";
import { taskService } from "@/lib/taskService";

export const useTasks = () => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState(true);

  const fetchTasks = async () => {
    setLoading(true);
    const data = await taskService.getAll();
    setTasks(data);
    setLoading(false);
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  return { tasks, loading, refetch: fetchTasks };
};
