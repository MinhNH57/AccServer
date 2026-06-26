namespace Voucher.Sgas.Model.Store;

public class GetInventoryForTradingRequest
{
    public string Parameter { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string UserCode { get; set; } = string.Empty;
    public int CodeUnit { get; set; } = 0;
    public string WarehoseCode { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public string CodeSupplier { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string ListProduct { get; set; } = string.Empty;
    public bool OnlyExists { get; set; } = false;
}
