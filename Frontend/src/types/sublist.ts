export interface SublistDto {
  sublistId: string
  listId: string
  title: string
  order: number
  isDeleted: boolean
  updatedAt: string
}
export interface Sublist extends SublistDto {
  isSynced?: boolean
}
