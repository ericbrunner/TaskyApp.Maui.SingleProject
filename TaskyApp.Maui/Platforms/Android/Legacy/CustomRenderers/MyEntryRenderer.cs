using Android.Content;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

namespace TaskyApp.Maui.SingleProject.Platforms.Android.Legacy.CustomRenderers;

public class MyEntryRenderer : EntryRenderer
{
    public MyEntryRenderer(Context context) : base(context)
    {
    }

    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    {
        base.OnElementChanged(e);

        if (Control == null) return;

        Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
    }
}