using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using TaskyApp.Contracts;
using TaskyApp.Maui.SingleProject.CustomControls;
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
            .ConfigureMauiHandlers((mauiHandlerCollection) =>
            {
#if ANDROID

                #region MyEntry Customization

                // uncomment to use the old xamarin.forms renderer: 'MyEntryRenderer'
                //mauiHandlerCollection.AddCompatibilityRenderer(
                //    typeof(MyEntry),
                //    typeof(TaskyApp.Droid.CustomRenderer.MyEntryRenderer));

                // comment to use the old xamarin.forms renderer: 'MyEntryRenderer'
                //mauiHandlerCollection.AddHandler(
                //    typeof(MyEntry),
                //    typeof(Platforms.Android.Handler.MyEntryHandler));


                Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyEntryCustomization", (handler, entry) =>
                {
                    if (entry is not MyEntry) return;

                    System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: 'MyEntryCustomization' invoked.");

                    handler.PlatformView.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
                });

                #endregion

                mauiHandlerCollection.AddHandler(
                    typeof(MyButton),
                    typeof(Platforms.Android.Handler.MyButtonHandler));

#elif IOS
                #region MyEntry Customization

                // uncomment to use the old xamarin.forms renderer: 'MyEntryRenderer'
                //mauiHandlerCollection.AddCompatibilityRenderer(
                //    typeof(MyEntry),
                //    typeof(TaskyApp.iOS.CustomRenderer.MyEntryRenderer));

                // comment to use the old xamarin.forms renderer: 'MyEntryRenderer'
                //mauiHandlerCollection.AddHandler(
                //    typeof(MyEntry),
                //    typeof(Platforms.iOS.Handler.MyEntryHandler));

                Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyEntryCustomization", (handler, entry) =>
                {
                    if (entry is not MyEntry) return;

                    System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: 'MyEntryCustomization' invoked.");

                    handler.PlatformView.TextColor = UIKit.UIColor.White;
                    handler.PlatformView.BackgroundColor = UIKit.UIColor.Blue;
                    handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.RoundedRect;
                });
                #endregion

                mauiHandlerCollection.AddHandler(
                    typeof(MyButton),
                    typeof(TaskyApp.Maui.SingleProject.Platforms.iOS.Handler.MyButtonHandler));

#endif
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