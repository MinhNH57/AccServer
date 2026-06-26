using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.QrCodeProduct;
using SmartAccCloud.Application.Services.Catalogs.QrCodeForProduct;
using SmartAccCloud.Domain.Entity.Catalogs;


namespace SmartAccCloud.API.Controllers.Catalogs;
[Route("api/catalog-qr-for-product")]
[ApiController]
public class QrCodeProductController(
    ICrudServicesAsync services,
    IMapper mapper,
    IApplicationDbContext context,
    IQrCodeForProductServices qrCodeForProductServices) : ControllerBase
{

  
    [HttpGet]
    [Route("get-all")]
    public IResult GetAll()
    {
        var lstData = services.ReadManyNoTracked<CatalogQrCodeProduct>()
            .ToList();
        return Results.Ok(lstData);
    }

  
    [HttpGet]
    [Route("get-qr-for-product/{productCode}")]
    public IResult GetQrCodeProduct(string productCode)
    {
        var listQrcode = services.ReadManyNoTracked<CatalogQrCodeProduct>().Where(x => x.ProductCode == productCode)
            .ToList();
        return Results.Ok(listQrcode);
    }

  
    [HttpPost]
    [Route("add-qr-for-product")]
    public async Task<IResult> AddQrCodeProduct(List<QrCodeProductDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await qrCodeForProductServices.CreateQrCodeProduct(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }

  
    [HttpPut]
    [Route("edit-qr-for-product")]
    public async Task<IResult> EditQrCodeProduct(List<QrCodeProductDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await qrCodeForProductServices.EditQrCodeProduct(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }

  
    [HttpPut]
    [Route("delete-qr-for-product")]
    public async Task<IResult> DeleteQrCodeProduct(List<QrCodeProductDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await qrCodeForProductServices.DeleteQrCodeProduct(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }

  
    [HttpDelete]
    [Route("delete-all-qr-for-product/{productCode}")]
    public async Task<IResult> DeleteQrCodeProduct(string productCode)
    {
        var lstQrCode = context.CatalogQrCodeProduct.Where(x => x.ProductCode == productCode)
            .AsNoTracking().ToList();
        context.CatalogQrCodeProduct.RemoveRange(lstQrCode);
        int count = await context.SaveChangesAsync();
        if (count > 0)
            return Results.Ok();
        return Results.BadRequest();
    }
}