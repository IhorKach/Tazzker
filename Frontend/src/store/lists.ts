import { create } from 'zustand'
import { List, ListDto } from '@/types/list'
import { getLists, syncLists } from '@/api/lists'

interface ListStore {
  lists: List[]
  setLists: (lists: List[]) => void
  fetchLists: () => Promise<void>
  syncLists: () => Promise<void>
  addList: (list: List) => void
  softDeleteList: (listId: string) => void
}

export const useListStore = create<ListStore>((set, get) => ({
  lists: [],

  setLists: (lists) => set({ lists }),

  fetchLists: async () => {
    try {
      const data: ListDto[] = await getLists()
      const withSync = data.map((list) => ({ ...list, isSynced: true }))
      set({ lists: withSync })
    } catch (error) {
      console.error('Ошибка при загрузке списков:', error)
    }
  },

  syncLists: async () => {
    try {
      const unsynced = get().lists.filter((l) => !l.isSynced)
      if (unsynced.length === 0) return

      await syncLists(unsynced)
      const synced = get().lists.map((l) => ({ ...l, isSynced: true }))
      set({ lists: synced })
    } catch (error) {
      console.error('Ошибка при синхронизации списков:', error)
    }
  },
  softDeleteList: (listId: string) =>
    set((state) => ({
      lists: state.lists.map((l) =>
        l.listId === listId ? { ...l, isDeleted: true, isSynced: false } : l
      )
    })),
  addList: (list: List) =>
    set((state) => ({
      lists: [...state.lists, list]
    }))
}))
