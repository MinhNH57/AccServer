namespace Catalog.Base.Entities;
public class CatalogConversionUnitForProduct
{
    public Guid Id { get; set; } = Guid.NewGuid();
 
    public Guid IdProduct { get; set; }
    public int CodeUnit { get; set; } = 100;
    public string CodeProduct { get; set; } = string.Empty;
    public string CodeUnitOfCalculation { get; set; } = string.Empty;
    public string NameUnitOfCalculation { get; set; } = string.Empty;
    public string NumberOfConversionUnits { get; set; } = string.Empty;
    public double? ConversionRateToMainUnit { get; set; }
    public string ConversionCalculation { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public double? Price1 { get; set; } = 0;
    public double? Price2 { get; set; } = 0;
    public double? Price3 { get; set; } = 0;
    public double? FixedPrice { get; set; } = 0;
    public bool IsActive { get; set; } = true;
}
