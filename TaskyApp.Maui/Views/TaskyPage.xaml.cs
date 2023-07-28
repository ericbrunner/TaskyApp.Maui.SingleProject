using System.Windows.Input;
using TaskyApp.Contracts;
using TaskyApp.Maui.SingleProject.CustomControls;

namespace TaskyApp.Maui.SingleProject.Views
{
    public partial class TaskyPage : ContentPage
    {
        public TaskyPage()
        {
            InitializeComponent();

            BindingContext = App.Get<ITaskyViewModel>();

            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI) return;
            
            var myButton = new MyButton
            {
                Text = "In Shared Code", 
                TextColor = Colors.Green
            };

            myButton.Clicked += OnMyButtonClicked;

            RootLayout.Add(myButton);
        }

        private void OnMyButtonClicked(object? sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: OnMyButtonClicked called.");

            if (sender is not IMyButton myButton) return;

            myButton.BackgroundColor = Equals(myButton.BackgroundColor, Colors.LightBlue) ? default : Colors.LightBlue;
        }

        private void OnOpenWindowClicked(object? sender, EventArgs e)
        {
            Application.Current.OpenWindow(new Window(new MainPage()));
        }


        #region LongPress and Press Exploration

        void PressableGrid_OnPressed(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(MainPage)}.{nameof(PressableGrid_OnPressed)} invoked");
        }


        private void PressableGrid_OnLongPressed(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(MainPage)}.{nameof(PressableGrid_OnLongPressed)} invoked");
        }
        #endregion
    }
}