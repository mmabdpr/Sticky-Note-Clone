using StickyNote.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace StickyNote.Controller
{
    public class ListNotesController
    {
        public ListNotesController(View.ListNotes listNoteView) => ListNotesView = listNoteView;

        public View.ListNotes ListNotesView { get; set; }

        HashSet<string> pendingNotes = new HashSet<string>();

        public void ShowSavedNote(object sender, RoutedEventArgs e)
        {
            var clickedItem = (ListBoxItem)sender;
            string title = clickedItem.Content.ToString();

            if (!pendingNotes.Contains(title))
            {
                ShowNote(title);
            }
        }

        public void ShowNewNote() => ShowNote("Note");

        public void NoteWindow_Closing(object sender, CancelEventArgs e)
        {
            pendingNotes.Remove(((View.Note)sender).pendingTitle);
            GenerateListOfNotes();
        }

        public void GenerateListOfNotes()
        {
            ListNotesView.NoteList.Items.Clear();

            using (var context = new NoteContext())
            {
                foreach (var note in context.Notes)
                {
                    var noteItem = new ListBoxItem { Content = note.Title };
                    noteItem.MouseDoubleClick += ShowSavedNote;
                    ListNotesView.NoteList.Items.Add(noteItem);
                }
            }
        }

        public void ShowNote(string title)
        {
            var noteWindow = new View.Note(title);
            noteWindow.Show();
            noteWindow.Closing += NoteWindow_Closing;
            pendingNotes.Add(title);
        }
    }
}
