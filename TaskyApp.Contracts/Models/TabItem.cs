using MvvmHelpers;

namespace TaskyApp.Contracts.Models;

public class TabItem : ObservableObject, ITabItem
{
    private View? _view;

    public View? View
    {
        get => _view;
        set => SetProperty(ref _view, value);
    }

    public void Init(View view)
    {
        View = view;
    }
}