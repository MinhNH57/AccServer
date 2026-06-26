namespace FileHandle.Models.Request;

public class CreateExcelSmartDataRequest 
{
    public List<ExcelSmartData> DataImport { get; set; } = new();
    public string? DataType { get; set; } = string.Empty;

}
