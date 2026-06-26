namespace SmartAccCloud.Application.Models.Catalogs.CatalogPersonalIncomeTaxRates;
public class CatalogPersonalIncomeTaxRatesDto
{
    public string PersonalIncomeTaxRatesName { get; set; }
    public string IncomeType { get; set; }
    public int? TaxBracket { get; set; }
    public decimal? IncomeFrom { get; set; }
    public decimal? IncomeTo { get; set; }
    public decimal? TaxRate { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
