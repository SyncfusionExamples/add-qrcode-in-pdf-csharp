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

    // Create QR barcode instance
    PdfQRBarcode qrBarcode = new PdfQRBarcode();

    // Set QR code properties
    qrBarcode.Text = "https://syncfusion.com";

    //Set the QR size
    qrBarcode.Size = new SizeF(120, 120);

    // Draw the QR code
    qrBarcode.Draw(page.Graphics, new PointF(100, 10));

    //Add text to the PDF document page
    page.Graphics.DrawString("Scan QR Code to Visit Syncfusion", new PdfStandardFont(PdfFontFamily.Helvetica, 20), PdfBrushes.Black, new PointF(0, 150));

    // Save the document
    document.Save("qrcode-in-pdf.pdf");    
}
