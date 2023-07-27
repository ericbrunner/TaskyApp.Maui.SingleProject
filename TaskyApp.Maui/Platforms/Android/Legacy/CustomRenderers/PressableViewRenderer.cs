using Android.Content;
using Android.Views;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using TaskyApp.Maui.SingleProject.CustomControls;

namespace TaskyApp.Maui.SingleProject.Platforms.Android.Legacy.CustomRenderers;

public class PressableViewRenderer : VisualElementRenderer<PressableView>
{
    public PressableViewRenderer(Context context) : base(context)
    {
        Touch += Control_Touch;
    }

    private bool isLongPressRaised = false;
    private CancellationTokenSource? _cts;

    private async void Control_Touch(object sender, TouchEventArgs e)
    {
        if (Element == null) return;

        System.Diagnostics.Debug.WriteLine($"Control_Touch occured - e: {e.Handled}, {e.Event?.Action}");


        switch (e.Event.Action)
        {
            case MotionEventActions.Down:

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

                break;

            case MotionEventActions.Up:

                _cts?.Cancel();

                if (!isLongPressRaised)
                {
                    Element.RaisePressed();
                }
                else
                {
                    isLongPressRaised = false;
                }

                break;

            default:
                break;
        }
    }
}