using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.RequirePayment;

public class CreateRequiPaymentDataModel
{
    //public Guid Id { get; set; }
    public string? DataType { get; set; } = string.Empty;
    public string? DataName { get; set; } = string.Empty;
    public DateTime? VoucherDate { get; set; }
    public DateTime RecordDate { get; set; } = DateTime.Now;
    public DateTime? MaturityDate { get; set; } = DateTime.Now;
    public DateTime? DateComplete { get; set; } = DateTime.Now;

    public string? NumberOfVouchers { get; set; } = string.Empty;
    public string? ObjectCode { get; set; } = string.Empty;
    public string? ObjectName { get; set; } = string.Empty;
    public string? PersonName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Notes { get; set; } = string.Empty;
    public string? SWIFTCode { get; set; } = string.Empty;
    public Guid IdDocumment { get; set; }
    public int? CodeUnit { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public bool SaveTemp { get; set; }
    public bool IsImport { get; set; }
    public string? StaffCode { get; set; } = string.Empty;
    public string? StaffName { get; set; } = string.Empty;
    public string? ContractCode { get; set; } = string.Empty;
    public string? ContractName { get; set; } = string.Empty;
    public string? NameRoom { get; set; } = string.Empty;
    public string? CodeRoom { get; set; } = string.Empty;
    public string? ReasonCode { get; set; } = string.Empty;
    public string? ForeignCurrencyType { get; set; } = "VND";
    public string? AccountingShorcutCode { get; set; } = string.Empty;
    public string? BankAccount { get; set; } = string.Empty;
    public string? BankName { get; set; } = string.Empty;
    public string? BankAddress { get; set; } = string.Empty;
    public string? DeclarationNo { get; set; } = string.Empty;
    public Guid? DeclarationId { get; set; } 
    public double ExchangeRate { get; set; } = 1;
    public bool? Priority { get; set; }
    public string? CodeWork { get; set; } = string.Empty;
    public string? NameWork { get; set; } = string.Empty;
    public string? ReasonName { get; set; } = string.Empty;
    public string? MethodOfPaymentsName { get; set; } = string.Empty;
    public string? MethodOfPaymentsCode { get; set; } = string.Empty;
    public decimal TotalMoney { get; set; }
    public decimal PaymentedMoney { get; set; }
    public decimal RemainingMoney { get; set; }
    public bool? AccountsPayable { get; set; }
    public string? StatusProfile { get; set; }
    public string? TrackingNumber { get; set; }
    public string? TrackingNumber1 { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsCreatePlanPay { get; set; }
    public Guid? PaymentPlanId { get; set; }
    public List<CreateRequiPaymentDataContentsModel> DataContentsList { get; set; } = new();
    public List<PaymentPlanContents>? PaymentPlanContents { get; set; }
    public List<RequirePaymentMoney>? RequirePaymentMoneys { get; set; }
    public List<TravelExpenses>? TravelExpensess { get; set; }

    public List<Guid> HeadInvoiceIds { get; set; } = new();

}