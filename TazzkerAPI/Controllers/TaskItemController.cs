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

        [HttpPut("/UpdateTask{id}")]
        public async Task<IActionResult> UpdateTaskItem(Guid id, [FromBody] UpdateTaskItemDto dto)
        {
            dto.TaskId = id;
            var result = await _taskItemService.UpdateTaskItemAsync(dto);
            return result == null ? BadRequest("No such Item!") : Ok(result);
        }
        [HttpDelete("/DeleteTask{id}")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            var result = await _taskItemService.DeleteTaskItemAsync(id);
            return result == true ? NoContent() : BadRequest("No such task to delete");
        }
        [HttpPatch("/softDelete{id}")]
        public async Task<IActionResult> SoftDeleteTaskItem(Guid id)
        {
            var result = await _taskItemService.SoftDeleteTaskItemAsync(id);
            return result == true? NoContent() : NotFound("No such Task To Soft Delete");
        }

    }
}
