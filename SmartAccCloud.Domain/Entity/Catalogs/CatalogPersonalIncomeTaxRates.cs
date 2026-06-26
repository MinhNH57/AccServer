using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogPersonalIncomeTaxRates
{
    public string PersonalIncomeTaxRatesName { get; set; }
    public string IncomeType { get; set; }
    public int? TaxBracket { get; set; }
    [Precision(16, 2)]
    public decimal? IncomeFrom { get; set; }
    [Precision(16, 2)]
    public decimal? IncomeTo { get; set; }
    [Precision(16, 2)]
    public decimal? TaxRate { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}
