using System.Collections.ObjectModel;
using TaskyApp.Contracts.Models;
using TaskyApp.Contracts.ViewModels;
using TaskyApp.ViewModels;

namespace TaskyApp.Maui.SingleProject.ViewModels;

public class CustomTabbedPageViewModel : BaseViewModel, ICustomTabbedPageViewModel
{
    public void Init(IEnumerable<ITabItem> tabItems)
    {
        ItemsSource = new ObservableCollection<ITabItem>(tabItems);
    }

    private ObservableCollection<ITabItem>? _itemsSource;



    public ObservableCollection<ITabItem>? ItemsSource
    {
        get => _itemsSource;
        set => SetProperty(ref _itemsSource, value);
    }
}