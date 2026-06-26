using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SmartAccCloud.Domain;
public class SalesSmartData
{
    [Key]
    public Guid Id { get; set; }
    public string? DataType { get; set; }
    public string? DataName { get; set; }
    public string? WarehoseData { get; set; }
    [Precision(18,0)]
    public decimal? AmountTransferred { get; set; }
    public DateTime VoucherDate { get; set; }
    public DateTime RecordDate { get; set; }
    public string? NumberOfVouchers { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? PersonCode { get; set; }
    public string? PersonName { get; set; }
    public string? PersonAddress { get; set; }
    public string? PersonTaxCode { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? ObjectAddress { get; set; }
    public string? ObjectTaxCode { get; set; }
    public string? GroupAreaCode { get; set; }
    public string? GroupAreaName { get; set; }
    public string? GroupCode { get; set; }
    public string? GroupName { get; set; }
    public double? DiscountRate { get; set; }
    public string? WarehoseCode { get; set; }
    public string? WarehoseName { get; set; }
    public string? WarehoseCodeReceive { get; set; }
    public string? WarehoseNameReceive { get; set; }
    public string? ReasonCode { get; set; }
    public string? ReasonName { get; set; }
    public string? MethodOfPaymentsCode { get; set; }
    public string? MethodOfPaymentsName { get; set; }
    public string? ShippingMethodsCode { get; set; }
    public string? ShippingMethodsName { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? StaffCode { get; set; }
    public string? StaffName { get; set; }
    //public Guid? IdDocumment { get; set; }
    public bool Register { get; set; }
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //public int? IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
    public bool? IsActive { get; set; } = false;
    public DateTime CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    //public bool? InvoiceCancel { get; set; } = false;
    public string? ObjectEmail { get; set; }
    //public string? ContractCode { get; set; }
    //public string? ContractName { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectName { get; set; }
    //public int? SmartSoftId { get; set; }
    //public string? MemberRate { get; set; }
    public string? NumberOfVoucherInherit { get; set; }
    public bool? ComfirmVoucher { get; set; } = false;
}
