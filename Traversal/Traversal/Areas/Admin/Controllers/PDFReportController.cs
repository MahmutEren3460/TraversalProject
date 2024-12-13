using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PDFReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StaticPdfReport()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfreport/"+"Dosya1.pdf");
            var stream = new FileStream(path, FileMode.Create);

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document,stream);

            document.Open();

            Paragraph paragraph = new Paragraph("Traversal Rezervasyon Pdf Raporu");

            document.Add(paragraph);
            document.Close();
            string fileName = $"Liste_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            return File("/pdfreport/Dosya1.pdf","application/pdf",fileName);
        }
        public IActionResult StaticCustomerReport()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfreport/" + "Dosya3.pdf");
            var stream = new FileStream(path, FileMode.Create);

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);

            document.Open();

            PdfPTable pdfPTable = new PdfPTable(3);


            pdfPTable.AddCell("Misafir Adı");
            pdfPTable.AddCell("Misafir Soyadı");
            pdfPTable.AddCell("Misafir TC");

            pdfPTable.AddCell("Mahmut");
            pdfPTable.AddCell("Yüce");
            pdfPTable.AddCell("11111111110");

            pdfPTable.AddCell("Kemal");
            pdfPTable.AddCell("Yıldırım");
            pdfPTable.AddCell("22222222222");

            pdfPTable.AddCell("Mustafa");
            pdfPTable.AddCell("Yüce");
            pdfPTable.AddCell("44444444445");

            document.Add(pdfPTable);

            document.Close();
            string fileName = $"Liste_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            return File("/pdfreport/Dosya3.pdf", "application/pdf", fileName);
        }
    }
}
