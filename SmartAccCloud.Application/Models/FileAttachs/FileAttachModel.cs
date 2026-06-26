using Microsoft.AspNetCore.Http;

namespace SmartAccCloud.Application.Models.FileAttachs;

public class FileAttachModel
{
    public Guid IdContent { get; set; } = Guid.NewGuid();
    public IList<IFormFile>? Files { get; set; } = [];
    public string? ColumnTable { get; set; }
    public string? DataType { get; set; }
    public string? KeyTable { get; set; }
    public int CodeUnit { get; set; }
}