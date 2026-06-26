namespace Catalog.SGas.Entities.Sgas;
public class SalesCatalogLossRate
{
    public Guid Id { get; set; }
    public string? GroupCode { get; set; }
    public string? TypeLossRate { get; set; }
    public double? ImportLoss { get; set; }
    public double? ExportLoss { get; set; }
    public double? ContainedLoss { get; set; }
    public double? WashLoss { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; } 
}
