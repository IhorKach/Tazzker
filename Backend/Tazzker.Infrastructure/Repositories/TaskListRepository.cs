using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Infrastructure.Contexts;
using Tazzker.Application.Interfaces;
using Tazzker.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Tazzker.Domain;
using System.Reflection.Metadata.Ecma335;

namespace Tazzker.Infrastructure.Repositories
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly TazzkerDbContext _context;

        public TaskListRepository(TazzkerDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskList>> GetAllTaskListsAsync(Guid userId)
        {
            return await _context.TaskLists.Where(t => t.UserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskList?> GetTaskListByIdAsync(Guid taskListId, Guid userId)
        {
            var taskList = await _context.TaskLists.FirstOrDefaultAsync(t => t.TaskListId == taskListId && t.UserId == userId);
            return taskList == null ? null : taskList;
        }

        public async Task<TaskList> UpdateTaskListAsync(TaskList updatedTaskList)
        {
            _context.TaskLists.Update(updatedTaskList);
            await _context.SaveChangesAsync();
            return updatedTaskList;
        }

        public async Task<bool> DeleteTaskListAsync(Guid taskListId, Guid userId)
        {
            var task = await _context.TaskLists.FirstOrDefaultAsync(t => t.TaskListId == taskListId && t.UserId == userId);
            if (task == null) return false;

            _context.TaskLists.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaskList> CreateTaskListAsync(TaskList newTaskList)
        {
            _context.TaskLists.Add(newTaskList);
            await _context.SaveChangesAsync();
            return newTaskList;

        }
    }
}
