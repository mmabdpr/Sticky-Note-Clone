using System.Windows.Input;
using StickyNote.Controller;

namespace StickyNote.View
{
    /// <summary>
    /// Interaction logic for ListNotes.xaml
    /// </summary>
    public partial class ListNotes
    {
        public ListNotesController Controller;

        public ListNotes()
        {
            InitializeComponent();

            Controller = new ListNotesController(this);

            this.ShowCloseButton = false;
            this.ShowMaxRestoreButton = false;
            this.ShowMinButton = false;

            Controller.GenerateListOfNotes();
        }

        public void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        public void NewNoteButton_Click(object sender, System.Windows.RoutedEventArgs e) => Controller.ShowNewNote();

        public void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e) => Close();
    }
}
