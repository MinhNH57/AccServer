using Carter;
using Catalog.Base;

namespace Catalog.Features.Web.Catalog.Queries.GetCatalogDistinctBy;

public class GetCatalogDistintByEndpont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");
        var api = vApi.MapGroup("catalog/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("/get-catalog-distint", GetCodeVillage)
            .WithTags("Catalogs");
    }

    private async Task<IResult> GetCodeVillage([AsParameters] CatalogService service,
        [AsParameters]GetCatalogDistintByQuery _query, CancellationToken token)
    {
        var query = new GetCatalogDistintByCommand(_query);
        var result = await service.Mediator.Send(query, token);
        return Results.Ok(result);
    }
}