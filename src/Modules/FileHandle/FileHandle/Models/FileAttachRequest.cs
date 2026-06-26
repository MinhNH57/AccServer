using Microsoft.AspNetCore.Mvc;

namespace FileHandle.Models;

public class FileAttachRequest
{
    [FromForm]
    public string Type { get; set; } = string.Empty;
    [FromForm]
    public Guid IdContent { get; set; }
    [FromForm]
    public string? NumberOfVouchers { get; set; }
    [FromForm]
    public IList<IFormFile> Files { get; set; } = [];
    // public List<FileAttachModelWeb> FileAttachMode { get; set; }
    [FromForm]
    //public List<SmartFileAttach>? SmartFileAttachs { get; set; }
    public string? SmartFileAttachs { get; set; }
}
