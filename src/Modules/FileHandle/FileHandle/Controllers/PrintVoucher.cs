using System.Data;
using AsyncDapperExtensions;
using BoldReports.Web;
using BoldReports.Writer;
using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using Dapper;
using FileHandle.Data;
using FileHandle.Data.Entites;
using FileHandle.Extensions;
using FileHandle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Pdf;

namespace FileHandle.Controllers;

[Route("report")]
[ApiController]
[Authorize]
public class PrintVoucher(IWebHostEnvironment hostingEnvironment, ICurrentUser currentUser, SmartDataServices smartDataServices, FileHandleDbContext dbContext) : ControllerBase
{
    private const string ReportsFolder = "Reports";
    [HttpPost("print")]
    public async Task<IActionResult> Print(PrintVoucherRequest request)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), ReportsFolder, currentUser.TenantId!);
        string rootPath = hostingEnvironment.WebRootPath;
        await using var reportStream = await LoadFileToMemory(path, request.Parameter, request.BankCode, request.IsForeignCurrency , request.filePath);
        ReportWriter writer = new ReportWriter();
        //writer.ReportErrorOccurred += Writer_ReportErrorOccurred;
        ConfigPDFOption(writer, rootPath);

        var userParameters = AddParameter(request);

        string fileName = null;
        WriterFormat format;
        string type = null;

        fileName = "voucher.pdf";
        type = "pdf";
        format = WriterFormat.PDF;

        writer.LoadReport(reportStream);
        writer.SetParameters(userParameters);
        MemoryStream memoryStream = new MemoryStream();
        writer.Save(memoryStream, format);

        // Download the generated export document to the client side.
        memoryStream.Position = 0;
        //var buffer = memoryStream.ToArray();
        return File(memoryStream, "application/pdf");
       
        //var test = Convert.ToBase64String(buffer);
        //return Ok(Convert.ToBase64String(buffer));
    }

    [HttpGet]
    [Route("test-export")]
    public async Task<IActionResult> ExportData(CancellationToken cancellationToken)
    {
        string storeName = "AccountDetailsBook";
        string pathTemplate = @"C:\Users\smartdev_datmt\Documents\TestExcel\SoChiTietTaiKhoanTheoDoiTuong.xlsx";
        string pathExport = @"C:\Users\smartdev_datmt\Documents\TestExcel\Test.xlsx";
        var paramStore = new
        {
            Parameter = "DetailAcount",
            Id = "",
            UserCode = currentUser.CodeUser,
            CodeUnit = currentUser.CodeUnit,
            AccountSymbol = "1561",
            BeginDate = "01/01/2025",
            EndDate = "10/30/2025",
            Date = "",
            PathImages = "",
            PathLogo = "",
            WareHouseCode = "",
            ProductCode = "",
            SmartSoftware = ""
        };
        var connection = dbContext.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }

        await using var reader = await connection.ExecuteReaderAsync(storeName, paramStore, commandType: CommandType.StoredProcedure);
        var dt = new DataTable("ReportAccountDetailsBook");
        dt.Load(reader);
        var ds = new DataSet();
        ds.Tables.Add(dt);
        var info = new DataTable("Smart");
        info.Columns.Add("SuperiorName");
        info.Columns.Add("SubordinateName");
        info.Columns.Add("PositionDir");
        info.Columns.Add("PositionAcc");
        info.Columns.Add("PositionReport");
        info.Columns.Add("DirectorName");
        info.Columns.Add("AccName");
        info.Columns.Add("ReporterName");
        info.Rows.Add("", "", "", "", "", "", "", "");
        TemplateExcel.FillReport(pathExport, pathTemplate, ds, ["{", "}"]);
        return Ok();
    }

    private async Task<Stream> LoadFileToMemory(
        string path,
        string param,
        string? bankCode = null,
        bool? isForeignCurrency = null,
        string? fileDoc = null)
    {
        string fileName = null;

        if (!string.IsNullOrEmpty(fileDoc))
        {
            fileName = fileDoc;
        }
        else
        {
            fileName = param switch
            {
                "UNC" => await GetUNCFile(param, bankCode, isForeignCurrency),
                "OrderBuy" => await GetOrderBuyFile(param, isForeignCurrency),
                _ => await GetDefaultFile(param)
            };
        }

        var fullPath = Path.Combine(path, fileName);

        if (!System.IO.File.Exists(fullPath))
            throw new FileNotFoundException($"Không tìm thấy file report: {fullPath}");

        return new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 64 * 1024, true);
    }


    private async Task<string> GetUNCFile(string param, string? bankCode, bool? isForeignCurrency)
    {
        var optionReport = await dbContext.OptionPrintOrder
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Prameters == param &&
                                      c.Template == bankCode &&
                                      c.IsForeignCurrency == isForeignCurrency);

        if (optionReport == null && isForeignCurrency == true)
        {
            optionReport = await dbContext.OptionPrintOrder
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Prameters == param && c.Template == bankCode);
        }

        return optionReport?.FileDoc ?? "UyNhiemChi/UNC_BIDV.rdl";
    }

    private async Task<string> GetOrderBuyFile(string param, bool? isForeignCurrency)
    {
        var optionReport = await dbContext.OptionPrintOrder
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Prameters == param && c.IsForeignCurrency == isForeignCurrency);
        return optionReport?.FileDoc ?? "DonHangMua.rdl";
    }


    private async Task<string> GetDefaultFile(string param)
    {
        var optionReport = await dbContext.OptionPrintOrder
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Prameters == param);

        if (!string.IsNullOrEmpty(optionReport?.FileDoc))
            return optionReport.FileDoc;

        return param switch
        {
            "NHAP" => "Voucher.rdl",
            "NKHAU" => "VoucherNKhau.rdl",
            "Request" => "YeuCauCungCap.rdl",
            "RequireImp" => "ThongBaoNhapHang.rdl",
            _ => throw new Exception($"Invalid parameter: {param}")
        };
    }




    private void ConfigPDFOption(ReportWriter writer, string rootPath)
    {
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
    }

    private List<ReportParameter> AddParameter(PrintVoucherRequest request) =>
        [
            new() { Name = "Parameter", Values = [request.Parameter], DataType = "String", },
            new() { Name = "Id", Values = [request.Id], DataType = "String",  },
            new() { Name = "UserCode", Values = [currentUser.CodeUser], DataType = "String" },
            new() { Name = "CodeUnit", Values = [currentUser.CodeUnit.ToString()], DataType = "String" },
            new() { Name = "AccountSymbol", Values = [request.Accountsymbol], DataType = "String" },
            new() { Name = "BeginDate", Values = [request.BeginDate], DataType = "String" },
            new() { Name = "EndDate", Values = [request.EndDate], DataType = "String" },
            new() { Name = "Date", Values = [request.Date], DataType = "String" },
            new() { Name = "PathImages", Values = [request.PathImages], DataType = "String" },
            new() { Name = "PathLogo", Values = [request.PathLogo], DataType = "String" },
            new() { Name = "SmartSoftware", Values = [request.SmartSofware], DataType = "String" },
        ];


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
