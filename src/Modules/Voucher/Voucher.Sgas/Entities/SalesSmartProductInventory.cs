using BuildingBlocks.SmartMapper;
using System.ComponentModel.DataAnnotations;
using Voucher.Sgas.Model.Contracts;

namespace Voucher.Sgas.Entities;

public class SalesSmartProductInventory
{
    [Key]
    [SmartMapIgnore] 
    public Guid Id { get; set; }
    [SmartMapIgnore] 
    public Guid IdContents { get; set; }
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

    public int? CodeUnit { get; set; }

    public bool IsActive { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    [MaxLength(50)]
    public string? LOAIPHIEU { get; set; }

}
