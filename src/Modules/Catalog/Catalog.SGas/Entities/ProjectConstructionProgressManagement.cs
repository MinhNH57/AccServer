namespace Catalog.SGas.Entities;
public class ProjectConstructionProgressManagement
{
    public string Code { get; set; }
    public string? Name { get; set; }
    public string? UnitPsc { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? ExecutionVolume { get; set; }
    public decimal? PlannedVolume { get; set; }
    public decimal? ContractVolume { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? Progress { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
