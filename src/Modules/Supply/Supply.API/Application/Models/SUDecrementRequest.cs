namespace Supply.API.Application.Models;

#region Get

public class GetAvailableSupplyForDecrementQuery
{
    public DateTime RefDate { get; set; }
}

#endregion

#region Create

public class SUDecrementCreateRequest
{
    public Guid? TenantId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int? DisplayOnBook { get; set; }
    public int? RefOrder { get; set; }
    public DateTime? RefDate { get; set; }
    public bool IsPostedManagement { get; set; }
    public bool IsPostedFinance { get; set; }
    public double? TotalAmount { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public int State { get; set; } = 0;
    public string BranchName { get; set; }
    public string AttachmentIdList { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }
}

public class SUDecrementDetailCreateRequest
{
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public Guid? SUAllocationId { get; set; }
    public Guid? SUAuditRefId { get; set; }
    public int SortOrder { get; set; }
    public double? UseQuantity { get; set; }
    public double? DecrementQuantity { get; set; }
    public double? DecrementAmount { get; set; }
    public double? RemainingDecrementAmount { get; set; }
    public string Reason { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int State { get; set; } = 0;
}

#endregion

#region Update

public class SUDecrementUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid RefId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int? DisplayOnBook { get; set; }
    public int? RefOrder { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedManagement { get; set; }
    public bool IsPostedFinance { get; set; }
    public double? TotalAmount { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; } = 0;
    public string BranchName { get; set; }
    public int? EditVersion { get; set; }
    public string AttachmentIdList { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }
    public SUDecrementDto OldData { get; set; }
}

public class SUDecrementDetailUpdateRequest
{
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public Guid? SUAllocationId { get; set; }
    public Guid? SUAuditRefId { get; set; }
    public int SortOrder { get; set; }
    public double? UseQuantity { get; set; }
    public double? DecrementQuantity { get; set; }
    public double? DecrementAmount { get; set; }
    public double? RemainingDecrementAmount { get; set; }
    public string Reason { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public SUDecrementDetailDto OldData { get; set; }
}

#endregion