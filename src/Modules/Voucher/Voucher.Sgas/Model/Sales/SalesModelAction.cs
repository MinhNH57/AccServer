namespace Voucher.Sgas.Model.Sales;
public class SalesModelAction
{
    public Guid IdVoucher { get; set; }
    public SalesSmartDataDto SmartHead { get; set; } = new();
    public List<SalesSmartContentsDataDto> SmartContents { get; set; } = new();
    public List<SalesSmartProductInventoryDto> SalesSmartProductInventory { get; set; } = new();
}
