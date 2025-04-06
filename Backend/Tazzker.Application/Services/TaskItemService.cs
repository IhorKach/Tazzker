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

        private TaskItemDto CreateDtoObject(TaskItem task)
        {
            return new TaskItemDto
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
                Order = await _taskItemRepository.GetMaxOrderInList(dto.ListId, _userContext.UserId)
            };
            await _taskItemRepository.CreateTaskItemAsync(newTask);

            return CreateDtoObject(newTask);
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllTaskItemsAsync()
        {
            var tasks = await _taskItemRepository.GetAllTaskItemsAsync(_userContext.UserId);

            return tasks.Select(t => CreateDtoObject(t)).ToList();
        }

        public async Task<TaskItemDto?> GetTaskItemByIdAsync(Guid taskId)
        {
            var task = await _taskItemRepository.GetTaskItemByIdAsync(taskId, _userContext.UserId);

            return task == null ? null : CreateDtoObject(task);
        }

        public async Task<bool> DeleteTaskItemAsync(Guid id)
        {
            return await _taskItemRepository.DeleteTaskItemAsync(id, _userContext.UserId);
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

            return query.Select(t => CreateDtoObject(t)).ToList();
        }
        public async Task<TaskItemDto?> UpdateTaskItemAsync(UpdateTaskItemDto dto)
        {
            var task = await _taskItemRepository.GetTaskItemByIdAsync(dto.TaskId, _userContext.UserId);
            
            if (task == null) return null;

            if (dto.ListId.HasValue)
                task.ListId = dto.ListId.Value;
            if (dto.ParentTaskId.HasValue)
                task.ParentTaskId = dto.ParentTaskId.Value;
            if (dto.Title != null)
                task.Title = dto.Title;
            if (dto.Description != null)
                task.Description = dto.Description;
            if (dto.DueTime.HasValue)
                task.DueTime = dto.DueTime.Value;
            if (dto.ReminderAt.HasValue)
                task.ReminderAt = dto.ReminderAt.Value;
            if (dto.IsCompleted.HasValue)
                task.IsCompleted = dto.IsCompleted.Value;
            if (dto.IsDeleted.HasValue)
                task.IsDeleted = dto.IsDeleted.Value;
            if (dto.Order.HasValue)
                task.Order = dto.Order.Value;
            if (dto.UpdatedAt.HasValue)
                task.UpdatedAt = dto.UpdatedAt.Value;
            else
                task.UpdatedAt = DateTime.UtcNow;

            await _taskItemRepository.UpdateTaskItemAsync(task);
            return CreateDtoObject(task);
        }
    }
}