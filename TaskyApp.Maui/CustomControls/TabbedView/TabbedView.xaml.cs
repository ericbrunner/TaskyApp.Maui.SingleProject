using System.Collections.ObjectModel;
using TaskyApp.Contracts.Models;

namespace TaskyApp.Maui.SingleProject.CustomControls.TabbedView;

public partial class TabbedView : ITabbedView
{
    public TabbedView()
    {
        InitializeComponent();
    }



    public static readonly BindableProperty TestTextProperty = BindableProperty.Create(
        nameof(TestText), 
        typeof(string), 
        typeof(TabbedView), 
        default(string),
        propertyChanged: TestTextPropertyChanged);

    
    private static void TestTextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(TestTextPropertyChanged)} invoked. old: {oldvalue}, new: {newvalue}");

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

    private static void TabItemsPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(TabItemsPropertyChanged)} new: {newvalue}");


        if (bindable is not TabbedView tabbedView) return;
        if (newvalue is not ObservableCollection<ITabItem>  tabItems) return;


        //tabbedView.CollectionView.ItemsSource = new ObservableCollection<ITabItem>(tabItems);
    }

    public ObservableCollection<ITabItem> TabItems   
    {
        get => (ObservableCollection<ITabItem>)GetValue(TabItemsProperty);
        set => SetValue(TabItemsProperty, value);
    }
}

public interface ITabbedView
{
}