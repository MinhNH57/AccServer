using System.ComponentModel.DataAnnotations.Schema;

namespace Voucher.Acc.Model;

public class SmartDataBillOfMaterials
{
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? UnitPcs { get; set; }
    public double? QuantityProduct { get; set; }
    public double? QuantityBom { get; set; }
    public double? Quantity { get; set; }
    public double? Price { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public string? Notes { get; set; }
    public string? DataType { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; }
    public Guid IdContents { get; set; }=Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid IdSource { get; set; } = Guid.NewGuid();
    public bool IsKit { get; set; }
    public string? StageCode { get; set; }
    public string? StageName { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? ProductSize { get; set; }
    public string? ProductColor { get; set; }
}
