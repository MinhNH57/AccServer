namespace Voucher.Acc.Model;

public class PaymentPlanContents
{
    public Guid Id {get;set;}
    //public int IdAsc {get;set;}
    public Guid? IdContent {get;set;}
    public DateTime? DatePayment {get;set;}
    public DateTime? DatePaymentPlan {get;set;}
    public decimal AmountOfMoney {get;set;}
    public decimal AmountOfMoneyForeignCurrency {get;set;}
    public string? ObjectCode {get;set;}
    public string? ObjectName {get;set;}
    public string? Description {get;set;}
    public string? Notes {get;set;}
    public string? Currency {get;set;}
    public Guid? IdVoucherRef {get;set;}
    public string? NoVoucherRef {get;set;}
    public string? SourcePaymentCode {get;set;}
    public string? SourcePaymentName {get;set;}
    //public DateTime CreatedDate {get;set;}
    //public string? CreatedBy {get;set;}
    //public DateTime? ModifiedDate {get;set;}
    //public string? ModifiedBy {get;set;}
}