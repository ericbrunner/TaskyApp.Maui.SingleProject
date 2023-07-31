namespace TaskyApp.Maui.SingleProject.CustomControls;

public partial class BadgeView : ContentView
{
    public BadgeView()
    {
        InitializeComponent();

        BindingContext = this;
    }


    public static readonly BindableProperty AutoHideProperty = BindableProperty.Create(
        nameof(AutoHide),
        typeof(bool),
        typeof(BadgeView),
        false);
    
    public static readonly BindableProperty BadgeTextProperty = BindableProperty.Create(
        nameof(BadgeText),
        typeof(string),
        typeof(BadgeView),
        string.Empty);

    public static readonly BindableProperty BadgeTextColorProperty = BindableProperty.Create(
        nameof(BadgeTextColor),
        typeof(Color),
        typeof(BadgeView),
        Colors.White);

    public static readonly BindableProperty BadgeBackgroundColorProperty = BindableProperty.Create(
        nameof(BadgeBackgroundColor), 
        typeof(Color), 
        typeof(BadgeView), 
        Colors.Crimson);

    public static readonly BindableProperty BadgeFontSizeProperty = BindableProperty.Create(
        nameof(BadgeFontSize), 
        typeof(double), 
        typeof(BadgeView), 
        11d);

    public bool AutoHide
    {
        get => (bool)GetValue(AutoHideProperty);
        set => SetValue(AutoHideProperty, value);
    }

    public string? BadgeText
    {
        get => (string)GetValue(BadgeTextProperty);
        set => SetValue(BadgeTextProperty, value);
    }

    public Color BadgeTextColor
    {
        get => (Color)GetValue(BadgeTextColorProperty);
        set => SetValue(BadgeTextColorProperty, value);
    }


    public Color BadgeBackgroundColor
    {
        get => (Color)GetValue(BadgeBackgroundColorProperty);
        set => SetValue(BadgeBackgroundColorProperty, value);
    }

    public double BadgeFontSize
    {
        get => (double)GetValue(BadgeFontSizeProperty);
        set => SetValue(BadgeFontSizeProperty, value);
    }
}