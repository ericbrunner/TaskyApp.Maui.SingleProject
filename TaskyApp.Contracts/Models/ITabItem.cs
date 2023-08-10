namespace TaskyApp.Contracts.Models;

public interface ITabItem
{
    View View { get; }

    void Init(Func<View> viewFactory);
}