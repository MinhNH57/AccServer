using Microsoft.EntityFrameworkCore;

namespace SmartAccCloud.Domain.Entity;

public class DataControlled
{
    public string? UnitSign { get; set; }
    public int CodeUnit { get; set; }
    public string? DataType { get; set; }
    public string Id { get; set; }
    public string? NumberOfVouchers { get; set; }
    public DateTime RecordDate { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    [Precision(12,0)]
    public decimal? CurrentBalance { get; set; }
    [Precision(12,0)]
    public decimal? DebtPalanceLimit { get; set; }
    [Precision(12,0)]
    public decimal? ValueOrder { get; set; }
    [Precision(12,0)]
    public decimal OverLimit { get; set; }
    public bool SaveTemp { get; set; }
    public bool Controlled { get; set; }
    public bool ComfirmVoucher { get; set; }
    public string? Description { get; set; }
}