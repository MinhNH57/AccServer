using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogBranch
{
    [Key]
    [Unique(nameof(CatalogBranch), nameof(BranchCode), ErrorMessage = "Mã tồn tại")]
    [Required(ErrorMessage = "Mã phiếu không được để trống")]
    public string BranchCode { get; set; }
    public string? BranchName { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
