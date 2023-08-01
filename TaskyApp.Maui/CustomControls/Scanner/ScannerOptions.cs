namespace TaskyApp.Maui.SingleProject.CustomControls.Scanner;

public class ScannerOptions
{
    public bool AutoRotate { get; init; }

    public IList<ScannerBarcodeFormat> PossibleFormats { get; init; } = new List<ScannerBarcodeFormat>();
}