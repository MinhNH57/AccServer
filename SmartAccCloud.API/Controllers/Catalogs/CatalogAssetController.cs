using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.CatalogAsset;
using SmartAccCloud.Application.Pagination;
using SmartAccCloud.Application.Services.Catalogs.CatalogAsset;
using SmartAccCloud.Domain.Entity.Catalogs;

namespace SmartAccCloud.API.Controllers.Catalogs;
/// <summary>
/// Api danh mục tài sản
/// </summary>
[Route("api/catalog-asset")]
[ApiController]
[Authorize]
public class CatalogAssetController(
    ICrudServicesAsync services, IMapper mapper
    , IApplicationDbContext context, IConfiguration configuration
    , ICatalogAssetServices assetServices) : ControllerBase
{
   
    [HttpGet]
    [Route("get-all")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogAsset)]
    public IResult GetAll()
    {
        var lstData = services.ReadManyNoTracked<CatalogAsset>()
            .OrderByDescending(x => x.IdAsc).ToList();
        return Results.Ok(lstData);
    }
   
    [HttpPost]
    [Route("get-all-paging")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogAsset)]
    public async Task<IResult> GetAllPaging(PaginationRequest query, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(query);

        var queryPaging = context.CatalogAsset
            .AsNoTracking()
            .Distinct() // Lọc các giá trị trùng lặp;
            .OrderByDescending(x => x.IdAsc);
        var queryResult = queryPaging.
            ProjectTo<CatalogAssetVm>(mapper.ConfigurationProvider);
        var pagedResult = await queryResult.PaginateAsync(query, token);

        return Results.Ok(Result<PagedResult<CatalogAssetVm>>.Success(pagedResult));
    }

   
    [HttpGet]
    [Route("get-asset/{codeAsset}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogAsset)]

    public async Task<IResult> GetCatalogAsset(string codeAsset)
    {
        codeAsset = codeAsset.Trim();
        var result = await services.ReadSingleAsync<CatalogAsset>(codeAsset);
        return Results.Ok(result);
    }

   
    [HttpPost]
    [Route("add-asset")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogAsset)]
    public async Task<IResult> AddCatalogAsset(CatalogAssetDto param)
    {
        param.TrimStrings();
        var result = await assetServices.CreateAsset(param);
        return Results.Ok(result);
    }

   
    [HttpPut]
    [Route("edit-asset")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogAsset)]
    public async Task<IResult> EditCatalogAsset(CatalogAssetDto param)
    {
        param.TrimStrings();
        var result = await assetServices.EditAsset(param);
        return Results.Ok(result);
    }

   
    [HttpDelete]
    [Route("delete-asset/{codeAsset}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogAsset)]

    public async Task<IResult> DeleteCatalogAsset(string codeAsset)
    {
        codeAsset = codeAsset.Trim();

        var existsCatalogAsset = await services.ReadSingleAsync<CatalogAsset>(codeAsset);
        if (existsCatalogAsset != null)
        {
            await services.DeleteAndSaveAsync<CatalogAsset>(codeAsset);
            var lstProduct = await context.CatalogProductForAsset.AsNoTracking().Where(x => x.CodeAsset == existsCatalogAsset.AssetCode).ToListAsync();
            context.CatalogProductForAsset.RemoveRange(lstProduct);
            await context.SaveChangesAsync();
            return Results.Ok(Result<bool>.Success(true));
        }

        return Results.NotFound(Result<bool>.Failure(new Error(
            ((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(),
            $"Xóa thất bại do mã '{codeAsset}' không tồn tại")));
    }
}