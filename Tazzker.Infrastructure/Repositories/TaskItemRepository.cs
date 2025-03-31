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

namespace Tazzker.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly TazzkerDbContext _context;
        private readonly IUserContext _userContext;

        public TaskItemRepository(TazzkerDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllTaskItemsAsync()
        {
            return await _context.TaskItems.Where(t=>t.UserId == _userContext.UserId).Select(t => new TaskItemDto
            {
                ListId = t.ListId,
                TaskId = t.TaskId,
                Title = t.Title,
                Description = t.Description,
                DueTime = t.DueTime,
                ReminderAt = t.ReminderAt,
                UpdatedAt = t.UpdatedAt,
                IsDeleted = t.IsDeleted,
                IsCompleted = t.IsCompleted,
                ParentTaskId = t.ParentTaskId
            }).ToListAsync();
        }

        public async Task<TaskItemDto?> GetTaskItemByIdAsync(Guid id)
        {
            var result = await _context.TaskItems.AsNoTracking().FirstOrDefaultAsync(t => t.TaskId == id);

            return result == null ? null : new TaskItemDto
            {
                TaskId = result.TaskId,
                Title = result.Title,
                Description = result.Description,
                DueTime = result.DueTime,
                ReminderAt = result.ReminderAt,
                UpdatedAt = result.UpdatedAt,
                IsDeleted = result.IsDeleted,
                IsCompleted = result.IsCompleted,
                ListId = result.ListId,
                ParentTaskId = result.ParentTaskId,
            };
        }

        public async Task<TaskItemDto> CreateTaskItemAsync(CreateTaskItemDto dto)
        {
            var newTask = new TaskItem
            {
                Title = dto.Title,
                ListId = dto.ListId,
                UserId = _userContext.UserId,
                Description = dto.Description,
                DueTime = dto.DueTime,
                ReminderAt = dto.ReminderAt,
                UpdatedAt = dto.UpdatedAt,
                ParentTaskId = dto.ParentTaskId
            };

            await _context.TaskItems.AddAsync(newTask);
            await _context.SaveChangesAsync();

            return new TaskItemDto
            {
                ListId = newTask.ListId,
                TaskId = newTask.TaskId,
                Title = newTask.Title,
                Description = newTask.Description,
                DueTime = newTask.DueTime,
                ReminderAt = newTask.ReminderAt,
                UpdatedAt = newTask.UpdatedAt,
                IsCompleted = newTask.IsCompleted,
                IsDeleted = newTask.IsDeleted,
                ParentTaskId = newTask.ParentTaskId
                
                
            };
        }
        public async Task<TaskItemDto?> UpdateTaskItemAsync(UpdateTaskItemDto dto)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(x => x.TaskId == dto.TaskId && x.UserId == _userContext.UserId);

            if (task == null) return null;

            task.ListId = dto.ListId;
            task.ParentTaskId = dto.ParentTaskId;
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.ReminderAt = dto.ReminderAt;
            task.DueTime = dto.DueTime;
            task.UpdatedAt = DateTime.UtcNow;
            task.IsCompleted = dto.IsCompleted;
            task.IsDeleted = dto.IsDeleted;

            await _context.SaveChangesAsync();

            return new TaskItemDto
            {
                ListId = task.ListId,
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueTime = task.DueTime,
                ReminderAt = task.ReminderAt,
                UpdatedAt = task.UpdatedAt,
                IsCompleted = task.IsCompleted,
                IsDeleted = task.IsDeleted,
                ParentTaskId = task.ParentTaskId
            };
        }

        public async Task<bool> DeleteTaskItemAsync(Guid id)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.TaskId == id && t.UserId == _userContext.UserId);
            if (task == null) return false;

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
