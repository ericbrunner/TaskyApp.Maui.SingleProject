using System.Diagnostics;
using Camera.MAUI;
using Camera.MAUI.ZXingHelper;
using TaskyApp.Contracts;
using TaskyApp.Maui.SingleProject.CustomControls;

namespace TaskyApp.Maui.SingleProject.Views
{
    public partial class TaskyPage : ContentPage
    {
        private readonly Label barcodeResult;
        private readonly CameraView _cameraView;

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


            #region Camera.Maui

            barcodeResult = new Label { FontSize = 20 };
            RootLayout.Add(barcodeResult);

            _cameraView = new CameraView { WidthRequest = 300, HeightRequest = 200 };

            _cameraView.CamerasLoaded += CameraView_OnCamerasLoaded;
            _cameraView.BarcodeDetected += CameraView_BarcodeDetected;
            _cameraView.BarCodeOptions = new BarcodeDecodeOptions
            {
                PossibleFormats = { ZXing.BarcodeFormat.All_1D, ZXing.BarcodeFormat.QR_CODE }
            };

            _cameraView.BarCodeDetectionEnabled = true;

            RootLayout.Add(_cameraView);
            #endregion
        }

        #region Camera.Maui

        private void CameraView_BarcodeDetected(object sender, BarcodeEventArgs args)
        {
            Debug.WriteLine("BarcodeText=" + args.Result[0].Text);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                barcodeResult.Text = $"{args.Result[0].BarcodeFormat}: {args.Result[0].Text}";
            });
        }


        private void CameraView_OnCamerasLoaded(object? sender, EventArgs e)
        {
            if (_cameraView.Cameras.Count > 0)
            {
                _cameraView.Camera = _cameraView.Cameras.First();

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _cameraView.StopCameraAsync();
                    await _cameraView.StartCameraAsync();
                });
            }
        }

        #endregion

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