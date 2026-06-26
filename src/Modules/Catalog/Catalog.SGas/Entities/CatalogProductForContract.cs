using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Catalog.SGas.Entities;
public class CatalogProductForContract
{
    public string? CodeContract { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? UnitPcs { get; set; }
    public double? Quantity { get; set; }
    public double? Price { get; set; }
    [Precision(18, 0)]
    public decimal? AmountOfMoney { get; set; }
    public double? TransferRate { get; set; }
    [Precision(18, 0)]
    public decimal? TransferMoney { get; set; }
    public double? ValueAddedTaxPercent { get; set; }
    [Precision(18, 0)]
    public decimal? ValueAddedTax { get; set; }
    [Precision(18, 0)]
    public decimal? TotalAmountTransfer { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; } = 100;
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
