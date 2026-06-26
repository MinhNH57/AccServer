using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.Products.GroupProduct;

public class GroupProductDto
{
    [Key]
    [Required(ErrorMessage = "Mã nhóm không được để trống")]

    public string GroupCode { get; set; }

    [Required(ErrorMessage = "Tên nhóm hàng hóa không được để trống")]
    public string? GroupName { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    public bool IsAutogen { get; set; }

    public string? Note { get; set; }
}