using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Catalog.Base;
using Catalog.Features.Web.Catalog.Models;
using Serilog;

namespace Catalog.Features.Web.Catalog.Commands.CatalogLogic.CatalogSelecttedPrint;

public class CatalogSelecttedPrintApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");
        var api = vApi.MapGroup("catalog/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPost("/create-selected-print", CreateSelectedPrint)
            .WithName("create-selected-print")
            .WithSummary("Tạo danh mục select")
            .WithDescription("Tạo mới danh mục select")
            .WithTags("Catalogs");
    }
    /// <summary>
    /// Hàm này để danh sách select
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <param name="currentUser"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<IResult> CreateSelectedPrint(
    [AsParameters] CatalogService service,
    CatalogSelecttedPrintRequest request,
    ICurrentUser currentUser,
    CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        var existSelected = await service.DbContext.CatalogSelecttedPrint
            .AsNoTracking().Where(x => x.UserCode == currentUser.CodeUser).ToListAsync(cancellationToken: token);

        service.DbContext.CatalogSelecttedPrint.RemoveRange(existSelected);

        var catalogSelecttedPrint = new List<Base.Entities.CatalogSelecttedPrint>();

        foreach (var value in request.ValueSelecteds)
        {
            catalogSelecttedPrint.Add(

                new()
                {
                    UserCode = currentUser.CodeUser,
                    CatalogName = request.CatalogTable,
                    ValueSelectted = value
                });
        }

        await service.DbContext.CatalogSelecttedPrint.AddRangeAsync(catalogSelecttedPrint, token);


        if (await service.DbContext.SaveChangesAsync(token) > 0)
        {
            Log.Information($"[Catalog] User: {currentUser.CodeUser} created CatalogSelecttedPrint: {currentUser.CodeUser}");
            return Results.Ok(Result.Success(true));
        }

        //Log.Error(ex, "[Catalog] CreateAccountSymbol failed for {Acc}", request.AccountSymbol);
        return Results.BadRequest(Result.Failure(new Error("400", "Có lỗi khi cất giữ.")));
    }

}