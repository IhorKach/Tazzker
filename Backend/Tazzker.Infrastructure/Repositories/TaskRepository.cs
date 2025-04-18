using Tazzker.Infrastructure.Contexts;
using Tazzker.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tazzker.Infrastructure.Repositories
{
    public class TaskRepository  :ITaskRepository
    {
        private readonly TazzkerDbContext _context;
        public TaskRepository(TazzkerDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Domain.Task>> GetAllTasksAsync(Guid userId)
        {
            return await _context.Tasks.Where(t=>t.UserId == userId).AsNoTracking().ToListAsync();
        }
        public async Task<Domain.Task?> GetTaskByIdAsync(Guid taskId, Guid userId)
        {
            return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t=> t.TaskId == taskId && t.UserId == userId);
        }

        public async System.Threading.Tasks.Task CreateTaskAsync(Domain.Task newTask)
        {
            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
            return;
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(Domain.Task taskToDelete)
        {
            _context.Tasks.Remove(taskToDelete);
            await _context.SaveChangesAsync();
            return;
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(Domain.Task updateTask)
        {
            _context.Tasks.Update(updateTask);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
