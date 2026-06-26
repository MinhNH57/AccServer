using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.Project;

public class ProjectDto
{
    [Key]
    [Required(ErrorMessage = "Mã dự án không được để trống")]
    public string ProjectCode { get; set; }

    [Required(ErrorMessage = "Tên dự án không được để trống")]

    public string? ProjectName { get; set; }

    public string? OwnerOfProjectCode { get; set; }
    public string? OwnerOfProjectName { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? ValueProject { get; set; }
    public string? Notes { get; set; }
    [NotMapped] public Guid Id { get; set; } = Guid.NewGuid();

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    public string? ProductActivCode { get; set; }
    public string? ProductActivName { get; set; }
    public string? GrpCode { get; set; }
    public string? GrpName { get; set; }
    public string? AddressProject { get; set; }
    public string? TypeMoney { get; set; }
    public string? NumberProject { get; set; }
    public DateTime? DateRelease { get; set; }
    public string? IssuingAgency { get; set; }
    public bool TrackingStatus { get; set; }
}