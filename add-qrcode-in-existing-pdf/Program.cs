using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Parsing;

//Replace "YOUR_LICENSE_KEY" with the key from your Syncfusion account 
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY");

//Load the existing PDF file
using (PdfLoadedDocument loadedDocument = new PdfLoadedDocument("data/input.pdf"))
{

    //Create a QR code
    PdfQRBarcode qrBarcode = new PdfQRBarcode()
    {
        Text = "support@adventure-works.com",
        XDimension = 2.5f
    };

    //Get the first page of the PDF document
    PdfLoadedPage loadedPage = loadedDocument.Pages[0] as PdfLoadedPage;

    //Draw the QR code on that page.
    qrBarcode.Draw(loadedPage, new Syncfusion.Drawing.PointF(50, 710));    

    //Save the document
    loadedDocument.Save("qrcode-in-existing-pdf.pdf");
}



