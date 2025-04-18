import { create } from "zustand";
import { Sublist, SublistDto } from "@/types/sublist";
import { getSublists, syncSublists } from "@/api/sublists";

interface SublistStore {
  sublists: Sublist[];
  setSublists: (sublists: Sublist[]) => void;
  fetchSublists: () => Promise<void>;
  syncSublists: () => Promise<void>;
}

export const useSublistStore = create<SublistStore>((set, get) => ({
  sublists: [],

  setSublists: (sublists: Sublist[]) => set({ sublists }),

  fetchSublists: async () => {
    try {
      const data: SublistDto[] = await getSublists();
      const withSync = data.map((s) => ({ ...s, isSynced: true }));
      set({ sublists: withSync });
    } catch (e) {
      console.error("Ошибка при загрузке подсписков:", e);
    }
  },

  syncSublists: async () => {
    try {
      const unsynced = get().sublists.filter((s) => !s.isSynced);
      if (unsynced.length === 0) return;
      await syncSublists(unsynced);
      const synced = get().sublists.map((s) => ({ ...s, isSynced: true }));
      set({ sublists: synced });
    } catch (e) {
      console.error("Ошибка при синке подсписков:", e);
    }
  }
}));
