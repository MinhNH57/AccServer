using Lock = SmartAccCloud.Domain.Entity.Rules.Lock;

namespace SmartAccCloud.Infrastructure.Persistence;

public partial class ApplicationDbContext
{
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
    public DbSet<CatalogVoucherNumber> CatalogVoucherNumber { get; set; }
    public DbSet<RetailPriceList> RetailPriceList { get; set; }
    public DbSet<CatalogBankAccountForObject> CatalogBankAccountForObject { get; set; }
    public DbSet<BillOfMaterials> BillOfMaterials { get; set; }
    public DbSet<UnitInfo> UnitInfo { get; set; }
    public DbSet<CatalogDependents> CatalogDependents { get; set; }
    public DbSet<CatalogQrCodeProduct> CatalogQrCodeProduct { get; set; }
    public DbSet<CatalogProductForContract> CatalogProductForContract { get; set; }
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
    public DbSet<SalarySummaryOfTimekeeping> SalarySummaryOfTimekeeping { get; set; }
    public DbSet<SalaryTimeSheet> SalaryTimeSheet { get; set; }
    public DbSet<Menu> WSmartMenu { get; set; }
    public DbSet<SmartData> SmartData { get; set; }
    public DbSet<SmartContentsData> SmartContentsData { get; set; }
    public DbSet<SmartVatTaxList> SmartVatTaxList { get; set; }
    public DbSet<FileAttach> FileAttach { get; set; }
    public DbSet<RuleUser> RuleUser { get; set; }
    public DbSet<RuleUnit> RuleUnit { get; set; }
    public DbSet<CatalogFunction> CatalogFuntion { get; set; }
    public DbSet<RuleAccount> RuleAccountNumber { get; set; }
    public DbSet<Lock> Locks { get; set; }
    public DbSet<DataSelect> WebDataSelect { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<SalaryTimeSheetSummary> SalaryTimeSheetSummary { get; set; }
    public DbSet<SalaryTimeSheetSummaryDetails> SalaryTimeSheetSummaryDetails { get; set; }
    public DbSet<CatalogDashboardConfigs> CatalogDashboardConfigs { get; set; }
    public DbSet<DataBalanceFluctuations> DataBalanceFluctuations { get; set; }
    public DbSet<SalesSmartContentsData> SalesSmartContentsData { get; set; }
    public DbSet<SalesSmartData> SalesSmartData { get; set; }

    public DbSet<CatalogUnitOfCalculation> CatalogUnitOfCalculation { get; set; }
    public DbSet<CatalogConversionUnitForProduct> CatalogConversionUnitForProduct { get; set; }
    public DbSet<CatalogManufacturingStage> CatalogManufacturingStage { get; set; }
    public DbSet<ProductManufacturingStage> ProductManufacturingStage { get; set; }
    public DbSet<CatalogTypeOfTaxRate> CatalogTypeOfTaxRate { get; set; }
    public DbSet<CatalogPaymentTerm> CatalogPaymentTerm { get; set; }


    //View của data Tôn
    public DbSet<DataControlled> DataControlled { get; set; }
    public DbSet<DataThuChi> VTDATATHUCHI { get; set; }


    //Quỹ
    public DbSet<CatalogCreditProducts> CatalogCreditProducts { get; set; }
    public DbSet<CatalogJob> CatalogJob { get; set; }
    public DbSet<CatalogRelationship> CatalogRelationship { get; set; }
    public DbSet<CatalogVillage> CatalogVillage { get; set; }
    public DbSet<CatalogWards> CatalogWards { get; set; }
    public DbSet<CatalogFamilyRelationship> CatalogFamilyRelationship { get; set; }
    public DbSet<SmartFileAttach> SmartFileAttach { get; set; }
    public DbSet<RuleBaseUnion> RuleBaseUnion { get; set; }
    public DbSet<SurveyExpertise> SurveyExpertise { get; set; }
    public DbSet<CatalogFamilyCircumstance> CatalogFamilyCircumstance { get; set; }

}