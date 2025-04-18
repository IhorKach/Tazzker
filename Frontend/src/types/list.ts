export interface ListDto {
  listId: string;
  title: string;
  updatedAt: string;
  createdAt: string;
  assignedDay?: string;
  isDeleted: boolean;
}
export interface List extends ListDto {
  isSynced?: boolean;
}
