using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.Products.Product;

public class ProductVm
{
    [Key]
    [Required(ErrorMessage = "Mã không được để trống")]
    public string ProductCode { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]
    public string? ProductName { get; set; }

    public string? UnitPsc { get; set; }
    public double? PriceExp { get; set; }
    public double? UnitPrice { get; set; }
    public double? PriceRetail { get; set; }
    public string? GrpName { get; set; }
    public double? VatRate { get; set; }
    public string? LocationContent { get; set; }
}