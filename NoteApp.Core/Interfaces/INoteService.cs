using NoteApp.Core.Dtos;
using NoteApp.Data.Entities;

namespace NoteApp.Core.Interfaces;

public interface INoteService
{
    Task<IEnumerable<NoteDto>> GetAllNotesAsync();
    Task<NoteDto> GetNoteByIdAsync(int id);
    Task<IEnumerable<NoteDto>> SearchNotesByTitleAsync(string searchTerm);
    Task<NoteDto> CreateNoteAsync(NoteDto noteDto);
    Task<NoteDto> UpdateNoteAsync(int id, NoteDto noteDto);
    Task<bool> DeleteNoteAsync(int id);
}
