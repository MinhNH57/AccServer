namespace FixedAsset.API.Application.Models;

#region Get
public class GetFATransferAvailableFixedAssetsQuery
{
    public Guid? BranchID { get; set; }
    public DateTime PostedDate { get; set; }
    public bool IsManagementBook { get; set; }
}

#endregion

#region Create

public class FATransferCreateRequest
{
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public int RefOrder { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? PostedDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public string RefNo { get; set; }
    public string HandOverName { get; set; }
    public string RecipientName { get; set; }
    public string JournalMemo { get; set; }
    public int State { get; set; }
    public string AttachmentIdList { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }

}

public class FATransferDetailCreateRequest
{
    public Guid? FixedAssetId { get; set; }
    public string FixedAssetName { get; set; }
    public Guid? FromOrganizationUnitId { get; set; }
    public Guid? ToOrganizationUnitId { get; set; }
    public Guid? ListItemId { get; set; }
    public Guid? ContractId { get; set; }
    public Guid? OrderId { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public Guid? JobId { get; set; }
    public int SortOrder { get; set; }
    public string CostAccount { get; set; }
    public string ContractCode { get; set; }
    public string ExpenseItemCode { get; set; }
    public string JobCode { get; set; }
    public string ListItemCode { get; set; }
    public string OrderCode { get; set; }
    public string ProjectWorkCode { get; set; }
    public string ExpenseItemName { get; set; }
    public string JobName { get; set; }
    public string ListItemName { get; set; }
    public string ProjectWorkName { get; set; }
    public string FromOrganizationUnitCode { get; set; }
    public string ToOrganizationUnitCode { get; set; }
    public string FromOrganizationUnitName { get; set; }
    public string ToOrganizationUnitName { get; set; }
    public string FixedAssetCode { get; set; }
    public int State { get; set; }

}

#endregion

#region Update

public class FATransferUpdateRequest
{
    public Guid RefId { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public int RefOrder { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? PostedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public string RefNo { get; set; }
    public string HandOverName { get; set; }
    public string RecipientName { get; set; }
    public string JournalMemo { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; }
    public int EditVersion { get; set; }
    public string AttachmentIdList { get; set; }
    public string BranchName { get; set; }
    public bool AutoRefNo { get; set; }
    public bool ForceUpdate { get; set; }

    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];

    public FATransferDto OldData { get; set; }
}

public class FATransferDetailUpdateRequest
{
    public Guid? RefDetailId { get; set; }
    public Guid? FixedAssetId { get; set; }
    public string FixedAssetName { get; set; }
    public Guid? FromOrganizationUnitId { get; set; }
    public Guid? ToOrganizationUnitId { get; set; }
    public Guid? ListItemId { get; set; }
    public Guid? ContractId { get; set; }
    public Guid? OrderId { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public Guid? JobId { get; set; }
    public int SortOrder { get; set; }
    public string CostAccount { get; set; }
    public string ContractCode { get; set; }
    public string ExpenseItemCode { get; set; }
    public string JobCode { get; set; }
    public string ListItemCode { get; set; }
    public string OrderCode { get; set; }
    public string ProjectWorkCode { get; set; }
    public string ExpenseItemName { get; set; }
    public string JobName { get; set; }
    public string ListItemName { get; set; }
    public string ProjectWorkName { get; set; }
    public string FromOrganizationUnitCode { get; set; }
    public string ToOrganizationUnitCode { get; set; }
    public string FromOrganizationUnitName { get; set; }
    public string ToOrganizationUnitName { get; set; }
    public string FixedAssetCode { get; set; }
    public int State { get; set; }
    public int EditVersion { get; set; }
    public FATransferDetailDto OldData { get; set; }
}

#endregion