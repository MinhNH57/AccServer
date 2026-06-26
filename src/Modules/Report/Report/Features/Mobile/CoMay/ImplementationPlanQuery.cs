using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Microsoft.EntityFrameworkCore;
using Report.Features.Mobile.CoMay.Models;
using Report.Infrastructure;

namespace Report.Features.Mobile.CoMay;

public class ImplementationPlanEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");
        var api = vApi.MapGroup("report").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("implementation-plan", Handle)
            .WithSummary("Báo cáo kế hoạc triển khai");
    }

    private async Task<IResult> Handle([AsParameters] ReportService service, [AsParameters] ImplementationPlanQueryStore request)
    {
        var query = new ImplementationPlanQuery(request);
        var result = await service.Mediator.Send(query);
        return TypedResults.Ok(result);
    }
}

public class ImplementationPlanQueryStore : QueryStoreBase
{
    public string? Date { get; set; }
    public string? PathLogo { get; set; }
    public string? PathImages { get; set; }
}

public record ImplementationPlanQuery(ImplementationPlanQueryStore QueryStore) : IQuery<Result>;

internal sealed class ImplementationPlanQueryHandler(
    SmartDataServices smartDataServices,
    ReportDbContext dbContext,
    ICurrentUser currentUser) : IQueryHandler<ImplementationPlanQuery, Result>
{
    public async Task<Result> Handle(ImplementationPlanQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);
        query.QueryStore.CodeUnit = currentUser.CodeUnit;
        query.QueryStore.UserCode = currentUser.CodeUser;

        var data = await smartDataServices.GetListObject<object>("ImplementationPlan",
            dbContext.Database.GetConnectionString()!,
            query.QueryStore);

        return Result.Success(data);
    }
}