using Microsoft.AspNetCore.Mvc;

namespace FileHandle.Models;

public class FileAttachModel
{
    [FromForm]
    public Guid IdContent { get; set; } = Guid.NewGuid();

    [FromForm]
    public IList<IFormFile>? Files { get; set; } = [];

    [FromForm]
    public string? ColumnTable { get; set; }

    [FromForm]
    public string? Description { get; set; }

    [FromForm]
    public string? DataType { get; set; }

    [FromForm]
    public string? KeyTable { get; set; }

    [FromForm]
    public int CodeUnit { get; set; }

    [FromForm]
    public string? NumberVoucher { get; set; }
}