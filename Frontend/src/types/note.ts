export interface NoteDto {
    noteId: string;
    title: string;
    body: string;
    updatedAt: string;
    createdAt: string;
    isDeleted: boolean;
  }
  export interface Note extends NoteDto {
    isSynced?: boolean;
  }
  