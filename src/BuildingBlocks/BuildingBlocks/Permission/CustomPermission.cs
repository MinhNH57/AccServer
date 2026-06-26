namespace BuildingBlocks.Permission;

public abstract class CustomAction
{
    public const string AllowInsert = nameof(AllowInsert);
    public const string AllowEdit = nameof(AllowEdit);
    public const string AllowDelete = nameof(AllowDelete);
    public const string AllowView = nameof(AllowView);
    public const string AllowPrint = nameof(AllowPrint);

}

public class CustomPermission
{
    public static string NameFor(string action, string resource) => $"Permission-{resource}-{action}";
}

public class Resource
{
    public const string CatalogRoom = nameof(CatalogRoom);
    public const string RuleFunction = nameof(RuleFunction);

 
    public const string CatalogUsers= nameof(CatalogUsers);
    public const string CatalogUnit= nameof(CatalogUnit);



    public const string LockOpen= nameof(LockOpen);
    public const string LockOpenUnit= nameof(LockOpenUnit);


    #region Catalogs
    public const string AccountCostSold = nameof(AccountCostSold);
    public const string CatalogAccountMovement = nameof(CatalogAccountMovement);
    public const string CatalogAccountSymbol = nameof(CatalogAccountSymbol);
    public const string CatalogExciseTaxItems = nameof(CatalogExciseTaxItems);
    public const string CatalogAccountType = nameof(CatalogAccountType);
    public const string CatalogArea = nameof(CatalogArea);
    public const string BillOfMaterials = nameof(BillOfMaterials);
    public const string GroupWarehose = nameof(GroupWarehose);
    public const string CatalogError = nameof(CatalogError);
    public const string CatalogConstruction = nameof(CatalogConstruction);
    public const string CatalogContract = nameof(CatalogContract);
    public const string CatalogForeignCurrency = nameof(CatalogForeignCurrency);
    public const string CatalogFundingSource = nameof(CatalogFundingSource);
    public const string CatalogGroupArea = nameof(CatalogGroupArea);
    public const string CatalogGroupConstruction = nameof(CatalogGroupConstruction);
    public const string CatalogGroupContract = nameof(CatalogGroupContract);
    public const string CatalogGroupObj = nameof(CatalogGroupObj);
    public const string CatalogGroupProduct = nameof(CatalogGroupProduct);
    public const string CatalogGroupProject = nameof(CatalogGroupProject);
    public const string CatalogImpExpReason = nameof(CatalogImpExpReason);
    public const string CatalogMethodOfPayments = nameof(CatalogMethodOfPayments);
    public const string CatalogObject = nameof(CatalogObject);
    public const string CatalogProductionActivitie = nameof(CatalogProductionActivitie);
    public const string CatalogProduct = nameof(CatalogProduct);
    public const string ProductTank = nameof(ProductTank);
     public const string CatalogProductType = nameof(CatalogProductType);
    public const string CatalogProject = nameof(CatalogProject);
    public const string CatalogBog = nameof(CatalogBog);
    public const string RetailPrice = nameof(RetailPrice);
    public const string CatalogRev = nameof(CatalogRev);
    public const string CatalogExp = nameof(CatalogExp);
    public const string CategoryWarehose = nameof(CategoryWarehose);
    public const string CatalogLocationWarehouse = nameof(CatalogLocationWarehouse);
    public const string CatalogServices = nameof(CatalogServices);
    public const string CatalogMemberRankings = nameof(CatalogMemberRankings);
    public const string CatalogAssetGroup = nameof(CatalogAssetGroup);
    public const string CatalogAsset = nameof(CatalogAsset);
    public const string CatalogAccountingShortcuts = nameof(CatalogAccountingShortcuts);
    public const string CatalogPersonalIncomeTaxRates = nameof(CatalogPersonalIncomeTaxRates);
    public const string CatalogTimekeepingSymbols = nameof(CatalogTimekeepingSymbols);
    public const string CatalogSalaryInsuranceRegulations = nameof(CatalogSalaryInsuranceRegulations);
    public const string CatalogShoppingList = nameof(CatalogShoppingList);
    public const string CatalogDiscountList = nameof(CatalogDiscountList);


    #endregion

    #region Salary

    public const string SalarySummaryOfTimekeeping = nameof(SalarySummaryOfTimekeeping);
    public const string SalaryTimeSheetSummary = nameof(SalaryTimeSheetSummary);
    public const string SalaryTimeSheet = nameof(SalaryTimeSheet);
    

    #endregion
}

