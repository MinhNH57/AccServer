namespace SmartAccCloud.Application.Models.Catalogs.Contract;

public class TradingHistory
{
    public string? ContactNumber { get; set; }
    public string? Content { get; set; }
    public string? NumberSubmit { get; set; }
    public DateTime? DateSubmit { get; set; }

    public double? ValueContract { get; set; }
    public string? Notes { get; set; }
}