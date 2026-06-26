using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogProductForAsset
{
    public string? CodeAsset { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? UnitPcs { get; set; }
    public double? Quantity { get; set; }
    public double? Price { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; } = 100;
    [Key]
    public Guid Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

    public int IdAsc { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
