namespace Catalog.SGas.Entities.Sgas;
public class SalesCatalogTank
{
    public string TankCode { get; set; }
    public string? TankName { get; set; }
    public string? TypeTank { get; set; }
    public string? Notes { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? CodeWarehose { get; set; }
    public decimal? TankHigh { get; set; }
    public decimal? TankLength { get; set; }
    public decimal? LowLevelWarning { get; set; }
    public decimal? HighLevelWarning { get; set; }
    public string? SensorId { get; set; }
    public string? NameWarehose { get; set; }
    public string? UnitPsc { get; set; }
    public double? Capacity { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; }
    public Guid Id { get; set; }  
}
