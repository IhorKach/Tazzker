using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;
using Tazzker.Application.DTOs;
namespace Tazzker.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        public TaskItemService(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllTaskItemsAsync()
        {
            return await _taskItemRepository.GetAllTaskItemsAsync();
        }

        public async Task<TaskItemDto?> GetTaskItemByIdAsync(Guid id)
        {
            return await _taskItemRepository.GetTaskItemByIdAsync(id);
        }

        public async Task<TaskItemDto> CreateTaskItemAsync(CreateTaskItemDto dto)
        {
            return await _taskItemRepository.CreateTaskItemAsync(dto);
        }

        public async Task<TaskItemDto?> UpdateTaskItemAsync(UpdateTaskItemDto dto)
        {
            return await _taskItemRepository.UpdateTaskItemAsync(dto);
        }

        public async Task<bool> DeleteTaskItemAsync(Guid id)
        {
            return await _taskItemRepository.DeleteTaskItemAsync(id);
        }
    }
}
