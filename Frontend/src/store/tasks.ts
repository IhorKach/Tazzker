import { create } from "zustand";
import { Task, TaskDto } from "@/types/task";
import { getTasks, syncTasks } from "@/api/tasks";

interface TaskStore {
  tasks: Task[];
  setTasks: (tasks: Task[]) => void;
  fetchTasks: () => Promise<void>;
  syncTasks: () => Promise<void>;
}

export const useTaskStore = create<TaskStore>((set, get) => ({
  tasks: [],

  setTasks: (tasks: Task[]) => set({ tasks }),

  fetchTasks: async () => {
    try {
      const data: TaskDto[] = await getTasks();
      const withSync = data.map((t) => ({ ...t, isSynced: true }));
      set({ tasks: withSync });
    } catch (e) {
      console.error("Ошибка при загрузке задач:", e);
    }
  },

  syncTasks: async () => {
    try {
      const unsynced = get().tasks.filter((t) => !t.isSynced);
      if (unsynced.length === 0) return;
      await syncTasks(unsynced);
      const synced = get().tasks.map((t) => ({ ...t, isSynced: true }));
      set({ tasks: synced });
    } catch (e) {
      console.error("Ошибка при синке задач:", e);
    }
  }
}));
