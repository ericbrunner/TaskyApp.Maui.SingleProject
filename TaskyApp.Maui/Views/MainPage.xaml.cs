namespace TaskyApp.Maui.SingleProject.Views;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void OnCloseClicked(object? sender, EventArgs e)
    {
        var window = GetParentWindow();

        if (window is null) return;

        Application.Current.CloseWindow(window);
    }
}