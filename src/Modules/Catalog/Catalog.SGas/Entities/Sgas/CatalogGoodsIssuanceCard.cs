namespace Catalog.SGas.Entities.Sgas;
public class CatalogGoodsIssuanceCard
{
    public bool IsActive { get; set; }
    public string CardNo { get; set; } = string.Empty;
    public string? CardName { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? ObjectAddress { get; set; }
    public string? ObjectTaxCode { get; set; }
    public string? PersonName { get; set; }
    public string? LicensePlates { get; set; }
    public string? LimitNo { get; set; }
    public string? LimitName { get; set; }
    public double? Quantity { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool AutoRenewal { get; set; }
    public int? CodeUnit { get; set; } 
    public string? Notes { get; set; } 
    public Guid Id { get; set; } = Guid.NewGuid();
}
