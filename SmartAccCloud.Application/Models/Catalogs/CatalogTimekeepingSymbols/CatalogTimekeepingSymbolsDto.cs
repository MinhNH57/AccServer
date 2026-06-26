using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogTimekeepingSymbols;
public class CatalogTimekeepingSymbolsDto
{
    [Key]
    [Unique(nameof(CatalogTimekeepingSymbols), nameof(SymbolCode), ErrorMessage = "Mã đã tồn tại")]
    [Required(ErrorMessage = "Mã ký hiệu chấm công không được để trống")]
    public string SymbolCode { get; set; }
    public string? Description { get; set; }
    public decimal SalaryRate { get; set; }
    public bool IsSymbolDefault { get; set; }
    public string? OverTimeSymbolName { get; set; }
    public bool IsHalfDay { get; set; }
    public bool IsActive { get; set; }
    public bool IsSystem { get; set; }
    public bool IsOverTime { get; set; }
    public int? OverTimeSymbol { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
