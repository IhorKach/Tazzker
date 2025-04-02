using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;
using Tazzker.Application.DTOs;
using System.Net.Http.Headers;
namespace Tazzker.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IUserContext _userContext;
        public TaskItemService(ITaskItemRepository taskItemRepository, IUserContext userContext)
        {
            _taskItemRepository = taskItemRepository;
            _userContext = userContext;
        }

        public async Task<TaskItemDto> CreateTaskItemAsync(CreateTaskItemDto dto)
        {

            var newTask = new TaskItem
            {
                Title = dto.Title,
                ListId = dto.ListId,
                UserId = _userContext.UserId,
                Description = dto.Description,
                DueTime = dto.DueTime,
                ReminderAt = dto.ReminderAt,
                UpdatedAt = dto.UpdatedAt,
                ParentTaskId = dto.ParentTaskId,
                Order = dto.Order
            };
            await _taskItemRepository.CreateTaskItemAsync(newTask);

            return new TaskItemDto
            {
                ListId = newTask.ListId,
                TaskId = newTask.TaskId,
                Title = newTask.Title,
                Description = newTask.Description,
                DueTime = newTask.DueTime,
                ReminderAt = newTask.ReminderAt,
                UpdatedAt = newTask.UpdatedAt,
                IsCompleted = newTask.IsCompleted,
                IsDeleted = newTask.IsDeleted,
                ParentTaskId = newTask.ParentTaskId,
                Order = newTask.Order
            };
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllTaskItemsAsync()
        {
            var tasks = await _taskItemRepository.GetAllTaskItemsAsync(_userContext.UserId);

            return tasks.Select(t => new TaskItemDto
            {
                ListId = t.ListId,
                TaskId = t.TaskId,
                Title = t.Title,
                Description = t.Description,
                DueTime = t.DueTime,
                ReminderAt = t.ReminderAt,
                UpdatedAt = t.UpdatedAt,
                IsDeleted = t.IsDeleted,
                IsCompleted = t.IsCompleted,
                ParentTaskId = t.ParentTaskId,
                Order = t.Order
            }).ToList();
        }

        public async Task<TaskItemDto?> GetTaskItemByIdAsync(Guid taskId)
        {

            var task = await _taskItemRepository.GetTaskItemByIdAsync(_userContext.UserId, taskId);


            return task == null ? null : new TaskItemDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueTime = task.DueTime,
                ReminderAt = task.ReminderAt,
                UpdatedAt = task.UpdatedAt,
                IsDeleted = task.IsDeleted,
                IsCompleted = task.IsCompleted,
                ListId = task.ListId,
                ParentTaskId = task.ParentTaskId,
                Order = task.Order
            };

        }

        public async Task<bool> DeleteTaskItemAsync(Guid id)
        {
            return await _taskItemRepository.DeleteTaskItemAsync(id);
        }
        public async Task<bool> SoftDeleteTaskItemAsync(Guid id)
        {
            return await _taskItemRepository.SoftDeleteTaskItemAsync(id);
        }
        public async Task<TaskItemDto?> UpdateTaskItemAsync(UpdateTaskItemDto dto)
        {

            var task = await _taskItemRepository.GetTaskItemByIdAsync(dto.TaskId, _userContext.UserId);
            if (task == null) return null;

            
            task.ListId = dto.ListId;
            task.ParentTaskId = dto.ParentTaskId;
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.ReminderAt = dto.ReminderAt;
            task.DueTime = dto.DueTime;
            task.UpdatedAt = DateTime.UtcNow;
            task.IsCompleted = dto.IsCompleted;
            task.IsDeleted = dto.IsDeleted;
            task.Order = dto.Order;

            task = await _taskItemRepository.UpdateTaskItemAsync(task);


            return new TaskItemDto
            {
                ListId = task.ListId,
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueTime = task.DueTime,
                ReminderAt = task.ReminderAt,
                UpdatedAt = task.UpdatedAt,
                IsCompleted = task.IsCompleted,
                IsDeleted = task.IsDeleted,
                ParentTaskId = task.ParentTaskId,
                Order = task.Order
            };
        }
        public async Task<IEnumerable<TaskItemDto>> GetFilteredTaskItemsAsync(TaskItemFilterDto dto)
        {

            var query = await _taskItemRepository.GetAllTaskItemsAsync(_userContext.UserId);

            if (dto.ListId.HasValue)
                query = query.Where(t => t.ListId == dto.ListId.Value);
            if (dto.IsCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == dto.IsCompleted.Value);
            if (dto.DueTime.HasValue)
                query = query.Where(t => t.DueTime == dto.DueTime.Value);
            if (dto.CreatedAt.HasValue)
                query = query.Where(t => t.CreatedAt == dto.CreatedAt.Value);

            if (!string.IsNullOrWhiteSpace(dto.SortBy))

                query = dto.SortBy
                    switch
                {
                    "CreatedAt" => dto.Descending ?
                    query.OrderByDescending(t => t.CreatedAt)
                    : query.OrderBy(t => t.CreatedAt),
                    "Order" or _ => dto.Descending ?
                   query.OrderByDescending(t => t.Order)
                   : query.OrderBy(t => t.Order)
                };

            return query.Select(t => new TaskItemDto
            {
                ListId = t.ListId,
                Description = t.Description,
                DueTime = t.DueTime,
                IsCompleted = t.IsCompleted,
                IsDeleted = t.IsDeleted,
                ParentTaskId = t.ParentTaskId,
                ReminderAt = t.ReminderAt,
                TaskId = t.TaskId,
                Title = t.Title,
                UpdatedAt = t.UpdatedAt,
                Order = t.Order
            }).ToList();
        }
    }
}
