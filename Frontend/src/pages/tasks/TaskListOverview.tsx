import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { LayoutWrapper } from "@/components/layout/LayoutWrapper";
import { listService } from "@/lib/listService";
import { TaskList } from "@/types/list";

export function TaskListOverview() {
  const [lists, setLists] = useState<TaskList[]>([]);
  const [newList, setNewList] = useState("");
  const [editingListId, setEditingListId] = useState<string | null>(null);
  const [editedName, setEditedName] = useState("");
  const [deleteConfirmId, setDeleteConfirmId] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    listService.getAll().then(setLists);
  }, []);

  const handleCreate = async () => {
    if (!newList.trim()) return;
    const created = await listService.create({ name: newList });
    setLists((prev) => [...prev, created]);
    setNewList("");
  };

  const handleUpdate = async (id: string) => {
    if (!editedName.trim()) return;
    const updated = await listService.update({ taskListId: id, name: editedName });
    setLists((prev) => prev.map((l) => (l.taskListId === id ? updated : l)));
    setEditingListId(null);
  };

  const handleDelete = async (id: string) => {
    await listService.delete(id);
    setLists((prev) => prev.filter((l) => l.taskListId !== id));
    setDeleteConfirmId(null);
  };

  return (
    <LayoutWrapper>
      <h2 className="text-2xl font-semibold mb-4">Ваши списки</h2>

      <div className="mb-6 flex gap-2">
        <input
          value={newList}
          onChange={(e) => setNewList(e.target.value)}
          placeholder="Новый список"
          className="border p-2 rounded w-full"
        />
        <button
          onClick={handleCreate}
          className="bg-yellow-300 hover:bg-yellow-400 px-4 py-2 rounded"
        >
          +
        </button>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {lists.map((list) => (
          <div
            key={list.taskListId}
            className="border p-4 rounded shadow hover:bg-yellow-50"
          >
            {editingListId === list.taskListId ? (
              <div className="flex gap-2 mb-2">
                <input
                  value={editedName}
                  onChange={(e) => setEditedName(e.target.value)}
                  className="border px-2 py-1 rounded w-full"
                />
                <button
                  onClick={() => handleUpdate(list.taskListId)}
                  className="text-green-600 hover:underline"
                >✔</button>
                <button
                  onClick={() => setEditingListId(null)}
                  className="text-gray-400 hover:underline"
                >✖</button>
              </div>
            ) : (
              <div
                className="cursor-pointer"
                onClick={() => navigate(`/tasks/${list.taskListId}`)}
              >
                <h3 className="font-bold text-lg flex justify-between items-center">
                  {list.name}
                  <span className="text-sm text-gray-400 ml-2 flex gap-2">
                    <button
                      onClick={(e) => {
                        e.stopPropagation();
                        setEditedName(list.name);
                        setEditingListId(list.taskListId);
                      }}
                    >✏️</button>
                    <button
                      onClick={(e) => {
                        e.stopPropagation();
                        setDeleteConfirmId(list.taskListId);
                      }}
                    >🗑️</button>
                  </span>
                </h3>
                <p className="text-xs text-gray-500">
                  Создан: {new Date(list.createdAt).toLocaleDateString()}
                </p>
              </div>
            )}

            {deleteConfirmId === list.taskListId && (
              <div className="mt-2 p-2 border bg-white rounded text-sm text-center">
                <p className="mb-2">Удалить список <strong>{list.name}</strong>?</p>
                <div className="flex justify-center gap-4">
                  <button
                    onClick={() => handleDelete(list.taskListId)}
                    className="text-red-600 hover:underline"
                  >Да</button>
                  <button
                    onClick={() => setDeleteConfirmId(null)}
                    className="text-gray-500 hover:underline"
                  >Отмена</button>
                </div>
              </div>
            )}
          </div>
        ))}
      </div>
    </LayoutWrapper>
  );
}