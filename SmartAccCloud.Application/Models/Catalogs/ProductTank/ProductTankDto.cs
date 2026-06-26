using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.ProductTank;

public class ProductTankDto
{
    [Required(ErrorMessage = "Không được để trống mã bể")]
    public string? TankCode { get; set; }

    [Required(ErrorMessage = "Không được để trống mã sản phẩm")]
    public string? ProductCode { get; set; }

    public string? ProductName { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public int? CodeUnit { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}