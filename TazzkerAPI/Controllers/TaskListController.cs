using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;

namespace TazzkerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private readonly ITaskListService _service;

        public TaskListController(ITaskListService service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListDto>>> GetAllTaskLists()
        {
            var taskLists =  await _service.GetAllTaskListsAsync();
            return Ok(taskLists);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskListById(Guid id)
        {
            var result =  await _service.GetTaskListByIdAsync(id);

            if (result == null) return NotFound();
            return Ok(result);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskList(Guid id,[FromBody] UpdateTaskListDto dto)
        {
            dto.TaskListId = id;
            var result = await _service.UpdateTaskListAsync(dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskList([FromBody] CreateTaskListDto dto)
        {
            var result =  await _service.CreateTaskListAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskList(Guid id)
        {
            var result =  await _service.DeleteTaskListAsync(id);
            return result == true ? NoContent() : NotFound();
        }
    }
}
