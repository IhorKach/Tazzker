using Tazzker.Infrastructure.Contexts;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;
using Microsoft.EntityFrameworkCore;
namespace Tazzker.Infrastructure.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly TazzkerDbContext _context;
        public ListRepository(TazzkerDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<List>> GetAllListsAsync(Guid userId)
        {
            return await _context.Lists.Where(l => l.UserId == userId).AsNoTracking().ToListAsync();
        }
        public async Task<List?> GetListByIdAsync(Guid listId, Guid userId)
        {
            return await _context.Lists.FirstOrDefaultAsync(l => l.ListId == listId && l.UserId == userId);
        }
        public async System.Threading.Tasks.Task CreateListAsync(List newList)
        {
            await _context.Lists.AddAsync(newList);
            await _context.SaveChangesAsync();
            return;
        }
        public async System.Threading.Tasks.Task DeleteListAsync(List listToDelete)
        {
            _context.Lists.Remove(listToDelete);
            await _context.SaveChangesAsync();
            return;
        }
        public async System.Threading.Tasks.Task UpdateListAsync(List updateList)
        {
            _context.Lists.Update(updateList);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
