using Android.App;
using Android.Runtime;
using TaskyApp.Contracts;
using TaskyApp.Droid.Tasky;
using TaskyApp.Maui.SingleProject;

namespace TaskyApp.Droid
{
    [Application]
    public class MainApplication : MauiApplication
{
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp(RegisterPlatformAction);

        private static void RegisterPlatformAction(IServiceCollection? services)
        {
            services?.AddSingleton<ITaskRunner, TaskRunner>();
        }

    }
}
