using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;

//Replace "YOUR_LICENSE_KEY" with the key from your Syncfusion account 
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY");

// Create a new PDF document
using (PdfDocument document = new PdfDocument())
{
    // Add a new page
    PdfPage page = document.Pages.Add();

    string qrText = "Syncfusion";


    // Create QR barcode instance
    PdfQRBarcode qrBarcode = new PdfQRBarcode();

    // Set QR code properties
    qrBarcode.Text = qrText;

    //Set the QR size
    qrBarcode.Size = new SizeF(120, 120);

    //Create logo for QR code
    QRCodeLogo logo = new QRCodeLogo(PdfBitmap.FromStream(new FileStream("data/logo.png", FileMode.Open, FileAccess.Read)));

    //Set logo in qrcode
    qrBarcode.Logo = logo;
        ;

    // Draw the QR code
    qrBarcode.Draw(page.Graphics, new PointF((page.GetClientSize().Width - qrBarcode.Size.Width) / 2, 10));

    //Draw the qrcode text
    PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

    //Messure the text size
    SizeF textSize = font.MeasureString(qrText);

    //Calculate the center point
    float x = (page.GetClientSize().Width - textSize.Width) / 2;

    page.Graphics.DrawString(qrText, font, PdfBrushes.Black, new PointF(x, 130));

    // Save the document
    document.Save("qrcode-with-logo.pdf");
}
