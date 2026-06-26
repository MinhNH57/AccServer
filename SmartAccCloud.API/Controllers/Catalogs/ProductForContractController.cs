using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.CatalogProductForContract;
using SmartAccCloud.Application.Services.Catalogs.ProductForContract;
using SmartAccCloud.Domain.Entity.Catalogs;


namespace SmartAccCloud.API.Controllers.Catalogs;
[Route("api/product-for-contract")]
[ApiController]
[Authorize]
public class ProductForContractController(
    ICrudServicesAsync services, IMapper mapper,
        IApplicationDbContext context,IProductForContractServices productForContractServices) : ControllerBase
{

    
    [HttpGet]
    [Route("get-all-product-for-contract-by/{contractNumber}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogContract)]
    public IResult GetAll(string contractNumber)
    {
        var decodedParam = Uri.UnescapeDataString(contractNumber);

        var lstData = services.ReadManyNoTracked<CatalogProductForContract>().Where(x => x.CodeContract == decodedParam)
            .ToList();
        return Results.Ok(lstData);
    }

    
    [HttpPost]
    [Route("add-product-for-contract")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogContract)]
    public async Task<IResult> AddProductForContract(List<ProductForContractDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await productForContractServices.CreateProductForContract(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    
    [HttpPut]
    [Route("edit-product-for-contract")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogContract)]
    public async Task<IResult> EditProductForContract(List<ProductForContractDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await productForContractServices.EditProductForContract(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);

    }

    
    [HttpPut]
    [Route("delete-lst-product-for-contract")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogContract)]
    public async Task<IResult> DeleteProductForContract(List<ProductForContractDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        var result = await productForContractServices.DeleteProductForContract(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);

    }

    
    [HttpDelete]
    [Route("delete-product-for-contract/{id}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogContract)]
    public async Task<IResult> DeleteProductForContract(Guid id)
    {
        var exitsPrd = await services.ReadSingleAsync<CatalogProductForContract>(id);
        if (exitsPrd != null)
        {
            await services.DeleteAndSaveAsync<CatalogProductForContract>(id);
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Xóa thất bại do mã không đã tồn tại")));
    }
    
    [HttpDelete]
    [Route("delete-all-product-for-contract/{contractNumber}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogContract)]
    public async Task<IResult> DeleteAllProductForContract(string contractNumber)
    {
        var decodedParam = Uri.UnescapeDataString(contractNumber);

        var lstProduct = await context.CatalogProductForContract.AsNoTracking().Where(x => x.CodeContract == decodedParam).ToListAsync();
        context.CatalogProductForContract.RemoveRange(lstProduct);
        int count = await context.SaveChangesAsync();
        if (count > 0)
        {
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.BadRequest();
    }
}
