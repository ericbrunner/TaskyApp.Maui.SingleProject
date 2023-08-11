using MvvmHelpers;

namespace TaskyApp.Contracts.Models;

public sealed class TabItem : ObservableObject, ITabItem
{
    private View? _view;

    public View? View => _view ??= ViewFactory?.Invoke();

    private ImageSource? _icon;

    public ImageSource? Icon
    {
        get => _icon;
        set => SetProperty(ref _icon, value);
    }

    private string? _iconFile;

    public string? IconFile
    {
        get => _iconFile;
        set
        {
            if (_iconFile == value) return;
            _iconFile = value;

            if (string.IsNullOrEmpty(_iconFile)) return;

            try
            {
                Icon = ImageSource.FromFile(_iconFile);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
    public Func<View>? ViewFactory { get; set; }


    private bool _isActive;

    public bool IsActive
    {
        get => _isActive;
        set
        {
            SetProperty(ref _isActive, value);

            OnPropertyChanged(nameof(IconOpacity));
        }
    }

    private double _iconHeight;

    public double IconHeight
    {
        get => _iconHeight; 
        set => SetProperty(ref _iconHeight, value);
    }

    private double _iconWidth;

    public double IconWidth
    {
        get => _iconWidth; 
        set => SetProperty(ref _iconWidth, value);
    }


    public double IconOpacity => IsActive ? 1.0 : InactiveIconOpacity;

    public double InactiveIconOpacity { get; set; }
    public ITabItem? ShallowCopy()
    {
        return MemberwiseClone() as ITabItem;
    }
}