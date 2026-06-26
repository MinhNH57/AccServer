using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Report.Infrastructure;
using Report.Models;

namespace Report.Features.Web.GetDataReport;
public record GetListDataArisingBalanceCommand(GetDataReportRequest Request) : ICommand<Result>;
public class GetListDataArisingBalanceHandler(SmartDataServices dataServices, ReportDbContext dbContext) : ICommandHandler<GetListDataArisingBalanceCommand, Result>
{
    public async Task<Result> Handle(GetListDataArisingBalanceCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        //[dbo].[ListDataArisingBalance] 'DebtArise','ADMIN',888,'13111','09/01/2022','09/30/2022'
        string queryStr =
            $@"exec ListDataArisingBalance '{request.Request.Parameter}', '{request.Request.UserCode}', '{request.Request.CodeUnit}', '{request.Request.AccountSymbol}',   '{request.Request.BeginDate}', '{request.Request.EndDate}'";

        var result = await dataServices
            .GetListObject<object>(queryStr, dbContext.Database.GetConnectionString()!)
            .ConfigureAwait(false);

        return Result.Success(result);
    }
}