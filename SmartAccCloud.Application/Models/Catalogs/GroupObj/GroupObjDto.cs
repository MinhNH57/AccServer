using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.GroupObj;

public class GroupObjDto
{
    [Required(ErrorMessage = "Mã nhóm đơn vị không được để trống")]
    public string CodeGroupObj { get; set; }

    [Required(ErrorMessage = "Tên nhóm đơn vị không được để trống")]
    public string NameGroupObj { get; set; }

    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public bool IsAutogen { get; set; }
    public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
}