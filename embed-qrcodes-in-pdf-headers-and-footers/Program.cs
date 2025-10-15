using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;

//Replace "YOUR_LICENSE_KEY" with the key from your Syncfusion account 
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY");

// Create a new PDF document and define page settings
using (PdfDocument document = new PdfDocument())
{
    //Set the page margins
    document.PageSettings.Margins.Top = 0;
    document.PageSettings.Margins.Bottom = 0;

    //Add page to the document
    PdfPage page = document.Pages.Add();

    //Get the page size
    SizeF pageSize = page.GetClientSize();

    //Create the header template and add to the document
    document.Template.Top = CreateHeaderTemplate(new SizeF(pageSize.Width, 150));

    //Create the footer template and add to the document
    document.Template.Bottom = CreateFooterTemplate(new SizeF(pageSize.Width, 50));

    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

    //Read the text from the text file
    string text = System.IO.File.ReadAllText("data/input.txt");

    PdfTextElement element = new PdfTextElement(text, font);

    element.Draw(page, new PointF(0, 0));

    //Save the document
    document.Save("qrcode-in-header-and-footer.pdf");

    document.Close(true);
}

//Create header template and return
PdfPageTemplateElement CreateHeaderTemplate(SizeF headerSize)
{
    //Create PdfPageTemplateElement
    PdfPageTemplateElement header = new PdfPageTemplateElement(new RectangleF(0, 0, headerSize.Width, headerSize.Height));
    string headerText = "PDF Succinctly";
    //font
    PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Regular);

    //Measure the text size
    SizeF textSize = font.MeasureString(headerText);

    //Draw the text with center alignment
    header.Graphics.DrawString("PDF Succinctly", font, PdfBrushes.Black, new PointF(0, (headerSize.Height - font.Height) / 2));

    //Create a QR code and draw
    PdfQRBarcode qrBarcode = new PdfQRBarcode()
    {
        Text = "https://www.syncfusion.com/succinctly-free-ebooks/pdf",
        Size = new SizeF(75, 75),
    };

    //Draw the QR code on the header
    qrBarcode.Draw(header.Graphics, new PointF(headerSize.Width - (qrBarcode.Size.Width + 40), (headerSize.Height - qrBarcode.Size.Height) / 2));

    //Draw line to separate header
    header.Graphics.DrawLine(new PdfPen(PdfBrushes.LightGray, 0.5f), new PointF(0, headerSize.Height - 5), new PointF(headerSize.Width, headerSize.Height - 5));

    return header;
}

//Create footer template and return
PdfPageTemplateElement CreateFooterTemplate(SizeF footerSize)
{
    PdfPageTemplateElement footer = new PdfPageTemplateElement(new RectangleF(0, 0, footerSize.Width, footerSize.Height));
    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
    footer.Graphics.DrawString("Copyright © 2001 - 2025 Syncfusion Inc. All Rights Reserved.", font, PdfBrushes.Gray, new PointF(0, (footerSize.Height - font.Height) / 2));
    //Draw line to separate footer
    footer.Graphics.DrawLine(new PdfPen(PdfBrushes.LightGray, 0.5f), new PointF(0, 0), new PointF(footerSize.Width, 0));
    return footer;
}
