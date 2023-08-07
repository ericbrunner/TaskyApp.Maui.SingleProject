﻿using TaskyApp.Contracts.Factories;
using TaskyApp.Contracts.Models;

namespace TaskyApp.Maui.SingleProject.Factories;

public class TabItemFactory : ITabItemFactory
{
    public ITabItem Create(View view)
    {
        var tabItem = App.Get<ITabItem>();

        ArgumentNullException.ThrowIfNull(tabItem, nameof(tabItem));
        tabItem.Init(view);

        return tabItem;
    }
}