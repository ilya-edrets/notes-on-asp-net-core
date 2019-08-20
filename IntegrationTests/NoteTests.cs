namespace IntegrationTests
{
    using System;
    using Xunit;
    using Settings;
    using DataAccess.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class NoteTests : IClassFixture<NoteFixture>
    {
        private NoteFixture noteFixture;

        public NoteTests(NoteFixture noteFixture)
        {
            this.noteFixture = noteFixture;
        }

        [Fact]
        public void CanFindAllNotesForUser()
        {
            var foundNotes = Note.GetAllByUserId(this.noteFixture.User1.Id);

            Assert.Equal(foundNotes.Count, this.noteFixture.User1Notes.Count);
            this.noteFixture.User1Notes.ForEach(expectedNote =>
            {
                var foundNote = foundNotes.Single(note => note.Id == expectedNote.Id);
                Assert.Equal(expectedNote.UserId, foundNote.UserId);
                Assert.Equal(expectedNote.Text, foundNote.Text);
            });
        }

        [Fact]
        public void CanUpdateNote()
        {
            var expectedText = Guid.NewGuid().ToString();
            var note = Note.GetAllByUserId(this.noteFixture.User1.Id).First();
            note.Text = expectedText;
            note.Update();
            var updatedNote = Note.GetAllByUserId(this.noteFixture.User1.Id).Single(x => x.Id == note.Id);

            Assert.Equal(expectedText, updatedNote.Text);
        }
    }

    public class NoteFixture : IDisposable
    {
        public User User1 { get; set; }

        public User User2 { get; set; }

        public List<Note> User1Notes { get; set; }

        public List<Note> User2Notes { get; set; }

        public NoteFixture()
        {
            var settings = new Settings
            {
                ConnectionString = @"Data Source=.\MSSQLSERVER2017;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;Initial Catalog=notes"
            };

            Settings.Initialize(settings);

            this.User1 = new User();
            this.User1.Login = Guid.NewGuid().ToString();
            this.User1.Password = Guid.NewGuid().ToString();
            this.User1.Insert();

            this.User2 = new User();
            this.User2.Login = Guid.NewGuid().ToString();
            this.User2.Password = Guid.NewGuid().ToString();
            this.User2.Insert();

            this.User1Notes = new List<Note>(new[]
            {
                new Note(this.User1.Id),
                new Note(this.User1.Id),
                new Note(this.User1.Id)
            });

            this.User2Notes = new List<Note>(new[]
            {
                new Note(this.User2.Id),
                new Note(this.User2.Id),
                new Note(this.User2.Id)
            });

            this.User1Notes.ForEach(note => note.Text = Guid.NewGuid().ToString());
            this.User2Notes.ForEach(note => note.Text = Guid.NewGuid().ToString());

            this.User1Notes.ForEach(note => note.Insert());
            this.User2Notes.ForEach(note => note.Insert());
        }

        public void Dispose()
        {
            this.User1Notes.ForEach(note => note.Delete());
            this.User2Notes.ForEach(note => note.Delete());
            this.User1.Delete();
            this.User2.Delete();
        }
    }
}
