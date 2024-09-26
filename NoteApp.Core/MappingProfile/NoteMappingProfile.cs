using AutoMapper;
using NoteApp.Core.Dtos;
using NoteApp.Data.Entities;

namespace NoteApp.Core.MappingProfile;

public class NoteMappingProfile : Profile
{
    public NoteMappingProfile()
    {
        CreateMap<Note, NoteDto>().ReverseMap();
    }
}
