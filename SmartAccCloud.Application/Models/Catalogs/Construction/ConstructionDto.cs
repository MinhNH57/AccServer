using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.Construction;

public class ConstructionDto
{
    public string? ProjectCode { get; set; }
    public string? ProjectName { get; set; }
    public string? GrpCode { get; set; }
    public string? GrpName { get; set; }

    [Required(ErrorMessage = "Mã công trình không được để trống")]
    public string ConstructionCode { get; set; }

    [Required(ErrorMessage = "Tên công trình không được để trống")]
    public string? ConstructionName { get; set; }

    public decimal? ValueConstruction { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? PaymentTerm { get; set; }
    public string? Notes { get; set; }
    [NotMapped] public Guid Id { get; set; } = Guid.NewGuid();

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    public string? Status { get; set; }
    public string? Investor { get; set; }
    public string? AddressConstruction { get; set; }
    public string? Interpretation { get; set; }
    public bool TrackingStatus { get; set; }
}