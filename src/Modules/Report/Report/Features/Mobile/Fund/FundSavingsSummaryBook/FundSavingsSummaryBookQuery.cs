using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Report.Infrastructure;
using Report.Models;
using Report.Infrastructure.Views;

namespace Report.Features.Mobile.Fund.FundSavingsSummaryBook;

public class SmummaryFundSaving
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal PrincipalEnd { get; set; }
    public decimal InterestEnd { get; set; }
    public decimal ContractValue { get; set; }
    public string? ContractNumber { get; set; }
}

public record FundSavingsSummaryBookQuery(
    string Parameter,
    string? Code,
    string? BeginDate,
    string? EndDate,
    string? Type,
    string? FundSourceCode) : IQuery<Result>;

public class FundSavingsSummaryBookCommandHandler(ReportDbContext dbContext, SmartDataServices dataServices, ICurrentUser currentUser) : IQueryHandler<FundSavingsSummaryBookQuery, Result>
{
    public async Task<Result> Handle(FundSavingsSummaryBookQuery query, CancellationToken cancellationToken)
    {
        var values = new { Parameter = "RuleBaseUnionView", MaUser = currentUser.CodeUser, CodeUnit = 888 };
        var lstDataRule = await dataServices
            .GetListObject<RuleBaseUnion>("CreateRuleFucnAndReport", dbContext.Database.GetConnectionString()!, values)
            .ConfigureAwait(false) ?? new();

        List<string?> listVillageCode = lstDataRule.Where(c => !string.IsNullOrEmpty(c.CodeObject)).Select(c => c.CodeObject).ToList();

        var store = new FundSavingsSummaryBookStore(query.Parameter, "", currentUser.CodeUser, 888, "", query.BeginDate, query.EndDate, "", "", "", "", "", query.FundSourceCode);

        var result = await dataServices.GetListObject<ReportFundSavingSummary>(store.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, store.Parameters);

        List<SmummaryFundSaving> data;
        if (currentUser.CodeUser == "ADMIN")
        {
            data = SummarySaving(result, query.Type, query.Code, null);
            return Result.Success(data);
        }
        data = SummarySaving(result, query.Type, query.Code, listVillageCode);
        return Result.Success(data);
    }

    private List<SmummaryFundSaving> SummarySaving(in List<ReportFundSavingSummary> data, string condition, string code, List<string>? codeVillage)
    {
        List<SmummaryFundSaving> listSummanry;
        switch (condition)
        {
            case "group":
                listSummanry = (from item in data.Where(c => c.CodeWards == code
                                                             && !string.IsNullOrEmpty(c.GroupCode)
                                                             && (codeVillage is not null && codeVillage.Contains(c.GroupCode) || codeVillage == null))
                                group item by new { item.GroupCode, item.GroupName }
                    into g
                                select new SmummaryFundSaving()
                                {
                                    Code = g.Key.GroupCode,
                                    Name = g.Key.GroupName,
                                    PrincipalEnd = g.Sum(c => c.PrincipalEnd),
                                    InterestEnd = g.Sum(c => c.InterestEnd),
                                    ContractValue = g.Sum(c => c.ContractValue)
                                }).ToList();
                break;
            case "detail":
                listSummanry = data.Where(c => c.GroupCode == code).Select(a => new SmummaryFundSaving()
                {
                    Code = a.ObjectCode,
                    Name = a.ObjectName,
                    ContractNumber = a.ContractNumber,
                    ContractValue = a.ContractValue,
                    PrincipalEnd = a.PrincipalEnd,
                    InterestEnd = a.InterestEnd
                }).ToList();
                break;

            default:
                listSummanry = (from item in data.Where(c => !string.IsNullOrEmpty(c.GroupCode)
                                                             && (codeVillage is not null && codeVillage.Contains(c.GroupCode) || codeVillage == null))
                                group item by new { item.CodeWards, item.NameWards }
                    into g
                                select new SmummaryFundSaving()
                                {
                                    Code = g.Key.CodeWards,
                                    Name = g.Key.NameWards,
                                    PrincipalEnd = g.Sum(c => c.PrincipalEnd),
                                    InterestEnd = g.Sum(c => c.InterestEnd),
                                    ContractValue = g.Sum(c => c.ContractValue)
                                }).ToList();
                break;
        }

        return listSummanry;
    }

}