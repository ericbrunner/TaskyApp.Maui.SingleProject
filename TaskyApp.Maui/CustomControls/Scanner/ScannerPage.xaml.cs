using Camera.MAUI.ZXingHelper;
using TaskyApp.Maui.SingleProject.CustomControls.Scanner;
using ZXing;

namespace TaskyApp.Maui.SingleProject.CustomControls;

public partial class ScannerPage : ContentPage, IScannerPage
{
    public ScannerPage()
    {
        InitializeComponent();

        BindingContext = this;
    }


    private void CameraView_CamerasLoaded(object? sender, EventArgs e)
    {
        if (CameraView.Cameras.Count <= 0) return;
        
        CameraView.Camera = CameraView.Cameras.First();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await CameraView.StopCameraAsync();
            await CameraView.StartCameraAsync();
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CameraView.BarCodeDetectionEnabled = true;
        CameraView.CamerasLoaded += CameraView_CamerasLoaded;
        CameraView.BarcodeDetected += CameraView_BarcodeDetected;
    }

    protected override void OnDisappearing()
    {
        CameraView.CamerasLoaded -= CameraView_CamerasLoaded;
        CameraView.BarCodeDetectionEnabled = false;
        CameraView.BarcodeDetected -= CameraView_BarcodeDetected;
        
        base.OnDisappearing();
    }

    private string? _topText;
    private Action<string>? _onScanResult;

    public string? TopText
    {
        get => _topText;
        set
        {
            if (_topText == value) return;

            _topText = value;
            OnPropertyChanged();
        }
    }


    private string? _bottomText;

    public string? BottomText
    {
        get => _bottomText;
        set
        {
            if (BottomText == value) return;

            _bottomText = value;
            OnPropertyChanged();
        }
    }

    public void Init(Action<string> onScanResult, ScannerOptions scannerOptions,
        ScannerOverlayOptions? overlayOptions = null)
    {
        ArgumentNullException.ThrowIfNull(onScanResult,nameof(onScanResult));
        ArgumentNullException.ThrowIfNull(scannerOptions, nameof(scannerOptions));

        _onScanResult = onScanResult;

        CameraView.BarCodeOptions = new BarcodeDecodeOptions()
        {
            AutoRotate = scannerOptions.AutoRotate,
            PossibleFormats = GetPossibleFormats(scannerOptions)
        };

        if (overlayOptions == null) return;
        
        TopText = overlayOptions.TopText;
        BottomText = overlayOptions.BottomText;
    }

    private static IList<BarcodeFormat> GetPossibleFormats(ScannerOptions scannerOptions)
    {
        var formats = new List<BarcodeFormat>();
        
        if (scannerOptions.PossibleFormats.Contains(ScannerBarcodeFormat.All_1D))
        {
            formats.Add(BarcodeFormat.All_1D);
        }

        if (scannerOptions.PossibleFormats.Contains(ScannerBarcodeFormat.QR_CODE))
        {
            formats.Add(BarcodeFormat.QR_CODE);
        }
        
        return formats;
    }

    private void CameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        _onScanResult?.Invoke(args.Result[0].Text);
    }
}