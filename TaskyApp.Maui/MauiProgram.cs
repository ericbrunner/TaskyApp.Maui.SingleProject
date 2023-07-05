using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using TaskyApp.Contracts;
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

        return builder.Build();
    }
}