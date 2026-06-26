using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogDiscountList
{
    [Key]
    [Unique(nameof(CatalogDiscountList), nameof(DiscountListCode), ErrorMessage = "Mã tồn tại")]
    [Required(ErrorMessage = "Mã mua hàng không được để trống")]
    public string DiscountListCode { get; set; }
    public string? DiscountListName { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
