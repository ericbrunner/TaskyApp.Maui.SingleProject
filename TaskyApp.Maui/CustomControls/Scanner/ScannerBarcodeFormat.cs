namespace TaskyApp.Maui.SingleProject.CustomControls.Scanner;

[Flags]
public enum ScannerBarcodeFormat
{
    /// <summary>CODABAR 1D format.</summary>
    CODABAR = 2,
    /// <summary>Code 39 1D format.</summary>
    CODE_39 = 4,
    /// <summary>Code 93 1D format.</summary>
    CODE_93 = 8,
    /// <summary>Code 128 1D format.</summary>
    CODE_128 = 16, // 0x00000010
    /// <summary>EAN-8 1D format.</summary>
    EAN_8 = 64, // 0x00000040
    /// <summary>EAN-13 1D format.</summary>
    EAN_13 = 128, // 0x00000080
    /// <summary>ITF (Interleaved Two of Five) 1D format.</summary>
    ITF = 256, // 0x00000100
    /// <summary>RSS 14</summary>
    RSS_14 = 4096, // 0x00001000
    /// <summary>RSS EXPANDED</summary>
    RSS_EXPANDED = 8192, // 0x00002000
    /// <summary>UPC-A 1D format.</summary>
    UPC_A = 16384, // 0x00004000
    /// <summary>UPC-E 1D format.</summary>
    UPC_E = 32768, // 0x00008000

    /// <summary>QR Code 2D barcode format.</summary>
    QR_CODE = 2048, // 0x00000800

    /// <summary>
    /// UPC_A | UPC_E | EAN_13 | EAN_8 | CODABAR | CODE_39 | CODE_93 | CODE_128 | ITF | RSS_14 | RSS_EXPANDED
    /// without MSI (to many false-positives) and IMB (not enough tested, and it looks more like a 2D)
    /// </summary>
    All_1D = UPC_E | UPC_A | RSS_EXPANDED | RSS_14 | ITF | EAN_13 | EAN_8 | CODE_128 | CODE_93 | CODE_39 | CODABAR, // 0x0000F1DE
}