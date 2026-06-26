using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voucher.Acc.Model.ProductPlant;

public class SmartProductionPlan
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime? VoucherDate { get; set; } = DateTime.Now;
    public DateTime? RecordDate { get; set; } = DateTime.Now;
    public string? NumberOfVouchers { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public bool Register { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; } = 888;
    public bool IsActive { get; set; }
    public DateTime? CreateDate { get; set; } = DateTime.Now;
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; } = DateTime.Now;
    public string? ModifyBy { get; set; }
    public bool? ComfirmVoucher { get; set; }
    public string? VoucherNoInherit { get; set; }
    public bool Delivered { get; set; }
    public bool SaveTemp { get; set; }
    public Guid? IdVoucherSource { get; set; }
    public string? VoucherStatus { get; set; }
    public string? DataType { get; set; }
    public List<SmartDataProductionPlan>? SmartDataProductionPlans  { get; set; } = new List<SmartDataProductionPlan>();
}
