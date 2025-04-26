using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Domain;

namespace Tazzker.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid userId);
        System.Threading.Tasks.Task AddAsync(User user);
        System.Threading.Tasks.Task UpdateAsync(User user);
    }
}
