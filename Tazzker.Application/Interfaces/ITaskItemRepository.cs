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
        Task<IEnumerable<TaskItemDto>> GetAllTaskItemsAsync();
        Task<TaskItemDto?> GetTaskItemByIdAsync(Guid id);
        Task<TaskItemDto> CreateTaskItemAsync(CreateTaskItemDto dto);
        Task<TaskItemDto?> UpdateTaskItemAsync(UpdateTaskItemDto dto);
        Task<bool> DeleteTaskItemAsync(Guid id);
    }
}
