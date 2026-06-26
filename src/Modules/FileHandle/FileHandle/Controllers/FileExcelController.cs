using Asp.Versioning;
using FileHandle.Models.Request;
using FileHandle.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileHandle.Controllers;
[ApiVersion(1)]
[Route("fileExcel")]
[ApiController]
[Authorize]
public class FileExcelController(IHandleExcelFileService handleExcelFileService, IExcelSmartDataServices smartExcelServices) : ControllerBase
{
    [HttpPost]
    [Route("upload-file-excel")]
    [EndpointSummary("Tải lên file Excel")]
    public async Task<IResult> ImportExcelSalaryTask([FromForm] IFormFile fileAttach, [FromForm] string type, CancellationToken token)
    {
        if (type == "Canidate")
        {
            var result = await handleExcelFileService.ImportDataLeave(fileAttach, token);
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
        else if (type == "RollCallLocation")
        {
            var result = await handleExcelFileService.ImportDataRollCallLocation(fileAttach, token);
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
        if (type == "ManufacturingStage")
        {
            var result = await handleExcelFileService.ImportManufacturingStage(fileAttach, token);
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
        else
        {
            var result = await handleExcelFileService.ImportDataRecruimentPosition(fileAttach, token);
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
    }
    [HttpPost]
    [Route("import-file-excel-smart")]
    [EndpointSummary("Nhập file Excel")]
    public async Task<IResult> ImportExcelSalaryTask([FromForm] IFormFile fileAttach, CancellationToken token)
    { 
        var result = await handleExcelFileService.UploadFileExcelDynamic(fileAttach, token);
        if (result.IsSuccess)
        {
            return TypedResults.Ok(result);
        }
        return TypedResults.BadRequest(result);

    }
    [HttpPost]
    [Route("create-excel-smart-data")]
    [EndpointSummary("Tạo dữ liệu excel đã import vào")]
    public async Task<IResult> ImportExcelSalaryTask(CreateExcelSmartDataRequest request, CancellationToken token)
    { 
        var result = await smartExcelServices.CreateExcelSmartData(request, token);
        if (result.IsSuccess)
        {
            return TypedResults.Ok(result);
        }
        return TypedResults.BadRequest(result);

    }
    [HttpPost]
    [Route("create-voucher-smart-data-by-excel")]
    [EndpointSummary("Tạo dữ liệu excel đã import vào")]
    public async Task<IResult> ImportExcelSalaryTask(CreateVoucherToExcelRequest request, CancellationToken token)
    { 
        var result = await smartExcelServices.CreateVoucherToExcel(request, token);
        if (result.IsSuccess)
        {
            return TypedResults.Ok(result);
        }
        return TypedResults.BadRequest(result);

    }
}
