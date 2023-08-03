using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Core;

namespace TaskyApp.Maui.SingleProject.CustomControls.SignaturePad;

public partial class SignatureDrawingView
{
    public event EventHandler? Cleared;
    public event EventHandler<DrawingLineCompletedEventArgs>? DrawingLineCompleted
    {
        add => DrawingPad.DrawingLineCompleted += value;
        remove => DrawingPad.DrawingLineCompleted -= value;
    }

    public SignatureDrawingView()
    {
        InitializeComponent();

        ClearedCommand = new Command(() =>
        {
            Cleared?.Invoke(this, EventArgs.Empty);

            DrawingPad.Clear();
        });

        BindingContext = this;
    }

    public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
        nameof(LineColor), 
        typeof(Color), 
        typeof(SignatureDrawingView), 
        DrawingViewDefaults.LineColor,
        propertyChanged: LinePropertyChanged);

    public static readonly BindableProperty LineWidthProperty = BindableProperty.Create(
        nameof(LineWidth), 
        typeof(float), 
        typeof(SignatureDrawingView), 
        DrawingViewDefaults.LineWidth,
        propertyChanged: LineWidthPropertyChanged);

    public static readonly BindableProperty IsMultiLineModeEnabledProperty = BindableProperty.Create(
        nameof(IsMultiLineModeEnabled), 
        typeof(bool), 
        typeof(SignatureDrawingView), 
        DrawingViewDefaults.IsMultiLineModeEnabled,
        propertyChanged: IsMultiLineModePropertyChanged);

    public static readonly BindableProperty ClearedTextProperty = BindableProperty.Create(
        nameof(ClearedText), 
        typeof(string), 
        typeof(SignatureDrawingView), 
        "Clear");



    private static void IsMultiLineModePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not SignatureDrawingView signatureDrawingView) return;

        signatureDrawingView.DrawingPad.IsMultiLineModeEnabled = (bool)newvalue;
    }

    private static void LineWidthPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not SignatureDrawingView signatureDrawingView) return;

        signatureDrawingView.DrawingPad.LineWidth = (float)newvalue;
    }

    private static void LinePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not SignatureDrawingView signatureDrawingView) return;

        signatureDrawingView.DrawingPad.LineColor = (Color)newvalue;
    }
    
    public Color LineColor
    {
        get => (Color)GetValue(LineColorProperty);
        set => SetValue(LineColorProperty, value);
    }

    public float LineWidth
    {
        get => (float)GetValue(LineWidthProperty);
        set => SetValue(LineWidthProperty, value);
    }

    public bool IsMultiLineModeEnabled
    {
        get => (bool)GetValue(IsMultiLineModeEnabledProperty);
        set => SetValue(IsMultiLineModeEnabledProperty, value);
    }

    public string ClearedText
    {
        get => (string)GetValue(ClearedTextProperty);
        set => SetValue(ClearedTextProperty, value);
    }

    public ICommand ClearedCommand { get; }
    public ObservableCollection<IDrawingLine> Lines => DrawingPad.Lines;
    public void Clear() => DrawingPad.Clear();

    public ValueTask<Stream> GetImageStream(double imageSizeWidth, double imageSizeHeight) => DrawingPad.GetImageStream(imageSizeWidth, imageSizeHeight);
}