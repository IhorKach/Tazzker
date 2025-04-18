import axios from "./axios";
import { NoteDto } from "@/types/note";

export const getNotes = async (): Promise<NoteDto[]> => {
  const response = await axios.get("/api/notes/getNotes");
  return response.data;
};

export const syncNotes = async (notes: NoteDto[]) => {
  const response = await axios.post("/api/notes/syncNotes", notes);
  return response.data;
};

export const clearTrashedNotes = async (noteIds: string[]) => {
  const response = await axios.post("/api/notes/clearTrashedNotes", noteIds);
  return response.data;
};
