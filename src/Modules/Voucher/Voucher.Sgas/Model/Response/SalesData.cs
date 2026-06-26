using Voucher.Sgas.Entities;

namespace Voucher.Sgas.Model.Response;
public class SalesData
{
    public SalesSmartData SalesSmartData { get; set; } = new();
    public List<SalesSmartContentsData> SalesSmartContentsData { get; set; } = new(); 
    public List<SalesSmartProductInventory> SalesSmartProductInventory { get; set; } = new(); 
}
