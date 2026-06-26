using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogAssetGroup
{
    [Key]
    [Required(ErrorMessage = "Mã tài sản không được để trống")]
    public string AssetGroupCode { get; set; } = null!;
    public string? AssetGroupName { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public double? PercentPerAllocationPeriod { get; set; }
    public int? NumberYear { get; set; }
}
