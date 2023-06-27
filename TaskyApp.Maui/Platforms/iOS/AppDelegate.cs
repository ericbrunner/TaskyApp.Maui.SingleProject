
using Foundation;
using TaskyApp.Contracts;
using TaskyApp.iOS.Tasky;

namespace TaskyApp.Maui.SingleProject;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp(RegisterPlatformAction);

    private static void RegisterPlatformAction(IServiceCollection? services)
    {
        services?.AddSingleton<ITaskRunner, TaskRunner>();
    }
}
