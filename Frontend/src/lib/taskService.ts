import axios from "axios";
import { Task, CreateTaskDto, UpdateTaskDto } from "@/types/task";

const API = axios.create({
  baseURL: "https://localhost:7164",
});

API.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const taskService = {
  getAll: async (): Promise<Task[]> => {
    const res = await API.get("/getAllTasks");
    return res.data;
  },
  getById: async (id: string): Promise<Task> => {
    const res = await API.get(`/getById/${id}`);
    return res.data;
  },
  filter: async (params: any): Promise<Task[]> => {
    const res = await API.get("/filter", { params });
    return res.data;
  },
  create: async (dto: CreateTaskDto): Promise<Task> => {
    const res = await API.post("/createTask", dto);
    return res.data;
  },
  update: async (dto: UpdateTaskDto): Promise<Task> => {
    const res = await API.patch("/updateTask", dto);
    return res.data;
  },
  delete: async (id: string): Promise<void> => {
    await API.delete(`/deleteTask/${id}`);
  },
};