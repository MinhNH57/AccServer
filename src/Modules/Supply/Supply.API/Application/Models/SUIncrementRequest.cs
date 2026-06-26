namespace Supply.API.Application.Models;

#region Get

public class GetSupplyArisingQuery
{
    public Guid SupplyId { get; set; }
    public int DisplayOnBook { get; set; }
}

#endregion

#region Create

public class SUIncrementCreateRequest
{
    public Guid? TenantId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? SupplyCategoryId { get; set; }
    public Guid? SUAuditRefId { get; set; }
    public Guid? SupplyOtherBookId { get; set; }
    public Guid? FADecrementRefId { get; set; }
    public int RefType { get; set; }
    public int? AllocationTime { get; set; }
    public int? RemainingAllocationTime { get; set; }
    public int? DisplayOnBook { get; set; }
    public int? RefOrder { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedManagement { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool SuspendAllocate { get; set; }
    public double? Quantity { get; set; }
    public double? UnitPrice { get; set; }
    public double? Amount { get; set; }
    public double? AllocatedAmount { get; set; }
    public double? RemainingAmount { get; set; }
    public double? TermlyAllocationAmount { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public string RefNo { get; set; }
    public string Unit { get; set; }
    public string AllocationAccount { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public Guid? InPuRefDetailId { get; set; }
    public string ReasonIncrement { get; set; }
    public string SupplyGroup { get; set; }
    public string SupplyCategoryCode { get; set; }
    public string SupplyCategoryName { get; set; }
    public int? State { get; set; }
    public string BranchName { get; set; }
    public string ReasonInactive { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];
}

public class SUIncrementDetailDepartmentCreateRequest
{
    public Guid? OrganizationUnitId { get; set; }
    public int SortOrder { get; set; }
    public int? AllocationTime { get; set; }
    public int? RemainingAllocationTime { get; set; }
    public double? Quantity { get; set; }
    public double? UnitPrice { get; set; }
    public double? Amount { get; set; }
    public double? AllocatedAmount { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int? OrganizationUnitType { get; set; }
    public int State { get; set; } = 0;
}

public class SUIncrementDetailAllocationCreateRequest
{
    public Guid? ObjectId { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public int SortOrder { get; set; }
    public int? ObjectType { get; set; }
    public double? AllocationRate { get; set; }
    public string CostAccount { get; set; }
    public string ObjectCode { get; set; }
    public string ObjectName { get; set; }
    public int State { get; set; } = 0;
    public string ExpenseItemCode { get; set; }
    public Guid? ListItemId { get; set; }
    public string ListItemCode { get; set; }
}

public class SUIncrementDetailCreateRequest
{
    public int SortOrder { get; set; }
    public string Description { get; set; }
    public string NumberNo { get; set; }
    public int State { get; set; } = 0;
}

public class SUIncrementDetailSourceCreateRequest
{
    public Guid RefId { get; set; }
    public Guid? RefDetailId { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public Guid? FixedAssetId { get; set; }
    public int RefType { get; set; }
    public int SortOrder { get; set; }
    public string JournalMemo { get; set; }
    public string CreditAccount { get; set; }
    public string DebitAccount { get; set; }
    public string RefNo { get; set; }
    public double? Amount { get; set; }
    public DateTime? RefDate { get; set; }
    public int State { get; set; } = 0;
    public int? DetailPostOrder { get; set; }
}

#endregion

#region Update

public class SUIncrementUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid SupplyId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? SupplyCategoryId { get; set; }
    public Guid? SUAuditRefId { get; set; }
    public Guid? SupplyOtherBookId { get; set; }
    public Guid? FADecrementRefId { get; set; }
    public int RefType { get; set; }
    public int? AllocationTime { get; set; }
    public int? RemainingAllocationTime { get; set; }
    public int? DisplayOnBook { get; set; }
    public int? RefOrder { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedManagement { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool SuspendAllocate { get; set; }
    public double? Quantity { get; set; }
    public double? UnitPrice { get; set; }
    public double? Amount { get; set; }
    public double? AllocatedAmount { get; set; }
    public double? RemainingAmount { get; set; }
    public double? TermlyAllocationAmount { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public string RefNo { get; set; }
    public string Unit { get; set; }
    public string AllocationAccount { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public Guid? InPuRefDetailId { get; set; }
    public string ReasonIncrement { get; set; }
    public string SupplyGroup { get; set; }
    public string SupplyCategoryCode { get; set; }
    public string SupplyCategoryName { get; set; }
    public int? State { get; set; }
    public int? EditVersion { get; set; }
    public string BranchName { get; set; }
    public string ReasonInactive { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];

    public SUIncrement OldData { get; set; }
}

public class SUIncrementDetailDepartmentUpdateRequest
{
    public Guid? TenantId { get; set; }

    public Guid SupplyDetailId { get; set; }

    public Guid? SupplyId { get; set; }

    public Guid? OrganizationUnitId { get; set; }

    public int SortOrder { get; set; }

    public int? AllocationTime { get; set; }

    public int? RemainingAllocationTime { get; set; }

    public double? Quantity { get; set; }

    public double? UnitPrice { get; set; }

    public double? Amount { get; set; }

    public double? AllocatedAmount { get; set; }

    public string OrganizationUnitCode { get; set; }

    public string OrganizationUnitName { get; set; }
    public int? OrganizationUnitType { get; set; }

    public int State { get; set; } = 0;

    public int? EditVersion { get; set; }
    public SUIncrementDetailDepartment OldData { get; set; }
}

public class SUIncrementDetailAllocationUpdateRequest
{
    public Guid? TenantId { get; set; }

    public Guid SupplyDetailId { get; set; }

    public Guid? SupplyId { get; set; }

    public Guid? ObjectId { get; set; }

    public Guid? ExpenseItemId { get; set; }

    public int SortOrder { get; set; }

    public int? ObjectType { get; set; }

    public double? AllocationRate { get; set; }

    public string CostAccount { get; set; }

    public string ObjectCode { get; set; }

    public string ObjectName { get; set; }

    public int State { get; set; } = 0;

    public int? EditVersion { get; set; }

    public string ExpenseItemCode { get; set; }

    public Guid? ListItemId { get; set; }

    public string ListItemCode { get; set; }
    public SUIncrementDetailAllocation OldData { get; set; }
}

public class SUIncrementDetailUpdateRequest
{
    public Guid SupplyDetailId { get; set; }

    public Guid? SupplyId { get; set; }

    public int SortOrder { get; set; }

    public string Description { get; set; }

    public string NumberNo { get; set; }

    public int State { get; set; } = 0;

    public int? EditVersion { get; set; }
    public SUIncrementDetail OldData { get; set; }
}

public class SUIncrementDetailSourceUpdateRequest
{
    public Guid? TenantId { get; set; }

    public Guid SupplyDetailId { get; set; }

    public Guid? SupplyId { get; set; }

    public Guid RefId { get; set; }

    public Guid? RefDetailId { get; set; }

    public Guid? OrganizationUnitId { get; set; }

    public Guid? FixedAssetId { get; set; }

    public int RefType { get; set; }

    public int SortOrder { get; set; }

    public string JournalMemo { get; set; }

    public string CreditAccount { get; set; }

    public string DebitAccount { get; set; }

    public string RefNo { get; set; }

    public double? Amount { get; set; }

    public DateTime? RefDate { get; set; }

    public int State { get; set; } = 0;

    public int? EditVersion { get; set; }

    public int? DetailPostOrder { get; set; }

    public int? RefIdFake { get; set; }
    public SUIncrementDetailSource OldData { get; set; }
}

#endregion
