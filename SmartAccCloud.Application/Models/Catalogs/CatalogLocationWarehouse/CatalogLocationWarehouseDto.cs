using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogLocationWarehouse;

public class CatalogLocationWarehouseDto
{
    [Key]
    [Required(ErrorMessage = "Mã phiếu không được để trống")]
    public string LocationCode { get; set; }

    public string? LocationContent { get; set; }
    public string? Notes { get; set; }
}