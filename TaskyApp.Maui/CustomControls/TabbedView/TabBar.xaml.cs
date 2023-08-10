
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using MvvmHelpers;
using TaskyApp.Contracts.Models;

namespace TaskyApp.Maui.SingleProject.CustomControls.TabbedView;

public partial class TabBar : ITabBar
{
    public TabBar()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource), 
        typeof(ObservableCollection<ITabItem>), 
        typeof(TabBar),
        propertyChanged: ItemsSourcePropertyChanged);

    private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TabBar tabBar) return;
        if (newvalue is not ObservableCollection<ITabItem> tabItems) return;


        tabBar.TabItems = tabItems.ToList();

        foreach (var tabBarTabItem in tabBar.TabItems)
        {
            tabBarTabItem.InactiveIconOpacity = tabBar.InactiveIconOpacity;
        }

        tabBar.CollectionView.ItemsSource = tabBar.TabItems;

        System.Diagnostics.Debug.WriteLine($"TABBAR - ItemSource set.");

    }

    protected override Size ArrangeOverride(Rect bounds)
    {
        System.Diagnostics.Debug.WriteLine($"TABBAR-SIZE - {nameof(ArrangeOverride)} {nameof(bounds)}: {bounds}");

        var tabBarHeight = bounds.Height;
        var iconDimension = tabBarHeight / 2;
        NavIconHeight = NavIconWidth = iconDimension;

        foreach (var tabBarTabItem in TabItems)
        {
            tabBarTabItem.IconHeight = iconDimension;
            tabBarTabItem.IconWidth = iconDimension;
        }
        
        return base.ArrangeOverride(bounds);
    }




    public ObservableCollection<ITabItem> ItemsSource
    {
        get => (ObservableCollection<ITabItem>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public List<ITabItem> TabItems { get; set; }

    private double _navIconHeight;

    public double NavIconHeight
    {
        get => _navIconHeight; 
        set => SetProperty(ref _navIconHeight, value);
    }

    private double _navIconWidth;
    public static readonly BindableProperty TabBarTypeProperty = BindableProperty.Create(
        nameof(TabBarType), 
        typeof(TabBarTypeEnum), 
        typeof(TabBar),
        propertyChanged: TabBarTypePropertyChanged);

    public static readonly BindableProperty SelectedTabProperty = BindableProperty.Create(
        nameof(SelectedTab), 
        typeof(int), 
        typeof(TabBar), 
        -1,
        propertyChanged: SelectedTabPropertyChanged);

    public static readonly BindableProperty ShowArrowsProperty = BindableProperty.Create(
        nameof(ShowArrows), 
        typeof(bool), 
        typeof(TabBar), 
        default(bool));



    private static void SelectedTabPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TabBar tabBar) return;

        var selectedTab = (int)newvalue;

        if (selectedTab < 0) return;

        for (var i = 0; i < tabBar.TabItems.Count; i++)
        {
            tabBar.TabItems[i].IsActive = selectedTab == i;
        }

    }

    private static void TabBarTypePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TabBar tabBar) return;
        
        var castedValue = (TabBarTypeEnum)newvalue;

        tabBar.ContainerWidth = tabBar.TabBarType == TabBarTypeEnum.Top ? 120 : 48;
        tabBar.InactiveIconOpacity = castedValue == TabBarTypeEnum.Top ? .74d : .38d;
    }

    public double InactiveIconOpacity { get; set; }

    public double NavIconWidth
    {
        get => _navIconWidth; 
        set => SetProperty(ref _navIconWidth, value);
    }

    public TabBarTypeEnum TabBarType
    {
        get => (TabBarTypeEnum)GetValue(TabBarTypeProperty);
        set => SetValue(TabBarTypeProperty, value);
    }

    public int SelectedTab
    {
        get => (int)GetValue(SelectedTabProperty);
        set => SetValue(SelectedTabProperty, value);
    }

    public bool ShowArrows
    {
        get => (bool)GetValue(ShowArrowsProperty);
        set => SetValue(ShowArrowsProperty, value);
    }

    private double _containerWidth;

    public double ContainerWidth
    {
        get => _containerWidth; 
        set => SetProperty(ref _containerWidth, value);
    }


    protected virtual bool SetProperty<T>(
        ref T backingStore, 
        T value,
        [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;

        backingStore = value;

        OnPropertyChanged(propertyName);
        return true;
    }


}

public enum TabBarTypeEnum
{
    None,
    Top,
    Bottom
}

public interface ITabBar
{
    ObservableCollection<ITabItem> ItemsSource { get; set; }
    double InactiveIconOpacity { get; set; }
}