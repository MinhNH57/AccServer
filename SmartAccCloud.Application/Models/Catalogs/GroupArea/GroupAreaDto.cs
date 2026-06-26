using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.GroupArea;

public class GroupAreaDto
{
    [NotMapped] public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;

    [Key]
    [NotMapped]
    [Required(ErrorMessage = "Mã nhóm khu vực không được để trống")]
    public string CodeGroupArea { get; set; }

    [Required(ErrorMessage = "Tên nhóm khu vực không được để trống")]
    public string? NameGroupArea { get; set; }

    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}