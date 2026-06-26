using System.ComponentModel.DataAnnotations;

namespace Catalog.HRM.Entities.View;
public class vw_HRM_CourseSummary
{
    public Guid? Id { get; set; }
    public Guid? CourseId { get; set; }
    public string? CourseCode { get; set; }
    public bool? IsActive { get; set; }

    public string? CourseName { get; set; }
    public string? CreatedBy {get; set; }
    public string? Type { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? CodeUnit { get; set; }
    public string? ParticipantList { get; set; }
    public string? ParticipantsJson {get; set;}
    public DateTime CreatedDate { get; set; }
}
