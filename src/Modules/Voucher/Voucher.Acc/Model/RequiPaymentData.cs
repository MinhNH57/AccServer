using BuildingBlocks.SmartMapper;
using System.ComponentModel.DataAnnotations;
using Voucher.Acc.Model.Contracts;

namespace Voucher.Acc.Model;

public class RequiPaymentData : IBaseEntity
{
    public Guid Id { get; set; }
    public string? DataType { get; set; } = string.Empty;
    public string? DataName { get; set; } = string.Empty;
    [DataType("smalldatetime")]
    public DateTime? VoucherDate { get; set; }
    [DataType("smalldatetime")]
    public DateTime RecordDate { get; set; } = DateTime.Now;
    [DataType("smalldatetime")]
    public DateTime? MaturityDate { get; set; } = DateTime.Now;
    public DateTime? DatePaymentPlan { get; set; }
    public DateTime? DateComplete { get; set; } = DateTime.Now;

    public string? NumberOfVouchers { get; set; } = string.Empty;
    public string? ObjectCode { get; set; } = string.Empty;
    public string? ObjectName { get; set; } = string.Empty;
    public string? PersonName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? SWIFTCode { get; set; } = string.Empty;
    public string? Notes { get; set; } = string.Empty;
    public Guid? IdDocumment { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; } = true;
    [SmartMapIgnore]
    [DataType("datetime")]
    public DateTime CreatedDate { get; set; }
    [SmartMapIgnore]
    public string? CreatedBy { get; set; }
    [DataType("datetime")]
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public bool SaveTemp { get; set; }
    public bool IsImport { get; set; } = false;
    public string? StaffCode { get; set; } = string.Empty;
    public string? StaffName { get; set; } = string.Empty;
    public string? ContractCode { get; set; } = string.Empty;
    public string? ContractName { get; set; } = string.Empty;
    public string? ReasonCode { get; set; } = string.Empty;
    public string? ForeignCurrencyType { get; set; } = "VND";
    public string? DeclarationNo { get; set; } = string.Empty;
    public Guid? DeclarationId { get; set; } 
    public double ExchangeRate { get; set; } = 1;
    public string? ReasonName { get; set; } = string.Empty;
    public string? AccountingShorcutCode { get; set; } = string.Empty;
    public string? BankAccount { get; set; } = string.Empty;
    public string? BankName { get; set; } = string.Empty;
    public string? BankAddress { get; set; } = string.Empty;
    public string? MethodOfPaymentsName { get; set; } = string.Empty;
    public string? MethodOfPaymentsCode { get; set; } = string.Empty;
    public string? CodeWork { get; set; } = string.Empty;
    public string? NameWork { get; set; } = string.Empty;
    //public bool? Priority { get; set; }
    public decimal TotalMoney { get; set; }
    public decimal PaymentedMoney { get; set; }
    public decimal RemainingMoney { get; set; }
    public bool AccountsPayable { get; set; }
    public string? StatusProfile { get; set; }
    public string? TrackingNumber { get; set; }
    public string? TrackingNumber1 { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsCreatePlanPay { get; set; }
    public bool Declared { get; set; }
    public bool IsOrtherPayment { get; set; }
    public Guid? PaymentPlanId { get; set; }
    [SmartMapIgnore]
    public List<RequiPaymentDataContents>? DataContentsList { get; set; }
    [SmartMapIgnore]

    public List<HeadInvoiceInputs>? HeadInvoiceInputs { get; set; }
    [SmartMapIgnore]

    public List<PaymentPlanContents>? PaymentPlanContents { get; set; }
    [SmartMapIgnore]

    public List<RequirePaymentMoney>? RequirePaymentMoneys { get; set; }
    [SmartMapIgnore]

    public List<TravelExpenses>? TravelExpensess { get; set; }
    //public List<Guid> HeadInvoiceIds { get; set; } = new();
}