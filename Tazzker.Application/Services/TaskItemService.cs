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
            await _taskItemRepository.AddAsync(newTask);


            //return await _taskItemRepository.CreateTaskItemAsync(dto);

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
            return await _taskItemRepository.GetAllTaskItemsAsync();
        }

        public async Task<TaskItemDto?> GetTaskItemByIdAsync(Guid id)
        {
            return await _taskItemRepository.GetTaskItemByIdAsync(id);
        }

        public async Task<IEnumerable<TaskItemDto>> GetFilteredTaskItemsAsync(TaskItemFilterDto dto)
        {
            return await _taskItemRepository.GetFilteredTaskItemsAsync(dto);
        }


        public async Task<TaskItemDto?> UpdateTaskItemAsync(UpdateTaskItemDto dto)
        {
            return await _taskItemRepository.UpdateTaskItemAsync(dto);
        }

        public async Task<bool> DeleteTaskItemAsync(Guid id)
        {
            return await _taskItemRepository.DeleteTaskItemAsync(id);
        }
        public async Task<bool> SoftDeleteTaskItemAsync(Guid id)
        {
            return await _taskItemRepository.SoftDeleteTaskItemAsync(id);
        }
    }
}
