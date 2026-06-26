using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.QrCodeProduct;

public class QrCodeProductDto
{
    [Key] public Guid Id { get; set; }
    public string? Barcode { get; set; }
    public string? SupplierCode { get; set; }
    public string? SupplierName { get; set; }
    public string? ProductCode { get; set; }
    public double? PriceImp { get; set; }
    public double? PriceExp { get; set; }
    public int? CodeUnit { get; set; } = 100;
}