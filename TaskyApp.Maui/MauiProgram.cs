
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TaskyApp.Contracts;
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
			.UseMauiCommunityToolkit()
            .UseMauiApp(serviceProvider => new App(serviceProvider))
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		platformServices?.Invoke(builder.Services);

        builder.Services.AddSingleton<ITodoStore, TodosDataStore>();
        builder.Services.AddTransient<ITaskyViewModel, TaskyViewModel>();
		
		return builder.Build();
	}
}
