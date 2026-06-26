using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogQuotaBog
{
    public Guid Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; } = 889;
    public string? Content { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? GasolineA92 { get; set; }
    public double? GasolineA95 { get; set; }
    public double? OilDiezen { get; set; }
    public double? Gas { get; set; }
    public double? OilMazut { get; set; }
    public double? ExpenseGasolineA92 { get; set; }
    public double? ExpenseGasolineA95 { get; set; }
    public double? ExpenseOilDiezen { get; set; }
    public double? ExpenseGas { get; set; }
    public double? ExpenseOilMazut { get; set; }
    public string? Notes { get; set; }
}
