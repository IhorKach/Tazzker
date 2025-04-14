using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;

namespace TazzkerAPI.Controllers
{
    [Authorize]
    [Route("api/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("getNotes")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNotes()
        {
            return Ok(await _noteService.GetNotesAsync());
        }

        [HttpPost("syncNotes")]
        public async Task<IActionResult> SyncNotes([FromBody] IEnumerable<NoteDTO> notes)
        {
            return await _noteService.SyncNotesAsync(notes)
                ? Ok("Successfully synced!")
                : BadRequest("During sync something went wrong, try again!");
        }

        [HttpPost("clearTrashedNotes")]
        public async Task<IActionResult> DeleteTrashedNotes([FromBody] IEnumerable<Guid> noteIds)
        {
            return await _noteService.DeleteNotesAsync(noteIds)
                ? NoContent()
                : BadRequest("During Cleaning something went wrong, try again!");
        }
    }
}
