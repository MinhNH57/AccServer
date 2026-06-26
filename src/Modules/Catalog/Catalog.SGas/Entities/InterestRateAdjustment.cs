using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities;
public class InterestRateAdjustment
{
    public Guid IdContents { get; set; } = Guid.NewGuid();  
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; } 
    public decimal? InterestRate { get; set; }
    public decimal? OverdueInterestRate { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public int? CodeUnit { get; set; } = 888;
    public string? Notes { get; set; }
}
