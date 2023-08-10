using TaskyApp.Contracts.Factories;
using TaskyApp.Contracts.Models;
using TaskyApp.Maui.SingleProject.Views.CustomViews;

namespace TaskyApp.Maui.SingleProject.Factories;

public class TabItemFactory : ITabItemFactory
{
    public ITabItem Create(Func<View> viewFactory)
    {
        var tabItem = App.Get<ITabItem>();

        ArgumentNullException.ThrowIfNull(tabItem, nameof(tabItem));
        tabItem.Init(viewFactory);

        return tabItem;
    }
}