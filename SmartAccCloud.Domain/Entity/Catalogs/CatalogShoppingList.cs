using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogShoppingList
{
    [Key]
    [Unique(nameof(CatalogShoppingList), nameof(ShoppingListCode), ErrorMessage = "Mã tồn tại")]
    [Required(ErrorMessage = "Mã mua hàng không được để trống")]
    public string ShoppingListCode { get; set; }
    public string? ShoppingListName { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
