namespace TaskyApp.Maui.SingleProject.Partials;



public partial class PlatformCustomizer
{
    static partial void Handle(IMauiHandlersCollection? mauiHandlerCollection = null);

    public static void RegisterHandlerAndCallbacks(IMauiHandlersCollection? mauiHandlerCollection = null)
    {
        Handle(mauiHandlerCollection);
    }


    static partial void HandleEffects(IEffectsBuilder? effectsBuilder = null);

    public static void RegisterEffects(IEffectsBuilder? effectsBuilder = null)
    {
        HandleEffects(effectsBuilder);
    }
}