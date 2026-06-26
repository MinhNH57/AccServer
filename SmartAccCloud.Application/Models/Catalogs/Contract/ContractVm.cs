namespace SmartAccCloud.Application.Models.Catalogs.Contract;

public class ContractVm
{
    public string? GrpName { get; set; }
    public string ContractNumber { get; set; }
    public string? ContentContract { get; set; }
    public DateTime? SigningDate { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? ValueContract { get; set; }
    public string? Notes { get; set; }
    public string? ReferenceNumber { get; set; }
}