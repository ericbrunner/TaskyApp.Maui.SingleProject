using CommunityToolkit.Maui.Views;
using MvvmHelpers;

namespace TaskyApp.Contracts.Models;

public class TabItem : ObservableObject, ITabItem
{
    private View? _view;
    private Func<View> _viewFactory;

    public View View => _view ??= _viewFactory();

    public void Init(Func<View> viewFactory)
    {
        _viewFactory = viewFactory;
    }
}