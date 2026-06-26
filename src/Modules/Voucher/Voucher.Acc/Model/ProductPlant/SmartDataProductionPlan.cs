using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voucher.Acc.Model.ProductPlant;

public class SmartDataProductionPlan
{
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public string? DataType { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? StatusRequi { get; set; }
    public string? StageCode { get; set; }
    public string? StageName { get; set; }
    public string? MachineCode { get; set; }
    public string? MachineName { get; set; }
    public string? CommodityCode { get; set; }
    public string? CommodityName { get; set; }
    public string? UnitPcs { get; set; }
    public double? Quantity { get; set; }
    public double? QuantityProduced { get; set; }
    public double? Quantity01 { get; set; }
    public double? QuantityProduced01 { get; set; }
    public double? Quantity02 { get; set; }
    public double? QuantityProduced02 { get; set; }
    public double? Quantity03 { get; set; }
    public double? QuantityProduced03 { get; set; }
    public double? Quantity04 { get; set; }
    public double? QuantityProduced04 { get; set; }
    public double? Quantity05 { get; set; }
    public double? QuantityProduced05 { get; set; }
    public double? Quantity06 { get; set; }
    public double? QuantityProduced06 { get; set; }
    public string? NumberOfVouchers { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreateDate { get; set; } = DateTime.Now;
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public Guid IdData { get; set; }
    [Key]
    public Guid IdSource { get; set; }
    public Guid IdVouchers { get; set; }
    public DateTime? RecordDate { get; set; }
    public DateTime? VoucherDate { get; set; }
    public double? QuantityOfInventory { get; set; }
    public DateTime? StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; } = DateTime.Now;
    public string? ProductSize { get; set; }
    public string? ProductColor { get; set; }
    public string? ProductSizeName { get; set; }
    public string? ProductColorName { get; set; }
}

