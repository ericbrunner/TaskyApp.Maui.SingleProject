using Foundation;

namespace TaskyApp.Maui.SingleProject;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp(RegisterPlatformAction);

    private static void RegisterPlatformAction(IServiceCollection? services)
    {
       //TODO add your platform specific implementations to the IoC container
    }
}
