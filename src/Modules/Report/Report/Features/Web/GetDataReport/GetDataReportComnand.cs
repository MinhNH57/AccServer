using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Report.Infrastructure;
using Report.Models;

namespace Report.Features.Web.GetDataReport;


public record GetDataReportComnand(GetDataReportRequest Request) : ICommand<Result>;

public class GetDataReportHandler(SmartDataServices dataServices, ReportDbContext dbContext) : ICommandHandler<GetDataReportComnand, Result>
{
    public async Task<Result> Handle(GetDataReportComnand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        string queryStr =
            $@"exec {request.Request.StoreName} '{request.Request.Parameter}', '{request.Request.Id}', '{request.Request.UserCode}', '{request.Request.CodeUnit}',   '{request.Request.AccountSymbol}', '{request.Request.BeginDate}', '{request.Request.EndDate}', '{request.Request.Date}', '{request.Request.PathImages}',  '{request.Request.PathLogo}', '{request.Request.WareHouseCode}',  '{request.Request.ProductCode}', '{request.Request.SmartSoftware}'";

        var result = await dataServices.GetListObject<object>(queryStr, dbContext.Database.GetConnectionString()!, cancellationToken)
            .ConfigureAwait(false);

        return Result.Success(result);
    }
}