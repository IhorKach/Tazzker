using Tazzker.Infrastructure.Contexts;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Tazzker.Infrastructure.Repositories
{
    public class SublistRepository :ISublistRepository
    {
        private readonly TazzkerDbContext _context;
        public SublistRepository(TazzkerDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Sublist>> GetAllSublistsAsync(Guid userId)
        {
            return await _context.Sublists.Where(s => s.UserId == userId).AsNoTracking().ToListAsync();
        }
        public async Task<Sublist?> GetSublistByIdAsync(Guid sublistId, Guid userId)
        {
            return await _context.Sublists.AsNoTracking().FirstOrDefaultAsync(s => s.SublistId == sublistId && s.UserId == userId);
        }

        public async System.Threading.Tasks.Task CreateSublistAsync(Sublist newSublist)
        {
            await _context.Sublists.AddAsync(newSublist);
            await _context.SaveChangesAsync();
            return;
        }

        public async System.Threading.Tasks.Task DeleteSublistAsync(Sublist sublistToDelete)
        {
            _context.Sublists.Remove(sublistToDelete);
            await _context.SaveChangesAsync();
            return;
        }

        public async System.Threading.Tasks.Task UpdateSublistAsync(Sublist updateSublist)
        {
            _context.Sublists.Update(updateSublist);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
