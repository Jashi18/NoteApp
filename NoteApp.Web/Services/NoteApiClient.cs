using NoteApp.Web.Models;

namespace NoteApp.Web.Services;

public class NoteApiClient : INoteApiClient
{
    private readonly HttpClient _httpClient;

    public NoteApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<NoteViewModel>> GetAllNotesAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<NoteViewModel>>("api/notes");
    }

    public async Task<NoteViewModel> GetNoteByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<NoteViewModel>($"api/notes/{id}");
    }

    public async Task<IEnumerable<NoteViewModel>> SearchNotesByTitleAsync(string searchTerm)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<NoteViewModel>>($"api/notes/search?title={searchTerm}");
    }

    public async Task<NoteViewModel> CreateNoteAsync(NoteViewModel note)
    {
        var response = await _httpClient.PostAsJsonAsync("api/notes", note);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<NoteViewModel>();
    }

    public async Task<NoteViewModel> UpdateNoteAsync(int id, NoteViewModel note)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/notes/{id}", note);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<NoteViewModel>();
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/notes/{id}");
        return response.IsSuccessStatusCode;
    }
}
