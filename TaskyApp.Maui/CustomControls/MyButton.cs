namespace TaskyApp.Maui.SingleProject.CustomControls;

public class MyButton : View, IMyButton
{
    public string Text { get; set; }
    public Color TextColor { get; set; }

    public event EventHandler Clicked;

    public void RaiseClicked()
    {
        Clicked?.Invoke(this, EventArgs.Empty);
    }


    public void Completed()
    {
        throw new NotImplementedException();
    }
}