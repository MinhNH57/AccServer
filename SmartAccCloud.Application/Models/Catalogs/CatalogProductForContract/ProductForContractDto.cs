using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogProductForContract;

public class ProductForContractDto
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string? CodeContract { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? UnitPcs { get; set; }
    public double? Quantity { get; set; }
    public double? Price { get; set; }
    public decimal? AmountOfMoney { get; set; }
    public double? TransferRate { get; set; }
    public decimal? TransferMoney { get; set; }
    public double? ValueAddedTaxPercent { get; set; }
    public decimal? ValueAddedTax { get; set; }
    public decimal? TotalAmountTransfer { get; set; }
}