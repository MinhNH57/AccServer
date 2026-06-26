using Carter;
using Catalog.Base;

namespace Catalog.Features.Web.Catalog.Queries.GetAccountSymbol;

public class GetAccountSymbolEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");

        var api = vApi.MapGroup("catalog/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("acc-symbol", GetCatalogAccSymbol)
            .WithSummary("Nhận danh sách danh mục tài khoản được phân trang.")
            .WithTags("Catalogs");
    }

    private async Task<IResult> GetCatalogAccSymbol([AsParameters] CatalogService service,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] PaginationRequest pagination, CancellationToken token)
    {
        var query = new GetAccountSymbolQuery(filtering, pagination);
        var result = await service.Mediator.Send(query, token);
        return Results.Ok(result);
    }

}