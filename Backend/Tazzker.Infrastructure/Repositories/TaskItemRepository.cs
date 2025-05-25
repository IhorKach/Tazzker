using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Infrastructure.Contexts;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;
using Tazzker.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Net.Quic;

namespace Tazzker.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly TazzkerDbContext _context;

        public TaskItemRepository(TazzkerDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task CreateTaskItemAsync(TaskItem newTaskItem)
        {
            await _context.AddAsync(newTaskItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync(Guid userId)
        {
            return await _context.TaskItems.Where(t => t.UserId == userId /*&& !t.IsDeleted*/).AsNoTracking().ToListAsync();
        }

        public async Task<TaskItem?> GetTaskItemByIdAsync(Guid taskId, Guid userId)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.UserId == userId && t.TaskId == taskId /*&& !t.IsDeleted*/);
            return task == null ? null : task;
        }

        public async Task<TaskItem> UpdateTaskItemAsync(TaskItem taskItem)
        {
            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<bool> DeleteTaskItemAsync(Guid taskId, Guid userId)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.TaskId == taskId && t.UserId == userId);
            if (task == null) return false;

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<float> GetMaxOrderInList(Guid listId, Guid userId)
        {
            var orders = await _context.TaskItems
                .Where(t => t.ListId == listId && t.UserId == userId)
                .Select(t => t.Order)
                .ToListAsync();

            return (orders.Count == 0 ? 0 : orders.Max()) + 1;
        }
    }
}