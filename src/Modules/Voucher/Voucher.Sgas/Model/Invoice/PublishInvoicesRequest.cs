namespace Voucher.Sgas.Model.Invoice;

public class PublishInvoicesRequest
{
    public List<Guid> Ids { get; set; }
    public string ProviderName { get; set; }
}
