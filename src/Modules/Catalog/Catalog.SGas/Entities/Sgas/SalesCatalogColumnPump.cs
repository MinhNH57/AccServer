namespace Catalog.SGas.Entities.Sgas;
public class SalesCatalogColumnPump
{
    public string ColumnCode { get; set; }
    public string? ColumnName { get; set; }
    public string? Notes { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? CodeWarehose { get; set; }
    public string? NameWarehose { get; set; }
    public double? NumberBegin { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; }
    public Guid Id { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool NotUse { get; set; }
    public string? TankCode { get; set; }
    public string? TankName { get; set; }
    public Guid IdTank { get; set; }
}
