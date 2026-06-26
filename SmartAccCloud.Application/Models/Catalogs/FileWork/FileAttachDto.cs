namespace SmartAccCloud.Application.Models.Catalogs.FileWork;

public class FileAttachDto
{
    public Guid Id { get; set; }
    public string? DataType { get; set; }
    public string? NumberOfVoucher { get; set; }
    public string? FilePath { get; set; }
    public string? FileNames { get; set; }
    public bool IsActive { get; set; }
    public string? TableName { get; set; }
    public string? KeyTable { get; set; }
    public string? Notes { get; set; }
}