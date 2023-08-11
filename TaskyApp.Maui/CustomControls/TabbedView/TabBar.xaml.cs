using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskyApp.Contracts.Enums;
using TaskyApp.Contracts.Models;
using TaskyApp.Contracts.Views;

namespace TaskyApp.Maui.SingleProject.CustomControls.TabbedView;

public partial class TabBar : ITabBar
{
    private const string NavLeft = "left";
    private const string NavRight = "right";

    public TabBar()
    {
        TabItemSelectCommand = new Command<ITabItem>(TabItemSelect);


        NavLeftCommand = new Command(NavLeftExecute, () =>
        {
            
            bool canExecute = SelectedTab > 0;

            System.Diagnostics.Debug.WriteLine($"{nameof(NavLeftCommand)}.CanExecute: {canExecute} / {nameof(SelectedTab)}:{SelectedTab}");
            return canExecute;
        });

        NavRightCommand = new Command(NavRightExecute, () =>
        {

            bool canExecute = TabItems != null && SelectedTab < TabItems.Count - 1;

            System.Diagnostics.Debug.WriteLine($"{nameof(NavRightCommand)}.CanExecute: {canExecute} / {nameof(SelectedTab)}:{SelectedTab}");
            return canExecute;
        });


        InitializeComponent();
    }

    private void NavLeftExecute()
    {
        SelectedTab--;

        System.Diagnostics.Debug.WriteLine($"{nameof(NavLeftCommand)} - NEW {nameof(SelectedTab)}:{SelectedTab}");
    }

    private void NavRightExecute()
    {
        SelectedTab++;

        System.Diagnostics.Debug.WriteLine($"{nameof(NavRightCommand)} - NEW {nameof(SelectedTab)}:{SelectedTab}");
    }

    private void TabItemSelect(ITabItem tabItem)
    {
        if (TabItems == null) return;

        SelectedTab = TabItems.IndexOf(tabItem);

        System.Diagnostics.Debug.WriteLine($"selected tabitem index: {SelectedTab}");
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


        tabBar.TabItems = new List<ITabItem>();

        foreach (var tabBarTabItem in tabItems)
        {
            var clone = tabBarTabItem.ShallowCopy();

            if (clone == null) continue;

            clone.InactiveIconOpacity = tabBar.InactiveIconOpacity;

            tabBar.TabItems.Add(clone);
        }

        tabBar.CollectionView.ItemsSource = tabBar.TabItems;
    }

    protected override Size ArrangeOverride(Rect bounds)
    {
        System.Diagnostics.Debug.WriteLine($"TABBAR-SIZE - {nameof(ArrangeOverride)} {nameof(bounds)}: {bounds}");

        var tabBarHeight = bounds.Height;
        var iconDimension = tabBarHeight / 2;
        NavIconHeight = NavIconWidth = iconDimension;

        if (TabItems == null) return base.ArrangeOverride(bounds);

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

    public List<ITabItem>? TabItems { get; set; }

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
        defaultBindingMode: BindingMode.TwoWay,
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

        if (tabBar.TabItems == null) return;

        for (var i = 0; i < tabBar.TabItems.Count; i++)
        {
            tabBar.TabItems[i].IsActive = selectedTab == i;
        }

        tabBar.OnPropertyChanged(nameof(NavLeftOpacity));
        tabBar.OnPropertyChanged(nameof(NavRightOpacity));
    }

    private static void TabBarTypePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TabBar tabBar) return;

        var castedValue = (TabBarTypeEnum)newvalue;

        tabBar.ContainerWidth = tabBar.TabBarType == TabBarTypeEnum.Top ? 120 : 48;

        tabBar.InactiveIconOpacity = castedValue == TabBarTypeEnum.Top ? TopInactiveOpacity : BottomInactiveOpacity;
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
        set
        {
            SetValue(SelectedTabProperty, value);

            OnPropertyChanged(nameof(NavLeftOpacity));
            OnPropertyChanged(nameof(NavRightOpacity));
        }
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

    public Command<ITabItem> TabItemSelectCommand { get; }
    public ICommand NavLeftCommand { get; }

    public ICommand NavRightCommand { get; }

    private const double BottomInactiveOpacity = .38;
    private const double TopInactiveOpacity = .74;


    public double NavRightOpacity => TabItems != null && SelectedTab == TabItems.Count - 1 ? BottomInactiveOpacity : 1.0;
    public double NavLeftOpacity => SelectedTab == 0 ? BottomInactiveOpacity : 1.0;



    private void SetProperty<T>(ref T backingStore,
        T value,
        [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value)) return;

        backingStore = value;

        OnPropertyChanged(propertyName);
    }
}