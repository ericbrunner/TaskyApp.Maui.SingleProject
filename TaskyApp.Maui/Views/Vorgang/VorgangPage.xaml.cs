

using TaskyApp.Maui.SingleProject.Views.Vorgang.Model;

namespace TaskyApp.Maui.SingleProject.Views.Vorgang
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VorgangPage : ContentPage
	{
		public VorgangPage ()
		{
			InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var view = new AufgabeOverViewV2(OnItemSelectedFunc);
            await view.Init();

            Content = view;
        }

        Task OnItemSelectedFunc(AppData.AufgabeOverviewData data) => DisplayAlert("Vorgang", $"Item {data.Bezeichnung} selected", "Ok");
    }
}