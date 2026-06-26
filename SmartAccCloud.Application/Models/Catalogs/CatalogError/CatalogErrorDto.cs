using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogError;

public class CatalogErrorDto
{
    [Key]
    [Required(ErrorMessage = "Mã lỗi không được để trống")]
    public string ErrorCode { get; set; }
    public string? ErrorName { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}