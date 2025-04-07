interface CalendarDayCardProps {
    date: Date;
    isToday?: boolean;
    hasTasks?: boolean;
    onClick?: () => void;
  }
  
  export function CalendarDayCard({ date, isToday, hasTasks, onClick }: CalendarDayCardProps) {
    return (
      <div
        className={`p-4 border rounded text-center cursor-pointer transition hover:bg-yellow-100
          ${isToday ? "border-yellow-500 bg-yellow-50" : ""}
        `}
        onClick={onClick}
      >
        <div className="font-semibold">{date.getDate()}</div>
        {hasTasks && <div className="text-green-600 text-sm mt-1">• задачи</div>}
      </div>
    );
  }