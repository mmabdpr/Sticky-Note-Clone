using MahApps.Metro.Controls;
using MahApps.Metro;
using StickyNote.Controller;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;


namespace StickyNote.View
{
    public partial class Note : MetroWindow
    {
        NoteController Controller;

        public string pendingTitle;

        public Note(string title)
        {
            InitializeComponent();

            pendingTitle = title;

            MouseLeftButtonDown += Window_MouseLeftButtonDown;

            ShowCloseButton = false;
            ShowMaxRestoreButton = false;
            ShowMinButton = false;

            NoteTitleTextBlock.Text = title;
            NoteTitleTextBlock.MouseLeftButtonDown += NoteTitleTextBlock_MouseLeftButtonDown;

            NoteTitleTextBox.Visibility = System.Windows.Visibility.Collapsed;
            NoteTitleTextBox.LostKeyboardFocus += NoteTitleTextBox_LostKeyboardFocus;

            LostKeyboardFocus += Note_LostFocus;
            GotKeyboardFocus += Note_GotFocus;

            ColorPicker.SelectedColorChanged += ColorPicker_SelectedColorChanged;

            Controller = new NoteController(this);
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            var color = e.NewValue;
            Controller.ChangeColor(color);
        }

        public void Note_GotFocus(object sender, System.Windows.RoutedEventArgs e) => ShowTitleBar = true;

        public void Note_LostFocus(object sender, System.Windows.RoutedEventArgs e) => ShowTitleBar = false;

        public void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        public void SaveNoteButton_Click(object sender, System.Windows.RoutedEventArgs e) => Controller.SaveNote();

        public void RemoveNoteButton_Click(object sender, System.Windows.RoutedEventArgs e) => Controller.DeleteNote();

        public void NoteTitleTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NoteTitleTextBlock.Visibility = System.Windows.Visibility.Collapsed;
            NoteTitleTextBox.Visibility = System.Windows.Visibility.Visible;
            NoteTitleTextBox.Text = NoteTitleTextBlock.Text;
        }

        public void NoteTitleTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            NoteTitleTextBlock.Visibility = System.Windows.Visibility.Visible;
            NoteTitleTextBox.Visibility = System.Windows.Visibility.Collapsed;
            NoteTitleTextBlock.Text = NoteTitleTextBox.Text;
        }
    }
}
