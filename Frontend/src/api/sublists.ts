import axios from "./axios";
import { SublistDto } from "@/types/sublist";

export const getSublists = async (): Promise<SublistDto[]> => {
  const response = await axios.get("/api/sublists/getSublists");
  return response.data;
};

export const syncSublists = async (sublists: SublistDto[]) => {
  const response = await axios.post("/api/sublists/syncSublists", sublists);
  return response.data;
};

export const clearTrashedSublists = async (sublistIds: string[]) => {
  const response = await axios.post("/api/sublists/clearTrashedSublists", sublistIds);
  return response.data;
};
