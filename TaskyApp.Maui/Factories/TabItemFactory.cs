using TaskyApp.Contracts.Factories;
using TaskyApp.Contracts.Models;

namespace TaskyApp.Maui.SingleProject.Factories;

public class TabItemFactory : ITabItemFactory
{
    public ITabItem Create()
    {
        var tabItem = App.Get<ITabItem>();

        ArgumentNullException.ThrowIfNull(tabItem, nameof(tabItem));

        return tabItem;
    }
}