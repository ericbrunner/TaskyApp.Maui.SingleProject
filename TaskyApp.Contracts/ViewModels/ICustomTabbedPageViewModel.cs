using System.Collections.ObjectModel;
using TaskyApp.Contracts.Enums;
using TaskyApp.Contracts.Models;

namespace TaskyApp.Contracts.ViewModels;

public interface ICustomTabbedPageViewModel
{
    string Title { get; set; }
    ObservableCollection<ITabItem> ItemsSource { get; set; }
    public TabBarTypeEnum TabBarType { get; set; }
    void Init(IEnumerable<ITabItem> tabItems);
}