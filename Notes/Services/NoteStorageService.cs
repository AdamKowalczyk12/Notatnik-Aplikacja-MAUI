using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Notes.Services
{
    public interface INoteStorageService
    {
        Task AddNote(string text);

        Task RemoveNote(Guid id);

        Task EditNote(Guid id,string text);

        Task ArchiveNote(Guid id);

        Task<IEnumerable<Note>> GetAll();

        Task<IEnumerable<Note>> GetAllArchived();

        Task<Note> FindByID(Guid id);
    }

    public class NoteStorageService : INoteStorageService
    {
        private async Task<List<Note>> AllNotes()
        {
            ILocalStorageService localStorageService = Microsoft.Maui.Controls.Application.Current.MainPage.Handler.MauiContext.Services.GetService<ILocalStorageService>();

            string text = await localStorageService.GetValue<string>("notes");

            if (string.IsNullOrEmpty(text))
            {
                return new List<Note>();
            }

            List<Note> notes = JsonConvert.DeserializeObject<List<Note>>(text);
            return notes;
        }


        public async Task AddNote(string text)
        {
            var notes = (await AllNotes()).ToList();
            
            notes.Add(new Note()
            {
                Id = Guid.NewGuid(),
                Text = text,
                CreatingDate = DateTime.Now,
                ArchiveTime = null,

            });
            ILocalStorageService localStorageService = Microsoft.Maui.Controls.Application.Current.MainPage.Handler.MauiContext.Services.GetService<ILocalStorageService>();
            
            await localStorageService.SetValue("notes", JsonConvert.SerializeObject(notes));
        }

        public async Task ArchiveNote(Guid id)
        {
            var notes = await AllNotes();

            var foundednote = notes.FirstOrDefault(x => x.Id == id);


            if (foundednote == null)
            {
                return;

            }
            else
            {
                foundednote.ArchiveTime = DateTime.Now;
                ILocalStorageService localStorageService = Microsoft.Maui.Controls.Application.Current.MainPage.Handler.MauiContext.Services.GetService<ILocalStorageService>();
                await localStorageService.SetValue("notes", JsonConvert.SerializeObject(notes));
            }

        }
        
        public async Task EditNote(Guid id, string text)
        {
            var notes = await AllNotes();
            var foundednote = notes.FirstOrDefault(x => x.Id == id);

            if (foundednote == null)
            {
                return;

            }
            else
            {
                foundednote.Text = text;
                ILocalStorageService localStorageService = Microsoft.Maui.Controls.Application.Current.MainPage.Handler.MauiContext.Services.GetService<ILocalStorageService>();
                await localStorageService.SetValue("notes", JsonConvert.SerializeObject(notes));
            }
        }
        

        public async Task<Note> FindByID(Guid id)
        {
            var notes = await AllNotes();
            var foundednote = notes.FirstOrDefault(x => x.Id == id);

            if (foundednote == null)
            {
                return null;

            }
            else
            {
                return foundednote;
            }
        }

        public async Task<IEnumerable<Note>> GetAll() //niezarchiwizowane
        {
            return (await AllNotes()).Where(x => !x.ArchiveTime.HasValue);
        }

        public async Task<IEnumerable<Note>> GetAllArchived()
        {

            return (await AllNotes()).Where(x => x.ArchiveTime.HasValue);
        }

        public async Task RemoveNote(Guid id)
        {
            ILocalStorageService localStorageService = Microsoft.Maui.Controls.Application.Current.MainPage.Handler.MauiContext.Services.GetService<ILocalStorageService>();
            var notes = await AllNotes();

            var foundednote = notes.FirstOrDefault(x => x.Id == id);

            if (foundednote == null) 
            {
                return;
            }
            else
            {
                notes = notes.Where(x => x.Id != id).ToList();
               
                await localStorageService.SetValue("notes", JsonConvert.SerializeObject(notes));


            }    
        }
    }
}
