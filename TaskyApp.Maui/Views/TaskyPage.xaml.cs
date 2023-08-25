using System.Diagnostics;
using Camera.MAUI;
using Camera.MAUI.ZXingHelper;
using TaskyApp.Contracts;
using TaskyApp.Contracts.Enums;
using TaskyApp.Maui.SingleProject.CustomControls;
using TaskyApp.Maui.SingleProject.CustomControls.Scanner;
using TaskyApp.Maui.SingleProject.Views.Vorgang;

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


        private void OnScanResult(string scannedText)
        {
            Debug.WriteLine("BarcodeText=" + scannedText);

            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Navigation.PopAsync();

                BarcodeResult2.Text = $"{scannedText}";
            });
        }

        private async void Button_OnClicked(object? sender, EventArgs e)
        {
            var scannerPage = App.Get<IScannerPage>();

            if (scannerPage == null) return;

            var scannerOptions = new ScannerOptions
            {
                AutoRotate = false,
                PossibleFormats = { ScannerBarcodeFormat.All_1D, ScannerBarcodeFormat.QR_CODE }
            };

            var overlayOptions = new ScannerOverlayOptions
            {
                TopText = "Please scan code",
                BottomText = "Align the code within the frame"
            };

            scannerPage.Init(OnScanResult, scannerOptions, overlayOptions);

            await Navigation.PushAsync(scannerPage as Page);
        }

        private async void OpenSigningPad(object? sender, EventArgs e)
        {
            var signPage = new SignaturePage();

            await Navigation.PushAsync(signPage);
        }

        private async void OpenCustomTabbedPage(object? sender, EventArgs e)
        {
            var customTabbedPage = new CustomTabbedPage();
            customTabbedPage.ViewModel.TabBarType = TabBarTypeEnum.Bottom;

            await Navigation.PushAsync(customTabbedPage);
        }

        private async void OpenCustomTabbedPage2(object? sender, EventArgs e)
        {
            var customTabbedPage = new CustomTabbedPage();
            customTabbedPage.ViewModel.TabBarType = TabBarTypeEnum.Top;

            await Navigation.PushAsync(customTabbedPage);
        }

        private async void OpenCustomTabbedPage3(object? sender, EventArgs e)
        {
            var customTabbedPage = new CustomTabbedPage();
            customTabbedPage.ViewModel.TabBarType = TabBarTypeEnum.None;

            await Navigation.PushAsync(customTabbedPage);
        }

        private async void OpenTaskPage(object? sender, EventArgs e)
        {
            var taskPage = new VorgangPage();

            await Navigation.PushAsync(taskPage);
        }
    }
}