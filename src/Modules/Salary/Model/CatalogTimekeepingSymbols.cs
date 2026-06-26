using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Salary.Model.Contracts;

namespace Salary.Model;
public class CatalogTimekeepingSymbols : IBaseEntity
{
    [Key]
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
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; } = string.Empty;
    public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    public string? ModifiedBy { get; set; } = string.Empty;
}
