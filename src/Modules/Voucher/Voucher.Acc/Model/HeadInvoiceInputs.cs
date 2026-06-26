namespace Voucher.Acc.Model;

public class HeadInvoiceInputs
{
    public Guid Id {get;set;}
    public bool? IsActive {get;set;}
    public Guid? GuidInv {get;set;} = Guid.NewGuid();
    public Guid? IdRqPayment {get;set;}
    public int? InvoiceTypeID {get;set;}
    public string? InvoiceType {get;set;}
    public string? InvoiceForm {get;set;}
    public string? InvoiceSerial {get;set;}
    public string? InvoiceNo {get;set;}
    public DateTime? InvoiceDate {get;set;}
    public DateTime? SignedDate {get;set;}
    public DateTime? DateOfAuthorityCode {get;set;}
    public string? InvoiceCode {get;set;}
    public int? InvoiceStatus {get;set;}
    public string? CurrencyID {get;set;}
    public int? ExchangeRate {get;set;}
    public int? PayMethodID {get;set;}
    public int? OriginalInvoiceForm {get;set;}
    public int? OriginalInvoiceNo {get;set;}
    public int? OriginalInvoiceTypeID {get;set;}
    public string? SellerTaxCode {get;set;}
    public string? SellerUnitName {get;set;}
    public string? SellerAddress {get;set;}
    public string? BuyerTaxCode {get;set;}
    public string? BuyerUnitName {get;set;}
    public string? BuyerAddress {get;set;}
    public string? BuyerBankName {get;set;}
    public double? SumItemAmount {get;set;}
    public double? SumTaxAmount {get;set;}
    public double? SumDiscountAmount {get;set;}
    public double? SumPaymentAmount {get;set;}
    public DateTime? LastModifiedTime {get;set;}
    public int? CodeUnit {get;set;}
    public string? DataType {get;set;}
    //public int IdAsc {get;set;}
    public Guid? IdDocument {get;set;} = Guid.NewGuid();
    public string? NumberVouchersDocument {get;set;}
    public bool? HasBeenDeclared {get;set;}
    public bool? NotCompleted {get;set;}
    public bool IsPayment {get;set;}
    public string? MethodOfPaymentsName {get;set;}
    public string? CodeWarehose {get;set;}
}
