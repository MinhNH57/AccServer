using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.ProductionActivitie;

public class ProductionActivitieDto
{
    [Required(ErrorMessage = "Mã không được để trống")]

    public string ProductActivCode { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]
    public string? ProductActivName { get; set; }

    public string? Notes { get; set; }
    [NotMapped] public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
}