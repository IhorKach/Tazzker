using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;

namespace TazzkerAPI.Controllers
{
    [Authorize]
    [Route("api/sublists")]
    [ApiController]
    public class SublistController : ControllerBase
    {

        private readonly ISublistService _sublistService;
        public SublistController(ISublistService sublistService)
        {
            _sublistService = sublistService;
        }

        [HttpGet("getSublists")]
        public async Task<ActionResult<IEnumerable<SublistDTO>>> GetSublists()
        {
            return Ok(await _sublistService.GetSublistsAsync());
        }

        [HttpPost("syncSublists")]
        public async Task<IActionResult> SyncTasks([FromBody] IEnumerable<SublistDTO> sublists)
        {
            return await _sublistService.SyncSublistsAsync(sublists)
                ? Ok("Sublists successfully synced.")
                : BadRequest("Error synchronizing sublists. Please try again.");
        }

        [HttpPost("clearTrashedSublists")]
        public async Task<IActionResult> DeleteTrashedSublists([FromBody] IEnumerable<Guid> sublistIds)
        {
            return await _sublistService.DeleteSublistsAsync(sublistIds)
                ? NoContent()
                : BadRequest("Error deleting sublists. Please try again.");
        }
    }
}
