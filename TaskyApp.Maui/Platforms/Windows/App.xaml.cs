using TaskyApp.Contracts;
using TaskyApp.Maui.SingleProject.WinUI.Tasky;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TaskyApp.Maui.SingleProject.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
	/// <summary>
	/// Initializes the singleton application object.  This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
		this.InitializeComponent();
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp(RegisterPlatformAction);


    private static void RegisterPlatformAction(IServiceCollection? services)
    {
        services?.AddSingleton<ITaskRunner, TaskRunner>();
    }
}

