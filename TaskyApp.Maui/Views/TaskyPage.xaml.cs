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
        }

        private void OnMyButtonClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"MAUI-HANDLER: OnMyButtonClicked called.");

            if (sender is not IMyButton myButton) return;
            
            myButton.BackgroundColor = Equals(myButton.BackgroundColor, Colors.LightBlue) ? default : Colors.LightBlue;

        }
    }
}