export interface Task {
    taskId: string;
    listId: string;
    parentTaskId?: string;
    title?: string;
    description?: string;
    updatedAt: string;
    reminderAt?: string;
    dueTime?: string;
    isCompleted: boolean;
    isDeleted: boolean;
    order: number;
  }
  
  export interface CreateTaskDto {
    listId: string;
    parentTaskId?: string;
    title?: string;
    description?: string;
    updatedAt: string;
    reminderAt?: string;
    dueTime?: string;
    order: number;
  }
  
  export interface UpdateTaskDto {
    taskId: string;
    listId?: string;
    parentTaskId?: string;
    title?: string;
    description?: string;
    updatedAt?: string;
    reminderAt?: string;
    dueTime?: string;
    isCompleted?: boolean;
    isDeleted?: boolean;
    order?: number;
  }
  