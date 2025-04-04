using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.DTOs;
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

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
