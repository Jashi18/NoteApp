using Microsoft.AspNetCore.Mvc;
using NoteApp.Core.Dtos;
using NoteApp.Core.Interfaces;

namespace NoteApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly ILogger<NotesController> _logger;

    public NotesController(INoteService noteService, ILogger<NotesController> logger)
    {
        _noteService = noteService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetAllNotes()
    {
        _logger.LogInformation("Retrieving all notes");
        var notes = await _noteService.GetAllNotesAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> GetNoteById(int id)
    {
        _logger.LogInformation("Retrieving note with ID: {NoteId}", id);
        var note = await _noteService.GetNoteByIdAsync(id);
        if (note == null)
        {
            _logger.LogInformation("Note with ID: {NoteId} not found", id);
            return NotFound();
        }
        return Ok(note);
    }

    [HttpPost]
    public async Task<ActionResult<NoteDto>> CreateNote(NoteDto noteDto)
    {
        _logger.LogInformation("Creating a new note");
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Invalid model state for note creation");
            return BadRequest(ModelState);
        }

        var createdNote = await _noteService.CreateNoteAsync(noteDto);
        _logger.LogInformation("Note created successfully with ID: {NoteId}", createdNote.Id);
        return CreatedAtAction(nameof(GetNoteById), new { id = createdNote.Id }, createdNote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, NoteDto noteDto)
    {
        try
        {
            var updatedNote = await _noteService.UpdateNoteAsync(id, noteDto);
            _logger.LogInformation($"Note with ID : {id} Was Updated");
            return Ok(updatedNote);
        }
        catch (KeyNotFoundException)
        {
            _logger.LogInformation($"Note with ID : {id} Was Not found");
            return NotFound();
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<NoteDto>>> SearchNotes([FromQuery] string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            _logger.LogWarning("Search attempted with empty title");
            return BadRequest(new { error = "Search term cannot be empty" });
        }

        try
        {
            var notes = await _noteService.SearchNotesByTitleAsync(title);
            return Ok(notes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching notes with title: {Title}", title);
            return StatusCode(500, new { error = "An error occurred while searching notes" });
        }
    }

        [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var result = await _noteService.DeleteNoteAsync(id);
        _logger.LogInformation($"Note with ID: {id} was deleted");
        if (!result)
            return NotFound();
        return NoContent();
    }
}
