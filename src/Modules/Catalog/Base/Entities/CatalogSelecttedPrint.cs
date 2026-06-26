using System.ComponentModel.DataAnnotations;

namespace Catalog.Base.Entities;
public class CatalogSelecttedPrint
{
    [MaxLength(50)]
    public string? CatalogName { get; set; }

    [MaxLength(50)]
    public string? ValueSelectted { get; set; }

    [MaxLength(50)]
    public string? UserCode { get; set; }

    public Guid Id { get; set; } = Guid.CreateVersion7(TimeProvider.System.GetUtcNow());

    [MaxLength(255)]
    public string? Notes { get; set; } 

    public double? AmountFrom { get; set; }

    public double? AmountEnd { get; set; }

    public double? MoneyFrom { get; set; }

    public double? MoneyEnd { get; set; }
}
