import { Menu } from "lucide-react"
import { Button } from "@/components/ui/button"

export function Header({ onMenuClick }: { onMenuClick: () => void }) {
  return (
    <header className="flex items-center justify-between p-4 bg-white shadow-sm border-b">
      <Button variant="ghost" size="icon" onClick={onMenuClick}>
        <Menu className="w-5 h-5" />
      </Button>
      <h1 className="text-lg font-bold">Tazzker</h1>
      <div></div> {/* Спейс для выравнивания */}
    </header>
  )
}
