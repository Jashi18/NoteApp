using NoteApp.Data.Entities;

namespace NoteApp.Core.Interfaces;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllNotesAsync();

    Task<Note> GetNoteByIdAsync(int id);

    Task<IEnumerable<Note>> SearchNotesByTitleAsync(string searchTerm);

    Task<Note> CreateNoteAsync(Note note);

    Task<Note> UpdateNoteAsync(Note note);

    Task<bool> DeleteNoteAsync(int id);
}
