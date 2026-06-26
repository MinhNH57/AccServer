using SmartAccCloud.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogTimekeepingSymbols
{
    [Key]
    [Unique(nameof(CatalogTimekeepingSymbols), nameof(SymbolCode), ErrorMessage = "Mã đã tồn tại")]
    [Required(ErrorMessage = "Mã ký hiệu chấm công không được để trống")]
    public string SymbolCode { get; set; }
    public string? Description { get; set; }
    [Precision(16, 2)]
    public decimal SalaryRate { get; set; }
    public bool IsSymbolDefault { get; set; }
    public string? OverTimeSymbolName { get; set; }

    public bool IsHalfDay { get; set; }
    public bool IsOverTime { get; set; }
    public bool IsSystem { get; set; }
    public int? OverTimeSymbol { get; set; } 
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
}
