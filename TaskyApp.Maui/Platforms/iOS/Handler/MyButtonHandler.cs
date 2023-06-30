using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Handlers;
using TaskyApp.Maui.SingleProject.CustomControls;
using UIKit;

namespace TaskyApp.Maui.SingleProject.Platforms.iOS.Handler;

public class MyButtonHandler : ViewHandler<IMyButton, UIButton>
{
    public MyButtonHandler(): base(PropertyMap)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: ctor MyButtonHandler (default)");
    }

    public MyButtonHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null) 
        : base(mapper ?? PropertyMap, commandMapper)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: ctor MyButtonHandler(IPropertyMapper? {nameof(mapper)}{(mapper == null ? "null" : "not null")}, CommandMapper? commandMapper = null)");
    }


    protected override UIButton CreatePlatformView()
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: CreatePlatformView. Native iOS '{nameof(UIButton)}' created.");
        return new UIButton();
    }

    protected override void ConnectHandler(UIButton nativeView)
    {
        // Note: Good place for initializing objects, setting event handlers, etc.

        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: ConnectHandler");

        nativeView.TouchDown += NativeView_TouchDown;

        base.ConnectHandler(nativeView);
    }

    private void NativeView_TouchDown(object? sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: {nameof(NativeView_TouchDown)} invoked.");
        VirtualView?.RaiseClicked();
    }

    protected override void DisconnectHandler(UIButton nativeView)
    {
        // Note: Good place for disposing objects, removing event handlers, etc.

        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: DisconnectHandler");

        nativeView.TouchDown -= NativeView_TouchDown;
        
        base.DisconnectHandler(nativeView);
    }

    public static PropertyMapper<IMyButton, MyButtonHandler> PropertyMap = new(ViewHandler.ViewMapper)
    {
        [nameof(IMyButton.Text)] = MapText,
        [nameof(IMyButton.TextColor)] = MapTextColor,
        [nameof(IMyButton.BackgroundColor)] = MapBackgroundColor
    };

    private static void MapBackgroundColor(MyButtonHandler handler, IMyButton button)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: Map BackgroundColor");

        handler.PlatformView.BackgroundColor = button.BackgroundColor?.AsUIColor() ?? default;
    }

    private static void MapTextColor(MyButtonHandler handler, IMyButton button)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: MapTextColor: {button.TextColor}");
        handler.PlatformView.SetTitleColor(button.TextColor.AsUIColor(), UIControlState.Normal);
    }

    private static void MapText(MyButtonHandler handler, IMyButton button)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: MapText: {button.Text}");
        handler.PlatformView.SetTitle(button.Text, UIControlState.Normal);
    }
}