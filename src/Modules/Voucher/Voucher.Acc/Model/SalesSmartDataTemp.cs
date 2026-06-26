using Voucher.Acc.Model.Contracts;

namespace Voucher.Acc.Model;

public class SalesSmartDataTemp : IBaseEntity
{
    public Guid Id {get;set;}
    public string? DataType {get;set;}
    public string? DataName {get;set;}
    public string? WarehoseData {get;set;}
    public DateTime? VoucherDate { get; set; } = DateTime.Now;
    public DateTime? RecordDate { get; set; } = DateTime.Now;
    public string? NumberOfVouchers {get;set;}
    public string? InvoiceNumber {get;set;}
    public string? ObjectCode {get;set;}
    public string? ObjectName {get;set;}
    public string? ObjectAddress {get;set;}
    public string? ObjectTaxCode {get;set;}
    public double? DiscountRate {get;set;}
    public string? WarehoseCode {get;set;}
    public string? WarehoseName {get;set;}
    public string? Description {get;set;}
    public string? Notes {get;set;}
    public string? StaffCode {get;set;}
    public string? StaffName {get;set;}
    //public int IdAsc {get;set;}
    public int? CodeUnit {get;set;}
    public bool IsActive {get;set;}
    public DateTime CreateDate {get;set;}  =DateTime.Now;
    public string? CreateBy {get;set;}
    public DateTime? ModifyDate {get;set;}
    public string? ModifyBy {get;set;}
    public decimal? DebtBalanc {get;set;}
    public decimal? DebitBalance {get;set;}
    public string? ContractCode { get; set; }
    public string? ContractName { get; set; }
}