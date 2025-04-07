// --- src/pages/CalendarPage.tsx ---
import { LayoutWrapper } from "@/components/layout/LayoutWrapper";
import { CalendarMonthView } from "@/components/calendar/CalendarMonthView";

export function CalendarPage() {
  return (
    <LayoutWrapper>
      <h2 className="text-2xl font-semibold mb-6">Календарь</h2>
      <CalendarMonthView />
    </LayoutWrapper>
  );
}