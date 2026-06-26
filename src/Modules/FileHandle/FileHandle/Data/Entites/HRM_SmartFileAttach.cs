using System.ComponentModel.DataAnnotations;

namespace FileHandle.Data.Entites;

public class HRM_SmartFileAttach
{
    [Key]
    public Guid Id { get; set; }
    public Guid IdContents { get; set; }
    public string? Description { get; set; }
    public string? PathFile { get; set; }
    public string? FileNames { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public string? NumberOfVouchers { get; set; }
    public string? SizeFile { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public string? TypeFile { get; set; }
    public string? CodeUser { get; set; }
}
