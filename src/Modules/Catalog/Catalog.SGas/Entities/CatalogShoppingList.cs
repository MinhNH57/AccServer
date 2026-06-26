using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities;
public class CatalogShoppingList
{
    [Key]
    [Required(ErrorMessage = "Mã mua hàng không được để trống")]
    public string ShoppingListCode { get; set; }
    public string? ShoppingListName { get; set; }
    public string? TypeVoucher { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
