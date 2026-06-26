using FileHandle.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.EJ2.FileManager.Base;
using Syncfusion.EJ2.FileManager.PhysicalFileProvider;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace FileHandle.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FileManagerController : ControllerBase
{
    string basePath;
    PhysicalFileProvider operation;
    private readonly IFileAttachServices _fileManagerService;

    public FileManagerController(IWebHostEnvironment hostingEnvironment , IFileAttachServices fileManagerService)
    {
        basePath = hostingEnvironment.ContentRootPath;
        operation = new PhysicalFileProvider();
        operation.RootFolder(basePath + "\\Uploads\\0109393468");
        _fileManagerService = fileManagerService;
    }

    [HttpPost("FileOperations")]
    public async Task<object> FileOperations([FromBody] FileManagerDirectoryContent args , [FromQuery] string CodeUser)
    {
        switch (args.Action)
        {
            case "read":
                var files = this.operation.GetFiles(args.Path, args.ShowHiddenItems);
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
                var filtered = files.Files.Where(f => f.IsFile == false || allowedExtensions.Contains(Path.GetExtension(f.Name).ToLower())).ToList();
                files.Files = filtered;
                return this.operation.ToCamelCase(files);
            case "delete":
                return this.operation.ToCamelCase(this.operation.Delete(args.Path, args.Names));
            case "copy":
                return this.operation.ToCamelCase(this.operation.Copy(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
            case "move":
                return this.operation.ToCamelCase(this.operation.Move(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
            case "details":
                return this.operation.ToCamelCase(this.operation.Details(args.Path, args.Names));
            case "create":
                return this.operation.ToCamelCase(this.operation.Create(args.Path, args.Name));
            case "search":
                return this.operation.ToCamelCase(this.operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive));
            case "rename":
                return this.operation.ToCamelCase(this.operation.Rename(args.Path, args.Name, args.NewName));
            default:
                return null;
        }
    }

    [Route("Download")]
    public IActionResult Download(string downloadInput)
    {
        FileManagerDirectoryContent content = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
        return operation.Download(content.Path, content.Names);
    }


    [Route("Upload")]
    public IActionResult Upload(string path, IList<IFormFile> uploadFiles, string action)
    {
        FileManagerResponse uploadResponse = operation.Upload(path, uploadFiles, action, null);
        if (uploadResponse.Error != null)
        {
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.StatusCode = Convert.ToInt32(uploadResponse.Error.Code);
            Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = uploadResponse.Error.Message;
        }
        return Content("");
    }

    [HttpGet("GetImage")]
    public IActionResult GetImage(FileManagerDirectoryContent args)
    {
        return operation.GetImage(args.Path, null, false, null, null);
    }
}
