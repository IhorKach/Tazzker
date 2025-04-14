using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;

namespace TazzkerAPI.Controllers
{
    [Authorize]
    [Route("api/lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;
        public ListController(IListService listService)
        {
            _listService = listService;
        }

        [HttpGet("getLists")]
        public async Task<ActionResult<IEnumerable<ListDTO>>> GetLists()
        {
            return Ok(await _listService.GetListsAsync());
        }

        [HttpPost("syncLists")]
        public async Task<IActionResult> SyncLists([FromBody] IEnumerable<ListDTO> lists)
        {
            return await _listService.SyncListsAsync(lists)
                ? Ok("Successfully synced!")
                : BadRequest("During sync something went wrong, try again!");
        }

        [HttpPost("clearTrashedLists")]
        public async Task<IActionResult> DeleteTrashedLists([FromBody] IEnumerable<Guid> listIds)
        {
            return await _listService.DeleteListsAsync(listIds)
                ? NoContent()
                : BadRequest("During Cleaning something went wrong, try again!");
        }
    }
}
