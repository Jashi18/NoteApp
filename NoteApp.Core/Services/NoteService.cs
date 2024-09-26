using AutoMapper;
using NoteApp.Core.Dtos;
using NoteApp.Core.Interfaces;
using NoteApp.Data.Entities;

namespace NoteApp.Core.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;

    public NoteService(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
    {
        var notes = await _noteRepository.GetAllNotesAsync();
        return _mapper.Map<IEnumerable<NoteDto>>(notes);
    }

    public async Task<NoteDto> GetNoteByIdAsync(int id)
    {
        var note = await _noteRepository.GetNoteByIdAsync(id);
        return _mapper.Map<NoteDto>(note);
    }

    public async Task<IEnumerable<NoteDto>> SearchNotesByTitleAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            throw new ArgumentException("Search term cannot be empty", nameof(searchTerm));

        var notes = await _noteRepository.SearchNotesByTitleAsync(searchTerm);
        return _mapper.Map<IEnumerable<NoteDto>>(notes);
    }

    public async Task<NoteDto> CreateNoteAsync(NoteDto noteDto)
    {
        ValidateNoteDto(noteDto);
        var note = _mapper.Map<Note>(noteDto);
        var createdNote = await _noteRepository.CreateNoteAsync(note);
        return _mapper.Map<NoteDto>(createdNote);
    }

    public async Task<NoteDto> UpdateNoteAsync(int id, NoteDto noteDto)
    {
        ValidateNoteDto(noteDto);
        var existingNote = await _noteRepository.GetNoteByIdAsync(id);
        if (existingNote == null)
            throw new KeyNotFoundException($"Note with id {id} not found");

        _mapper.Map(noteDto, existingNote);
        var updatedNote = await _noteRepository.UpdateNoteAsync(existingNote);
        return _mapper.Map<NoteDto>(updatedNote);
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
