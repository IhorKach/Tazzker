using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.DTOs;

namespace Tazzker.Application.Interfaces
{
    public interface ITaskListRepository
    {
        Task<IEnumerable<TaskListDto>> GetAllTaskListsAsync();
        Task<TaskListDto?> GetTaskListByIdAsync(Guid id);
        Task<TaskListDto?> UpdateTaskListAsync(UpdateTaskListDto dto);
        Task<bool> DeleteTaskListAsync(Guid id);
        Task<TaskListDto> CreateTaskListAsync(CreateTaskListDto dto);
    }
}
