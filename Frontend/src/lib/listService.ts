import axios from "axios";
import { TaskList, CreateTaskListDto, UpdateTaskListDto } from "@/types/list";

const API = axios.create({ baseURL: "https://localhost:7164" });

API.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const listService = {
  getAll: async (): Promise<TaskList[]> => {
    const res = await API.get("/getAllTaskLists");
    return res.data;
  },
  create: async (dto: CreateTaskListDto): Promise<TaskList> => {
    const res = await API.post("/createTaskList", dto);
    return res.data;
  },
  update: async (dto: UpdateTaskListDto): Promise<TaskList> => {
    const res = await API.patch("/updateTaskList", dto);
    return res.data;
  },
  delete: async (id: string): Promise<void> => {
    await API.delete(`/deleteTaskList/${id}`);
  },
};
