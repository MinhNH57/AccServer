namespace FixedAsset.API.Application.Models;

public class FADepreciationDto
{
    public Guid? TenantId { get; set; }
    public Guid RefId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int Month { get; set; }
    public int? Year { get; set; }
    public int DisplayOnBook { get; set; }
    public int RefOrder { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public double? TotalAmount { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; } = 0;
    public int EditVersion { get; set; }
    public string AttachmentIdList { get; set; }
    public string BranchName { get; set; }
}

public class FADepreciationDetailDto
{
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid? FixedAssetId { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public int SortOrder { get; set; }
    public double MonthlyDepreciationAmount { get; set; }
    public double AmountReasonableCost { get; set; }
    public double AmountUnReasonableCost { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public string FixedAssetCategoryName { get; set; }
    public Guid FixedAssetCategoryId { get; set; }
    public string FixedAssetCategoryCode { get; set; }
    public int State { get; set; }
    public int EditVersion { get; set; }
}

public class FADepreciationDetailAllocationDto
{
    public Guid? TenantId { get; set; }
    public Guid RefDetailId { get; set; }
    public Guid? RefId { get; set; }
    public Guid? FixedAssetId { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public Guid? AllocationObjectId { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public Guid? ListItemId { get; set; }
    public int SortOrder { get; set; }
    public double? MonthlyDepreciationAmount { get; set; }
    public double? AllocationRate { get; set; }
    public double? AllocationAmount { get; set; }
    public string CostAccount { get; set; }
    public string OrganizationUnitName { get; set; }
    public string AllocationObjectCode { get; set; }
    public string AllocationObjectName { get; set; }
    public int? AllocationObjectType { get; set; }
    public int State { get; set; } = 0;
    public int EditVersion { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public string ListItemCode { get; set; }
    public string ListItemName { get; set; }
    public string ExpenseItemCode { get; set; }
    public string ExpenseItemName { get; set; }
    public string DepreciationAccount { get; set; }
    public string OrganizationUnitCode { get; set; }
}

public class FADepreciationDetailPostDto
{
    public Guid? TenantId { get; set; }
    public Guid RefDetailId { get; set; }
    public Guid? RefId { get; set; }
    public Guid? DebitAccountObjectId { get; set; }
    public Guid? CreditAccountObjectId { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public Guid? JobId { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public Guid? OrderId { get; set; }
    public Guid? ContractId { get; set; }
    public Guid? ListItemId { get; set; }
    public int SortOrder { get; set; }
    public bool UnReasonableCost { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public string OrganizationUnitName { get; set; }
    public string ListItemCode { get; set; }
    public string ExpenseItemCode { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string JobCode { get; set; }
    public string JobName { get; set; }
    public string ExpenseItemName { get; set; }
    public string ListItemName { get; set; }
    public string ProjectWorkCode { get; set; }
    public string CreditAccountObjectCode { get; set; }
    public string DebitAccountObjectCode { get; set; }
    public string CreditAccountObjectName { get; set; }
    public string DebitAccountObjectName { get; set; }
    public string OrderCode { get; set; }
    public string ContractCode { get; set; }
    public string ContractSubject { get; set; }
    public string ProjectWorkName { get; set; }
    public string AccountName { get; set; }
    public DateTime? ContractSignDate { get; set; }
    public int State { get; set; }
    public int EditVersion { get; set; }
    public int? AllocationObjectType { get; set; }
}

public class FADepreciationResponse
{
    public List<FADepreciationDto> FADepreciation { get; set; }
    public List<FADepreciationDetailDto> FADepreciationDetail { get; set; }
    public List<FADepreciationDetailAllocationDto> FADepreciationDetailAllocation { get; set; }
    public List<FADepreciationDetailPostDto> FADepreciationDetailPost { get; set; }
}

public class FADepreciationDetailResponse
{
    public List<FADepreciationDetailDto> FADepreciationDetail { get; set; }
    public List<FADepreciationDetailAllocationDto> FADepreciationDetailAllocation { get; set; }
    public List<FADepreciationDetailAllocationDto> FADepreciationDetailAllocationLast { get; set; }
}

public class FADepreciationCreateResponse
{
    public List<FADepreciationSaveFullResponse> FADepreciation { get; set; }
    public List<FADepreciationDetailSaveFullResponse> FADepreciationDetail { get; set; }
    public List<FADepreciationDetailSaveFullResponse> FADepreciationDetailAllocation { get; set; }
    public List<FADepreciationDetailSaveFullResponse> FADepreciationDetailPost { get; set; }
}

public class FADepreciationUpdateResponse
{
    public List<FADepreciationSaveFullResponse> FADepreciation { get; set; } = [];
    public List<FADepreciationDetailSaveFullResponse> FADepreciationDetail { get; set; } = [];
    public List<FADepreciationDetailSaveFullResponse> FADepreciationDetailAllocation { get; set; } = [];
    public List<FADepreciationDetailSaveFullResponse> FADepreciationDetailPost { get; set; } = [];
}

public class FADepreciationSaveFullResponse
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

public class FADepreciationDetailSaveFullResponse
{
    public Guid? RefDetailId { get; set; }
    public int SortOrder { get; set; }
}