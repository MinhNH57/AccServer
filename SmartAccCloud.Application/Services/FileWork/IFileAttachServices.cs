using Microsoft.AspNetCore.Http;
using SmartAccCloud.Application.Models.FileAttachs;
using SmartAccCloud.Application.Models.QueryModels;

namespace SmartAccCloud.Application.Services.FileWork;

public interface IFileAttachServices
{
    Task<Response.Result<string>> UploadFile(FileAttachModel fileAttach);
    Task<Response.Result<bool>> DeleteFile(Guid idFile);
    Task<Response.Result<bool>> DeleteAllFile(QueryFile query);
    Task<Response.Result<List<FileAttach>>> GetFilePath(QueryFile query);

    //Quỹ

    Task<Response.Result<string>> UploadFileFund(FileAttachModel fileAttach);
    Task<Response.Result<bool>> DeleteFileFund(List<string> idFile);
    Task<Response.Result<bool>> DeleteAllFileFund(QueryFile query);


}