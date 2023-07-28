using Android.Content;
using Android.Views;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using TaskyApp.Maui.SingleProject.CustomControls;

namespace TaskyApp.Maui.SingleProject.Platforms.Android.Legacy.CustomRenderers;

public sealed class PressableViewRenderer : VisualElementRenderer<PressableView>
{
    public PressableViewRenderer(Context context) : base(context)
    {
        Touch += Control_Touch;
    }

    private bool _isLongPressRaised;
    private CancellationTokenSource? _cts;

    private async void Control_Touch(object? sender, TouchEventArgs e)
    {
        if (Element == null || e.Event == null) return;


        switch (e.Event.Action)
        {
            case MotionEventActions.Down:

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
                    System.Diagnostics.Debug.WriteLine("LongPressed successfully suppressed");
                }

                break;

            case MotionEventActions.Up:

                _cts?.Cancel();

                if (!_isLongPressRaised)
                {
                    Element.RaisePressed();
                }
                else
                {
                    _isLongPressRaised = false;
                }

                break;

            case MotionEventActions.ButtonPress:
                break;
            case MotionEventActions.ButtonRelease:
                break;
            case MotionEventActions.Cancel:
                break;
            case MotionEventActions.HoverEnter:
                break;
            case MotionEventActions.HoverExit:
                break;
            case MotionEventActions.HoverMove:
                break;
            case MotionEventActions.Mask:
                break;
            case MotionEventActions.Move:
                break;
            case MotionEventActions.Outside:
                break;
            case MotionEventActions.Pointer1Down:
                break;
            case MotionEventActions.Pointer1Up:
                break;
            case MotionEventActions.Pointer2Down:
                break;
            case MotionEventActions.Pointer2Up:
                break;
            case MotionEventActions.Pointer3Down:
                break;
            case MotionEventActions.Pointer3Up:
                break;
            case MotionEventActions.PointerIdMask:
                break;
            case MotionEventActions.PointerIdShift:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}