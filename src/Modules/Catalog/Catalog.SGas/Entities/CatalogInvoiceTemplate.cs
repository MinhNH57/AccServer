namespace Catalog.SGas.Entities;
public class CatalogInvoiceTemplate
{
    public string CodeTemplate { get; set; }
    public string? NameTemplate { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public string? Notes { get; set; }
}
