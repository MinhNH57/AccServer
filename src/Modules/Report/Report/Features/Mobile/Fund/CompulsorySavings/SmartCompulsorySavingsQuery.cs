using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Report.Features.Mobile.Fund.FundSavingsSummaryBook;
using Report.Infrastructure;
using Report.Infrastructure.Views;
using Report.Models;

namespace Report.Features.Mobile.Fund.CompulsorySavings;

public record SmartCompulsorySavingsQuery(
    string Parameter,
    string? Code,
    string? BeginDate,
    string? EndDate,
    string? Type,
    string? FundSourceCode) : IQuery<Result>;


public class SmartFundContractQueryHandler(ReportDbContext dbContext, SmartDataServices dataServices, ICurrentUser currentUser)
    : IQueryHandler<SmartCompulsorySavingsQuery, Result>
{
    public async Task<Result> Handle(SmartCompulsorySavingsQuery query, CancellationToken cancellationToken)
    {

        var values = new { Parameter = "RuleBaseUnionView", MaUser = currentUser.CodeUser, CodeUnit = 888 };
        var lstDataRule = await dataServices
            .GetListObject<RuleBaseUnion>("CreateRuleFucnAndReport", dbContext.Database.GetConnectionString()!, values)
            .ConfigureAwait(false) ?? new();
        List<string?> listVillageCode = lstDataRule.Where(c => !string.IsNullOrEmpty(c.CodeObject)).Select(c => c.CodeObject).ToList();

        if (!string.IsNullOrEmpty(query.Code))
        {
            var data = await dbContext.FundCreditContractContents
                .AsNoTracking()
                .Where(c => c.IdContents == Guid.Parse(query.Code))
                .ToListAsync(cancellationToken: cancellationToken);

            return Result.Success(new { type = "detail", data });
        }

        var store = new FundSavingsSummaryBookStore("SavingSummaryMobile", "", "", 888, "", query.BeginDate, query.EndDate, "", "", "", "", "", query.FundSourceCode);

        var result = await dataServices.GetListObject<ReportFundSavingSummary>(store.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, store.Parameters);

        List<ReportFundSavingSummary> dataQuery;
        if (currentUser.CodeUser == "ADMIN")
        {
            return Result.Success(new { type = "all", data = result });
        }
        
        dataQuery = result.Where(c => listVillageCode.Contains(c.GroupCode)).ToList();
        return Result.Success(new { type = "all", data = dataQuery });
    }
}