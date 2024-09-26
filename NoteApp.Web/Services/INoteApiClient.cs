using NoteApp.Web.Models;

namespace NoteApp.Web.Services;

public interface INoteApiClient
{
    Task<IEnumerable<NoteViewModel>> GetAllNotesAsync();
    Task<NoteViewModel> GetNoteByIdAsync(int id);
    Task<IEnumerable<NoteViewModel>> SearchNotesByTitleAsync(string searchTerm);
    Task<NoteViewModel> CreateNoteAsync(NoteViewModel note);
    Task<NoteViewModel> UpdateNoteAsync(int id, NoteViewModel note);
    Task<bool> DeleteNoteAsync(int id);
}
