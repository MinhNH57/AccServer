namespace SmartAccCloud.Application.Models.Catalogs.CatalogDependents;

public class DependentsDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ObjCode { get; set; }
    public string? NameDependents { get; set; }
    public string? Relationship { get; set; }
    public string? ContactInfo { get; set; }
    public string? CCCD { get; set; }
    public string? Nationality { get; set; }
    public string? TaxCode { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? DateBith { get; set; }
}