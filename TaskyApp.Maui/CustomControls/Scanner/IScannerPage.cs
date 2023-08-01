namespace TaskyApp.Maui.SingleProject.CustomControls.Scanner;

public interface IScannerPage
{
    void Init(Action<string> onScanResult, ScannerOptions scannerOptions, ScannerOverlayOptions? overlayOptions = null);
}