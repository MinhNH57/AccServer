using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogCollateralAsset
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdContent { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public string AssetCode { get; set; }
    public string? AssetName { get; set; }
    public string? Units { get; set; }
    public double? Price { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? AssetValues { get; set; }
    public decimal? OriginalPrice { get; set; }
    public double? DepreciationPeriod { get; set; }
    public double? DepreciationValuePerMonth { get; set; }
    public DateTime? DepreciationEndDate { get; set; }
    public DateTime? StartAmortizationDate { get; set; }
    public string? AssetGroupCode { get; set; }
    public string? AssetGroupName { get; set; }
    public string? RoomCode { get; set; }
    public string? RoomName { get; set; }
    public string? Notes { get; set; }
    public string? BankCode { get; set; }
    public string? BankName { get; set; }
    public int? CodeUnit { get; set; } = 100;
}
