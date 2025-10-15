using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;

//Replace "YOUR_LICENSE_KEY" with the key from your Syncfusion account 
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY");

//Create a new PDF document
using (PdfDocument document = new PdfDocument())
{
    //Add a page to the document
    PdfPage page = document.Pages.Add();

    // Create and configure advanced QR barcode
    PdfQRBarcode qrBarcode = new PdfQRBarcode()
    {
        Text = "https://www.syncfusion.com/document-sdk",

        // Size customizations
        Size = new SizeF(150, 150),

        //Set the dimension of the barcode.
        XDimension = 5,

        // Error correction configuration
        ErrorCorrectionLevel = PdfErrorCorrectionLevel.High,

        // Encoding mode optimization
        InputMode = InputMode.BinaryMode,

        // Version control (1-40 or Auto)
        Version = QRCodeVersion.Version10,

        // Foreground color for QR pattern
        ForeColor = Color.White,

        // Background color
        BackColor = new PdfColor(46,197, 190),

        //Set quiet zone (margin) around the QR code
        QuietZone = new PdfBarcodeQuietZones() { All = 5 }        
    };

    // Draw customized QR code
    qrBarcode.Draw(page.Graphics, new PointF(100, 10));

    //Draw text below the QR code
    var details = $"""
            QR Code Specifications:
            -> Text: {qrBarcode.Text}            
            -> XDimension: {qrBarcode.XDimension}
            -> Error Correction: {qrBarcode.ErrorCorrectionLevel}
            -> Version: {qrBarcode.Version}
            -> Input Mode: {qrBarcode.InputMode}
            -> ForeColor: RGB({qrBarcode.ForeColor.R}, {qrBarcode.ForeColor.G}, {qrBarcode.ForeColor.B})
            -> BackColor: RGB({qrBarcode.BackColor.R}, {qrBarcode.BackColor.G}, {qrBarcode.BackColor.B})
            -> Quiet Zone: {qrBarcode.QuietZone.All}
            """;

    page.Graphics.DrawString(details, new PdfStandardFont(PdfFontFamily.Courier, 10), new PdfSolidBrush(new PdfColor(51, 51, 51)), new PointF(50, 200));

    //Save the document
    document.Save("qrcode-customization.pdf");
}
