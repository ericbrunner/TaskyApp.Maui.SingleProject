using TaskyApp.Contracts;
using TaskyApp.Maui.SingleProject.Views;
using TaskyApp.ViewModels;

namespace TaskyApp.Maui.SingleProject;

public partial class App : Application
{
        public static TImplementation? Get<TImplementation>() where TImplementation : class => 
            ServiceProvider.GetService(typeof(TImplementation)) as TImplementation;

        public static IServiceProvider ServiceProvider { get; private set; } = default!;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            ServiceProvider = serviceProvider;
            //MainPage = new AppShell();

            MainPage = new NavigationPage(new TaskyPage());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            return window;
        }

        protected override void OnStart()
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now:O}-{nameof(OnStart)} invoked");

            if (!Preferences.Get(TaskyViewModel.GeoLocationWorkloadName, defaultValue: false)) return;
            
            var taskyViewModel = Get<ITaskyViewModel>();
            taskyViewModel?.StartGpsServiceCommand.Execute(null);
        }

        protected override void OnSleep()
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now:O}-{nameof(OnSleep)} invoked");
        }

        protected override void OnResume()
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now:O}-{nameof(OnResume)} invoked");
        }
}
