using NoteApp.Core.Dtos;
using NoteApp.Core.Interfaces;
using NoteApp.Data.Entities;

namespace NoteApp.Core.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await _noteRepository.GetAllNotesAsync();
    }

    public async Task<Note> GetNoteByIdAsync(int id)
    {
        return await _noteRepository.GetNoteByIdAsync(id);
    }

    public async Task<IEnumerable<Note>> SearchNotesByTitleAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            throw new ArgumentException("Search term cannot be empty", nameof(searchTerm));

        return await _noteRepository.SearchNotesByTitleAsync(searchTerm);
    }

    public async Task<Note> CreateNoteAsync(NoteDto noteDto)
    {
        ValidateNoteDto(noteDto);

        var note = new Note
        {
            Title = noteDto.Title,
            Description = noteDto.Description,
            ImageUrl = noteDto.ImageUrl,
            Phone = noteDto.Phone
        };

        return await _noteRepository.CreateNoteAsync(note);
    }

    public async Task<Note> UpdateNoteAsync(int id, NoteDto noteDto)
    {
        ValidateNoteDto(noteDto);

        var existingNote = await _noteRepository.GetNoteByIdAsync(id);
        if (existingNote == null)
            throw new KeyNotFoundException($"Note with id {id} not found");

        existingNote.Title = noteDto.Title;
        existingNote.Description = noteDto.Description;
        existingNote.ImageUrl = noteDto.ImageUrl;
        existingNote.Phone = noteDto.Phone;

        return await _noteRepository.UpdateNoteAsync(existingNote);
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        return await _noteRepository.DeleteNoteAsync(id);
    }

    private void ValidateNoteDto(NoteDto noteDto)
    {
        if (noteDto == null)
            throw new ArgumentNullException(nameof(noteDto));

        if (string.IsNullOrWhiteSpace(noteDto.Title))
            throw new ArgumentException("Title is required", nameof(noteDto));

        if (string.IsNullOrWhiteSpace(noteDto.Description))
            throw new ArgumentException("Description is required", nameof(noteDto));
    }
}
