namespace Tazzker.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Domain.Task>> GetAllTasksAsync(Guid userId);
        Task<Domain.Task?> GetTaskByIdAsync(Guid taskId, Guid userId);
        System.Threading.Tasks.Task CreateTaskAsync(Domain.Task newTask);
        System.Threading.Tasks.Task UpdateTaskAsync(Domain.Task updateTask);
        System.Threading.Tasks.Task DeleteTaskAsync(Domain.Task taskToDelete);
    }
}
