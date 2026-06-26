namespace Catalog.SGas.Entities;
public class SalesMaterialRemaining
{
    public string? AccountSymbol { get; set; }

    public string? WarehoseCode { get; set; }

    public string? WarehoseName { get; set; }

    public string? CommodityCode { get; set; }

    public string? CommodityName { get; set; }

    public string? UnitPcs { get; set; }

    public string? UnitPackage { get; set; }

    public double? ConversionFactor { get; set; }

    public double? PackageQuantity { get; set; }

    public double? QuantityOfInventory { get; set; }

    public double? Quantity { get; set; }

    public double? Quantity15 { get; set; }

    public double? Price { get; set; }

    public decimal? AmountOfMoney { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid(); 

    public int? CodeUnit { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    public Guid? IdData { get; set; }

    public string? WarehoseData { get; set; }

    public string? ShipmentNumber { get; set; }

    public DateTime? RecordDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public string? Origin { get; set; }

    public string? ObjectCode { get; set; }

    public string? ObjectName { get; set; }

    public DateTime? DateExpiration { get; set; }

    public string? StorageLocation { get; set; }

    public string? TankCode { get; set; }

    public string? TankName { get; set; }
}
