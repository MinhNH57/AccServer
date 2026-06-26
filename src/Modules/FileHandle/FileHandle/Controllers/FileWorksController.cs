using Asp.Versioning;
using FileHandle.Models;
using FileHandle.Services;
using FileHandle.Services.HRM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileHandle.Controllers;
[ApiVersion(1)]
[Route("file")]
[ApiController]
[Authorize]
public class FileWorksController(IFileAttachServices fileAttachServices , IFileHandleHRMService fileAttachHrmServices) : ControllerBase
{
    [HttpPost]
    [Route("upload-file")]
    [EndpointSummary("Tải file")]
    public async Task<IResult> UploadFileFundTask([FromForm] FileAttachModel fileAttach)
    {
        if (fileAttach.Files == null || !fileAttach.Files.Any())
        {
            return Results.BadRequest("Không có file cần tải lên.");
        }

        const long maxTotalFileSize = 5 * 1024 * 1024; 
        foreach (var file in fileAttach.Files)
        {

            long totalSize = file.Length;

            if (totalSize > maxTotalFileSize)
            {
                return Results.BadRequest($"Tổng dung lượng file tải lên vượt quá giới hạn cho phép (10MB).");
            }
        }
        var fileName = fileAttach.Files.First().FileName;
        var result = await fileAttachServices.UploadFileFund(fileAttach);
        return Results.Ok(result);
    }

    [HttpPost]
    [Route("upload-multi-files-v2")]
    [Authorize]
    [EndpointSummary("Upload nhiều file")]
    public async Task<IResult> UploadMultipleFile([FromForm] List<MultipleFileUploadModel> request, CancellationToken token)
    {
        var result = await fileAttachServices.UploadMutipleFileV2(request, token);
        if (!result.IsSuccess)
            return TypedResults.BadRequest(result);

        return TypedResults.Ok(result);
    }

    [HttpPost]
    [Route("upload-multi-files")]
    [Authorize]
    [EndpointSummary("Upload nhiều file")]
    public async Task<IResult> UploadFile([FromForm] FileAttachRequest request, CancellationToken token)
    {
        var result = await fileAttachServices.UploadFiles(request, token);
        if (!result.IsSuccess)
            return TypedResults.BadRequest(result);

        return TypedResults.Ok(result);
    }

    [HttpPost]
    [Route("upload-list-files")]
    [Authorize]
    [EndpointSummary("Upload nhiều file trong HRM")]
    public async Task<IResult> UploadListFile([FromForm] FileAttachRequest request, CancellationToken token)
    {
        var result = await fileAttachHrmServices.HRMUploadListFiles(request, token);
        if (!result.IsSuccess)
            return TypedResults.BadRequest(result);

        return TypedResults.Ok(result);
    }

    [HttpGet]
    [Route("get-file/{code}")]
    [Authorize]
    [EndpointSummary("Tải file đã up")]
    public async Task<IResult> GetFileByIdVoucher(string code, CancellationToken token)
    {
        var result = await fileAttachServices.GetFileAttchByCode(code, token);

        return Results.Ok(result);
    }

    [HttpGet]
    [Route("get-lstfile/{IdContents}")]
    [Authorize]
    [EndpointSummary("Lấy ra danh sách File theo IdContents")]
    public async Task<IResult> GetFileByIdContents(string IdContents, CancellationToken token)
    {
        var result = await fileAttachHrmServices.GetListFile(IdContents, token);

        return Results.Ok(result);
    }


    [HttpPost]
    [Route("HRMupload-file")]
    [EndpointSummary("Tải file")]
    public async Task<IResult> HRMUploadFileTask([FromForm] FileAttachModel fileAttach)
    {
        if (fileAttach.Files == null || !fileAttach.Files.Any())
        {
            return Results.BadRequest("Không có file cần tải lên.");
        }

        const long maxTotalFileSize = 5 * 1024 * 1024;
        foreach (var file in fileAttach.Files)
        {

            long totalSize = file.Length;

            if (totalSize > maxTotalFileSize)
            {
                return Results.BadRequest($"Tổng dung lượng file tải lên vượt quá giới hạn cho phép (10MB).");
            }
        }
        var result = await fileAttachServices.HRMUploadFile(fileAttach);
        return Results.Ok(result);
    }



    [HttpPost]
    [Route("delete-file")]
    [EndpointSummary("Xóa file")]
    public async Task<IResult> DeleteFileFund(List<string> idFile)
    {
        var isSuccess = await fileAttachServices.DeleteFileFund(idFile);

        return Results.Ok(isSuccess);
    }

    [HttpPost]
    [Route("delete-all-file")]
    [EndpointSummary("Xóa tất cả file")]
    public async Task<IResult> DeleteAllFileFundTask(QueryFile query)
    {
        var isSuccess = await fileAttachServices.DeleteAllFileFund(query);

        return Results.Ok(isSuccess);
    }

    [HttpPost]
    [Route("get-file")]
    [EndpointSummary("Tải file đã up")]
    public async Task<IResult> GetFileByProduct(QueryFile query)
    {
        var isSuccess = await fileAttachServices.GetFilePath(query);

        return Results.Ok(isSuccess);
    }

    [HttpGet("get-file-by-id/{id}")]
    [Authorize]
    [EndpointSummary("Download file bằng ID")]
    public async Task<IActionResult> GetFileById(Guid id)
    {
        var resultFile = await fileAttachHrmServices.GetSmartFileAttachById(id, CancellationToken.None);
        if (!resultFile.IsSuccess || resultFile.Data == null)
            return BadRequest("Không tìm thấy file cần tải");

        var fileInfo = resultFile.Data; // SmartFileAttach
        string relativePath = fileInfo.PathFile; // Ví dụ: Uploads/SignatureImages/906/369.jpg
        string fileName = fileInfo.FileNames ?? "downloads";

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

        if (!System.IO.File.Exists(filePath))
            return BadRequest("Không tìm thấy file trên server");

        var memory = new MemoryStream();
        await using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        string contentType = fileInfo.TypeFile ?? GetContentTypeFromDb(filePath);

        return File(memory.ToArray(), contentType, fileName);
    }

    [HttpGet]
    [Route("download-template-file/{type}")]
    [Authorize]
    [EndpointSummary("Tải file template")]
    public async Task<IActionResult> DownloadSampleFile(string type)
    {
        string path = string.Empty;
        string extension = string.Empty;
        switch (type)
        {
            case "Candidate":
                path = Path.Combine(Directory.GetCurrentDirectory(), "Downloads", "Template", "Candidate", "CandidateTemple.xlsx");
                extension = ".xlsx";
                break;
            case "PositionRecruiment":
                path = Path.Combine(Directory.GetCurrentDirectory(), "Downloads", "Template", "RecruitmentPosition", "RecruitmentPositionTemplate.xlsx");
                extension = ".xlsx";
                break;
            case "RollCallLocation":
                path = Path.Combine(Directory.GetCurrentDirectory(), "Downloads", "Template", "RollCallLocation", "RollCallLocation.xlsx");
                break;
            case "ManufacturingStage":
                path = Path.Combine(Directory.GetCurrentDirectory(), "Downloads", "Template", "ManufacturingStage", "ExcelManufacturingStage.xlsx");
                break;
        }
        if (!System.IO.File.Exists(path))
        {
            return BadRequest("Không tìm thấy file cần tải");
        }

        var memory = new MemoryStream();
        await using (var stream = new FileStream(path, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return File(memory.ToArray(), GetContentType(path), type + extension);
    }


    private string GetContentTypeFromDb(string path)
    {
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(path, out string contentType))
        {
            contentType = "application/octet-stream";
        }
        return contentType;
    }

    private string GetContentType(string path)
    {
        var ext = Path.GetExtension(path).ToLowerInvariant();
        switch (ext)
        {
            case ".xlsx":
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            case ".xls":
                return "application/vnd.ms-excel";

            default:
                return "application/octet-stream";
        }
    }
}
