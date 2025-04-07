import { useMemo } from "react";
import { useNavigate } from "react-router-dom";
import { CalendarDayCard } from "./CalendarDayCard";

export function CalendarMonthView() {
  const now = new Date();
  const year = now.getFullYear();
  const month = now.getMonth();
  const navigate = useNavigate();

  const days = useMemo(() => {
    const total = new Date(year, month + 1, 0).getDate();
    return Array.from({ length: total }, (_, i) => new Date(year, month, i + 1));
  }, [year, month]);

  const formatDate = (date: Date) => date.toISOString().split("T")[0];

  const handleDayClick = async (date: Date) => {
    const response = await fetch(`/api/tasklist/by-date?date=${formatDate(date)}`, {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    });

    if (response.ok) {
      const list = await response.json();
      navigate(`/tasks/${list.taskListId}`);
    } else {
      const create = await fetch("/api/tasklist", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        body: JSON.stringify({ date: formatDate(date) }),
      });
      const list = await create.json();
      navigate(`/tasks/${list.taskListId}`);
    }
  };

  return (
    <div className="grid grid-cols-7 gap-2">
      {days.map((day) => (
        <CalendarDayCard
          key={day.toISOString()}
          date={day}
          isToday={day.toDateString() === now.toDateString()}
          hasTasks={false}
          onClick={() => handleDayClick(day)}
        />
      ))}
    </div>
  );
}