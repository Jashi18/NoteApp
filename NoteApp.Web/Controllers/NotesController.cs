using Microsoft.AspNetCore.Mvc;
using NoteApp.Web.Models;
using NoteApp.Web.Services;

namespace NoteApp.Web.Controllers;

public class NotesController : Controller
{
    private readonly INoteApiClient _noteApiClient;

    public NotesController(INoteApiClient noteApiClient)
    {
        _noteApiClient = noteApiClient;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        var notes = string.IsNullOrEmpty(searchString)
            ? await _noteApiClient.GetAllNotesAsync()
            : await _noteApiClient.SearchNotesByTitleAsync(searchString);

        return View(notes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var note = await _noteApiClient.GetNoteByIdAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        return View(note);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NoteViewModel note)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var createdNote = await _noteApiClient.CreateNoteAsync(note);
                TempData["SuccessMessage"] = "Note created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating note: {ex.Message}");
            }
        }
        return View(note);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var note = await _noteApiClient.GetNoteByIdAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        return View(note);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NoteViewModel note)
    {
        if (id != note.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _noteApiClient.UpdateNoteAsync(id, note);
                TempData["SuccessMessage"] = "Note updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating note: {ex.Message}");
            }
        }
        return View(note);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _noteApiClient.DeleteNoteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
