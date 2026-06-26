using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Microsoft.EntityFrameworkCore;
using Report.Features.Mobile.CoMay.Models;
using Report.Infrastructure;

namespace Report.Features.Mobile.CoMay;

public class ReportPrintActivityEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");
        var api = vApi.MapGroup("report").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("activity", Handle)
            .WithSummary("Báo cáo hoạt động kinh doanh");

    }

    /// <summary>
    /// Expenses:  BC chi phí 
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    private async Task<IResult> Handle([AsParameters] ReportService service, [AsParameters] ReportPrintActivityStoreQuery request)
    {
        var query = new ReportPrintActivityQuery(request);
        var result = await service.Mediator.Send(query);
        return TypedResults.Ok(result);
    }
}

public class ReportPrintActivityStoreQuery : QueryStoreBase
{
    public string? Date { get; set; }
    public string? PathLogo { get; set; }
    public string? PathImages { get; set; }
}

public record ReportPrintActivityQuery(ReportPrintActivityStoreQuery QueryStore) : IQuery<Result>;

internal sealed class ReportPrintActivityQueryHandler(
    SmartDataServices smartDataServices,
    ReportDbContext dbContext,
    ICurrentUser currentUser) : IQueryHandler<ReportPrintActivityQuery, Result>
{
    
    public async Task<Result> Handle(ReportPrintActivityQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);
        query.QueryStore.CodeUnit = currentUser.CodeUnit;
        query.QueryStore.UserCode = currentUser.CodeUser;

        var data = await smartDataServices.GetListObject<object>("ReportPrintActivity",
            dbContext.Database.GetConnectionString()!,
            query.QueryStore);

        return Result.Success(data);
    }
}