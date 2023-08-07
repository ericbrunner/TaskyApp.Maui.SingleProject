namespace TaskyApp.Contracts.Models;

public interface ITabItem
{
    View View { get; set; }

    void Init(View  view);
}