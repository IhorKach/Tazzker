using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;
using Tazzker.Application.DTOs;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Authorization;

namespace TazzkerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;
        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAllTasks()
        {
            return Ok(await _taskItemService.GetAllTaskItemsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await _taskItemService.GetTaskItemByIdAsync(id);

            return result != null ? Ok(result) : BadRequest("No item");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskItemDto dto)
        {
            var result = await _taskItemService.CreateTaskItemAsync(dto);

            return result == null ? BadRequest("During Creation task something went wrong") : Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(Guid id, [FromBody] UpdateTaskItemDto dto)
        {
            dto.TaskId = id;
            var result = await _taskItemService.UpdateTaskItemAsync(dto);
            return result == null ? BadRequest("No such Item!") : Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            var result = await _taskItemService.DeleteTaskItemAsync(id);
            return result == true ? NoContent() : BadRequest("No such task to delete");
        }
    }
}
