using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogAsset;

public class CatalogAssetVm
{
    [Required(ErrorMessage = "Mã tài sản không được để trống")]
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
    public string? AssetGroupCode { get; set; }
    public string? AssetGroupName { get; set; }
    public string? Notes { get; set; }
}