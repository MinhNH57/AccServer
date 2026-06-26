using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogQrCodeProduct
{
    [Key]
    public Guid Id { get; set; }
    public string? Barcode { get; set; }
    public string? SupplierCode { get; set; }
    public string? SupplierName { get; set; }
    public string? ProductCode { get; set; }
    public double? PriceImp { get; set; }
    public double? PriceExp { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }

}
