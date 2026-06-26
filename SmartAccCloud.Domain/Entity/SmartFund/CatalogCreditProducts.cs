namespace SmartAccCloud.Domain.Entity.SmartFund;

public class CatalogCreditProducts
{
    public string? CreditProductCode {get;set;}
    public string? CreditProductCodeSaving {get;set;}
    public string? CreditProductName {get;set;}
    public int? Period {get;set;}
    public double? InterestRateMonth {get;set;}
    public double? InterestRateYear {get;set;}
    public double? InterestSaveRateYear {get;set;}
    public double? InterestSaveRateMonth {get;set;}
    public double? SavingsCollectionRate {get;set;}
    public double? OverdueInterestRate {get;set;}
    public int? NumberOfMonthsToPayInterestOnce {get;set;}
    public int? NumberOfMonthsToPayOnce {get;set;}
    public string? FormOfCapitalPayment {get;set;}
    public string? InterestPaymentMethod {get;set;}
    public bool InterestIsEqual {get;set;}
    public bool FixedAnnuity {get;set;}
    public bool InterestOnPrincipalBalance {get;set;}
    public bool ByWeek {get;set;}
    public bool DailyInterest {get;set;}
    public bool Rounding {get;set;}
    public bool LastPeriod {get;set;}
    public string? Notes {get;set;}
    public Guid Id {get;set;}
    public int IdAsc {get;set;}
    public int? CodeUnit {get;set;}
    public bool IsActive {get;set;}
    public decimal? AmountSavings {get;set;}
    public string? FundingSourceCode {get;set;}
    public string? FundingSourceName {get;set;}
    public bool IsUse {get;set;}
    public double? InsuranceRate {get;set;}
    public double? SavingsRate {get;set;}
    public double? TotalSavingsRate {get;set;}
    public double? CapitalRate {get;set;}
    public double? InterestRate {get;set;}
}