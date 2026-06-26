namespace Catalog.SGas.Entities;
public class CatalogStatistical
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? StatisticalCode { get; set; }
    public string? StatisticalName { get; set; }
    public string? StatisticalLevel { get; set; }
    public string? Status { get; set; }
    public int CodeUnit { get; set; } = 888;
    public DateTime Created { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public DateTime? Modified { get; set; } = DateTime.Now;
    public string? ModifiedBy { get; set; }
    public string? StatisticalBelong { get; set; }
    public string? Description { get; set; }
}
