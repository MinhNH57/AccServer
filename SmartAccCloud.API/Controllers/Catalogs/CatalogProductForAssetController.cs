using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.CatalogProductForAsset;
using SmartAccCloud.Application.Services.Catalogs.ProductForAsset;
using SmartAccCloud.Domain.Entity.Catalogs;

namespace SmartAccCloud.API.Controllers.Catalogs;

[Route("api/product-for-asset")]
[ApiController]
[Authorize]
public class CatalogProductForAssetController(
    ICrudServicesAsync services, IMapper mapper,
        IApplicationDbContext context, IProductForAssetServices productForAssetServices) : ControllerBase
{

   
    [HttpGet]
    [Route("get-all-product-for-asset-by/{assetCode}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogAsset)]
    public IResult GetAll(string assetCode)
    {
        var decodedParam = Uri.UnescapeDataString(assetCode);

        var lstData = services.ReadManyNoTracked<CatalogProductForAsset>().Where(x => x.CodeAsset == decodedParam)
            .ToList();
        return Results.Ok(lstData);
    }

   
    [HttpPost]
    [Route("add-product-for-asset")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogAsset)]
    public async Task<IResult> AddProductForAsset(List<CatalogProductForAssetDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await productForAssetServices.CreateProductForAsset(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
   
    [HttpPut]
    [Route("edit-product-for-asset")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogAsset)]
    public async Task<IResult> EditProductForAsset(List<CatalogProductForAssetDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
            param.TrimStrings();
        var result = await productForAssetServices.EditProductForAsset(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);

    }

   
    [HttpPut]
    [Route("delete-lst-product-for-asset")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogAsset)]
    public async Task<IResult> DeleteProductForAsset(List<CatalogProductForAssetDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await productForAssetServices.DeleteProductForAsset(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);

    }

   
    [HttpDelete]
    [Route("delete-product-for-asset/{id}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogAsset)]
    public async Task<IResult> DeleteProductForAsset(Guid id)
    {
        var exitsPrd = await services.ReadSingleAsync<CatalogProductForAsset>(id);
        if (exitsPrd != null)
        {
            await services.DeleteAndSaveAsync<CatalogProductForAsset>(id);
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Xóa thất bại do mã không đã tồn tại")));
    }
   
    [HttpDelete]
    [Route("delete-all-product-for-asset/{assetCode}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogAsset)]
    public async Task<IResult> DeleteAllProductForAsset(string assetCode)
    {
        var decodedParam = Uri.UnescapeDataString(assetCode);

        var lstProduct = await context.CatalogProductForAsset.AsNoTracking().Where(x => x.CodeAsset == decodedParam).ToListAsync();
        context.CatalogProductForAsset.RemoveRange(lstProduct);
        int count = await context.SaveChangesAsync();
        if (count > 0)
        {
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.BadRequest();
    }
}