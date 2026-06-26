namespace Voucher.Sgas.Model.Invoice;

public class CreateInvoiceRequest
{
    public Guid Id { get; set; }
    public string Parameter { get; set; } = string.Empty;
}
