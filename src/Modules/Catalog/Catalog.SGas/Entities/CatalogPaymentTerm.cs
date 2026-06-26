namespace Catalog.SGas.Entities;
public class CatalogPaymentTerm
{
    public string PaymentTermCode { get; set; }
    public string? PaymentTermName { get; set; }
    public int DueTime { get; set; }
    public int DiscountTime { get; set; }
    public double DiscountPercent { get; set; }
    public string? Notes { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; }
}
