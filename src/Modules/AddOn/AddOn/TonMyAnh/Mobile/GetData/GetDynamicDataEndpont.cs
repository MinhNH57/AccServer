using BuildingBlocks.Pagination.Version1;
using Carter;

namespace AddOn.TonMyAnh.Mobile.GetData;

public class GetDynamicDataEndpont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Steel");

        var api = vApi.MapGroup("steels")
            .HasApiVersion(1.0);

        api.RequireAuthorization();

        api.MapGet("/get-data/{tableName}", GetDynamicData)
            .WithName("GetData")
            .WithSummary("Lấy dữ liệu động");
    }

    private async Task<IResult> GetDynamicData(
       [AsParameters] AddOnService service,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        string tableName,
        CancellationToken token)
    {
        var query = new GetDynamicDataQuery(filtering, sorting, pagination, tableName);
        var result = await service.Mediator.Send(query, token);

        return Results.Ok(result);

    }
}