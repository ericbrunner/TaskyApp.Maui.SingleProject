using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskyApp.Contracts.Models;

namespace TaskyApp.Contracts.Views;

public interface ITabbedView
{
    ObservableCollection<ITabItem> TabItems { get; set; }
    ICommand SwipeRightCommand { get; }
    ICommand SwipeLeftCommand { get; }
}