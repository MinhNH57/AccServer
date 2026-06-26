using Systems.Infrastructure.Entities;

namespace Systems.Models.ExcelRequest;

public class ExcelDataUpdateRequest
{
    public string DataType { get; set; } = string.Empty;

    public List<SmartMapColumnExcel> ListConfig { get; set; } = new();
}
