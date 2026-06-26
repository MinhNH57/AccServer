using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogAssetGroup;

public class CatalogAssetGroupDto
{
    [Required(ErrorMessage = "Mã tài sản không được để trống")]
    public string AssetGroupCode { get; set; }

    public string? AssetGroupName { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? Notes { get; set; }
    public double? PercentPerAllocationPeriod { get; set; }
    public int? NumberYear { get; set; }
}