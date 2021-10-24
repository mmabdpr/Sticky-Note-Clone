using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace StickyNote.Model
{
    public class Note
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }
}
