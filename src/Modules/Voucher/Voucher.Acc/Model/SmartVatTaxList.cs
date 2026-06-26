using System.ComponentModel.DataAnnotations;
using Voucher.Acc.Model.Contracts;

namespace Voucher.Acc.Model;

public class SmartVatTaxList: IBaseEntity
{
    public Guid IdContents {get;set;}
    //public int? IdAsc { get; set; }
    public DateTime? VoucherDate { get; set; } = DateTime.Now;
	public DateTime? RecordDate {get;set;} = DateTime.Now;
    public DateTime? InvoiceDate {get;set;} = DateTime.Now;
    public string? AccountSymbol {get;set;}
	public string? TaxCode {get;set;}
	public string? ObjectCode {get;set;}
	public string? ObjectName {get;set;}
	public double? VatRate {get;set;}
	public decimal? AmountOfMoney {get;set;}
	public decimal? AmountVat {get;set;}
	public string? CodeVatType {get;set;}
	public string? VatType {get;set;}
	public string? NumberOfVouchers {get;set;}
	public string? InvoiceNumber {get;set;}
	public string? InvoiceSign {get;set;}
	public string? InvoiceTemplate {get;set;}
	public string? WarehoseCode {get;set;}
	public string? WarehoseName {get;set;}
	public string? ImportOrExport {get;set;}
	public string? Description {get;set;}
	public int? CodeUnit {get;set;}
	public bool IsActive {get;set;} 
	public string? Notes {get;set;}
	public string? LOAIPHIEU {get;set;}
	public Guid IdSource {get;set;}
	public Guid? GuidInv {get;set;}
    [DataType("datetime")]
	public DateTime CreatedDate {get;set;} = DateTime.Now;
    public string? CreatedBy {get;set;}
	//public DateTime? ModifiedDate {get;set;}
	public DateTime? Modified { get; set; } = DateTime.Now;
    public string? ModifiedBy {get;set;}
	public string? CommodityCode {get;set;}
	public string? CommodityName {get;set;}
	public string? AccountSymbolImp {get;set;}
	public decimal? AmountOfMoneyTaxImp {get;set;}
	public double? TaxImpRate {get;set;}
	public double? AmountTaxImp {get;set;}
	public string? AccountSymbolExcise {get;set;}
	public decimal? AmountOfMoneyExciseTax {get;set;}
	public double? ExciseTaxRate {get;set;}
	public decimal? AmountExciseTax {get;set;}
	public string? AccountSymbolVat {get;set;}
	public double? FeeEnvironRate {get;set;}
	public decimal? AmountFeeEnvironRate {get;set;}
	public decimal? AmountAntidumpingDuty {get;set;}
	public double? AntidumpingDutyRate {get;set;}
	public string? AccountSymbolFeeEnviron {get;set;}
	public string? AccountSymbolAntidumpingDuty {get;set;}
	public decimal? PreCustomsFeeFC {get;set;}
	public decimal? PreCustomsFee {get;set;}
	public decimal? DeliveryFeeToWarehouse {get;set;}
}