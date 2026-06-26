using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.GroupConstruction;

public class GroupConstructionDto
{
    [Key]
    [Required(ErrorMessage = "Mã nhóm công trình không được để trống")]

    public string GrpCode { get; set; }

    [Required(ErrorMessage = "Tên nhóm công trình không được để trống")]
    public string? GrpName { get; set; }

    public string? Notes { get; set; }
    [NotMapped] public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
}