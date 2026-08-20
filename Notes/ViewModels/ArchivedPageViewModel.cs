using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using Notes.Models;
using Notes.Models.MessengerReferences;
using Notes.Services;
using Notes.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class ArchivedPageViewModel : BaseViewModel
{
    public ObservableCollection<Note> AllNotes { get; }

    public ArchivedPageViewModel()
    {

        AllNotes = new ObservableCollection<Note>();
        _isHorizontalThemeEnabled = false;

    }

    private bool _isHorizontalThemeEnabled;
    public bool IsHorizontalThemeEnabled
    {
        get { return _isHorizontalThemeEnabled; }
        set {  _isHorizontalThemeEnabled = value;  RaisePropertyChanged(nameof(IsHorizontalThemeEnabled)); }
    }

    public ICommand OnAppearing => new Command(async () =>
    {
        INoteStorageService noteStorageService = Application.Current.MainPage.Handler.MauiContext.Services.GetService<INoteStorageService>();

        AllNotes.Clear();
        var allnotes = await noteStorageService.GetAllArchived();
        foreach (var note in allnotes )
        {
            AllNotes.Add(note);   
        }
        Debug.Write(JsonConvert.SerializeObject(allnotes));
        Debug.Write(JsonConvert.SerializeObject(AllNotes));
    });

    public ICommand NewNoteClickedCommand => new Command(async() => 
    {
        await Shell.Current.GoToAsync(nameof(Views.NotePage));
    });

    public ICommand NoteSelectedCommand => new Command<Note>(async (note) =>
    {
        if(note != null)
        {
            await Shell.Current.GoToAsync($"{nameof(NotePage)}?Id={note.Id}");
        }
        else
        {
            //await Shell.Current.GoToAsync($"{nameof(NotePage)}");
        }        
    });


}