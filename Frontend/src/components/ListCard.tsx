import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

type ListCardProps = {
  id: string;
  title: string;
  taskCount: number;
  onRename?: (newTitle: string) => void;
  onDelete?: () => void;
};

export default function ListCard({ id, title, taskCount, onRename, onDelete }: ListCardProps) {
  const [isEditing, setIsEditing] = useState(false);
  const [draft, setDraft] = useState(title);
  const navigate = useNavigate();

  const handleSave = () => {
    onRename?.(draft);
    setIsEditing(false);
  };

  return (
    <div className="relative border border-gray-300 rounded-xl p-4 mb-4 shadow-sm hover:shadow-md transition">
      {isEditing ? (
        <input
          value={draft}
          onChange={(e) => setDraft(e.target.value)}
          onBlur={handleSave}
          onKeyDown={(e) => e.key === 'Enter' && handleSave()}
          autoFocus
          className="w-full border-b focus:outline-none"
        />
      ) : (
        <div onClick={() => navigate(`/lists/${id}`)} className="cursor-pointer">
          <h2 className="text-lg font-semibold">{title}</h2>
          <p className="text-sm text-gray-500">{taskCount} задач</p>
        </div>
      )}

      {onDelete && (
        <button
          onClick={(e) => {
            e.stopPropagation();
            onDelete();
          }}
          className="absolute top-2 right-2 text-red-500 text-sm"
        >
          🗑
        </button>
      )}
    </div>
  );
}