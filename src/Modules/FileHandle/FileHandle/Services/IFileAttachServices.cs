using BuildingBlocks.Response;
using FileHandle.Data.Entites;
using FileHandle.Models;

namespace FileHandle.Services;

public interface IFileAttachServices
{
    Task<Result<string>> UploadFileFund(FileAttachModel fileAttach);
    Task<Result<string>> UploadMutipleFileV2(List<MultipleFileUploadModel> request, CancellationToken token);
    Task<Result<string>> HRMUploadFile(FileAttachModel fileAttach);
    Task<Result> UploadFiles(FileAttachRequest request, CancellationToken token);
    Task<Result<bool>> DeleteFileFund(List<string> idFile);
    Task<Result<bool>> DeleteAllFileFund(QueryFile query, string? tenantId = null);
    Task<Result<List<SmartFileAttach>>> GetFilePath(QueryFile query);
    Task<Result<SmartFileAttach>> GetFileAttchByCode(string code, CancellationToken token);
    Task<Result<SmartFileAttach>> GetSmartFileAttachById(Guid IdContents, CancellationToken token);
    Task<Result<List<SmartFileAttach>>> GetFileAttchByUser(string codeUser, CancellationToken token);
    Task<Result<List<SmartFileAttach>>> GetListFile(string IdContents, CancellationToken token);
}