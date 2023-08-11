using TaskyApp.Contracts.Models;
using TaskyApp.Contracts.ViewModels;

namespace TaskyApp.Contracts.Factories;

public interface ICustomTabbedPageViewModelFactory
{
    ICustomTabbedPageViewModel? Create(IEnumerable<ITabItem> tabItems);
}