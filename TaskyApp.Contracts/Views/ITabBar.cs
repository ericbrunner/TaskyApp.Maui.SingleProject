using System.Collections.ObjectModel;
using TaskyApp.Contracts.Models;

namespace TaskyApp.Contracts.Views;

public interface ITabBar
{
    ObservableCollection<ITabItem> ItemsSource { get; set; }
    double InactiveIconOpacity { get; set; }
}