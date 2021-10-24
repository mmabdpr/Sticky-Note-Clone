using System.Data.Entity;

namespace StickyNote.Model
{
    public class NoteContext : DbContext
    {
        public NoteContext() : base("NoteDB") { }

        public DbSet<Note> Notes { get; set; }
    }
}
