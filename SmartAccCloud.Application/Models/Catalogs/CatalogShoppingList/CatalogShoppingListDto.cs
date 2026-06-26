namespace SmartAccCloud.Application.Models.Catalogs.CatalogShoppingList;
public class CatalogShoppingListDto
{
    public string ShoppingListCode { get; set; }
    public string? ShoppingListName { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
