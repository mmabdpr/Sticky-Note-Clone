using StickyNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.Factory;
using TestStack.White.UIItems.ListBoxItems;

namespace StickyNote
{
    [TestFixture]
    public class ListNotesTests
    {
        [Test]
        public void AddANoteTest()
        { 
            var app = Application.Launch(CalculateApplicationPath());

            AddTestNote(app);

            var listNotesWindow = app.GetWindow("Notes");
            var notesList = listNotesWindow.Get<ListBox>("NoteList");
            var addedNoteItem = notesList.Items[notesList.Items.Count - 1];

            Assert.AreEqual(addedNoteItem.Text, "test note");

            app.Close();
        }

        [Test]
        public void RemoveANoteTest()
        {
            var app = Application.Launch(CalculateApplicationPath());

            var listNotesWindow = app.GetWindow("Notes");

            var notesList = listNotesWindow.Get<ListBox>("NoteList");

            var noteToDelete = notesList.Items.Where(n => n.Text == "test note").SingleOrDefault();

            if (noteToDelete == null) { AddTestNote(app); };

            noteToDelete.DoubleClick();

            var noteWindow = app.GetWindow(SearchCriteria.ByAutomationId("NoteWindow"), InitializeOption.NoCache);

            var removeNoteButton = noteWindow.Get<Button>("RemoveNoteButton");
            removeNoteButton.Click();

            Assert.IsTrue(noteWindow.IsClosed);

            notesList = listNotesWindow.Get<ListBox>("NoteList");
            var removedNoteItem = notesList.Items.Where(n => n.Text == "test note").SingleOrDefault();

            Assert.AreEqual(removedNoteItem, null);

            app.Close();
        }

        [Test]
        public void UpdateANoteTest()
        {
            var app = Application.Launch(CalculateApplicationPath());

            var listNotesWindow = app.GetWindow("Notes");

            var notesList = listNotesWindow.Get<ListBox>("NoteList");

            var noteToUpdate = notesList.Items.Where(n => n.Text == "test note").SingleOrDefault();

            if (noteToUpdate == null)
            {
                AddTestNote(app);
            }

            noteToUpdate = notesList.Items.Where(n => n.Text == "test note").Single();

            noteToUpdate.DoubleClick();

            var noteWindow = app.GetWindow(SearchCriteria.ByAutomationId("NoteWindow"), InitializeOption.NoCache);

            var noteTextBox = noteWindow.Get<TextBox>("NoteTextBox");

            noteTextBox.Text = "this is a updated test note";

            var noteTitle = noteWindow.Get<WPFLabel>("NoteTitleTextBlock");
            noteTitle.Click();

            var noteTitleTextBox = noteWindow.Get<TextBox>("NoteTitleTextBox");
            noteTitleTextBox.Text = "updated test note";

            noteTextBox.Click();

            Assert.AreEqual(noteTitle.Text, "updated test note");

            var saveNoteButton = noteWindow.Get<Button>("SaveNoteButton");
            saveNoteButton.Click();

            Assert.IsTrue(noteWindow.IsClosed);

            notesList = listNotesWindow.Get<ListBox>("NoteList");
            var updatedNoteItem = notesList.Items.Where(n => n.Text == "updated test note").SingleOrDefault();
            var removedNoteItem = notesList.Items.Where(n => n.Text == "test note").SingleOrDefault();

            updatedNoteItem.DoubleClick();
            noteWindow = app.GetWindow(SearchCriteria.ByAutomationId("NoteWindow"), InitializeOption.NoCache);
            noteTextBox = noteWindow.Get<TextBox>("NoteTextBox");

            Assert.AreEqual(noteTextBox.Text, "this is a updated test note");
            Assert.AreEqual(removedNoteItem, null);

            app.Close();
        }

        private static void AddTestNote(Application app)
        {
            var listNotesWindow = app.GetWindow("Notes");

            var newNoteButton = listNotesWindow.Get<Button>("NewNoteButton");

            newNoteButton.Click();

            var noteWindow = app.GetWindow(SearchCriteria.ByAutomationId("NoteWindow"), InitializeOption.NoCache);

            var noteTextBox = noteWindow.Get<TextBox>("NoteTextBox");

            noteTextBox.Text = "this is a test note";

            var noteTitle = noteWindow.Get<WPFLabel>("NoteTitleTextBlock");
            noteTitle.Click();

            var noteTitleTextBox = noteWindow.Get<TextBox>("NoteTitleTextBox");
            noteTitleTextBox.Text = "test note";

            noteTextBox.Click();

            Assert.AreEqual(noteTitle.Text, "test note");

            var saveNoteButton = noteWindow.Get<Button>("SaveNoteButton");
            saveNoteButton.Click();

            Assert.IsTrue(noteWindow.IsClosed);
        }

        private static string CalculateApplicationPath()
        {
            var appDir = TestContext.CurrentContext.TestDirectory;
            appDir += @"/../../../StickyNote/bin/Debug";
            var applicationPath = Path.Combine(appDir, "StickyNote.exe");
            return applicationPath;
        }
    }
}