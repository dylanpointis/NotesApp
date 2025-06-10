using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAppBackend.Models;
using NotesAppBackend.Repositories;
using NotesAppBackend.Services;

namespace NotesAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private NoteService _noteService;

        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllNotes()
        {
            try
            {
                List<Note> notes = await _noteService.GetAllNotes();
                return StatusCode(StatusCodes.Status200OK, notes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        //the url is api/notes/archived
        [HttpGet]
        [Route("archived")]
        public async Task<IActionResult> getArchivedNotes()
        {
            try
            {
                List<Note> notes = await _noteService.GetArchivedNotes();
                return StatusCode(StatusCodes.Status200OK, notes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        //api/notes/unarchived
        [HttpGet]
        [Route("unarchived")]
        public async Task<IActionResult> getUnarchivedNotes()
        {
            try
            {
                List<Note> notes = await _noteService.GetUnarchivedNotes();
                return StatusCode(StatusCodes.Status200OK, notes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }



        [HttpGet]
        //filter notes by category, title and isArchieved state
        //route is api/notes/filter?categoryId={categoryId}&title={title}&isArchived={isArchived}
        [Route("filter")]
        public async Task<IActionResult> GetFilteredNotes([FromQuery] int categoryId, [FromQuery] string? title, [FromQuery] bool? isArchived)
        {
            try
            {
                List<Note> filteredNotes = await _noteService.GetFilteredNotes(categoryId, title, isArchived);
                return StatusCode(StatusCodes.Status200OK, filteredNotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest(new { message = "The note is null" });
            }

            try
            {
                bool result = await _noteService.CreateNote(note);
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, note);
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict, new { message = "The note cannot be created because a note with the same title already exists." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                bool result = await _noteService.DeleteNote(id);
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, id);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "The note with the specified ID does not exist." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        //to edit the archived status of a note
        [HttpPut]
        [Route("{id:int}/archive")]
        public async Task<IActionResult> EditArchivedStatus(int id, [FromBody] bool isArchived)
        {
            try
            {
                bool result = await _noteService.EditArchivedStatus(id, isArchived);
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, id);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "The note with the specified ID does not exist." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }



        [HttpPut]
        public async Task<IActionResult> EditNote([FromBody] Note note)
        {
            try
            {
                bool result = await _noteService.EditNote(note);
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, note);
                }
                else
                {
                    return BadRequest(new { message = "The note is null" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

    }
}
