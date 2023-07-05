using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;
using static System.Diagnostics.Debug;


namespace TaskyApp.Maui.SingleProject.Platforms.Android.Legacy.Effects;

public class FocusEffect : PlatformEffect
{
    private readonly global::Android.Graphics.Color _originalBackgroundColor = new(0, 0, 0, 0);
    private global::Android.Graphics.Color _backgroundColor;

    protected override void OnAttached()
    {
        WriteLine($"EFFECT: {nameof(FocusEffect)}.{nameof(OnAttached)} invoked.");

        try
        {
            _backgroundColor = global::Android.Graphics.Color.LightGreen;
            Control.SetBackgroundColor(_backgroundColor);
        }
        catch (Exception e)
        {
            WriteLine($"EFFECT: Can't set property on attached control. Error: {e.Message}");
        }
    }

    protected override void OnDetached()
    {
        WriteLine($"EFFECT: {nameof(FocusEffect)}.{nameof(OnDetached)} invoked.");
    }

    protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
    {
        base.OnElementPropertyChanged(args);

        try
        {
            if (args.PropertyName == null) return;
            if (!args.PropertyName.Equals(nameof(Entry.IsFocused))) return;

            var currentColor = (Control.Background as global::Android.Graphics.Drawables.ColorDrawable)?.Color;

            if (currentColor == null) return;

            Control.SetBackgroundColor(currentColor == _backgroundColor ? _originalBackgroundColor : _backgroundColor);
        }
        catch (Exception e)
        {
            WriteLine($"Can't set property on attached control. Error: {e.Message}");
        }
    }
}