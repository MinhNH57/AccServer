using System.ComponentModel.DataAnnotations;

namespace Voucher.Acc.Model
{
    public class SmartDataManufacture
    {
        [Key]
        public Guid Id { get; set; }
        public string? DataType { get; set; } = string.Empty;
        public string? DataName { get; set; } = string.Empty;
        public DateTime? VoucherDate { get; set; } = DateTime.Now;
        public DateTime? RecordDate { get; set; } = DateTime.Now;
        public string? NumberOfVouchers { get; set; } = string.Empty;
        public string? StageCode { get; set; } = string.Empty;
        public string? StageName { get; set; } = string.Empty;
        public string? PersonCode { get; set; } = string.Empty;
        public string? PersonName { get; set; } = string.Empty;
        public string? PersonAddress { get; set; } = string.Empty;
        public string? ObjectCode { get; set; } = string.Empty;
        public string? ObjectName { get; set; } = string.Empty;
        public string? ObjectAddress { get; set; } = string.Empty;
        public string? WarehoseData { get; set; } = string.Empty;
        public string WarehoseCode { get; set; } = string.Empty;
        public string? WarehoseName { get; set; } = string.Empty;
        public string? WarehoseCodeReceive { get; set; } = string.Empty;
        public string? WarehoseNameReceive { get; set; } = string.Empty;
        public string? ReasonCode { get; set; } = string.Empty;
        public string? ReasonName { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? StaffName { get; set; } = string.Empty;
        public string? StaffCode { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public bool? Register { get; set; } = false;
        public int? CodeUnit { get; set; } = 888;
        public bool? IsActive { get; set; } = false;
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public string? CreateBy { get; set; } = string.Empty;
        public DateTime? ModifyDate { get; set; } = DateTime.Now;
        public string? ModifyBy { get; set; } = string.Empty;
        public string? ObjectEmail { get; set; } = string.Empty;
        public string? IdDataHead { get; set; } = string.Empty;
        public bool? ComfirmVoucher { get; set; } = false;
        public string? VoucherNoInherit { get; set; } = string.Empty;
        public bool? Delivered { get; set; } = false;
        public bool? SaveTemp { get; set; } = false;
        public Guid? IdVoucherSource { get; set; } = Guid.Empty;
        public string? VoucherStatus { get; set; } = string.Empty;
        public Guid? IdProductPlan { get; set; }
        public string? ProductPlanContent { get; set; }
        public List<SmartDataManufactureContents>? SmartDataManufactureContents { get; set; } = new List<SmartDataManufactureContents>();
        public List<SmartDataBillOfMaterials>? SmartDataBillOfMaterials { get; set; } = new List<SmartDataBillOfMaterials>();
    }
}


