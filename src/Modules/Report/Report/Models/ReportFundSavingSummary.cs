namespace Report.Models;

public class ReportFundSavingSummary
{
    public Guid IdVoucher { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? ContractNumber { get; set; }
    public string? GroupCode { get; set; }
    public string? GroupName { get; set; }
    public decimal PrincipalEnd { get; set; }
    public decimal InterestEnd { get; set; }
    public decimal ContractValue { get; set; }
    public string? CodeWards { get; set; }
    public string? NameWards { get; set; }
}