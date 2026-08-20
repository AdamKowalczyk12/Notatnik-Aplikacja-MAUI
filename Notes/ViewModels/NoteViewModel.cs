using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using Notes.Services;
using Notes.Models;
using CommunityToolkit.Mvvm.Messaging;
using Notes.Models.MessengerReferences;

namespace Notes.ViewModels;

internal class NoteViewModel : ObservableObject, IQueryAttributable
{
    private Models.Note _note;
    private string _text;


    public string Text
    {
        get => _text;
        set
        {
           _text = value;
            OnPropertyChanged();
            
        }
    }

    public Note Note
    {
        get { return _note; }
        set
        {
            _note = value;
            OnPropertyChanged();
        }
    }

    public DateTime Date => _note.CreatingDate;

    public ICommand SaveCommand => new Command(async () =>
    {
        INoteStorageService noteStorageService = Application.Current.MainPage.Handler.MauiContext.Services.GetService<INoteStorageService>();

        if (Note == null)
        {
            await noteStorageService.AddNote(Text);
        }
        else
            await noteStorageService.EditNote(Note.Id, Text);

        WeakReferenceMessenger.Default.Send(new NoteChanged());
        await Shell.Current.GoToAsync(".."); //do poprzedniej strony

    });

    public ICommand DeleteCommand => new Command(async () =>
    {
        INoteStorageService noteStorageService = Application.Current.MainPage.Handler.MauiContext.Services.GetService<INoteStorageService>();

        if (Note != null)
        {
            await noteStorageService.RemoveNote(Note.Id);
        }

        await Shell.Current.GoToAsync($"..");
    });

    public ICommand ArchiveCommand => new Command(async () =>
    {
        INoteStorageService noteStorageService = Application.Current.MainPage.Handler.MauiContext.Services.GetService<INoteStorageService>();

        if (Note != null) 
        {
            await noteStorageService.ArchiveNote(Note.Id);
        }

        await Shell.Current.GoToAsync($"..");
    });

    public NoteViewModel()
    {
    
    }

    async void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query) //pobieranie ideyntyfikatora
    {
        if (query.ContainsKey("Id"))
        {
            Guid id = Guid.Parse(query["Id"].ToString());

            INoteStorageService noteStorageService = Application.Current.MainPage.Handler.MauiContext.Services.GetService<INoteStorageService>();

            var foundednote = await noteStorageService.FindByID(id);

            if (foundednote == null) 
            {
                await Shell.Current.GoToAsync("..");
                return;
            }

            Note = foundednote;
            Text = Note.Text;
        }
    }

}