using System.ComponentModel.DataAnnotations.Schema;
using Voucher.Acc.Features.Commands.SmartData.CreateVoucher.Models;

namespace Voucher.Acc.Model;

public class CreditContract
{
    public Guid Id { get; set; }=Guid.NewGuid();
    public string? DataType { get; set; }
    public string? DataName { get; set; }
    public int CodeUnit { get; set; } = 888;
    public string? ContractCode { get; set; }
    public string? ContractName { get; set; }
    public string? BankCode { get; set; }
    public string? BankName { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? BankAccountNumber { get; set; }
    public string? BankAccountName { get; set; }
    public int? BorrowingTime { get; set; }
    public double? InterestRateYear { get; set; }
    public double? OverdueInterestRate  { get; set; }
    public decimal? ContractValue { get; set; }
    public int? NumberOfMonthsToPayOnce { get; set; }
    public int? NumberOfMonthsToPayInterestOnce { get; set; }
    public string? Description { get; set; }
    public string? NumberOfVouchers { get; set; } = string.Empty;
    public DateTime? CreateDate { get; set; } = DateTime.Now;
    public DateTime? DisbursementDate { get; set; } = DateTime.Now;
    public DateTime? DateDue { get; set; } = DateTime.Now;
    public string? ForeignCurrencyType { get; set; } = string.Empty;
    public double? ExchangeRate { get; set; } = 0;
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; } = DateTime.Now;
    public string? StatusName { get; set; } = string.Empty;
    public double? TotalMoney { get; set; } = 0;
    public string? LoanPurpose { get; set; } = string.Empty;
    public decimal? DeferredLcFee { get; set; } 
    public decimal? LcConfirmationFee { get; set; } 
    public decimal? InsuranceFee { get; set; }
    public decimal? GuaranteeFee { get; set; } 
    public List<CreditContractContents>? CreditContractContents { get; set; } = new List<CreditContractContents>();
    public List<SmartPaymentVendor>? SmartPaymentVendors { get; set; } = new List<SmartPaymentVendor>();
    public List<SmartContentsDebtRepaymentPlan>? SmartContentsDebtRepaymentPlans { get; set; } = new List<SmartContentsDebtRepaymentPlan>();
}
