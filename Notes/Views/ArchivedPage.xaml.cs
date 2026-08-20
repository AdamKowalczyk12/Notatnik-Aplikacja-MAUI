using Notes.ViewModels;

namespace Notes.Views;

public partial class ArchivedPage : ContentPage
{
    public ArchivedPage()
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

        (this.BindingContext as ArchivedPageViewModel).OnAppearing.Execute(this);
    }
}