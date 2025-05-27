using Microsoft.EntityFrameworkCore;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;
using Tazzker.Infrastructure.Contexts;

namespace Tazzker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TazzkerDbContext _context;

        public UserRepository(TazzkerDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.UserId == userId);
        }

        public async System.Threading.Tasks.Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
