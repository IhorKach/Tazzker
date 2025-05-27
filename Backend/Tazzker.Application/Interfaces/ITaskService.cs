using Tazzker.Application.DTOs;

namespace Tazzker.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetTasksAsync();
        Task<bool> SyncTasksAsync(IEnumerable<TaskDTO> tasks);
        Task<bool> DeleteTasksAsync(IEnumerable<Guid> taskIds);
    }
}
