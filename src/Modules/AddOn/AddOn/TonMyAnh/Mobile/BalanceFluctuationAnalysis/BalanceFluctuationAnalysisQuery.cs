using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace AddOn.TonMyAnh.Mobile.BalanceFluctuationAnalysis;

public class BalanceFluctuationAnalysisResponse
{
    public string? BankName { get; set; }
    public string? BankCode { get; set; }
    public string? AccountNumber { get; set; }
    public double? TotalAmountDebit { get; set; }
    public double? TotalAmountCredit { get; set; }
    public string RecordDate { get; set; }
}

public record BalanceFluctuationAnalysisQuery(
    DateTime? StartDate,
    DateTime? EndDate,
    string? BankName,
    string? BankOfAccountReceive) : IQuery<Result<List<BalanceFluctuationAnalysisResponse>>>;