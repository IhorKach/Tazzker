import { create } from "zustand";
import { Note, NoteDto } from "@/types/note";
import { getNotes, syncNotes } from "@/api/notes";

interface NoteStore {
  notes: Note[];
  setNotes: (notes: Note[]) => void;
  fetchNotes: () => Promise<void>;
  syncNotes: () => Promise<void>;
}

export const useNoteStore = create<NoteStore>((set, get) => ({
  notes: [],

  setNotes: (notes: Note[]) => set({ notes }),

  fetchNotes: async () => {
    try {
      const data: NoteDto[] = await getNotes();
      const withSync = data.map((n) => ({ ...n, isSynced: true }));
      set({ notes: withSync });
    } catch (e) {
      console.error("Ошибка при загрузке заметок:", e);
    }
  },

  syncNotes: async () => {
    try {
      const unsynced = get().notes.filter((n) => !n.isSynced);
      if (unsynced.length === 0) return;
      await syncNotes(unsynced);
      const synced = get().notes.map((n) => ({ ...n, isSynced: true }));
      set({ notes: synced });
    } catch (e) {
      console.error("Ошибка при синке заметок:", e);
    }
  }
}));
