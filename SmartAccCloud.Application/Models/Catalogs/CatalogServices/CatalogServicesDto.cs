using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogServices;

public class CatalogServicesDto
{
    [Required(ErrorMessage = "Mã dịch vụ không được để trống")]
    public string ServicesCode { get; set; }

    public string? ServicesName { get; set; }
    public string? Notes { get; set; }
}