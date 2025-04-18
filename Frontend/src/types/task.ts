export interface TaskDto {
  taskId: string;
  sublistId: string;
  order: number;
  title: string;
  description: string;
  isCompleted: boolean;
  isDeleted: boolean;
  updatedAt: string;
  reminderAt?: string;
  dueTime?: string;
}
export interface Task extends TaskDto {
  isSynced?: boolean;
}