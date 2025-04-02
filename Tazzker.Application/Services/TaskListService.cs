using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;

namespace Tazzker.Application.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IUserContext _userContext;
        public TaskListService(ITaskListRepository taskListRepository, IUserContext userContext)
        {
            _taskListRepository = taskListRepository;
            _userContext = userContext;
        }

        public async Task<IEnumerable<TaskListDto>> GetAllTaskListsAsync()
        {

            var taskLists = await _taskListRepository.GetAllTaskListsAsync(_userContext.UserId);

           return taskLists.Select(t => new TaskListDto
            {
                TaskListId = t.TaskListId,
                CreatedAt = t.CreatedAt,
                Name = t.Name
            }).ToList();
        }

        public async Task<TaskListDto?> GetTaskListByIdAsync(Guid id)
        {
            var taskList = await _taskListRepository.GetTaskListByIdAsync(id, _userContext.UserId);

            return taskList == null ? null : new TaskListDto
            {
                TaskListId = taskList.TaskListId,
                CreatedAt = taskList.CreatedAt,
                Name = taskList.Name
            };
        }
        public async Task<TaskListDto> CreateTaskListAsync(CreateTaskListDto dto)
        {

            var newTaskList = new TaskList
            {
                Name = dto.Name,
                UserId = _userContext.UserId,
            };

            await _taskListRepository.CreateTaskListAsync(newTaskList);

            return new TaskListDto
            {
                TaskListId = newTaskList.TaskListId,
                CreatedAt = newTaskList.CreatedAt,
                Name = newTaskList.Name
            };
        }

        public async Task<TaskListDto?> UpdateTaskListAsync(UpdateTaskListDto dto)
        {
            var taskList = await _taskListRepository.GetTaskListByIdAsync(dto.TaskListId, _userContext.UserId);

            if (taskList == null) return null;
            taskList.Name = dto.Name;
            
            await _taskListRepository.UpdateTaskListAsync(taskList);
            return new TaskListDto
            {
                TaskListId = taskList.TaskListId,
                Name = taskList.Name,
                CreatedAt = taskList.CreatedAt
            };
        }
        public async Task<bool> DeleteTaskListAsync(Guid id)
        {
            return await _taskListRepository.DeleteTaskListAsync(id, _userContext.UserId);
        }

    }
}
