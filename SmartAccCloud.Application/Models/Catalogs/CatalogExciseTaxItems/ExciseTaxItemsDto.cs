using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogExciseTaxItems;

public class ExciseTaxItemsDto
{
    [Required(ErrorMessage = "Mã không được để trống")]
    public string CodeExcise { get; set; }

    public string? NameExcise { get; set; }
    public string? GrpCode { get; set; }
    public string? UnitPsc { get; set; }
    public double? ExciseTaxRate { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}