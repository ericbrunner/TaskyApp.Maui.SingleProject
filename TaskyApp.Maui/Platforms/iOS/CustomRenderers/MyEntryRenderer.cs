using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;
using UIKit;

namespace TaskyApp.iOS.CustomRenderer;

public class MyEntryRenderer : EntryRenderer
{
    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    {
        base.OnElementChanged(e);

        if (Control == null) return;
        
        Control.TextColor = UIColor.White;
        Control.BackgroundColor = UIColor.Blue;
        Control.BorderStyle = UITextBorderStyle.RoundedRect;
    }
}