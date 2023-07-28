using Foundation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using TaskyApp.Maui.SingleProject.CustomControls;
using UIKit;


namespace TaskyApp.Maui.SingleProject.Platforms.iOS.Legacy.CustomRenderers;

public sealed class PressableViewRenderer : VisualElementRenderer<PressableView>
{
    public PressableViewRenderer()
    {
        UserInteractionEnabled = true;
    }

    private bool _isLongPressRaised;
    private CancellationTokenSource? _cts;
    public override async void TouchesBegan(NSSet touches, UIEvent? evt)
    {
        base.TouchesBegan(touches, evt);

        if (Element == null) return;

        try
        {
            _cts = new CancellationTokenSource();

            await Task.Delay(Element.LongPressDuration, _cts.Token);

            Element.RaiseLongPressed();
            _isLongPressRaised = true;
        }
        catch (TaskCanceledException)
        {
            _isLongPressRaised = false;
            System.Diagnostics.Debug.WriteLine($"LongPressed successfully suppressed");
        }
    }


    public override void TouchesEnded(NSSet touches, UIEvent? evt)
    {
        base.TouchesEnded(touches, evt);

        if (Element == null) return;

        _cts?.Cancel();

        if (!_isLongPressRaised)
        {
            Element.RaisePressed();
        }
        else
        {
            _isLongPressRaised = false;
        }
    }
}
