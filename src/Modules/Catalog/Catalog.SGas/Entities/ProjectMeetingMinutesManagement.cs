namespace Catalog.SGas.Entities;
public class ProjectMeetingMinutesManagement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Process { get; set; }
    public string? Category { get; set; }
    public string? SubCategory { get; set; }
    public string? Reference { get; set; }
    public string? DocumentName { get; set; }
    public string? DocumentNumber { get; set; }
    public string? DocumentType { get; set; }
    public string? DocumentCode { get; set; }
    public string? DocumentContent { get; set; }
    public string? IssuingUnit { get; set; }
    public string? ApprovingAgency { get; set; }
    public decimal? ProgressPercentage { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? StartDate { get; set; }
    public int? Duration { get; set; }
    public string? Executor { get; set; }
    public string? Position { get; set; }
    public string? Contact { get; set; }
    public decimal? BidPackageValue { get; set; }
    public decimal? BidValue { get; set; }
    public decimal? FinalSettlementValue { get; set; }
    public string? PersonInCharge { get; set; }
    public string? SupportingPerson { get; set; }
    public string? ContactAgency { get; set; }
    public string? ImplementationMethod { get; set; }
    public string? PaymentTerms { get; set; }
    public string? MeetingLocation { get; set; }
    public decimal? CompletionRate { get; set; }
    public DateTime? MeetingDate { get; set; }
    public string? Status { get; set; }
    public string? LegalDetails { get; set; }
    public string? Notes { get; set; }
    public string? DocumentReference { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
}
