using BuildingBlocks.SmartMapper;
using System.ComponentModel.DataAnnotations;

namespace Voucher.Sgas.Model.Sales;

public class SalesSmartProductInventoryDto
{ 
    public Guid Id { get; set; } 

    [MaxLength(50)]
    public string? CommodityCode { get; set; }

    [MaxLength(255)]
    public string? CommodityName { get; set; }

    [MaxLength(50)]
    public string? UnitPcs { get; set; }

    public double? Quantity { get; set; }

    public double? Quantity15 { get; set; }

    public double? CoefficientVcf { get; set; }

    public double? Temperature { get; set; }

    public double? CoefficientWcf { get; set; } 

    public bool IsActive { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    [MaxLength(50)]
    public string? LOAIPHIEU { get; set; }
}
