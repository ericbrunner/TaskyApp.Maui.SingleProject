using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Handlers;
using TaskyApp.Maui.SingleProject.CustomControls;

namespace TaskyApp.Maui.SingleProject.Platforms.Android.Handler;

public class MyButtonHandler : ViewHandler<IMyButton, AppCompatButton>
{
    public MyButtonHandler() : base(PropertyMap)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: ctor MyButtonHandler");
        
    }

    public MyButtonHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null)
        : base(mapper ?? PropertyMap, commandMapper)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: ctor MyButtonHandler(IPropertyMapper? {nameof(mapper)}{(mapper == null ? "null" : "not null")}, CommandMapper? commandMapper = null)");
    }

    protected override AppCompatButton CreatePlatformView()
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: CreatePlatformView. Native Android '{nameof(AppCompatButton)}' created.");
        return new AppCompatButton(Context);
    }

    private void AppCompatButtonOnClick(object? sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: {nameof(AppCompatButtonOnClick)} invoked.");
        VirtualView?.RaiseClicked();
    }

    protected override void ConnectHandler(AppCompatButton nativeView)
    {
        // Note: Good place for initializing objects, setting event handlers, etc.

        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: ConnectHandler");

        nativeView.Click += AppCompatButtonOnClick;

        base.ConnectHandler(nativeView);
    }

    protected override void DisconnectHandler(AppCompatButton nativeView)
    {
        // Note: Good place for disposing objects, removing event handlers, etc.

        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: DisconnectHandler");

        nativeView.Click -= AppCompatButtonOnClick;
        
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


        handler.PlatformView.SetBackgroundColor(button.BackgroundColor?.AsColor() ?? default);
}

    private static void MapTextColor(MyButtonHandler handler, IMyButton button)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: MapTextColor: {button.TextColor}");

        if (button.TextColor == null) return;
        handler.PlatformView.SetTextColor(button.TextColor.AsColor());
    }

    private static void MapText(MyButtonHandler handler, IMyButton button)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: MapText: {button.Text}");
        handler.PlatformView.Text = button.Text;
    }
}