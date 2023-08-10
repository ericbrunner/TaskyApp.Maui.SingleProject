using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TaskyApp.Contracts.Models;
using Command = Microsoft.Maui.Controls.Command;

namespace TaskyApp.Maui.SingleProject.CustomControls.TabbedView;

public partial class TabbedView : ITabbedView
{
    public TabbedView()
    {
        #region Init

        SwipeLeftCommand = new AsyncCommand(SwipeLeft);
        SwipeRightCommand = new Command(SwipeRight);

        #endregion

        InitializeComponent();
    }

    private async Task SwipeLeft()
    {
        System.Diagnostics.Debug.WriteLine($"SWIPE - {nameof(SwipeLeft)} current _selectedItemIndex:{SelectedTab}");
        
        if (_itemsSource == null) return;
        if (SelectedTab == -1) return;

        if (SelectedTab == _itemsSource.Count-1) return;

        SelectedTab++;

        var newView = _itemsSource.ElementAt(SelectedTab).View;
        ContainerView.Content = newView;

        System.Diagnostics.Debug.WriteLine($"SWIPE - {nameof(SwipeLeft)} new _selectedItemIndex:{SelectedTab}");
    }

    private void SwipeRight()
    {
        System.Diagnostics.Debug.WriteLine($"SWIPE - {nameof(SwipeRight)} current _selectedItemIndex:{SelectedTab}");

        if (_itemsSource == null) return;
        if (SelectedTab <= 0) return;

        SelectedTab--;
        ContainerView.Content = _itemsSource.ElementAt(SelectedTab).View;

        System.Diagnostics.Debug.WriteLine($"SWIPE - {nameof(SwipeRight)} new _selectedItemIndex:{SelectedTab}");
    }


    public static readonly BindableProperty TestTextProperty = BindableProperty.Create(
        nameof(TestText),
        typeof(string),
        typeof(TabbedView),
        default(string),
        propertyChanged: TestTextPropertyChanged);


    private static void TestTextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        System.Diagnostics.Debug.WriteLine(
            $"{nameof(TestTextPropertyChanged)} invoked. old: {oldvalue}, new: {newvalue}");

        if (bindable is not TabbedView tabbedView) return;

        //tabbedView.Label.Text = newvalue as string;
    }


    public string? TestText
    {
        get => (string?)GetValue(TestTextProperty);
        set => SetValue(TestTextProperty, value);
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

public interface ITabbedView
{
    ObservableCollection<ITabItem> TabItems { get; set; }
    ICommand SwipeRightCommand { get; }
    ICommand SwipeLeftCommand { get; }
}