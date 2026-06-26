namespace FixedAsset.API.Application.Models;

#region Get

public class GetFADecrementAvailableFixedAssetsQuery
{
    public Guid? BranchID { get; set; }
    public DateTime PostedDate { get; set; }
    public bool IsManagementBook { get; set; }
}

public class GetFADecrementCustomFixedAssetsQuery
{
    public string FixedAssetIDs { get; set; }
    public Guid? BranchID { get; set; }
    public DateTime? PostedDate { get; set; }
    public bool IsManagementBook { get; set; }
}

#endregion

#region Create

public class FADecrementCreateRequest
{
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public int RefOrder { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime PostedDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public double TotalAmount { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public int State { get; set; }
    public string AttachmentIdList { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }
}

public class FADecrementDetailCreateRequest
{
    public Guid FixedAssetId { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public int SortOrder { get; set; }
    public double OrgPrice { get; set; }
    public double DepreciationAmount { get; set; }
    public double AccumDepreciationAmount { get; set; }
    public double RemainingAmount { get; set; }
    public double DepreciationAmountInMonth { get; set; }
    public string OrgPriceAccount { get; set; }
    public string DepreciationAccount { get; set; }
    public string RemainingAccount { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int State { get; set; }
}

public class FADecrementDetailPostCreateRequest
{
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
    public int State { get; set; }
}

#endregion

#region Update

public class FADecrementUpdateRequest
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
    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    public bool AutoRefNo { get; set; }
    public bool ForceUpdate { get; set; }
    public int EditVersion { get; set; }
    public int State { get; set; }
    public FADecrementDto OldData { get; set; }
}

public class FADecrementDetailUpdateRequest
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
    public FADecrementDetailDto OldData { get; set; }
}

public class FADecrementDetailPostUpdateRequest
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
    public FADecrementDetailPostDto OldData { get; set; }
}

#endregion