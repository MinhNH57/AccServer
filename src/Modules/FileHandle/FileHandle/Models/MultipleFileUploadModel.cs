using Microsoft.AspNetCore.Mvc;

namespace FileHandle.Models;

public class MultipleFileUploadModel
{
    [FromForm]
    public Guid IdContent { get; set; } = Guid.NewGuid();

    [FromForm]
    public IFormFile Files { get; set; }  = null!;

    [FromForm]
    public string? Description { get; set; }

    [FromForm]
    public string? DataType { get; set; }

    [FromForm]
    public int CodeUnit { get; set; }

    [FromForm]
    public string? NumberOfVouchers { get; set; }
}