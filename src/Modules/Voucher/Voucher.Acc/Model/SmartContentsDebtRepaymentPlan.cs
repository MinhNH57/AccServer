using System.ComponentModel.DataAnnotations.Schema;

namespace Voucher.Acc.Model;

public class SmartContentsDebtRepaymentPlan
{
    public Guid IdContents { get; set; } = Guid.NewGuid();
    public Guid IdSource { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public string? DataType { get; set; }
    public int? CodeUnit { get; set; } = 888;
    public string? FundingSourceCode { get; set; }
    public string? FundingSourceName { get; set; }
    public DateTime? FirstDateOfPayment { get; set; } = DateTime.Now;
    public DateTime? DateOfPayment { get; set; } = DateTime.Now;
    public int? NumberOfDays { get; set; }
    public int? PaymentPeriod { get; set; }
    public decimal? OriginalBalance { get; set; }
    public decimal? PrincipalAmount { get; set; }
    public decimal? AmountOfInterest { get; set; }
    public decimal? TotalInterestPrincipal { get; set; }
    public decimal? AmountOfSavings { get; set; }
    public decimal? TotalAmount { get; set; }
    public string? Notes { get; set; }
    public Guid IdTracing { get; set; }
    public decimal? InterestRateMonth { get; set; }
    public decimal? Contractvalue { get; set; }
    public string? LOAIPHIEU { get; set; }
    public decimal? AmountOfSettlement { get; set; }
    public int? SmId { get; set; }
    public double? SavingsInterestRate { get; set; }
}
