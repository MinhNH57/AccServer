namespace Report.Infrastructure.Views;

public class SmartFundContract
{
    public Guid Id { get; set; }
    public string? DataType{ get; set; }
    public string? ContractNumber { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? GroupCode { get; set; }
    public string? GroupName { get; set; }
    public decimal? ContractValue { get; set; }
    public DateTime? DisbursementDate { get; set; }


}