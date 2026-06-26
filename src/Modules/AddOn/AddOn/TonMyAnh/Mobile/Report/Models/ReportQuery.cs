namespace AddOn.TonMyAnh.Mobile.Report.Models;

public class ReportQuery
{
    public string? Parameter { get; set; } = string.Empty;
    public string? UserCode { get; set; } = string.Empty;
    public string? GroupCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}