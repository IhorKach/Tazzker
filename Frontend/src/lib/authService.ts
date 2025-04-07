import axios from "axios";

const API = axios.create({ baseURL: "https://localhost:7164" });

export const authService = {
  login: async (credentials: { username: string; password: string }) => {
    const res = await API.post("/login", credentials);
    return res.data as string; // token
  },
  register: async (data: { username: string; email: string; password: string }) => {
    const res = await API.post("/register", data);
    return res.data as string; // token
  },
};