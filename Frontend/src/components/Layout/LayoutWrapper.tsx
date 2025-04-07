import { ReactNode } from "react";
import { Header } from "./Header";

export function LayoutWrapper({ children }: { children: ReactNode }) {
  return (
    <div className="min-h-screen bg-gray-50">
      <Header />
      <main className="p-6 max-w-6xl mx-auto">{children}</main>
    </div>
  );
}