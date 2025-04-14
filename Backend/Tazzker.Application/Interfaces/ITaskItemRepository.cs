using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Domain;
using Tazzker.Application.DTOs;
namespace Tazzker.Application.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync(Guid userId);
        Task<TaskItem?> GetTaskItemByIdAsync (Guid taskId, Guid userId);
        System.Threading.Tasks.Task CreateTaskItemAsync(TaskItem newTaskItem);
        Task<TaskItem> UpdateTaskItemAsync(TaskItem taskItem);
        Task<bool> DeleteTaskItemAsync(Guid taskId, Guid userId);

        Task<float> GetMaxOrderInList(Guid listId, Guid userId);



    }
}
