using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;
using UIKit;
using static System.Diagnostics.Debug;


namespace TaskyApp.Maui.SingleProject.Platforms.iOS.Legacy.Effects;

public class FocusEffect : PlatformEffect
{
    private UIColor backgroundColor;

    protected override void OnAttached()
    {
        WriteLine($"EFFECT: {nameof(FocusEffect)}.{nameof(OnAttached)} invoked.");

        try
        {
            backgroundColor = UIColor.FromRGB(210, 153, 245);
            Control.BackgroundColor = backgroundColor;
        }
        catch (Exception e)
        {
            WriteLine($"Can't set property on attached control. Error: {e.Message}");
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
            if (!args.PropertyName.Equals(nameof(Entry.IsFocused))) return;

            Control.BackgroundColor = Control.BackgroundColor == backgroundColor ? UIColor.White : backgroundColor;
        }
        catch (Exception e)
        {
            WriteLine($"Can't set property on attached control. Error: {e.Message}");
        }
    }
}