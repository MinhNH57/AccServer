namespace Catalog.SGas.Entities;
public class SalesCashRemaining
{
    public string? WarehoseCode { get; set; }

    public string? WarehoseName { get; set; }

    public double? PackageQuantity { get; set; }

    public double? QuantityOfInventory { get; set; }

    public decimal? AmountOfMoney { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();
      
    public int? CodeUnit { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;

    public string? CommodityCode { get; set; }

    public string? CommodityName { get; set; }

    public string? ObjectCode { get; set; }

    public string? ObjectName { get; set; }

    public string? ObjectTax { get; set; }
}
