using TaskyApp.Maui.SingleProject.CustomControls;

namespace TaskyApp.Maui.SingleProject.Partials;

public partial class PlatformCustomizer
{
    static partial void Handle(IMauiHandlersCollection? mauiHandlerCollection)
    {
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

        #region Register MyButton handler

        mauiHandlerCollection?.AddHandler(
            typeof(MyButton),
            typeof(Platforms.Android.Handler.MyButtonHandler));

        #endregion
    }
}