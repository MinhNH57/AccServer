using System.ComponentModel.DataAnnotations;

namespace FileHandle.Data.Entites;
public class FileAttach
{
    [Key]
    public Guid Id { get; set; }
    public string? ViewFile { get; set; }
    public string? DataType { get; set; }
    public string? NumberOfVoucher { get; set; }
    public Guid IdData { get; set; }
    public string? FileNames { get; set; }
    public string? FilePath { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public string? TableName { get; set; }
    public string? KeyTable { get; set; }
    public string? Notes { get; set; }
}
