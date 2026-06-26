using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.Products.Product;
using SmartAccCloud.Domain.Entity.Catalogs;


namespace SmartAccCloud.API.Controllers.Catalogs;

[Route("api/catalog-product")]
[ApiController]
[Authorize]
public class ProductController(
    ICrudServicesAsync services,
    IMapper mapper) : ControllerBase
{

    [HttpGet]
    [Route("get-all")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogProduct)]
    public IResult GetAll()
    {
        var lstData = services.ReadManyNoTracked<CatalogProduct>().ProjectTo<ProductVm>(mapper.ConfigurationProvider)
            .ToList();
        return Results.Ok(lstData);
    }
    [HttpGet]
    [Route("get-all-bom-product")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogProduct)]
    public IResult GetAllBom()
    {
        var lstData = services.ReadManyNoTracked<CatalogProduct>().Where(x => x.FinishedProduct == true).ProjectTo<ProductVm>(mapper.ConfigurationProvider)
            .ToList();
        return Results.Ok(lstData);
    }
    [HttpGet]
    [Route("get-product/{codeProduct}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogProduct)]
    public async Task<IResult> GetProduct(string codeProduct)
    {
        var costSoldBase = await services.ReadSingleAsync<CatalogProduct>(codeProduct);
        if (costSoldBase != null)
        {
            return Results.Ok(Result<CatalogProduct>.Success(costSoldBase));
        }

        return Results.BadRequest(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(),
            "Mã không đã tồn tại")));
    }

    [HttpPost]
    [Route("add-product")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogProduct)]
    public async Task<IResult> AddProduct(CatalogProduct param)
    {
        param.TrimStrings();
        var result = await services.CreateAndSaveAsync(param);
        return Results.Ok(Result<CatalogProduct>.Success(result));
    }

    [HttpPut]
    [Route("edit-product")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogProduct)]
    public async Task<IResult> EditProduct(ProductDto param)
    {
        param.TrimStrings();
        var existProduct =
            await services.ReadSingleAsync<CatalogProduct>(param.ProductCode).ConfigureAwait(false);
        if (existProduct != null)
        {
            existProduct = mapper.Map(param, existProduct);
            await services.UpdateAndSaveAsync(existProduct);
            return Results.Ok(Result<bool>.Success(true));
        }

        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.Duplicate).ToString(),
            $"Sửa thất bại do mã không đã tồn tại")));
    }

    [HttpDelete]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogProduct)]
    [Route("delete-product/{codeProduct}")]
    public async Task<IResult> DeleteProduct(string codeProduct)
    {
        var existProduct = await services.ReadSingleAsync<CatalogProduct>(codeProduct);
        if (existProduct != null)
        {
            await services.DeleteAndSaveAsync<CatalogProduct>(codeProduct);
            return Results.Ok(Result<bool>.Success(true));
        }

        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(),
            "Xóa thất bại do mã không đã tồn tại")));
    }
}