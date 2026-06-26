using Carter;
using Catalog.Base;
using Catalog.Features.Web.Fund.Store.StoreModel;

namespace Catalog.Features.Web.Fund.Store.Api;

public class StoreApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Store");
        var api = vApi.MapGroup("fund/store/").HasApiVersion(1.0).RequireAuthorization();
        api.MapPost("fund-store-handler", ImportExcelObj)
            .DisableAntiforgery()
            .WithSummary("Xử lý dữ liệu qua store")
            .WithDescription("Xử lý dữ liệu qua store.")
            .WithTags("Store");

 
    }

    private async Task<IResult> ImportExcelObj(
        [AsParameters] CatalogService service, StoreModelQuery file,
        CancellationToken token)
    {
        var request = new StoreQueryComnand(file);
        var resut = await service.Mediator.Send(request, token);
        return Results.Ok(resut);
    }
 
}
