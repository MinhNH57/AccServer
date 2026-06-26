using BuildingBlocks.Response;
using FileHandle.Data.Entites;
using FileHandle.Models;

namespace FileHandle.Services.HRM;

public interface IFileHandleHRMService
{
    Task<Result<List<HRM_SmartFileAttach>>> GetListFile(string IdContents, CancellationToken token);
    Task<Result<HRM_SmartFileAttach>> GetSmartFileAttachById(Guid Id, CancellationToken token);
    Task<Result> HRMUploadListFiles(FileAttachRequest request, CancellationToken token);
}
