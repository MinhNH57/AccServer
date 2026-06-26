using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogError
{
    [Key]
    [Unique(nameof(CatalogError), nameof(ErrorCode), ErrorMessage = "Mã tồn tại")]
    [Required(ErrorMessage = "Mã lỗi không được để trống")]
    public string ErrorCode { get; set; }
    public string? ErrorName { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
