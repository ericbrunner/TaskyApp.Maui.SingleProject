namespace TaskyApp.Contracts.Models;

public interface ITabItem
{
    View? View { get; }
    ImageSource Icon { get; set; }
    string? IconFile { get; set; }
    Func<View> ViewFactory { get; set; }
    bool IsActive { get; set; }
    double IconHeight { get; set; }
    double IconWidth { get; set; }
    double IconOpacity { get; }
    double InactiveIconOpacity { get; set; }
    public double ContainerWidth { get; set; }
}