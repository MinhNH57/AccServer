namespace FixedAsset.API.Application.Models;

public class FAAuditCheckExistFADecrementToFAAudit
{
    public Guid FixedAssetId { get; set; }
    public string FixedAssetCode { get; set; }
    public Guid? RefDetailId { get; set; }
    public string RefNo { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool AutoRefNo { get; set; }
    public bool ForceUpdate { get; set; }
    public int State { get; set; }
    public bool IsCreatedFromOldDb { get; set; }
}

public class FAAuditAvailableFixedAsset
{
    public Guid? RefDetailId { get; set; }
    public Guid? RefId { get; set; }
    public Guid FixedAssetId { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public double OrgPrice { get; set; }
    public double DepreciationAmount { get; set; }
    public double AccumDepreciationAmount { get; set; }
    public double RemainingAmount { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public int State { get; set; }
}


public class FAAuditDto
{
    public Guid? TenantId { get; set; }
    public Guid RefId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public DateTime RefDate { get; set; }
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
}

public class FAAuditDetailDto
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
}

public class FAAuditResponse
{
    public List<FAAuditDto> FAAudit { get; set; }
    public List<FAAuditDetailDto> FAAuditDetail { get; set; }
}

public class FAAuditCreateResponse
{
    public List<FAAuditSaveFullResponse> FAAudit { get; set; }
    public List<FAAuditDetailSaveFullResponse> FAAuditDetail { get; set; }
}

public class FAAuditUpdateResponse
{
    public List<FAAuditSaveFullResponse> FAAudit { get; set; } = [];
    public List<FAAuditDetailSaveFullResponse> FAAuditDetail { get; set; } = [];
}

public class FAAuditSaveFullResponse
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

public class FAAuditDetailSaveFullResponse
{
    public Guid? RefDetailId { get; set; }
    public int SortOrder { get; set; }
}