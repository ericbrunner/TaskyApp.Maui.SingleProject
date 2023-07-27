using Foundation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using TaskyApp.Maui.SingleProject.CustomControls;
using UIKit;


namespace TaskyApp.Maui.SingleProject.Platforms.iOS.Legacy.CustomRenderers;

public class PressableViewRenderer : VisualElementRenderer<PressableView>
{
    public PressableViewRenderer()
    {
        UserInteractionEnabled = true;
    }

    private bool isLongPressRaised = false;
    private CancellationTokenSource? _cts;
    public override async void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);

        if (Element == null) return;

        System.Diagnostics.Debug.WriteLine($"{nameof(TouchesBegan)} occured - e: {evt.Type}");

        try
        {
            _cts = new CancellationTokenSource();

            await Task.Delay(Element.LongPressDuration, _cts.Token);

            Element.RaiseLongPressed();
            isLongPressRaised = true;
        }
        catch (Exception exception)
        {
            isLongPressRaised = false;
            System.Diagnostics.Debug.WriteLine(exception);
        }
    }

    public override void TouchesCancelled(NSSet touches, UIEvent evt)
    {
        base.TouchesCancelled(touches, evt);

        //

        System.Diagnostics.Debug.WriteLine($"{nameof(TouchesCancelled)} occured - e: {evt.Type}");
    }

    public override void TouchesEnded(NSSet touches, UIEvent evt)
    {
        base.TouchesEnded(touches, evt);

        if (Element == null) return;

        System.Diagnostics.Debug.WriteLine($"{nameof(TouchesEnded)} occured - e: {evt.Type}");

        _cts?.Cancel();

        if (!isLongPressRaised)
        {
            Element.RaisePressed();
        }
        else
        {
            isLongPressRaised = false;
        }
    }
}
