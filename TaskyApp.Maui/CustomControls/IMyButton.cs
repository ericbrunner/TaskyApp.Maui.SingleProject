namespace TaskyApp.Maui.SingleProject.CustomControls;

public interface IMyButton : IView
{
    string Text { get; set; }
    Color TextColor { get; set; }
    Color? BackgroundColor { get; set; }
    void Completed();
    void RaiseClicked();
}