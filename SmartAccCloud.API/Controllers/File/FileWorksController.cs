using SmartAccCloud.Application.Models.FileAttachs;
using SmartAccCloud.Application.Models.QueryModels;
using SmartAccCloud.Application.Services.FileWork;


namespace SmartAccCloud.API.Controllers.File;
[ApiVersion(1)]
[Route("api/file-attach")]
[Route("api/v{v:apiVersion}/file-attach")]
[ApiController]
[Authorize]
public class FileWorksController(IFileAttachServices fileAttachServices) : ControllerBase
{
    [HttpPost]
    [Route("upload-file")]
    public async Task<IResult> UploadFileTask([FromForm] FileAttachModel fileAttach)
    {
        if (fileAttach.Files == null || !fileAttach.Files.Any())
        {
            return Results.BadRequest("No files provided.");
        }
        const long maxFileSize = 5 * 1024 * 1024;
        foreach (var item in fileAttach.Files)
        {
            if (item.Length > maxFileSize)
            {
                return Results.BadRequest($"File {item.FileName} quá dung lượng cho phép (20MB).");
            }
        }
        var result = await fileAttachServices.UploadFile(fileAttach);
        return Results.Ok(result);

    }

    [HttpDelete]
    [Route("delete-file/{idFile}")]
    public async Task<IResult> DeleteFile(Guid idFile)
    {
        var isSuccess = await fileAttachServices.DeleteFile(idFile);

        return Results.Ok(isSuccess);
    }


    [HttpPost]
    [Route("delete-all-file")]
    public async Task<IResult> DeleteAllFile(QueryFile query)
    {
        var isSuccess = await fileAttachServices.DeleteAllFile(query);

        return Results.Ok(isSuccess);
    }

    [HttpPost]
    [Route("upload-file-fund")]
    public async Task<IResult> UploadFileFundTask([FromForm] FileAttachModel fileAttach)
    {
        if (fileAttach.Files == null || !fileAttach.Files.Any())
        {
            return Results.BadRequest("Không có file cần tải lên.");
        }

        const long maxTotalFileSize = 10 * 1024 * 1024; // 10MB
        foreach (var file in fileAttach.Files)
        {

            long totalSize = file.Length;

            if (totalSize > maxTotalFileSize)
            {
                return Results.BadRequest($"Tổng dung lượng file tải lên vượt quá giới hạn cho phép (10MB).");
            }
        }

        var result = await fileAttachServices.UploadFileFund(fileAttach);
        return Results.Ok(result);

    }


    [HttpPost]
    [Route("delete-file-fund")]
    public async Task<IResult> DeleteFileFund(List<string> idFile)
    {
        var isSuccess = await fileAttachServices.DeleteFileFund(idFile);

        return Results.Ok(isSuccess);
    }

    [HttpPost]
    [Route("delete-all-file-fund")]
    public async Task<IResult> DeleteAllFileFundTask(QueryFile query)
    {
        var isSuccess = await fileAttachServices.DeleteAllFileFund(query);

        return Results.Ok(isSuccess);
    }

    [HttpPost]
    [Route("get-file-path")]
    public async Task<IResult> GetFilePathTask(QueryFile query)
    {
        var result = await fileAttachServices.GetFilePath(query);

        return Results.Ok(result);
    }
}
