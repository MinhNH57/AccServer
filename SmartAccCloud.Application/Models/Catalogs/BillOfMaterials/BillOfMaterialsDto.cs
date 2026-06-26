using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.BillOfMaterials;

public class BillOfMaterialsDto
{
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }

    [Required(ErrorMessage = "Không được để trống mã sản phẩm")]
    public string? CommodityCode { get; set; }

    public string? CommodityName { get; set; }
    public string? UnitPcs { get; set; }

    [Required(ErrorMessage = "Không được để trống")]
    public double? Quantity { get; set; }

    public double? Price { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public Guid Identifier { get; set; }
    public int? CodeUnit { get; set; }
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
}