using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskyApp.Contracts.Models;
using Command = Microsoft.Maui.Controls.Command;
using ITabbedView = TaskyApp.Contracts.Views.ITabbedView;

namespace TaskyApp.Maui.SingleProject.CustomControls.TabbedView;

public sealed partial class TabbedView : ITabbedView, IDisposable
{
    public TabbedView()
    {
        #region Init

        SwipeLeftCommand = new Command(SwipeLeft);
        SwipeRightCommand = new Command(SwipeRight);

        PropertyChanged += OnPropertyChanged;

        #endregion

        InitializeComponent();
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        System.Diagnostics.Debug.Write(
            $"{nameof(PropertyChanged)} - sender:{sender?.GetType().Name} / Changed Property: {e.PropertyName}");

        if (nameof(SelectedTab).Equals(e.PropertyName))
        {
            if (_itemsSource == null) return;
            ContainerView.Content = _itemsSource.ElementAt(SelectedTab).View;

            System.Diagnostics.Debug.WriteLine($" == {SelectedTab}");
        }
    }

    public void Dispose()
    {
        PropertyChanged -= OnPropertyChanged;
    }

    private void SwipeLeft()
    {
        System.Diagnostics.Debug.WriteLine($"SWIPE - {nameof(SwipeLeft)} current _selectedItemIndex:{SelectedTab}");

        if (_itemsSource == null) return;
        if (SelectedTab == -1) return;

        if (SelectedTab == _itemsSource.Count - 1) return;

        SelectedTab++;
    }

    private void SwipeRight()
    {
        System.Diagnostics.Debug.WriteLine($"SWIPE - {nameof(SwipeRight)} current _selectedItemIndex:{SelectedTab}");

        if (SelectedTab <= 0) return;

        SelectedTab--;
    }


    public static readonly BindableProperty TabItemsProperty = BindableProperty.Create(
        nameof(TabItems),
        typeof(ObservableCollection<ITabItem>),
        typeof(TabbedView),
        propertyChanged: TabItemsPropertyChanged);


    private List<ITabItem>? _itemsSource;

    private static void TabItemsPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(TabItemsPropertyChanged)} new: {newvalue}");


        if (bindable is not TabbedView tabbedView) return;
        if (newvalue is not ObservableCollection<ITabItem> tabItems) return;

        tabbedView._itemsSource = tabItems.ToList();

        var firstContent = tabbedView._itemsSource.FirstOrDefault();

        if (firstContent == null) return;

        tabbedView.ContainerView.Content = firstContent.View;
        tabbedView.SelectedTab = 0;
    }

    public ObservableCollection<ITabItem> TabItems
    {
        get => (ObservableCollection<ITabItem>)GetValue(TabItemsProperty);
        set => SetValue(TabItemsProperty, value);
    }

    public ICommand SwipeRightCommand { get; }
    public ICommand SwipeLeftCommand { get; }


    private int _selectedTab = -1;

    public int SelectedTab
    {
        get => _selectedTab;
        set => SetProperty(ref _selectedTab, value);
    }

    private void SetProperty<T>(ref T backingStore,
        T value,
        [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value)) return;

        backingStore = value;

        OnPropertyChanged(propertyName);
    }
}