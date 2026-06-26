namespace Catalog.SGas.Entities.Sgas;
public class CatalogSaleShifts
{
    public string ShiftCode { get; set; } = string.Empty;
    public string? ShiftName { get; set; }
    public TimeSpan? BeginTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? CodeWarehose { get; set; }
    public string? NameWarehose { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public int? CodeUnit { get; set; }
}
