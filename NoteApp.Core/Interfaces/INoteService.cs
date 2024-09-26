using NoteApp.Core.Dtos;
using NoteApp.Data.Entities;

namespace NoteApp.Core.Interfaces;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllNotesAsync();

    Task<Note> GetNoteByIdAsync(int id);

    Task<IEnumerable<Note>> SearchNotesByTitleAsync(string searchTerm);

    Task<Note> CreateNoteAsync(NoteDto noteDto);

    Task<Note> UpdateNoteAsync(int id, NoteDto noteDto);

    Task<bool> DeleteNoteAsync(int id);
}
