using Notes.ViewModels;

namespace Notes.Views;

public partial class AllNotesPage : ContentPage
{
    public AllNotesPage()
    {
        InitializeComponent();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        (this.BindingContext as NotesViewModel).OnAppearing.Execute(this);
    }
}