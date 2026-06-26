using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.RevExp;

public class RevExpDto
{
    [Required(ErrorMessage = "Mã để trống")]
    [NotMapped]
    public string CodeRevExp { get; set; }

    public string? GrpCode { get; set; }
    public string? GrpName { get; set; }

    [Required(ErrorMessage = "Tên để trống")]
    public string? NameRevExp { get; set; }

    [NotMapped] public string? TypeRevExp { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; } = 100;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public bool ByObject { get; set; }
}