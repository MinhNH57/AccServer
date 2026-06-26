using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities
{
    public class CatalogProduct
    {
        public string ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? UnitPsc { get; set; }
        public string? UnitPackage { get; set; }
        public double? Conversion { get; set; }
        public double? ProNumberInSlot { get; set; }
        public double? Density { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public double? PriceImp { get; set; }
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
        public int? CodeUnit { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
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
        public string? CodeISBN { get; set; }
        public bool NotBonus { get; set; }
        public string? Specifications { get; set; }
        public string? ShortAddressSupplier { get; set; }
        public string? AddressSupplier { get; set; }
        public string? ShortNameSupplier { get; set; }
        public double? ImpTaxRate { get; set; }
        public bool IsPromotion { get; set; }
        public bool Consignment { get; set; }
        public double? RandomQtyMax { get; set; }
        public double? RandomAmountMax { get; set; }
        public double? ProWeight { get; set; }
        public bool IsStop { get; set; }
        public double? LaborCosts { get; set; }
        public double? OccupancyRate { get; set; }
        public double? StoneWeight { get; set; }
        public double? GoldWeight { get; set; }
        public double? PackagingWeight { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchDate { get; set; }
        public string? IdBatch { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public string? StorageLocation { get; set; }
        public DateTime? DateExpiration { get; set; }
    }
}
