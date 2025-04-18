import { useNavigate } from 'react-router-dom';

type NoteCardProps = {
  id: string;
  title: string;
  preview: string;
  onDelete?: () => void;
};

export default function NoteCard({ id, title, preview, onDelete }: NoteCardProps) {
  const navigate = useNavigate();

  return (
    <div className="relative border border-gray-300 rounded-xl p-4 mb-4 shadow-sm hover:shadow-md cursor-pointer transition group">
      <h2
        className="text-lg font-semibold mb-2"
        onClick={() => navigate(`/notes/${id}`)}
      >
        {title}
      </h2>
      <p className="text-sm text-gray-500">{preview}</p>

      {onDelete && (
        <button
          onClick={(e) => {
            e.stopPropagation(); // чтобы не сработал переход
            onDelete();
          }}
          className="absolute top-2 right-2 text-red-500 text-sm opacity-0 group-hover:opacity-100 transition"
        >
          🗑
        </button>
      )}
    </div>
  );
}
