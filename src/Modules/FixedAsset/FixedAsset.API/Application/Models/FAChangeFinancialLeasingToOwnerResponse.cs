namespace FixedAsset.API.Application.Models;

public class FAChangeFinancialLeasingToOwnerAvailableFixedAsset
{
    public Guid? RefId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid FixedAssetId { get; set; }
    public string FixedAssetName { get; set; }
    public DateTime? PostedDate { get; set; }
    public DateTime? RefDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public bool OldIsLimitDepreciationAmount { get; set; }
    public bool NewIsLimitDepreciationAmount { get; set; }
    public double OldOrgPrice { get; set; }
    public double NewOrgPrice { get; set; }
    public double OldDepreciationAmount { get; set; }
    public double NewDepreciationAmount { get; set; }
    public double OldAccumDepreciationAmount { get; set; }
    public double NewAccumDepreciationAmount { get; set; }
    public double OldMonthlyDepreciationAmountByIncomeTax { get; set; }
    public double NewMonthlyDepreciationAmountByIncomeTax { get; set; }
    public double OldYearlyDepreciationAmount { get; set; }
    public double NewYearlyDepreciationAmount { get; set; }
    public double OldDepreciationAmountByIncomeTax { get; set; }
    public double NewDepreciationAmountByIncomeTax { get; set; }
    public double OldRemainingAmountByIncomeTax { get; set; }
    public double NewRemainingAmountByIncomeTax { get; set; }
    public double OldDepreciationRateMonth { get; set; }
    public double NewDepreciationRateMonth { get; set; }
    public double OldMonthlyDepreciationAmount { get; set; }
    public double NewMonthlyDepreciationAmount { get; set; }
    public double OldDepreciationRateYear { get; set; }
    public double NewDepreciationRateYear { get; set; }
    public double OldRemainingAmount { get; set; }
    public double NewRemainingAmount { get; set; }
    public double OldLifeTime { get; set; }
    public double NewLifeTime { get; set; }
    public double OldLifeTimeRemaining { get; set; }
    public double NewLifeTimeRemaining { get; set; }
    public string FixedAssetCode { get; set; }
    public string OldOrgPriceAccount { get; set; }
    public string OldDepreciationAccount { get; set; }
    public DateTime? ToDate { get; set; }
    public bool IsDecrement { get; set; }
    public bool IsChangeFinancialLeasing { get; set; }
    public int RefType { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool AutoRefNo { get; set; }
    public bool ForceUpdate { get; set; }
    public int State { get; set; }
}

public class FAChangeFinancialLeasingToOwnerDto
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
    public DateTime PostedDate { get; set; }
    public DateTime RefDate { get; set; }
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
}

public class FAChangeFinancialLeasingToOwnerDetailDto
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
}

public class FAChangeFinancialLeasingToOwnerResponse
{
    public List<FAChangeFinancialLeasingToOwnerDto> FAChangeFinancialLeasingToOwner { get; set; }
    public List<FAChangeFinancialLeasingToOwnerDetailDto> FAChangeFinancialLeasingToOwnerDetail { get; set; }
}

public class FAChangeFinancialLeasingToOwnerCreateResponse
{
    public List<FAChangeFinancialLeasingToOwnerSaveFullResponse> FAChangeFinancialLeasingToOwner { get; set; }
    public List<FAChangeFinancialLeasingToOwnerDetailSaveFullResponse> FAChangeFinancialLeasingToOwnerDetail { get; set; }
}

public class FAChangeFinancialLeasingToOwnerUpdateResponse
{
    public List<FAChangeFinancialLeasingToOwnerSaveFullResponse> FAChangeFinancialLeasingToOwner { get; set; } = [];
    public List<FAChangeFinancialLeasingToOwnerDetailSaveFullResponse> FAChangeFinancialLeasingToOwnerDetail { get; set; } = [];
}

public class FAChangeFinancialLeasingToOwnerSaveFullResponse
{
    public Guid RefId { get; set; }
    public int EditVersion { get; set; }
    public Guid BranchId { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string RefNo { get; set; }
    public int RefType { get; set; }

}

public class FAChangeFinancialLeasingToOwnerDetailSaveFullResponse
{
    public Guid RefDetailId { get; set; }
    public int SortOrder { get; set; }
}