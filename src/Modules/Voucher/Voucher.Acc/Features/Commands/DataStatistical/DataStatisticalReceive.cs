
namespace Voucher.Acc.Features.Commands.DataStatistical;

public class DataStatisticalReceive
{
    public string? StationId { get; set; } 
    public DateTime? RecordDate { get; set; }
    public string StatisticalCode { get; set; } = string.Empty;
    public string? UserCode { get; set; }
    public double Quantity { get; set; }
    public double QuantityNG { get; set; }
}