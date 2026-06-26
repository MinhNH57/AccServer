using Catalog.SGas.Entities;
using Catalog.SGas.Entities.Sgas;
using Microsoft.EntityFrameworkCore;

namespace Catalog.SGas.Infrastructure;

public partial class CatalogSGasDbContext
{
    #region Catalogs

    public DbSet<CatalogAccountCostSold> CatalogAccountCostSold { get; set; }
    public DbSet<CatalogAccountMovement> CatalogAccountMovement { get; set; }
    public DbSet<CatalogAccountSymbol> CatalogAccountSymbol { get; set; }
    public DbSet<CatalogAccountType> CatalogAccountType { get; set; }
    public DbSet<CatalogArea> CatalogArea { get; set; }
    public DbSet<CatalogConstruction> CatalogConstruction { get; set; }
    public DbSet<CatalogContract> CatalogContract { get; set; }
    public DbSet<CatalogForeignCurrency> CatalogForeignCurrency { get; set; }
    public DbSet<CatalogGroupArea> CatalogGroupArea { get; set; }
    public DbSet<CatalogGroupConstruction> CatalogGroupConstruction { get; set; }
    public DbSet<CatalogGroupContract> CatalogGroupContract { get; set; }
    public DbSet<CatalogGroupObj> CatalogGroupObj { get; set; }
    public DbSet<CatalogGroupProduct> CatalogGroupProduct { get; set; }
    public DbSet<CatalogGroupProject> CatalogGroupProject { get; set; }
    public DbSet<CatalogImpExpReason> CatalogImpExpReason { get; set; }
    public DbSet<CatalogMethodOfPayments> CatalogMethodOfPayments { get; set; }
    public DbSet<CatalogObject> CatalogObject { get; set; }
    public DbSet<CatalogProduct> CatalogProduct { get; set; }
    public DbSet<CatalogProductionActivitie> CatalogProductionActivitie { get; set; }
    public DbSet<CatalogProductType> CatalogProductType { get; set; }
    public DbSet<CatalogProject> CatalogProject { get; set; }
    public DbSet<CatalogRevExp> CatalogRevExp { get; set; }
    public DbSet<CatalogRoom> CatalogRoom { get; set; }
    public DbSet<CategoryWarehose> CategoryWarehose { get; set; }
    public DbSet<CatalogProductTank> CatalogProductTank { get; set; }
    public DbSet<CatalogBranch> CatalogBranch { get; set; }
    public DbSet<CatalogFundingSource> CatalogFundingSource { get; set; }
    public DbSet<CatalogQuotaBog> CatalogQuotaBog { get; set; }
    public DbSet<CatalogUnit> CatalogUnit { get; set; }
    public DbSet<RetailPriceList> RetailPriceList { get; set; }
    public DbSet<CatalogBankAccountForObject> CatalogBankAccountForObject { get; set; }
    public DbSet<BillOfMaterials> BillOfMaterials { get; set; }
    //public DbSet<UnitInfo> UnitInfo { get; set; }
    public DbSet<CatalogDependents> CatalogDependents { get; set; }
    public DbSet<CatalogQrCodeProduct> CatalogQrCodeProduct { get; set; }
    public DbSet<CatalogProductForContract> CatalogProductForContract { get; set; }
    public DbSet<CatalogCollateralAsset> CatalogCollateralAsset { get; set; }
    public DbSet<CatalogExciseTaxItems> CatalogExciseTaxItems { get; set; }
    public DbSet<CatalogError> CatalogError { get; set; }
    public DbSet<CatalogLocationWarehouse> CatalogLocationWarehouse { get; set; }
    public DbSet<CatalogServices> CatalogServices { get; set; }
    public DbSet<CatalogMemberRankings> CatalogMemberRankings { get; set; }
    public DbSet<CatalogAsset> CatalogAsset { get; set; }
    public DbSet<CatalogAssetGroup> CatalogAssetGroup { get; set; }
    public DbSet<CatalogAccountingShortcuts> CatalogAccountingShortcuts { get; set; }
    public DbSet<CatalogProductForAsset> CatalogProductForAsset { get; set; }
    public DbSet<CatalogPersonalIncomeTaxRates> CatalogPersonalIncomeTaxRates { get; set; }
    public DbSet<CatalogTimekeepingSymbols> CatalogTimekeepingSymbols { get; set; }
    public DbSet<CatalogSalaryInsuranceRegulations> CatalogSalaryInsuranceRegulations { get; set; }
    public DbSet<CatalogShoppingList> CatalogShoppingList { get; set; }
    public DbSet<CatalogDiscountList> CatalogDiscountList { get; set; }
    public DbSet<CatalogUnitOfCalculation> CatalogUnitOfCalculation { get; set; }
    public DbSet<CatalogConversionUnitForProduct> CatalogConversionUnitForProduct { get; set; }
    public DbSet<CatalogManufacturingStage> CatalogManufacturingStage { get; set; }
    public DbSet<ProductManufacturingStage> ProductManufacturingStage { get; set; }
    public DbSet<CatalogItems> CatalogItems { get; set; }
    public DbSet<CatalogPaymentTerm> CatalogPaymentTerm { get; set; }
    public DbSet<CatalogTypeOfTaxRate> CatalogTypeOfTaxRate { get; set; }
    public DbSet<CatalogBidPackage> CatalogBidPackage { get; set; }
    public DbSet<ProjectMeetingMinutesManagement> ProjectMeetingMinutesManagement { get; set; }
    public DbSet<ProjectLegalProgress> ProjectLegalProgress { get; set; }
    public DbSet<ProjectManagementExpenses> ProjectManagementExpenses { get; set; }
    public DbSet<ProjectConstructionProgressManagement> ProjectConstructionProgressManagement { get; set; }
    public DbSet<ProjectSystemRecordsManagement> ProjectSystemRecordsManagement { get; set; }
    public DbSet<CatalogVoucherNumber> CatalogVoucherNumber { get; set; }
    public DbSet<InterestRateAdjustment> InterestRateAdjustment { get; set; }
    public DbSet<CatalogStatistical> CatalogStatistical { get; set; }
    public DbSet<CatalogCostAggregation> CatalogCostAggregation { get; set; }
    public DbSet<OpenAccounting> OpenAccounting { get; set; }
    public DbSet<MaterialRemaining> MaterialRemaining { get; set; }
    public DbSet<CatalogInvoiceTemplate> CatalogInvoiceTemplate { get; set; }
    public DbSet<SmartDataProductionPlan> SmartDataProductionPlan { get; set; }
    public DbSet<CatalogTableCommon> CatalogTableCommon { get; set; }
    public DbSet<CatalogProductAttribute> CatalogProductAttribute { get; set; }
    public DbSet<SmartProductAttributeByOrder> SmartProductAttributeByOrder { get; set; }
    public DbSet<CatalogTypeOfMatarial> CatalogTypeOfMatarial { get; set; }
    public DbSet<SalesCatalogVoucherNumber> SalesCatalogVoucherNumber { get; set; }
    public DbSet<SalesCashRemaining> SalesCashRemaining { get; set; }
    public DbSet<SalesMaterialRemaining> SalesMaterialRemaining { get; set; }
    public DbSet<SalesRemainingDebts> SalesRemainingDebts { get; set; }
    #endregion

    public DbSet<SmartDataProductionStatistics> smartDataProductionStatistics { get; set; }
    public DbSet<ReportConfigPages> ReportConfigPages { get; set; }

    #region Salary
    public DbSet<SalarySummaryOfTimekeeping> SalarySummaryOfTimekeeping { get; set; }
    public DbSet<SalaryTimeSheet> SalaryTimeSheet { get; set; }
    public DbSet<SalaryTimeSheetDetail> SalaryTimeSheetDetail { get; set; }
    public DbSet<SalaryTimeSheetSummary> SalaryTimeSheetSummary { get; set; }
    public DbSet<SalaryTimeSheetSummaryDetails> SalaryTimeSheetSummaryDetails { get; set; }



    #endregion

    #region Sgas

    public DbSet<SalesCatalogColumnPump> SalesCatalogColumnPump { get; set; }
    public DbSet<CatalogPumpNozzle> CatalogPumpNozzle { get; set; }
    public DbSet<SalesCatalogTank> SalesCatalogTank { get; set; }
    public DbSet<SalesCatalogLossRate> SalesCatalogLossRate { get; set; }
    public DbSet<CatalogSaleShifts> CatalogSaleShifts { get; set; }
    public DbSet<CatalogGoodsIssuanceCard> CatalogGoodsIssuanceCard { get; set; }

    #endregion

}