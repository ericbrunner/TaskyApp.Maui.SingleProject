using Microsoft.Maui.Controls.Compatibility.Hosting;
using TaskyApp.Maui.SingleProject.CustomControls;
using TaskyApp.Maui.SingleProject.Platforms.iOS.Legacy.CustomRenderers;

namespace TaskyApp.Maui.SingleProject.Partials;

public partial class PlatformCustomizer
{
    static partial void Handle(IMauiHandlersCollection? mauiHandlerCollection)
    {
        #region MyEntry Customization

        // uncomment to use the old xamarin.forms renderer: 'MyEntryRenderer'
        //mauiHandlerCollection?.AddCompatibilityRenderer(
        //    typeof(MyEntry),
        //    typeof(Platforms.iOS.Legacy.CustomRenderers.MyEntryRenderer));

        // comment to use the old xamarin.forms renderer: 'MyEntryRenderer'
        //mauiHandlerCollection?.AddHandler(
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

        #region Register MyButton handler

        mauiHandlerCollection?.AddHandler(
            typeof(MyButton),
            typeof(Platforms.iOS.Handler.MyButtonHandler));

        #endregion

        mauiHandlerCollection?.AddHandler(
            typeof(PressableView),
            typeof(PressableViewRenderer));
    }


    static partial void HandleEffects(IEffectsBuilder? effectsBuilder)
    {
        effectsBuilder?.Add<Effects.FocusEffect, Platforms.iOS.Legacy.Effects.FocusEffect>();
    }
}