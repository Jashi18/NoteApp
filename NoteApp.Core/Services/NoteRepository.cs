using Microsoft.EntityFrameworkCore;
using NoteApp.Core.Interfaces;
using NoteApp.Data.Context;
using NoteApp.Data.Entities;

namespace NoteApp.Core.Services;

public class NoteRepository : INoteRepository
{
    private readonly NoteAppDbContext _context;

    public NoteRepository(NoteAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<Note> GetNoteByIdAsync(int id)
    {
        return await _context.Notes.FindAsync(id);
    }

    public async Task<IEnumerable<Note>> SearchNotesByTitleAsync(string searchTerm)
    {
        return await _context.Notes
            .Where(n => n.Title.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<Note> UpdateNoteAsync(Note note)
    {
        _context.Entry(note).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
            return false;

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return true;
    }
}
