using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;

namespace Tazzker.Application.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _taskListRepository;

        public TaskListService(ITaskListRepository taskListRepository)
        {
            _taskListRepository = taskListRepository;
        }

        public async Task<IEnumerable<TaskListDto>> GetAllTaskListsAsync()
        {
            return await _taskListRepository.GetAllTaskListsAsync();
        }

        public async Task<TaskListDto?> GetTaskListByIdAsync(Guid id)
        {
             return await _taskListRepository.GetTaskListByIdAsync(id);
        }
        public async Task<TaskListDto> CreateTaskListAsync(CreateTaskListDto dto)
        {
            return await _taskListRepository.CreateTaskListAsync(dto);
        }

        public async Task<TaskListDto?> UpdateTaskListAsync(UpdateTaskListDto dto)
        {
            return await _taskListRepository.UpdateTaskListAsync(dto);
        }
        public async Task<bool> DeleteTaskListAsync(Guid id)
        {
            return await _taskListRepository.DeleteTaskListAsync(id);
        }

    }
}
