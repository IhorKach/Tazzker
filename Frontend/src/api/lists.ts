import axios from "./axios";

import { ListDto } from "@/types/list";

export const getLists = async (): Promise<ListDto[]> => {
  const response = await axios.get("/api/lists/getLists");
  return response.data;
};

export const syncLists = async (lists: ListDto[]) => {
  const response = await axios.post("/api/lists/syncLists", lists);
  return response.data;
};

export const clearTrashedLists = async (listIds: string[]) => {
  const response = await axios.post("/api/lists/clearTrashedLists", listIds);
  return response.data;
};

export const createList = async (list: ListDto) => {
  return await syncLists([list]);
};