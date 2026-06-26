namespace Voucher.Sgas.Model.Store;
public class StoreRequestModel
{
    public string? StoreName { get; set; }
    public string? Parameter { get; set; }
    public string? Id { get; set; } = string.Empty;
    public string? UserCode { get; set; } = string.Empty;
    public int? CodeUnit { get; set; } = 0;
    public string? AccountSymbol { get; set; } = string.Empty;
    public string? BeginDate { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");
    public string? EndDate { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");
    public string? Date { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");
    public string? WareHouseCode { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    // Bổ sung
    public string? PathImages { get; set; } = string.Empty;
    public string? PathLogo { get; set; } = string.Empty;
    public string? SmartSoftware { get; set; } = string.Empty;
}
