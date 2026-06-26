using System.Linq.Dynamic.Core;
using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Report.Infrastructure;

namespace Report.Features.Mobile.Fund.FunRepaymentPlan;

public record FunRepaymentPlanQuery(string? SearchField, string? SortField) : IQuery<Result>;

public class FunRepaymentPlanQueryHandler(ReportDbContext dbContext, ICurrentUser currentUser,SmartDataServices dataServices) : IQueryHandler<FunRepaymentPlanQuery, Result>
{
    public async Task<Result> Handle(FunRepaymentPlanQuery query, CancellationToken cancellationToken)
    {
        IQueryable queryData;


        queryData = dbContext.FunRepaymentPlan
            .AsNoTracking()
            .Where("ContractNumber == @0 || ObjectCode == @0 || ObjectName == @0", query.SearchField);
        if (!string.IsNullOrEmpty(query.SortField))
        {
            var splitStr = query.SortField.Split(":");
            if (splitStr.Length != 2) throw new BadRequestException("Sort field không đúng định dạng");

            queryData = queryData.OrderBy($"{splitStr[0]} {splitStr[1]}");
        }
        else
        {
            queryData = queryData.OrderBy("FirstDateOfPayment");
        }

        var data = await queryData.ToDynamicListAsync(cancellationToken: cancellationToken);

        return Result.Success(data);
    }
}