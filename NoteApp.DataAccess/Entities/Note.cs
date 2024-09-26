using System.ComponentModel.DataAnnotations;

namespace NoteApp.Data.Entities;

public class Note
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    [MaxLength(20)]
    public string Phone { get; set; }
}
