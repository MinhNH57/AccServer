using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogServices
{
    [Key]
    [Unique(nameof(CatalogServices), nameof(ServicesCode), ErrorMessage = "Mã tồn tại")]
    [Required(ErrorMessage = "Mã dịch vụ không được để trống")]
    public string ServicesCode { get; set; }
    public string? ServicesName { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
