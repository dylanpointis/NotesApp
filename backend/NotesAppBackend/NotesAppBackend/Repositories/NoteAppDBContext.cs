using Microsoft.EntityFrameworkCore;
using NotesAppBackend.Models;

namespace NotesAppBackend.Repositories
{
    public class NoteAppDBContext: DbContext
    {
        public NoteAppDBContext(DbContextOptions<NoteAppDBContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
