using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Handlers;
using TaskyApp.Maui.SingleProject.CustomControls;

namespace TaskyApp.Maui.SingleProject.Platforms.Android.Handler;

public class MyEntryHandler : ViewHandler<IMyEntry, AppCompatEditText>
{
    public static PropertyMapper<IMyEntry, MyEntryHandler> PropertyMap = new()
    {
        [nameof(IMyEntry.Text)] = MapText
    };

    private static void MapText(MyEntryHandler handler, IMyEntry entry)
    {
        System.Diagnostics.Debug.WriteLine("MAUI-HANDLER: Map Text from Cross-Platform 2 Native Control.");

        handler.PlatformView.Text = entry.Text; 
    }

    public MyEntryHandler() : base(PropertyMap)
    {
        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: ctor {nameof(MyEntryHandler)}");
    }

    public MyEntryHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
        System.Diagnostics.Debug.WriteLine(
            $"MAUI-HANDLER: ctor {nameof(MyEntryHandler)}({nameof(IPropertyMapper)}? {nameof(mapper)}{(mapper == null ? "null" : "not null")}, CommandMapper? commandMapper = null)");
    }

    protected override AppCompatEditText CreatePlatformView()
    {
        System.Diagnostics.Debug.WriteLine(
            $"MAUI-HANDLER: CreatePlatformView. Native Android '{nameof(AppCompatEditText)}' created.");
        return new AppCompatEditText(Context);
    }

    protected override void ConnectHandler(AppCompatEditText platformView)
    {
        base.ConnectHandler(platformView);

        System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: {nameof(ConnectHandler)}");

        platformView.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
    }
}