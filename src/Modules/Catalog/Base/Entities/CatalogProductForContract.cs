using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogProductForContract
{
    public string? CodeContract { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? ProductParent { get; set; }
    public bool IsParent { get; set; }
    public string? UnitPcs { get; set; }
    public double Quantity { get; set; }
    public double Coefficient { get; set; }
    public double Price { get; set; }
    [Precision(18, 0)]
    public decimal AmountOfMoney { get; set; }
    public double TransferRate { get; set; }
    [Precision(18, 0)]
    public decimal TransferMoney { get; set; }
    public double ValueAddedTaxPercent { get; set; }
    [Precision(18, 0)]
    public decimal ValueAddedTax { get; set; }
    [Precision(18, 0)]
    public decimal TotalAmountTransfer { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public Guid IdContract { get; set; } = Guid.NewGuid();
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public bool FinishedProduct { get; set; }
    public double PriceExchangeRate { get; set; }
    public double AmountOfMoneyExchangeRate { get; set; }
    public double ExchangeRate { get; set; }
    public string? CurrencyType { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public string? Notes { get; set; }
    [MaxLength(200)]
    public string? PlaceOfOrigin { get; set; }

    public double? ImportRate { get; set; }

    public double? VatRate { get; set; }

    public double? EnvironmentalProtectionFee { get; set; }

    [MaxLength(150)]
    public string? UnitPackage { get; set; }
    public double? ConversionFactor { get; set; }

    public double? PackageQuantity { get; set; }
    public Guid? IdSource { get; set; } = Guid.Empty;
    public string? VoucherNoInherit { get; set; } = ""; 
    [MaxLength(200)]
    public string? ForeignName { get; set; } = "";
    [MaxLength(50)]
    public string? HsCode { get; set; } = "";
    [MaxLength(100)]
    public string? SupplierProductCode { get; set; } = "";
    [MaxLength(500)]
    public string? Specifications { get; set; } = "";
    [MaxLength(500)]
    public string? Describe { get; set; } = "";
}
