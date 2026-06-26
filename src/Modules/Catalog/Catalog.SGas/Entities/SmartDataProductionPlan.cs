using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities;
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
    public int? S90 { get; set; }
    public int? S100 { get; set; }
    public int? S110 { get; set; }
    public int? S120 { get; set; }
    public int? S130 { get; set; }
    public int? S140 { get; set; }
    public int? S150 { get; set; }
    public int? S160 { get; set; }
    public int? SSS { get; set; }
    public int? SXS { get; set; }
    public int? SS { get; set; }
    public int? SM { get; set; }
    public int? SL { get; set; }
    public int? SLL { get; set; }
    public int? S3L { get; set; }
    public int? S4L { get; set; }
    public int? S5L { get; set; }
    public int? SXXL { get; set; }
    public int? SXXXL { get; set; }
    public int? S2XL { get; set; }
    public int? S3XL { get; set; }
    public int? S4XL { get; set; }
    public int? S5XL { get; set; }
    public int? SPXS { get; set; }
    public int? SPS { get; set; }
    public int? SPM { get; set; }
    public int? SPL { get; set; }
    public int? SPXL { get; set; }
    public int? SPXXL { get; set; }
    public int? S0X { get; set; }
    public int? S1X { get; set; }
    public int? S2X { get; set; }
    public int? S3X { get; set; }
    public int? S4X { get; set; }
    public int? S5X { get; set; }
    public int? S28 { get; set; }
    public int? S30 { get; set; }
    public int? S32 { get; set; }
    public int? S34 { get; set; }
    public int? S36 { get; set; }
    public int? S38 { get; set; }
    public int? S40 { get; set; }
    public int? S42 { get; set; }
    public int? SPLUS1 { get; set; }
    public int? SPLUS2 { get; set; }
    public int? SPLUS3 { get; set; }
    public int? SPLUS4 { get; set; }
    public int? SPLUS5 { get; set; }
    public int? SPLUS6 { get; set; }
    public int? SPLUS7 { get; set; }
    public double? QuantityImp { get; set; }
    public double? QuantityProducedImp { get; set; }
    public string? ListColumn { get; set; }
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
    public Guid? IdVouchers { get; set; }
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

