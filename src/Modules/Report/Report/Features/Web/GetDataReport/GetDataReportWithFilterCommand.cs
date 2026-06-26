using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Report.Infrastructure;
using Report.Infrastructure.StoredProcedures;
using Report.Models;
using Microsoft.EntityFrameworkCore;

namespace Report.Features.Web.GetDataReport;

public record GetReportWithFilterQuery(
    GetDataReportFilterRequest Filtering) : ICommand<Result>;


public class GetDataReportFilterRequestHandler(SmartDataServices dataServices, ReportDbContext dbContext) : ICommandHandler<GetReportWithFilterQuery, Result>
{
    public async Task<Result> Handle(GetReportWithFilterQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);


        var storedProcedure = new ReportStoredProcedure(
            parameter: query.Filtering.Parameter,
            id: query.Filtering.Id,
            userCode: query.Filtering.UserCode,
            codeUnit: query.Filtering.CodeUnit,
            accountSymbol: query.Filtering.AccountSymbol,
            beginDate: query.Filtering.BeginDate,
            endDate: query.Filtering.EndDate,
            date: query.Filtering.Date,
            pathImages: query.Filtering.PathImages,
            pathLogo: query.Filtering.PathLogo,
            wareHouseCode: query.Filtering.WareHouseCode,
            productCode: query.Filtering.ProductCode,
            smartSoftware: query.Filtering.SmartSoftware,
            filter: query.Filtering.FilterStore);
        var smartRequest =
          await dataServices.GetListObject<object>(query.Filtering.StoreName, dbContext.Database.GetConnectionString()!, storedProcedure.Parameters);

        return Result.Success(smartRequest);
    }
}