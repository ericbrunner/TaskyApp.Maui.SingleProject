using TaskyApp.Contracts.Factories;
using TaskyApp.Contracts.Models;
using TaskyApp.Contracts.ViewModels;

namespace TaskyApp.Maui.SingleProject.Factories;

public class CustomTabbedPageViewModelFactory : ICustomTabbedPageViewModelFactory
{
    public ICustomTabbedPageViewModel Create(IEnumerable<ITabItem> tabItems)
    {
        var vm = App.Get<ICustomTabbedPageViewModel>();
        vm?.Init(tabItems);
        
        return vm;
    }
}