export interface UpdateTaskItemDto {
    taskId: string
    listId?: string
    parentTaskId?: string
    title?: string
    description?: string
    updatedAt?: string // ISO date string
    reminderAt?: string // ISO
    dueTime?: string // ISO
    isCompleted?: boolean
    isDeleted?: boolean
    order?: number
  }