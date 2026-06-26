using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.GroupArea;

public class AreaDto
{
    [Required(ErrorMessage = "Mã nhóm khu vực không được để trống")]
    public string? GrpCodeArea { get; set; }

    [Required(ErrorMessage = "Tên nhóm khu vực không được để trống")]
    public string? GrpNameArea { get; set; }

    [Key]
    [Required(ErrorMessage = "Mã khu vực không được để trống")]
    public string CodeArea { get; set; }

    [Required(ErrorMessage = "Tên khu vực không được để trống")]
    public string? NameArea { get; set; }

    [NotMapped] public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}