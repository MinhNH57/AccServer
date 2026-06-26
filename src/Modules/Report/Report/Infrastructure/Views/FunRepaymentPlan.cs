namespace Report.Infrastructure.Views;

public class FunRepaymentPlan
{
    public Guid Id { get; set; }
    public string? DataType { get; set; }
    public string? DataName { get; set; }
    public int? CodeUnit { get; set; }
    public string? ContractNumber { get; set; }
    public DateTime? SignDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? GroupCode { get; set; }
    public string? FundingSourceCode { get; set; }
    public string? FundingSourceName { get; set; }
    public DateTime? FirstDateOfPayment { get; set; }
    public DateTime? DateOfPayment { get; set; }
    public int? NumberOfDays { get; set; }
    public int? PaymentPeriod { get; set; }
    public decimal? OriginalBalance { get; set; }
    public decimal? PrincipalAmount { get; set; }
    public decimal? AmountOfInterest { get; set; }
    public decimal? TotalInterestPrincipal { get; set; }
    public decimal? AmountOfSavings { get; set; }
    public decimal? TotalAmount { get; set; }
    public string? Notes { get; set; }
    public int? BorrowingTime { get; set; }
    public double? InterestRateMonth { get; set; }
    public double? InterestRateYear { get; set; }
    public DateTime? DateDue { get; set; }
    public DateTime? DisbursementDate { get; set; }
}
