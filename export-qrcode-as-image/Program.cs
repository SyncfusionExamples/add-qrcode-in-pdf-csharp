using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;

// Create QR barcode instance
PdfQRBarcode qrBarcode = new PdfQRBarcode();

// Set QR code properties
qrBarcode.Text = "https://syncfusion.com";

qrBarcode.XDimension = 8;

//Export the QR code as image
Stream qrCodeImage = qrBarcode.ToImage();

//Save the image stream to file
using (FileStream fileStream = new FileStream("qrcode.png", FileMode.OpenOrCreate, FileAccess.ReadWrite))
{
    qrCodeImage.CopyTo(fileStream);
}
