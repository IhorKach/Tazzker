export interface TaskList {
    taskListId: string;
    name: string;
    createdAt: string;
  }
  
  export interface CreateTaskListDto {
    name: string;
  }
  
  export interface UpdateTaskListDto {
    taskListId: string;
    name: string;
  }
  