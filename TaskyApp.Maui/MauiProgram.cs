using Camera.MAUI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using TaskyApp.Contracts;
using TaskyApp.Maui.SingleProject.CustomControls;
using TaskyApp.Maui.SingleProject.CustomControls.Scanner;
using TaskyApp.Maui.SingleProject.Partials;
using TaskyApp.Services;
using TaskyApp.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TaskyApp.Maui.SingleProject;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp(Action<IServiceCollection?> platformServices)
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiCompatibility()
            .UseMauiCommunityToolkit()
            .UseMauiApp(serviceProvider => new App(serviceProvider))
            .UseMauiCameraView()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureMauiHandlers(PlatformCustomizer.RegisterHandlerAndCallbacks)
            .ConfigureEffects(PlatformCustomizer.RegisterEffects);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        platformServices?.Invoke(builder.Services);

        builder.Services.AddSingleton<ITodoStore, TodosDataStore>();
        builder.Services.AddTransient<ITaskyViewModel, TaskyViewModel>();

        builder.Services.AddSingleton<IScannerPage, ScannerPage>();

        return builder.Build();
    }
}