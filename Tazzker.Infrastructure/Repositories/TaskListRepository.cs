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
        private readonly IUserContext _userContext;


        public TaskListRepository(TazzkerDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }


        public async Task<IEnumerable<TaskListDto>> GetAllTaskListsAsync()
        {
            return await _context.TaskLists.Where(t => t.UserId == _userContext.UserId).Select(t => new TaskListDto
            {
                TaskListId = t.TaskListId,
                CreatedAt = t.CreatedAt,
                Name = t.Name
            }).ToListAsync();
        }

        public async Task<TaskListDto?> GetTaskListByIdAsync(Guid id)
        {
            var result = await _context.TaskLists.FirstOrDefaultAsync(t => t.TaskListId == id && t.UserId == _userContext.UserId);

            if (result == null) return null;

            return new TaskListDto
            {
                TaskListId = result.TaskListId,
                CreatedAt = result.CreatedAt,
                Name = result.Name
            };

        }

        public async Task<TaskListDto> CreateTaskListAsync(CreateTaskListDto dto)
        {
            var newTaskList = new TaskList
            {
                Name = dto.Name,
                UserId = _userContext.UserId,
            };
            
            _context.TaskLists.Add(newTaskList);
            await _context.SaveChangesAsync();

            return new TaskListDto
            {
                Name = newTaskList.Name,
                CreatedAt = newTaskList.CreatedAt,
                TaskListId = newTaskList.TaskListId
            };
        }
        public async Task<TaskListDto?> UpdateTaskListAsync(UpdateTaskListDto dto)
        {
            var taskList = await _context.TaskLists.FirstOrDefaultAsync(t => t.TaskListId == dto.TaskListId);
            if (taskList == null) return null;
            taskList.Name = dto.Name;
            await _context.SaveChangesAsync();
            return new TaskListDto
            {
                TaskListId = taskList.TaskListId,
                Name = taskList.Name,
                CreatedAt = taskList.CreatedAt
            };
        }
        public async Task<bool> DeleteTaskListAsync(Guid id)
        {
            var task = await _context.TaskLists.FirstOrDefaultAsync(t => t.TaskListId == id && t.UserId == _userContext.UserId);
            if (task == null) return false;

            _context.TaskLists.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
