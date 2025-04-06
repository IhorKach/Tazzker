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

        [HttpGet("/getAllTasks")]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAllTasks()
        {
            return Ok(await _taskItemService.GetAllTaskItemsAsync());
        }

        [HttpGet("/GetById{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await _taskItemService.GetTaskItemByIdAsync(id);

            return result != null ? Ok(result) : BadRequest("No item");
        }

        [HttpGet("/filter")]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetFilteredTaskItems([FromQuery] TaskItemFilterDto dto)
        {
            var result = await _taskItemService.GetFilteredTaskItemsAsync(dto);

            return Ok(result);
        }

        [HttpPost("/CreateTask")]
        public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskItemDto dto)
        {
            var result = await _taskItemService.CreateTaskItemAsync(dto);

            return result == null ? BadRequest("During Creation task something went wrong") : Ok(result);
        }

        [HttpPatch("/updateTask")]
        public async Task<IActionResult> UpdateTaskItem([FromBody] UpdateTaskItemDto dto)
        {
            var result = await _taskItemService.UpdateTaskItemAsync(dto);
            return result == null ? NotFound("No such task to update!") : Ok(result);
        }

        [HttpDelete("/DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            var result = await _taskItemService.DeleteTaskItemAsync(id);
            return result == true ? NoContent() : BadRequest("No such task to delete");
        }
    }
}