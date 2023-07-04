namespace TaskyApp.Maui.SingleProject.Partials;



public partial class PlatformCustomizer
{
    static partial void Handle(IMauiHandlersCollection? mauiHandlerCollection = null);

    public static void RegisterHandlerAndCallbacks(IMauiHandlersCollection? mauiHandlerCollection = null)
    {
        Handle(mauiHandlerCollection);
    }
}