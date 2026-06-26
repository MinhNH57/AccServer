using System;

namespace Ledger.API.Model;

public class FALedger : IBaseEntity
{
    public Guid FixedAssetId { get; set; }
    public Guid? FixedAssetCategoryId { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public Guid? BranchId { get; set; }
    public string BranchName { get; set; }
    public Guid? RefId { get; set; }
    public int RefType { get; set; }
    public int State { get; set; }
    public int Quality { get; set; }
    public int LifeTimeUnit { get; set; }
    public int LifeTimeRemainingUnit { get; set; }
    public int RefOrder { get; set; }
    public int DisplayOnBook { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? DepreciationDate { get; set; }
    public bool? IsNotDepreciation { get; set; } = false;
    public bool? IsLimitDepreciationAmount { get; set; } = false;
    public bool? IsEnoughVoucher { get; set; } = false;
    public bool? IsPostedManagement { get; set; } = false;
    public bool? IsPostedFinance { get; set; } = false;
    public bool? IsFixedAssetOfStateBudget { get; set; } = false;
    public double OrgPrice { get; set; }
    public double DepreciationAmount { get; set; }
    public double LifeTime { get; set; }
    public double LifeTimeRemaining { get; set; }
    public double LifeTimeInMonth { get; set; }
    public double LifeTimeRemainingInMonth { get; set; }
    public double MonthlyDepreciationAmount { get; set; }
    public double AccumDepreciationAmount { get; set; }
    public double RemainingAmount { get; set; }
    public string RefNo { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public string OrgPriceAccount { get; set; }
    public string DepreciationAccount { get; set; }
    public string OrganizationUnitName { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string FixedAssetCategoryName { get; set; }
    public int FixedAssetState { get; set; }
    public string FixedAssetCategoryCode { get; set; }
    public bool? IsChangeFinancialLeasingToOwner { get; set; } = false;
}