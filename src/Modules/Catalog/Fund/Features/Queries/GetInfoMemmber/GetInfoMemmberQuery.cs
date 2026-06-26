
using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Catalog.Fund.Infrastructure;
using Catalog.Fund.Infrastructure.StoredProcedures;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Features.Queries.GetInfoMemmber;


public record GetInfoMemmberQuery(
    string Parameter, string Id,
    DateTime? StartDate, DateTime? EndDate,
    string? SmartSoftware)
    : IQuery<Result<object>>;

public class GetInfoMemmberQueryHandler(SmartDataServices smartDataServices,
    CatalogFundContext dbContext,
    ICurrentUser currentUser) : IQueryHandler<GetInfoMemmberQuery, Result<object>>
{
    public async Task<Result<object>> Handle(GetInfoMemmberQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        //ArgumentNullException.ThrowIfNull(dbContext.Database.GetConnectionString());

        var storeProcedure = new GetInfoMemmberStoreProcedure(
            parameter: request.Parameter,
            id: request.Id,
            userCode: currentUser.CodeUser,
            codeUnit: currentUser.CodeUnit,
            accountSymbol: "",
            beginDate: request.StartDate ?? DateTime.Now.Date,
            endDate: request.EndDate ?? DateTime.Now.Date,
            date: request.StartDate ?? DateTime.Now.Date,
            pathImages: "",
            pathLogo: "",
            wareHouseCode: "",
            productCode: "",
            smartSoftware: request.SmartSoftware ?? string.Empty)
        {
            StoredProcedureName = "GetInfoMemmber"
        };

        var result = await smartDataServices.GetListObject<object>(storeProcedure.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, storeProcedure.Parameters);

        return Result.Success<object>(result);
    }
}