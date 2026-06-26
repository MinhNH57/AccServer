namespace Voucher.Acc.Model;

public class RequirePaymentMoney
{

    public Guid Id { get; set; }
    public string NumberOfVouchers { get; set; }
    public string? Description { get; set; }
    public DateTime? RecordDate { get; set; }
    public string? InvoiceNo { get; set; }
    public string? ContractNo { get; set; }
    public string? Currency { get; set; }
    public decimal MoneyPayment { get; set; }
    public decimal MoneyPaymentForeignCurrency { get; set; }
    public string? Notes { get; set; }
    public Guid? IdContents { get; set; }
    public Guid? IdReferenceVoucher { get; set; }
    public string? NoReferenceVoucher { get; set; }

    //public int IsAsc {get;set;}
}