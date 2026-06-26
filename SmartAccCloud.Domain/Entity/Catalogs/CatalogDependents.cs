using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogDependents
{
    public Guid Id { get; set; }
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
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
}
