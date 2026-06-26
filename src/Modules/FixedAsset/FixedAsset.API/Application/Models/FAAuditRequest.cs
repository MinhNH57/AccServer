namespace FixedAsset.API.Application.Models;

#region Get

public class GetFAAuditAvailableFixedAssetsQuery
{
    public DateTime AuditDate { get; set; }
}

public class GetFAAuditCustomFixedAssetsQuery
{
    public string FixedAssetIDs { get; set; }
    public Guid? BranchID { get; set; }
    public DateTime PostedDate { get; set; }
    public bool IsManagementBook { get; set; }
}

#endregion

#region Create

public class FAAuditCreateRequest
{
    public Guid BranchId { get; set; }
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime RefTime { get; set; }
    public DateTime InventoryDate { get; set; }
    public bool IsExecuted { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public string Summary { get; set; }
    public int State { get; set; }
    public string AuditMember { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }
}

public class FAAuditDetailCreateRequest
{
    public Guid FixedAssetId { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public int ExistInStock { get; set; }
    public int Quality { get; set; }
    public int Recommendation { get; set; }
    public int SortOrder { get; set; }
    public int OrgPrice { get; set; }
    public int DepreciationAmount { get; set; }
    public int AccumDepreciationAmount { get; set; }
    public int RemainingAmount { get; set; }
    public string Note { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public int State { get; set; }
}

#endregion

#region Update

public class FAAuditUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid RefId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? RefTime { get; set; }
    public DateTime? InventoryDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsExecuted { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public string Summary { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public string AuditMember { get; set; }
    public string AttachmentIdList { get; set; }
    public string BranchName { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];
    public FAAuditDto OldData { get; set; }
}

public class FAAuditDetailUpdateRequest
{
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid? FixedAssetId { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public int ExistInStock { get; set; }
    public int Quality { get; set; }
    public int Recommendation { get; set; }
    public int SortOrder { get; set; }
    public double OrgPrice { get; set; }
    public double DepreciationAmount { get; set; }
    public double AccumDepreciationAmount { get; set; }
    public double RemainingAmount { get; set; }
    public string Note { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public FAAuditDetailDto OldData { get; set; }
}

#endregion