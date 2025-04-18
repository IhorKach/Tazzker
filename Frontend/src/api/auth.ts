import axios from "./axios";

export const login = async (username: string, password: string) => {
  const response = await axios.post("/api/auth/login", {
    username,
    password
  });

  const token = response.data;
  localStorage.setItem("token", token);
  return token;
};
