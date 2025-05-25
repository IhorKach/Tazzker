using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;

namespace TazzkerAPI.Controllers
{
    [Authorize]
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("getTasks")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasks()
        {
            return Ok(await _taskService.GetTasksAsync());
        }

        [HttpPost("syncTasks")]
        public async Task<IActionResult> SyncTasks([FromBody] IEnumerable<TaskDTO> tasks)
        {
            return await _taskService.SyncTasksAsync(tasks)
                ? Ok("Tasks successfully synced.")
                : BadRequest("Error synchronizing tasks. Please try again.");
        }

        [HttpPost("clearTrashedTasks")]
        public async Task<IActionResult> DeleteTrashedTasks([FromBody] IEnumerable<Guid> taskIds)
        {
            return await _taskService.DeleteTasksAsync(taskIds)
                ? NoContent()
                : BadRequest("Error deleting tasks. Please try again.");
        }
    }
}
