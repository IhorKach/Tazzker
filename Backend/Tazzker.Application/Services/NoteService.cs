using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;

namespace Tazzker.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserContext _userContext;
        public NoteService(INoteRepository noteRepository, IUserContext userContext)
        {
            _noteRepository = noteRepository;
            _userContext = userContext;
        }
        public NoteDTO CreateNoteDTO(Note note)
        {
            return new NoteDTO
            {
                NoteId = note.NoteId,
                Title = note.Title,
                Body = note.Body,
                CreatedAt = note.CreatedAt,
                IsDeleted = note.IsDeleted,
                UpdatedAt = note.UpdatedAt
            };
        }
        public Note CreateNote(NoteDTO noteDTO, Guid? foundNoteId = null)
        {
            if (foundNoteId.HasValue)
                noteDTO.NoteId = foundNoteId.Value;

            return new Note
            {
                UserId = _userContext.UserId,
                NoteId = noteDTO.NoteId,
                Title = noteDTO.Title,
                Body = noteDTO.Body,
                CreatedAt = noteDTO.CreatedAt,
                IsDeleted = noteDTO.IsDeleted,
                UpdatedAt = noteDTO.UpdatedAt
            };
        }

        public async Task<IEnumerable<NoteDTO>> GetNotesAsync()
        {
            var notes = await _noteRepository.GetAllNotesAsync(_userContext.UserId);
            return notes.Select(n => CreateNoteDTO(n)).ToList();
        }

        public async Task<bool> SyncNotesAsync(IEnumerable<NoteDTO> notes)
        {
            try
            {
                foreach (var note in notes)
                {
                    var n = await _noteRepository.GetNoteByIdAsync(note.NoteId, _userContext.UserId);

                    if (n != null)
                        await _noteRepository.UpdateNoteAsync(CreateNote(note, n.NoteId));
                    else
                        await _noteRepository.CreateNoteAsync(CreateNote(note));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteNotesAsync(IEnumerable<Guid> noteIds)
        {
            try
            {
                foreach (var noteId in noteIds)
                {
                    var n = await _noteRepository.GetNoteByIdAsync(noteId, _userContext.UserId);
                    if (n != null)
                        await _noteRepository.DeleteNoteAsync(n);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
