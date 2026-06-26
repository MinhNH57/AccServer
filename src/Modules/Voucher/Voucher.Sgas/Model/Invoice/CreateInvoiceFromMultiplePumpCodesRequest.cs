namespace Voucher.Sgas.Model.Invoice;

public class CreateInvoiceFromMultiplePumpCodesRequest
{
    public string StoreName { get; set; } = string.Empty;
    public string Parameter { get; set; } = string.Empty;
    public List<Guid> Ids { get; set; } = [];
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
}
