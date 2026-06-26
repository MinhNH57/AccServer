namespace Catalog.Base.Features.Mobile;

public class Api: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");

        var api = vApi.MapGroup("mobile/catalog/").HasApiVersion(1.0).RequireAuthorization();

    }
}