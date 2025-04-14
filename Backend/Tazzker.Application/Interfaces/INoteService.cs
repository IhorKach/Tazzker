using Tazzker.Application.DTOs;

namespace Tazzker.Application.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDTO>> GetNotesAsync();
        Task<bool> SyncNotesAsync(IEnumerable<NoteDTO> notes);
        Task<bool> DeleteNotesAsync(IEnumerable<Guid> noteIds);
    }
}
