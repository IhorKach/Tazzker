using Tazzker.Domain;

namespace Tazzker.Application.Interfaces
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotesAsync(Guid userId);
        Task<Note?> GetNoteByIdAsync(Guid noteId, Guid userId);
        System.Threading.Tasks.Task CreateNoteAsync(Note newNote);
        System.Threading.Tasks.Task UpdateNoteAsync(Note updateNote);
        System.Threading.Tasks.Task DeleteNoteAsync(Note noteToDelete);
    }
}
