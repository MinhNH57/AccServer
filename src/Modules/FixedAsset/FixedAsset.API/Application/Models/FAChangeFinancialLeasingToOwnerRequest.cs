namespace FixedAsset.API.Application.Models;

#region Get

public class GetFAChangeFinancialLeasingToOwnerAvailableFixedAssetsQuery
{
    public DateTime ToDate { get; set; }
    public Guid? FixedAssetId { get; set; }
}

#endregion

#region Create

public class FAChangeFinancialLeasingToOwnerCreateRequest
{
    public Guid? BranchId { get; set; }
    public Guid FixedAssetId { get; set; }
    public string FixedAssetName { get; set; }
    public int DisplayOnBook { get; set; }
    public int RefType { get; set; }
    public string JournalMemo { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime RefDate { get; set; }
    public string RefNo { get; set; }
    public string FixedAssetCode { get; set; }
    public double TotalAmount { get; set; }
    public string OldOrgPriceAccount { get; set; }
    public string NewOrgPriceAccount { get; set; }
    public string OldDepreciationAccount { get; set; }
    public string NewDepreciationAccount { get; set; }
    public double OldOrgPrice { get; set; }
    public double NewOrgPrice { get; set; }
    public double OldDepreciationAmount { get; set; }
    public double NewDepreciationAmount { get; set; }
    public double OldAccumDepreciationAmount { get; set; }
    public double NewAccumDepreciationAmount { get; set; }
    public double OldRemainingAmount { get; set; }
    public double NewRemainingAmount { get; set; }
    public double OldLifeTime { get; set; }
    public double NewLifeTime { get; set; }
    public double OldLifeTimeRemaining { get; set; }
    public double NewLifeTimeRemaining { get; set; }
    public double OldDepreciationRateMonth { get; set; }
    public double NewDepreciationRateMonth { get; set; }
    public double OldMonthlyDepreciationAmount { get; set; }
    public double NewMonthlyDepreciationAmount { get; set; }
    public double OldDepreciationRateYear { get; set; }
    public double NewDepreciationRateYear { get; set; }
    public double OldYearlyDepreciationAmount { get; set; }
    public double NewYearlyDepreciationAmount { get; set; }
    public bool OldIsLimitDepreciationAmount { get; set; }
    public bool NewIsLimitDepreciationAmount { get; set; }
    public double OldDepreciationAmountByIncomeTax { get; set; }
    public double NewDepreciationAmountByIncomeTax { get; set; }
    public double OldRemainingAmountByIncomeTax { get; set; }
    public double NewRemainingAmountByIncomeTax { get; set; }
    public double OldMonthlyDepreciationAmountByIncomeTax { get; set; }
    public double NewMonthlyDepreciationAmountByIncomeTax { get; set; }
    public int State { get; set; }
    public double DifferentOrgPrice { get; set; }
    public double DifferentDepreciationAmount { get; set; }
    public double DifferentAccumDepreciationAmount { get; set; }
    public double DifferentRemainingAmount { get; set; }
    public double DifferentLifeTime { get; set; }
    public double DifferentLifeTimeRemaining { get; set; }
    public double DifferentDepreciationRateMonth { get; set; }
    public double DifferentMonthlyDepreciationAmount { get; set; }
    public double DifferentDepreciationRateYear { get; set; }
    public double DifferentYearlyDepreciationAmount { get; set; }
    public double DifferentDepreciationAmountByIncomeTax { get; set; }
    public double DifferentRemainingAmountByIncomeTax { get; set; }
    public double DifferentMonthlyDepreciationAmountByIncomeTax { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }
}

public class FAChangeFinancialLeasingToOwnerDetailCreateRequest
{
    public int SortOrder { get; set; }
    public string Description { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public double Amount { get; set; }
    public string ListItemCode { get; set; }
    public int State { get; set; }

}

#endregion

#region Update

public class FAChangeFinancialLeasingToOwnerUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid RefId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? FixedAssetId { get; set; }
    public string FixedAssetName { get; set; }
    public int? DisplayOnBook { get; set; }
    public int RefType { get; set; }
    public int? RefOrder { get; set; }
    public string JournalMemo { get; set; }
    public DateTime? PostedDate { get; set; }
    public DateTime? RefDate { get; set; }
    public string RefNo { get; set; }
    public string FixedAssetCode { get; set; }
    public double? TotalAmount { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public string OldOrgPriceAccount { get; set; }
    public string NewOrgPriceAccount { get; set; }
    public string OldDepreciationAccount { get; set; }
    public string NewDepreciationAccount { get; set; }
    public double OldOrgPrice { get; set; }
    public double NewOrgPrice { get; set; }
    public double OldDepreciationAmount { get; set; }
    public double NewDepreciationAmount { get; set; }
    public double OldAccumDepreciationAmount { get; set; }
    public double NewAccumDepreciationAmount { get; set; }
    public double OldRemainingAmount { get; set; }
    public double NewRemainingAmount { get; set; }
    public double OldLifeTime { get; set; }
    public double NewLifeTime { get; set; }
    public double OldLifeTimeRemaining { get; set; }
    public double NewLifeTimeRemaining { get; set; }
    public double OldDepreciationRateMonth { get; set; }
    public double NewDepreciationRateMonth { get; set; }
    public double OldMonthlyDepreciationAmount { get; set; }
    public double NewMonthlyDepreciationAmount { get; set; }
    public double OldDepreciationRateYear { get; set; }
    public double NewDepreciationRateYear { get; set; }
    public double OldYearlyDepreciationAmount { get; set; }
    public double NewYearlyDepreciationAmount { get; set; }
    public bool OldIsLimitDepreciationAmount { get; set; }
    public bool NewIsLimitDepreciationAmount { get; set; }
    public double OldDepreciationAmountByIncomeTax { get; set; }
    public double NewDepreciationAmountByIncomeTax { get; set; }
    public double OldRemainingAmountByIncomeTax { get; set; }
    public double NewRemainingAmountByIncomeTax { get; set; }
    public double OldMonthlyDepreciationAmountByIncomeTax { get; set; }
    public double NewMonthlyDepreciationAmountByIncomeTax { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public double DifferentOrgPrice { get; set; }
    public double DifferentDepreciationAmount { get; set; }
    public double DifferentAccumDepreciationAmount { get; set; }
    public double DifferentRemainingAmount { get; set; }
    public double DifferentLifeTime { get; set; }
    public double DifferentLifeTimeRemaining { get; set; }
    public double DifferentDepreciationRateMonth { get; set; }
    public double DifferentMonthlyDepreciationAmount { get; set; }
    public double DifferentDepreciationRateYear { get; set; }
    public double DifferentYearlyDepreciationAmount { get; set; }
    public double DifferentDepreciationAmountByIncomeTax { get; set; }
    public double DifferentRemainingAmountByIncomeTax { get; set; }
    public double DifferentMonthlyDepreciationAmountByIncomeTax { get; set; }
    public string AttachmentIdList { get; set; }
    public string BranchName { get; set; }

    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];

    public FAChangeFinancialLeasingToOwnerDto OldData { get; set; }
}

public class FAChangeFinancialLeasingToOwnerDetailUpdateRequest
{
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public int SortOrder { get; set; }
    public string Description { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public int? Amount { get; set; }
    public Guid? ListItemId { get; set; }
    public string ListItemCode { get; set; }
    public string ListItemName { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public FAChangeFinancialLeasingToOwnerDetailDto OldData { get; set; }
}

#endregion