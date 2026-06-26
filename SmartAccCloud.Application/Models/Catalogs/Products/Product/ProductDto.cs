using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.Products.Product;

public class ProductDto
{
    [Key]
    [Required(ErrorMessage = "Mã không được để trống")]
    public string ProductCode { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]

    public string? ProductName { get; set; }

    public string? UnitPsc { get; set; }
    public string? UnitPackage { get; set; }
    public double? Conversion { get; set; }
    public double? ProNumberInSlot { get; set; }
    public double? Density { get; set; }
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public double? PriceImp { get; set; }
    public double? UnitPrice { get; set; }
    public double? PriceExp { get; set; }
    public double? PriceRetail { get; set; }
    public double? EnvironmentalProtectionFee { get; set; }
    public double? VatRate { get; set; }
    public string? TypeVat { get; set; }
    public string? AccMaterial { get; set; }
    public string? AccCostOfCapital { get; set; }
    public string? AccRevenue { get; set; }
    public string? AccountSymbol { get; set; }
    public string? GrpCode { get; set; }
    public string? GrpName { get; set; }
    public string? TypeCode { get; set; }
    public string? TypeName { get; set; }
    public string? NameSupplier { get; set; }
    public string? CodeSupplier { get; set; }
    public string? ColorCode { get; set; }
    public string? ColorName { get; set; }
    public string? Notes { get; set; }
    public bool GreaseOil { get; set; }
    public string? Pictures { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; } = 100;
    [NotMapped] public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }

    public DateTime? Created { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; } = DateTime.Now;
    public string? LastModifiedBy { get; set; }
    public string? ProductCodeFts { get; set; }
    public bool FinishedProduct { get; set; }
    public string? BarCode { get; set; }
    public double? Discount1 { get; set; }
    public double? Discount2 { get; set; }
    public double? Discount3 { get; set; }
    public double? Discount4 { get; set; }
    public double? SurplusMaximum { get; set; }
    public double? SurplusMinimum { get; set; }
    public double? PercentExp { get; set; }
    public double? PercentRetail { get; set; }
    public double? ExciseTaxRate { get; set; }
    public double? CoefficientVcf { get; set; }
    public bool EnviromentByKg { get; set; }
    public double? RetailPrice { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string? CodeISBN { get; set; }
    public bool NotBonus { get; set; }
    public string? Specifications { get; set; }
    public string? ShortAddressSupplier { get; set; }
    public string? AddressSupplier { get; set; }
    public string? ShortNameSupplier { get; set; }
    public double? ImpTaxRate { get; set; }
    public string? Nature { get; set; }
    public string? Describe { get; set; }
    public string? WarrantyPeriod { get; set; }
    public int? MinimumQuantity { get; set; }
    public string? Warehouse { get; set; }
    public string? WarehouseCode { get; set; }
    public string? WarehouseAccount { get; set; }
    public string? DiscountAccount { get; set; }
    public string? AccountSale { get; set; }
    public string? ConversionUnit1 { get; set; }
    public string? ConversionUnit2 { get; set; }
    public string? ReturnAccount { get; set; }
    public string? GroupTaxProduct { get; set; }
    public double? Conversion1 { get; set; }
    public double? ImportRate { get; set; }
    public double? RateCkmh { get; set; }
    public int? LeveCk { get; set; }
    public double? ExportRate { get; set; }
    public bool IsGift { get; set; }
    public string? ProductCharacteristics { get; set; }
    public string? CodeLocation { get; set; }
    public string? LocationContent { get; set; }
}