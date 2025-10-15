using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

//Replace "YOUR_LICENSE_KEY" with the key from your Syncfusion account 
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY");

//Create event ticket with QR code
CreateEventTicketWithQRCode();

//Create invoice PDF with QR code
InvoicePDFWithQRCode();

static void CreateEventTicketWithQRCode()
{
    //Create a new PDF document
    using (PdfDocument document = new PdfDocument())
    {
        // Set page size
        document.PageSettings.Size = new SizeF(468, 180);
        document.PageSettings.Margins.All = 0;

        // Add a page
        PdfPage page = document.Pages.Add();

        // Draw ticket border
        page.Graphics.DrawRectangle(new PdfPen(PdfBrushes.Black, 2), 20, 20, page.GetClientSize().Width - 40, page.GetClientSize().Height - 40);

        //Create font
        PdfFont titleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
        PdfFont detailFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12);        

        // Event Title
        page.Graphics.DrawString("Tech Innovators Summit 2025", titleFont, PdfBrushes.DarkBlue, new PointF(30, 30));

        // Event Details
        page.Graphics.DrawString("Date: October 25, 2025", detailFont, PdfBrushes.Black, new PointF(30, 70));
        page.Graphics.DrawString("Time: 10:00 AM - 4:00 PM", detailFont, PdfBrushes.Black, new PointF(30, 90));
        page.Graphics.DrawString("Venue: Grand Convention Center, Chennai", detailFont, PdfBrushes.Black, new PointF(30, 110));
        page.Graphics.DrawString("Ticket ID: TIS2025-00123", detailFont, PdfBrushes.Black, new PointF(30, 130));

        // Generate QR Code
        PdfQRBarcode qrCode = new PdfQRBarcode();
        qrCode.InputMode = InputMode.BinaryMode;
        qrCode.Version = QRCodeVersion.Auto;
        qrCode.XDimension = 3;
        qrCode.Text = "https://eventcheckin.com/ticket/TIS2025-00123";

        // Draw QR Code on the ticket
        qrCode.Draw(page, new PointF(330, 40));

        // Save the document
        document.Save("event-ticket.pdf");
    }    
}
static void InvoicePDFWithQRCode()
{
    // Create a new PDF document
    using (PdfDocument document = new PdfDocument())
    {
        //Add a page
        PdfPage page = document.Pages.Add();

        // Set fonts
        PdfFont headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold);
        PdfFont labelFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

        // Draw header
        page.Graphics.DrawString("INVOICE", headerFont, PdfBrushes.DarkBlue, new PointF(30, 30));
        page.Graphics.DrawString("Invoice #: INV2025-04567", labelFont, PdfBrushes.Black, new PointF(30, 60));
        page.Graphics.DrawString("Date: October 15, 2025", labelFont, PdfBrushes.Black, new PointF(30, 80));
        page.Graphics.DrawString("Client: Sync Innovations Ltd.", labelFont, PdfBrushes.Black, new PointF(30, 100));

        // Create a PDF grid
        PdfGrid grid = new PdfGrid();

        // Set grid style
        grid.Style.CellPadding = new PdfPaddings(5, 5, 5, 5);

        // Add columns
        grid.Columns.Add(4);
        // Add headers
        grid.Headers.Add(1);
        PdfGridRow header = grid.Headers[0];
        header.Cells[0].Value = "Product";
        header.Cells[1].Value = "Quantity";
        header.Cells[2].Value = "Unit Price";
        header.Cells[3].Value = "Total";

        // Add rows
        PdfGridRow row1 = grid.Rows.Add();
        row1.Cells[0].Value = "Consulting Services";
        row1.Cells[1].Value = "1";
        row1.Cells[2].Value = "$2,500.00";
        row1.Cells[3].Value = "$2,500.00";

        PdfGridRow row2 = grid.Rows.Add();
        row2.Cells[0].Value = "Software License";
        row2.Cells[1].Value = "1";
        row2.Cells[2].Value = "$1,026.90";
        row2.Cells[3].Value = "$1,026.90";

        //Apply built-in style
        grid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

        // Draw grid
        PdfLayoutResult result = grid.Draw(page, new RectangleF(30, 130, page.GetClientSize().Width - 30, page.GetClientSize().Height - 130));

        // Total amount
        page.Graphics.DrawString("Total: $3,526.90", labelFont, PdfBrushes.DarkBlue, new PointF(400, result.Bounds.Bottom + 50));

        // QR Code for payment
        PdfQRBarcode qrCode = new PdfQRBarcode();
        qrCode.Text = "https://paynow.com/invoice/INV2025-04567";
        qrCode.XDimension = 3;
        qrCode.Version = QRCodeVersion.Auto;
        qrCode.InputMode = InputMode.BinaryMode;
        qrCode.Draw(page, new PointF(400, result.Bounds.Bottom + 80));

        // Add payment instruction text to the left of the QR code
        page.Graphics.DrawString("Pay using the QR code ->", new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Italic), PdfBrushes.Black, new PointF(260, result.Bounds.Bottom + 120));

        // Save the document
        document.Save("invoice.pdf");
    }
}
