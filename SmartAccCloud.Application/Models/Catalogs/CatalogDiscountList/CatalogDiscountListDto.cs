namespace SmartAccCloud.Application.Models.Catalogs.CatalogDiscountList;
public class CatalogDiscountListDto
{
    public string DiscountListCode { get; set; }
    public string? DiscountListName { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
