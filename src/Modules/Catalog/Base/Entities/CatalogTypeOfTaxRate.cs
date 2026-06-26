using System.ComponentModel.DataAnnotations;

namespace Catalog.Base.Entities;
public class CatalogTypeOfTaxRate
{
    [Key]
    public string TypeCode { get; set; }
    public string? TypeName { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();

    public double? VatRate { get; set; }
}
