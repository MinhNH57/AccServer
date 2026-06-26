namespace Voucher.Acc.Features.Commands.RequirePayment;

public class CreateRequiPaymentDataContentsModel
{
    //public Guid? IdContents { get; set; }
    public string? DataType { get; set; }
    public string? CreditObjectCode { get; set; }
    public string? CreditObjectName { get; set; }
    public decimal AmountOfMoney { get; set; }
    public decimal AmountVat { get; set; }
    public decimal AmountWithoutVat { get; set; }
    public decimal AmountOfMoneyForeignCurrency { get; set; }
    public string? Notes { get; set; }
    public int CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public bool DebitCO { get; set; }
    public DateTime CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public Guid? IdData { get; set; }
    //public Guid IdSource { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? Description { get; set; }
    public string? InvoiceNo { get; set; }
    public string? DataTypeSource { get; set; }
    public string? BudgetCode { get; set; }


    public Guid? IdTracing { get; set; }
}