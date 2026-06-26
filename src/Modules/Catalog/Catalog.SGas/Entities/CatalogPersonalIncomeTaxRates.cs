using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.SGas.Entities;
public class CatalogPersonalIncomeTaxRates
{
    public string PerrsonalIncomeCode { get; set; }   
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
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}
