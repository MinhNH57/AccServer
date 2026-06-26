namespace Voucher.Sgas.Model.SmartDataPumpCode;

public class UpdatePumpCodeRequest
{
    public List<Guid> Ids { get; set; }
    public string PersonName { get; set; } = string.Empty;
    public string LicensePlates { get; set; } = string.Empty;
    public string ObjectTaxCode { get; set; } = string.Empty;
    public string ObjectCode { get; set; } = string.Empty;
    public string ObjectName { get; set; } = string.Empty;
    public string ObjectAddress { get; set; } = string.Empty;
    public string ObjectEmail { get; set; } = string.Empty;
    public string MethodOfPaymentsCode { get; set; } = string.Empty;
    public string MethodOfPaymentsName { get; set; } = string.Empty;
    public string ReasonCode { get; set; } = string.Empty;
    public string ReasonName { get; set; } = string.Empty;
    public double? DiscountRate { get; set; }
    public bool NotEnvironment { get; set; }
    public bool NotPublish { get; set; }
}
