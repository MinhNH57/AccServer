using System.ComponentModel.DataAnnotations;
using BuildingBlocks.Response;
using Carter;
using Catalog.Base.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Base.Features;

public class CatalogUnitApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");

        var api = vApi.MapGroup("catalog-units").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("get-all", GetAll)
            .WithName("get-catalog-unit")
            .WithSummary("Danh sách danh mục đơn vị.")
            .WithDescription("Nhận danh sách các mục được phân trang trong danh mục.")
            .WithTags("Catalogs")
            .AllowAnonymous();

    }

    [AllowAnonymous]
    private static async Task<IResult> GetAll(
        [FromServices] CatalogDbContext dbContext,
        [FromHeader(Name = TenantConstant.TenantIdHeader)][Required]
        string tenantId,
        CancellationToken token)
    {
        var lst = await dbContext.CatalogUnit
           .Where(c => c.IsActive)
           .ToListAsync(cancellationToken: token);

        return Results.Ok(Result.Success(lst));
    }
}