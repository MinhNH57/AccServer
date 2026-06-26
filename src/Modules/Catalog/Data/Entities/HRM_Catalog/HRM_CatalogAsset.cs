using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.HRM_Catalog;
public class HRM_CatalogAsset
{
    public string? AssetCode { get; set; }
    public string? AssetName { get; set; }
    public string? Units { get; set; }
    public double? Price { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? AssetValues { get; set; }
    public decimal? OriginalPrice { get; set; }
    public double? DepreciationPeriod { get; set; }
    public double? DepreciationValuePerMonth { get; set; }
    public DateTime? DepreciationEndDate { get; set; }
    public string? AssetGroupCode { get; set; }
    public string? AssetGroupName { get; set; }
    public string? Notes { get; set; }
    public bool? IsActive { get; set; }
    public Guid? Identifier { get; set; }
    public int? CodeUnit { get; set; }
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public string? RoomCode { get; set; }
    public string? RoomName { get; set; }
    public double? ValuePerMonthRules { get; set; }
    public double? PercentPerAllocationPeriod { get; set; }
    public int? ProductionYear { get; set; }
    public string? Origin { get; set; }
    public double? Wattage { get; set; }
    public string? TypeAssetCode { get; set; }
    public string? TypeAssetName { get; set; }
    public int? QuantityAsset { get; set; }
    public string? BudgetSource { get; set; }
    public string? BudgetChapter { get; set; }
    public string? BudgetType { get; set; }
    public string? BudgetAmount { get; set; }
    public string? BudgetItem { get; set; }
    public string? BudgetSubsection { get; set; }
    public string? AssetNumber { get; set; }
    public string? WarrantyPeriod { get; set; }
    public string? YearOfManufacture { get; set; }
    public string? CodeSupplier { get; set; }
    public string? NameSupplier { get; set; }
    public string? AddressSupplier { get; set; }
    public string? Interpretation { get; set; }
    public string? ManufacturingCountry { get; set; }
    public bool IsTransferredAsset { get; set; }
    public DateTime? DateOfPurchase { get; set; }
    public DateTime? RecordDateIncreased { get; set; }
    public DateTime? DateOfUse { get; set; }
    public double? UtilizationRateForBusiness { get; set; }
    public string? FullPriceAccount { get; set; }
    public DateTime? StartDepreciationDate { get; set; }
    public int? TimeOfUse { get; set; }
    public double? WearRate { get; set; }
    public decimal? AnnualDepreciationValue { get; set; }
    public double? TimeRemaining { get; set; }
    public int? UntilYear { get; set; }
    public DateTime? StartAmortizationDate { get; set; }
    public decimal? AmortizationValue { get; set; }
    public DateTime? AmortizationPeriod { get; set; }
    public int? AmortizationDuration { get; set; }
    public double? AmortizationRate { get; set; }
    public decimal? MonthlyAmortizationValue { get; set; }
    public decimal? AccumulatedDepreciation { get; set; }
    public int? RemainingAmortizationMonths { get; set; }
    public decimal? AccumulatedWearAndTear { get; set; }
    public decimal? RemainingValue { get; set; }
    public bool IsTaxRegulationDepreciationLimit { get; set; }
    public string? DebitAccount { get; set; }
    public string? CreditAccount { get; set; }
    public string? FundingSource { get; set; }
    public double? AmortizationRateYear { get; set; }
    public decimal? YearAmortizationValue { get; set; }
    public decimal? AmortizationValueRule { get; set; }
    public decimal? MonthlyAmortizationValueRule { get; set; }
    public string? TypeTimeRemaining { get; set; }
    public string? CodePsc { get; set; }
}
