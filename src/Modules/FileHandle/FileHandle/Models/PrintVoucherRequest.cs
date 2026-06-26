namespace FileHandle.Models;

public class PrintVoucherRequest
{
    public string Parameter { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string? Accountsymbol { get; set; }
    public string? BeginDate { get; set; }
    public string? EndDate { get; set; }
    public string? Date { get; set; }
    public string? PathImages { get; set; }
    public string? PathLogo { get; set; }
    public string? filePath { get; set; } = string.Empty;
    public string? SmartSofware { get; set; }
    public string? BankCode { get; set; } = string.Empty;
    public bool? IsForeignCurrency { get; set; }
}