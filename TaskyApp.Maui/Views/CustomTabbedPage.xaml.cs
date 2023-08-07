using System.Collections.ObjectModel;
using TaskyApp.Contracts.Factories;
using TaskyApp.Contracts.Models;
using TaskyApp.Contracts.ViewModels;
using TaskyApp.Maui.SingleProject.Views.CustomViews;

namespace TaskyApp.Maui.SingleProject.Views;

public partial class CustomTabbedPage
{
    private readonly ICustomTabbedPageViewModel _viewModel;
    public CustomTabbedPage()
    {
        var view1= App.Get<ITabItemFactory>()!.Create(new View1());
        var view2= App.Get<ITabItemFactory>()!.Create(new View2());
        var view3= App.Get<ITabItemFactory>()!.Create(new View3());

        var tabItems = new List<ITabItem>()
        {
            view1,
            view2,
            view3
        };


        _viewModel = App.Get<ICustomTabbedPageViewModelFactory>()!.Create(tabItems);
        _viewModel.Title = "Etiquette";

        InitializeComponent();

        BindingContext = _viewModel;
    }
}