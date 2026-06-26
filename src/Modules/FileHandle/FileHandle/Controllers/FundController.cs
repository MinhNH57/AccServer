using Amazon.SimpleNotificationService.Model;
using BoldReports.Web;
using BoldReports.Writer;
using FileHandle.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Pdf;

namespace FileHandle.Controllers;
[Route("fund")]
[ApiController]
public class FundController(IWebHostEnvironment hostingEnvironment, FileHandleDbContext dbContext) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Export(Guid id)
    {
        string rootPath = hostingEnvironment.WebRootPath;
        // Here, we have loaded the sales-order-detail sample report from application the folder wwwroot\Resources.
        FileStream inputStream = new FileStream(rootPath + @"\Resources\DeNghiVay.rdlc", FileMode.Open, FileAccess.Read);
        MemoryStream reportStream = new MemoryStream();
        await inputStream.CopyToAsync(reportStream);
        reportStream.Position = 0;
        inputStream.Close();

        var data = await dbContext.ExcelCatalogObject.Where(c => c.Id == id).ToListAsync();
        if (data is null) throw new NotFoundException("Not found");


        ReportWriter writer = new ReportWriter();

        //writer.ReportErrorOccurred += Writer_ReportErrorOccurred;
        writer.PDFOptions.PdfConformanceLevel = PdfConformanceLevel.Pdf_A1B;
        writer.PDFOptions = new PDFOptions()
        {
            Fonts = new Dictionary<string, Stream>
            {
                {
                    "Times New Roman",
                    new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman regular.ttf", FileMode.Open,
                        FileAccess.Read)
                },
                {
                    "Times New Roman Bold",
                    new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman bold.ttf", FileMode.Open,
                        FileAccess.Read)
                },
                {
                    "Times New Roman Italic",
                    new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman italic.ttf", FileMode.Open,
                        FileAccess.Read)
                },
                {
                    "Times New Roman Bold Italic",
                    new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman bold italic.ttf", FileMode.Open,
                        FileAccess.Read)
                },
            }
        };
        //writer.PDFOptions.Fonts = new Dictionary<string, Stream>
        //{
        //    { "Times New Roman", new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman.ttf", FileMode.Open, FileAccess.Read) },
        //    { "Times New Roman Bold", new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman bold.ttf", FileMode.Open, FileAccess.Read) },
        //    { "Times New Roman Italic",new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman italic.ttf", FileMode.Open, FileAccess.Read) },
        //    { "Times New Roman Bold Italic", new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman bold italic.ttf", FileMode.Open, FileAccess.Read) },
        //    //{ "Roboto", new FileStream(rootPath + @"\fonts\roboto\Roboto.ttf", FileMode.Open, FileAccess.Read) },
        //    //{ "Roboto Bold", new FileStream(rootPath + @"\fonts\roboto\Roboto-Bold.ttf", FileMode.Open, FileAccess.Read) },
        //    //{ "Roboto Bold Regular", new FileStream(rootPath + @"\fonts\roboto\Roboto-Bold.ttf", FileMode.Open, FileAccess.Read) },
        //    //{ "Roboto Light Italic", new FileStream(rootPath + @"\fonts\roboto\Roboto-Light-Italic.ttf", FileMode.Open, FileAccess.Read) },
        //    //{ "Roboto Thin", new FileStream(rootPath + @"\fonts\roboto\Roboto-Thin.ttf", FileMode.Open, FileAccess.Read) }
        //};
        writer.DataSources.Clear();
        writer.DataSources.Add(new ReportDataSource { Name = "ExcelObject", Value = data });

        string fileName = null;
        WriterFormat format;
        string type = null;

        fileName = "denghivay.pdf";
        type = "pdf";
        format = WriterFormat.PDF;

        writer.LoadReport(reportStream);
        MemoryStream memoryStream = new MemoryStream();
        writer.Save(memoryStream, format);

        // Download the generated export document to the client side.
        memoryStream.Position = 0;
        byte[] byteArray = memoryStream.ToArray();
        string base64String = Convert.ToBase64String(byteArray);

        // Return the base64 string as JSON response
        return Ok(new { file = base64String, fileName, mineType = "application/pdf" });
        //FileStreamResult fileStreamResult = new FileStreamResult(memoryStream, "application/" + type)
        //{
        //    FileDownloadName = fileName
        //};
        //return fileStreamResult;
    }


    [HttpGet("export2")]
    public async Task<IActionResult> Export2(Guid id)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "0104111441");
        string rootPath = hostingEnvironment.WebRootPath;
        // Here, we have loaded the sales-order-detail sample report from application the folder wwwroot\Resources.
        await using FileStream inputStream = new(Path.Combine(path, "DeNghiVay.rdlc"), FileMode.Open, FileAccess.Read);

        //FileStream inputStream = new FileStream(rootPath + @"\Resources\DeNghiVay.rdlc", FileMode.Open, FileAccess.Read);
        MemoryStream reportStream = new MemoryStream();
        await inputStream.CopyToAsync(reportStream);
        reportStream.Position = 0;
        inputStream.Close();

        var data = await dbContext.ExcelCatalogObject.Where(c => c.Id == id).ToListAsync();
        if (data is null) throw new NotFoundException("Not found");

        ReportWriter writer = new ReportWriter();

        //writer.ReportErrorOccurred += Writer_ReportErrorOccurred;
        writer.PDFOptions = new PDFOptions();
        writer.PDFOptions.PdfConformanceLevel = PdfConformanceLevel.Pdf_A1B;
        writer.PDFOptions = new PDFOptions()
        {
            Fonts = new Dictionary<string, Stream>
            {
                {
                    "Times New Roman",
                    new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman regular.ttf", FileMode.Open,
                        FileAccess.Read)
                },
                {
                    "Times New Roman Bold",
                    new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman bold.ttf", FileMode.Open,
                        FileAccess.Read)
                },
                {
                    "Times New Roman Italic",
                    new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman italic.ttf", FileMode.Open,
                        FileAccess.Read)
                },
                {
                    "Times New Roman Bold Italic",
                    new FileStream(rootPath + @"\fonts\TimesNewRoman\times new roman bold italic.ttf", FileMode.Open,
                        FileAccess.Read)
                },
            }
        };
        writer.DataSources.Clear();
        writer.DataSources.Add(new ReportDataSource { Name = "ExcelObject", Value = data });

        string fileName = null;
        WriterFormat format;
        string type = null;

        fileName = "denghivay.pdf";
        type = "pdf";
        format = WriterFormat.PDF;

        writer.LoadReport(reportStream);
        MemoryStream memoryStream = new MemoryStream();
        writer.Save(memoryStream, format);

        // Download the generated export document to the client side.
        memoryStream.Position = 0;

        FileStreamResult fileStreamResult = new FileStreamResult(memoryStream, "application/" + type)
        {
            FileDownloadName = fileName
        };
        return fileStreamResult;
    }

    private void Writer_ReportErrorOccurred(object sender, ReportErrorOccurredEventArgs e)
    {
        WriteLogs($"Class Name: {e.ClassName} \n Method Name: {e.MethodName} \n Error Message: {e.Message}");
    }

    internal void WriteLogs(string errorMessage)
    {
        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "ErrorDetails.txt");
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.AutoFlush = true;
            writer.WriteLine(errorMessage);
        }
    }
}
