import axios from "./axios";
import { TaskDto } from "@/types/task";

export const getTasks = async (): Promise<TaskDto[]> => {
  const response = await axios.get("/api/tasks/getTasks");
  return response.data;
};

export const syncTasks = async (tasks: TaskDto[]) => {
  const response = await axios.post("/api/tasks/syncTasks", tasks);
  return response.data;
};

export const clearTrashedTasks = async (taskIds: string[]) => {
  const response = await axios.post("/api/tasks/clearTrashedTasks", taskIds);
  return response.data;
};
