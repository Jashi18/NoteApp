using System.ComponentModel.DataAnnotations;

namespace NoteApp.Web.Models;

public class NoteViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(30, ErrorMessage = "Title cannot be longer than 30 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    public string ImageUrl { get; set; }

    [RegularExpression(@"^\d+$", ErrorMessage = "Phone number should contain only digits.")]
    public string Phone { get; set; }
}
