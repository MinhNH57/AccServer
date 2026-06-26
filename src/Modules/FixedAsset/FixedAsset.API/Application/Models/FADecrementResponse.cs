namespace FixedAsset.API.Application.Models;

public class FADecrementResponse
{
    public List<FADecrementDto> FADecrement { get; set; }
    public List<FADecrementDetailDto> FADecrementDetail { get; set; }
    public List<FADecrementDetailPostDto> FADecrementDetailPost { get; set; }
}

public class FADecrementDto
{
    public Guid RefId { get; set; }
    public Guid BranchId { get; set; }
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public int RefOrder { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime PostedDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public int TotalAmount { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public string BranchName { get; set; }
    public string AttachmentIdList { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    public bool AutoRefNo { get; set; }
    public bool ForceUpdate { get; set; }
    public int EditVersion { get; set; }
    public int State { get; set; }
}

public class FADecrementDetailDto
{
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid FixedAssetId { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public int SortOrder { get; set; }
    public int OrgPrice { get; set; }
    public int DepreciationAmount { get; set; }
    public int AccumDepreciationAmount { get; set; }
    public int RemainingAmount { get; set; }
    public int DepreciationAmountInMonth { get; set; }
    public string OrgPriceAccount { get; set; }
    public string DepreciationAccount { get; set; }
    public string RemainingAccount { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int EditVersion { get; set; }
    public int State { get; set; }
}

public class FADecrementDetailPostDto
{
    public Guid? TenantId { get; set; }
    public Guid RefDetailId { get; set; }
    public Guid? RefId { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public string ExpenseItemCode { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public string OrganizationUnitCode { get; set; }
    public Guid? JobId { get; set; }
    public string JobCode { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public string ProjectWorkCode { get; set; }
    public Guid? OrderId { get; set; }
    public string OrderCode { get; set; }
    public Guid? ContractId { get; set; }
    public string ContractCode { get; set; }
    public Guid? ListItemId { get; set; }
    public string ListItemCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public string JobName { get; set; }
    public string ProjectWorkName { get; set; }
    public string ExpenseItemName { get; set; }
    public string ListItemName { get; set; }
    public Guid? AccountObjectId { get; set; }
    public string AccountObjectCode { get; set; }
    public string AccountObjectName { get; set; }
    public int SortOrder { get; set; }
    public bool UnReasonableCost { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public int EditVersion { get; set; }
    public int State { get; set; }
}


public class FADecrementCustomFixedAsset
{
    public Guid FixedAssetId { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public DateTime IncrementDate { get; set; }
    public double OrgPrice { get; set; }
    public string OrgPriceAccount { get; set; }
    public double DepreciationAmount { get; set; }
    public string DepreciationAccount { get; set; }
    public double RealAccumDepreciationAmount { get; set; }
    public double AccumDepreciationAmount { get; set; }
    public double DepreciationAmountInMonth { get; set; }
    public double MonthlyDepreciationAmount { get; set; }
    public double LifeTimeRemainingInMonth { get; set; }
}

public class FADecrementAvailableFixedAsset
{
    public Guid FixedAssetId { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public DateTime DepreciationDate { get; set; }
    public string DepreciationAccount { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public Guid FixedAssetCategoryId { get; set; }
    public bool IsFixedAssetOfStateBudget { get; set; }
    public bool IsNotDepreciation { get; set; }
    public bool HasDepreciationInMonth { get; set; }
    public double DepreciationAmountByIncomeTax { get; set; }
    public double SumAmountReasonableCost { get; set; }
    public double MonthlyDepreciationAmountByIncomeTax { get; set; }
    public double AccumDepreciationAmount { get; set; }
}

public class FADecrementCreateResponse
{
    public List<FADecrementSaveFullResponse> FADecrement { get; set; }
    public List<FADecrementDetailSaveFullResponse> FADecrementDetail { get; set; }
    public List<FADecrementDetailSaveFullResponse> FADecrementDetailPost { get; set; }
}

public class FADecrementUpdateResponse
{
    public List<FADecrementSaveFullResponse> FADecrement { get; set; } = [];
    public List<FADecrementDetailSaveFullResponse> FADecrementDetail { get; set; } = [];
    public List<FADecrementDetailSaveFullResponse> FADecrementDetailPost { get; set; } = [];
}

public class FADecrementSaveFullResponse
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

public class FADecrementDetailSaveFullResponse
{
    public Guid RefDetailId { get; set; }
    public int SortOrder { get; set; }
}