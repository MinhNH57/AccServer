namespace FileHandle.Models;

public class SmartTypeDataImportExcel
{
    public string DataType { get; set; }

    public string? DataName { get; set; }

    public string? TableName { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid(); 
    public int? CodeUnit { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; }
}
