using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogBudget
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; } = 0;

    public int? CodeUnit { get; set; } = 888;

    [MaxLength(50)]
    public string? AccountSymbol { get; set; }

    [MaxLength(50)]
    public string? ChapterCode { get; set; }

    [MaxLength(50)]
    public string? BudgetItemCode { get; set; }

    [MaxLength(50)]
    public string? BudgetSubItemCode { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    public bool IsActive { get; set; } = false;
}
