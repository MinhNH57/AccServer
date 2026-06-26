using Azure.Core;
using MiniExcelLibs;
using SmartAccCloud.Application.Models.QueryModels;
using SmartAccCloud.Application.Services.Extension;


namespace SmartAccCloud.API.Controllers.Extension;

[Route("api/code-generate")]
[ApiController]
public class CodeGenerateController(IExtensionServices extensionServices) : ControllerBase
{
    [HttpPost]
    [Route("get-code-catalog-group")]
    public async Task<IResult> GetCodeTask(QueryCodeCatalogByGroup query)
    {
        var result = await extensionServices.GetCodeCatalogByGroupCode(query);
        return Results.Ok(result);
    }
    [HttpPost]
    [Route("group-code-catalog")]
    public async Task<IResult> GroupCodeCatalog(GroupCodeQuery query)
    {
        var result = await extensionServices.GroupCodeCategory(query);
        return Results.Ok(result);
    }
    [HttpGet]
    [Route("get-tax-info-{taxCode}")]
    public async Task<IResult> GetTaxInfo(string taxCode)
    {
        var result = await extensionServices.GetTaxCodeInfo(taxCode);
        return Results.Ok(result);
    }

    [HttpGet]
    [Route("get-no-inv")]
    public async Task<IResult> GetNoCoupon([FromQuery] GetNoCouponRequest request, CancellationToken token)
    {
        var result = await extensionServices.GetNoCoupon(request, token);

        return Results.Ok(result);
    }



    [Route("upload-excel")]
    [HttpPost]
    public async Task<IResult> UploadFile(IFormFile file)
    {
        var result = await extensionServices.UploadFileExcel(file);

        return Results.Ok(result);
    }
    [Route("upload-excel-obj")]
    [HttpPost]
    public async Task<IResult> UploadFileObj(IFormFile file)
    {
        var result = await extensionServices.UploadFileExcelObj(file);

        return Results.Ok(result);
    }

}