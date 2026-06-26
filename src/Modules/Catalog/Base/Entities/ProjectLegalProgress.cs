namespace Catalog.Base.Entities;
public class ProjectLegalProgress
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public string? SubProject { get; set; }
    public string? ParentCodeNvarchar { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Duration { get; set; }
    public decimal? CompletionRate { get; set; }
    public int? CodeUnit { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}
