using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Microsoft.EntityFrameworkCore;
using Report.Features.Mobile.CoMay.Models;
using Report.Infrastructure;

namespace Report.Features.Mobile.CoMay;

public class MaterialSummaryBookEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");
        var api = vApi.MapGroup("report").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("material-summary", Handle)
            .WithSummary("Sổ tổng hợp vật tư");
    }

    private async Task<IResult> Handle([AsParameters] ReportService service, [AsParameters] MaterialSummaryBookStore request)
    {
        var query = new MaterialSummaryBookQuery(request);
        var result = await service.Mediator.Send(query);

        return TypedResults.Ok(result);
    }
}

public class MaterialSummaryBookStore : QueryStoreBase
{
    public string? Date { get; set; }
    public string? PathLogo { get; set; }
    public string? PathImages { get; set; }
}

public record MaterialSummaryBookQuery(MaterialSummaryBookStore MaterialSummaryBookStore) : IQuery<Result>;

/// <summary>
/// RPSummaryMaterial:Tổn kho tổng thể và cảnh báo chạm ngưỡng
/// </summary>
/// <param name="smartDataServices"></param>
/// <param name="dbContext"></param>
/// <param name="currentUser"></param>
internal sealed class MaterialSummaryBookQueryHandler(SmartDataServices smartDataServices,
    ReportDbContext dbContext,
    ICurrentUser currentUser) : IQueryHandler<MaterialSummaryBookQuery, Result>
{
    public async Task<Result> Handle(MaterialSummaryBookQuery query, CancellationToken cancellationToken)
    {
        query.MaterialSummaryBookStore.CodeUnit = currentUser.CodeUnit;
        query.MaterialSummaryBookStore.UserCode = currentUser.CodeUser;

        var data = await smartDataServices.GetListObject<object>("MaterialSummaryBook",
            dbContext.Database.GetConnectionString()!,
            query.MaterialSummaryBookStore);

        return Result.Success(data);
    }
}