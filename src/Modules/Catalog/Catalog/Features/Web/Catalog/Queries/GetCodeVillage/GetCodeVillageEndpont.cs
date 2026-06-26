using Carter;
using Catalog.Base;

namespace Catalog.Features.Web.Catalog.Queries.GetCodeVillage;

public class GetDebitBlancEndpont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");
        var api = vApi.MapGroup("catalog/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("/get-code-village", GetCodeVillage)
            .WithTags("Catalogs");
    }

    private async Task<IResult> GetCodeVillage([AsParameters] CatalogService service,
        [AsParameters] GetCodeVillageRequest request, CancellationToken token)
    {
        var query = new GetCodeVillagCommand(request);
        var result = await service.Mediator.Send(query, token);
        return Results.Ok(result);
    }
}