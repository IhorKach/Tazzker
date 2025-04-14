using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;

namespace Tazzker.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserContext _userContext;
        public TaskService(ITaskRepository taskRepository, IUserContext userContext)
        {
            _taskRepository = taskRepository;
            _userContext = userContext;
        }

        public TaskDTO CreateTaskDTO(Domain.Task task)
        {
            return new TaskDTO
            {
                TaskId = task.TaskId,
                Description = task.Description,
                DueTime = task.DueTime,
                IsCompleted = task.IsCompleted,
                IsDeleted = task.IsDeleted,
                Order = task.Order,
                ReminderAt = task.ReminderAt,
                SublistId = task.SublistId,
                Title = task.Title,
                UpdatedAt = task.UpdatedAt
            };
        }

        public Domain.Task CreateTask(TaskDTO taskDTO, Guid? foundTaskId = null)
        {
            if (foundTaskId.HasValue)
                taskDTO.TaskId = foundTaskId.Value;

            return new Domain.Task
            {
                UserId = _userContext.UserId,
                TaskId = taskDTO.TaskId,
                SublistId = taskDTO.SublistId,
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                Order = taskDTO.Order,
                UpdatedAt = taskDTO.UpdatedAt,
                DueTime = taskDTO.DueTime,
                ReminderAt = taskDTO.ReminderAt,
                IsCompleted = taskDTO.IsCompleted,
                IsDeleted = taskDTO.IsDeleted
            };
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync(_userContext.UserId);
            return tasks.Select(t => CreateTaskDTO(t)).ToList();
        }

        public async Task<bool> SyncTasksAsync(IEnumerable<TaskDTO> tasks)
        {
            try
            {
                foreach (var task in tasks)
                {
                    var t = await _taskRepository.GetTaskByIdAsync(task.TaskId, _userContext.UserId);
                    
                    if (t != null)
                        await _taskRepository.UpdateTaskAsync(CreateTask(task, t.TaskId));
                    else
                        await _taskRepository.CreateTaskAsync(CreateTask(task));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTasksAsync(IEnumerable<Guid> taskIds)
        {
            try
            {
                foreach (var taskId in taskIds)
                {
                    var t = await _taskRepository.GetTaskByIdAsync(taskId, _userContext.UserId);
                    if (t != null)
                        await _taskRepository.DeleteTaskAsync(t);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
