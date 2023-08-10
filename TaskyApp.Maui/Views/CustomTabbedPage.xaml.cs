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
        var view1 = App.Get<ITabItemFactory>()!.Create();
        view1.IconFile = "ic_time_outline.png";
        view1.ViewFactory = () => new View1();


        var view2= App.Get<ITabItemFactory>()!.Create();
        view2.IconFile = "ic_boxes_pallet.png";
        view2.ViewFactory = () => new View2();

        var view3= App.Get<ITabItemFactory>()!.Create();
        view3.IconFile = "ic_checklist.png";
        view3.ViewFactory = () => new View3();

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