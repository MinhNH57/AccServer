using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogExciseTaxItems
{
    [Key]
    [Unique(nameof(CatalogExciseTaxItems), nameof(CodeExcise), ErrorMessage = "Giá trị này đã tồn tại")]
    [Required(ErrorMessage = "Mã không được để trống")]
    public string CodeExcise { get; set; }
    public string? NameExcise { get; set; }
    public string? GrpCode { get; set; }
    public string? UnitPsc { get; set; }
    public double? ExciseTaxRate { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public string? Notes { get; set; }
    public bool IsActive { get; set; }

}
