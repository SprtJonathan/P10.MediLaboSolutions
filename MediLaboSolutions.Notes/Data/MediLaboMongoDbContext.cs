using MediLaboSolutions.Notes.Models.Note;
using Microsoft.EntityFrameworkCore;

namespace MediLaboSolutions.Notes.Data;

public class MediLaboMongoDbContext : DbContext
{
    public MediLaboMongoDbContext(DbContextOptions<MediLaboMongoDbContext> options)
        : base(options)
    {
    }

    public DbSet<NoteEF> Notes { get; set; }
}
