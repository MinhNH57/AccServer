namespace Voucher.Sgas.Model.Invoice;

public class CreateInvoiceFromPumpCodeResponse
{
    public Guid Id { get; set; }
    public string NumberOfVouchers { get; set; }
    public string DataType { get; set; }
    public double Quantity { get; set; }
}
