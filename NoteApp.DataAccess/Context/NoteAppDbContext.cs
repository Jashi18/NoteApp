using Microsoft.EntityFrameworkCore;
using NoteApp.Data.Entities;

namespace NoteApp.Data.Context;

public class NoteAppDbContext : DbContext
{
    public NoteAppDbContext(DbContextOptions<NoteAppDbContext> options) : base(options)
    {
    }

    public DbSet<Note> Notes { get; set; }
}
