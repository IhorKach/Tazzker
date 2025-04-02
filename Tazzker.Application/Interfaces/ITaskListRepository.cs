using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.DTOs;
using Tazzker.Domain;

namespace Tazzker.Application.Interfaces
{
    public interface ITaskListRepository
    {
        Task<IEnumerable<TaskList>> GetAllTaskListsAsync(Guid userId);
        Task<TaskList?> GetTaskListByIdAsync(Guid taskListId, Guid userId);
        Task<TaskList> UpdateTaskListAsync(TaskList updatedTaskList);
        Task<bool> DeleteTaskListAsync(Guid taskListId, Guid userId);
        Task<TaskList> CreateTaskListAsync(TaskList newTaskList);
    }
}
