namespace Catalog.Base.Entities;
public class ProjectSystemRecordsManagement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Catalog { get; set; }
    public string? Procedure { get; set; }
    public string? SubCategory { get; set; }
    public string? MinutesName { get; set; }
    public string? MinutesType { get; set; }
    public string? IssuingUnit { get; set; }
    public DateTime? IssueDate { get; set; }
    public string? Executor { get; set; }
    public decimal? BidPrice { get; set; }
    public string? ResponsiblePerson { get; set; }
    public string? ImplementationMethod { get; set; }
    public decimal? CompletionRate { get; set; }
    public string? LegalBasis { get; set; }
    public string? ApprovalAgency { get; set; }
    public DateTime? StartDate { get; set; }
    public string? Position { get; set; }
    public decimal? EstimateValue { get; set; }
    public string? SupportPerson { get; set; }
    public string? PaymentCondition { get; set; }
    public DateTime? MinutesDate { get; set; }
    public string? Reference { get; set; }
    public string? MinutesNumber { get; set; }
    public string? MinutesContent { get; set; }
    public decimal? ProgressPercentage { get; set; }
    public int? NumberOfDays { get; set; }
    public string? Contact { get; set; }
    public decimal? SettlementValue { get; set; }
    public string? RelatedAgency { get; set; }
    public string? MinutesLocation { get; set; }
    public string? LegalDetails { get; set; }
    public string? Status { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
}
