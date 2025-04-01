using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Domain;
using Tazzker.Application.DTOs;

namespace Tazzker.Application.Interfaces
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItemDto>> GetAllTaskItemsAsync();
        Task<TaskItemDto?> GetTaskItemByIdAsync(Guid id);
        Task<TaskItemDto> CreateTaskItemAsync(CreateTaskItemDto dto);

        Task<IEnumerable<TaskItemDto>> GetFilteredTaskItemsAsync(TaskItemFilterDto dto);
        Task<TaskItemDto?> UpdateTaskItemAsync(UpdateTaskItemDto dto);
        Task<bool> DeleteTaskItemAsync(Guid id);
        Task<bool> SoftDeleteTaskItemAsync(Guid id);
    }
}
