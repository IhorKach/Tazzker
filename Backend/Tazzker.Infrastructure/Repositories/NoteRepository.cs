using Tazzker.Infrastructure.Contexts;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;
using Microsoft.EntityFrameworkCore;
namespace Tazzker.Infrastructure.Repositories
{
    public class NoteRepository :INoteRepository
    {
        private readonly TazzkerDbContext _context;
        public NoteRepository(TazzkerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync(Guid userId)
        {
            return await _context.Notes.Where(n => n.UserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<Note?> GetNoteByIdAsync(Guid noteId, Guid userId)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.NoteId == noteId && n.UserId == userId);
        }

        public async System.Threading.Tasks.Task CreateNoteAsync(Note newNote)
        {
            await _context.Notes.AddAsync(newNote);
            await _context.SaveChangesAsync();
            return;
        }

        public async System.Threading.Tasks.Task DeleteNoteAsync(Note noteToDelete)
        {
            _context.Notes.Remove(noteToDelete);
            await _context.SaveChangesAsync();
            return;
        }


        public async System.Threading.Tasks.Task UpdateNoteAsync(Note updateNote)
        {
            _context.Notes.Update(updateNote);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
