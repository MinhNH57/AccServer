using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.Branch;

public class BranchDto
{
    [Required(ErrorMessage = "Mã kho không được để trống")]
    public string BranchCode { get; set; }

    [Required(ErrorMessage = "Tên kho không được để trống")]
    public string? BranchName { get; set; }

    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public int IdAsc { get; set; }
    [NotMapped] public Guid? Id { get; set; } = Guid.NewGuid();
}