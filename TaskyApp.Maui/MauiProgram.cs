using System.Collections.ObjectModel;
using Camera.MAUI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using TaskyApp.Contracts;
using TaskyApp.Contracts.Factories;
using TaskyApp.Contracts.Models;
using TaskyApp.Contracts.Services;
using TaskyApp.Contracts.ViewModels;
using TaskyApp.Contracts.Views;
using TaskyApp.Maui.SingleProject.CustomControls;
using TaskyApp.Maui.SingleProject.CustomControls.Scanner;
using TaskyApp.Maui.SingleProject.CustomControls.TabbedView;
using TaskyApp.Maui.SingleProject.Factories;
using TaskyApp.Maui.SingleProject.Partials;
using TaskyApp.Maui.SingleProject.Services;
using TaskyApp.Maui.SingleProject.ViewModels;
using TaskyApp.Services;
using TaskyApp.ViewModels;
using TabBar = TaskyApp.Maui.SingleProject.CustomControls.TabbedView.TabBar;

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

        builder.Services.AddSingleton<IMediaService, MediaService>();

        builder.Services.AddTransient<ICustomTabbedPageViewModel, CustomTabbedPageViewModel>();
        builder.Services.AddTransient<ITabItem, TabItem>();

        builder.Services.AddSingleton<ICustomTabbedPageViewModelFactory, CustomTabbedPageViewModelFactory>();
        builder.Services.AddSingleton<ITabItemFactory, TabItemFactory>();

        builder.Services.AddTransient<ITabBar, TabBar>();

        builder.Services.AddTransient<TaskyApp.Contracts.Views.ITabbedView, TabbedView>();
        return builder.Build();
    }
}