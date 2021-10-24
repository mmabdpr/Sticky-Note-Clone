using StickyNote.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace StickyNote.Controller
{
    public class NoteController
    {
        public View.Note NoteView;

        public NoteController(View.Note noteView)
        {
            NoteView = noteView;
            using (var context = new NoteContext())
            {
                var note = context.Notes.Where(n => n.Title == NoteView.NoteTitleTextBlock.Text).SingleOrDefault();
                NoteView.NoteTextBox.Text = note == null ? "" : note.Content;
                if (note == null)
                {
                    ChangeColor(Color.FromRgb(182, 208, 51));
                }
                else
                {
                    var color = Color.FromRgb((byte)note.R, (byte)note.G, (byte)note.B);
                    ChangeColor(color);
                    context.Notes.Remove(note);
                    context.SaveChanges();
                }
            }
        }

        public void SaveNote()
        {
            using (var context = new NoteContext())
            {
                var oldNote = context.Notes.Where(n => n.Title == NoteView.NoteTitleTextBlock.Text).SingleOrDefault();

                var color = ((SolidColorBrush)NoteView.WindowTitleBrush).Color;
                if (oldNote != null)
                {
                    oldNote.Content = NoteView.NoteTextBox.Text;
                    oldNote.Title = NoteView.NoteTitleTextBlock.Text;
                    oldNote.R = color.R;
                    oldNote.G = color.G;
                    oldNote.B = color.B;
                }
                else
                {
                    var note = new Note
                    {
                        Content = NoteView.NoteTextBox.Text,
                        Title = NoteView.NoteTitleTextBlock.Text,
                        R = color.R,
                        G = color.G,
                        B = color.B,
                    };
                    context.Notes.Add(note);
                }

                context.SaveChanges();
            }
            NoteView.Close();
        }

        public void DeleteNote()
        {
            using (var context = new NoteContext())
            {
                var note = context.Notes.Where(n => n.Title == NoteView.NoteTitleTextBlock.Text).SingleOrDefault();
                if (note != null)
                {
                    context.Notes.Remove(note);
                    context.SaveChanges();
                }
            }
            NoteView.Close();
        }

        public void ChangeColor(Color? color)
        {
            int r = color.GetValueOrDefault().R;
            int g = color.GetValueOrDefault().G;
            int b = color.GetValueOrDefault().B;
            NoteView.WindowTitleBrush = new SolidColorBrush { Color = Color.FromRgb((byte)r, (byte)g, (byte)b) };
            int x = 50;
            r = r + x > 255 ? 255 : r + x;
            g = g + x > 255 ? 255 : g + x;
            b = b + x > 255 ? 255 : b + x;
            NoteView.NoteTextBox.Background = new SolidColorBrush { Color = Color.FromRgb((byte)r, (byte)g, (byte)b) };
            var textColor = (r * 0.299 + g * 0.587 + b * 0.114) > 186 ? Color.FromRgb(0, 0, 0) : Color.FromRgb(255, 255, 255);
            NoteView.NoteTextBox.Foreground = new SolidColorBrush { Color = textColor };
        }
    }
}
