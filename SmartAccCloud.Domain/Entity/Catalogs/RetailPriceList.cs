using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class RetailPriceList
{
    public Guid Id { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? UnitPcs { get; set; }
    public double? Price { get; set; }
    public double? VatRate { get; set; }
    public double? EnvironmentalProtectionFee { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; } = 100;
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

    public int IdAsc { get; set; }
}
