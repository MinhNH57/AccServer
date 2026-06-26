namespace Supply.API.Application.Models;

#region Get

public class CheckExistsSUAllocationQuery
{
    public int Month { get; set; }
    public int Year { get; set; }
}

public class GetSUAllocationExpenseQuery
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}

#endregion

#region Create

public class SUAllocationCreateRequest
{
    public int RefType { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int DisplayOnBook { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime PostedDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public bool IsGetSupplyAllocated { get; set; }
    public double TotalAmount { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public int State { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }
}

public class SUAllocationDetailExpenseCreateRequest
{
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public string SupplyCategoryCode { get; set; }
    public string SupplyCategoryName { get; set; }
    public Guid SupplyId { get; set; }
    public Guid? SupplyCategoryId { get; set; }
    public int SortOrder { get; set; }
    public int TotalAllocationAmount { get; set; }
    public int AllocationAmount { get; set; }
    public int RemainingAmount { get; set; }
    public int State { get; set; }
}

public class SUAllocationDetailTableCreateRequest
{
    public Guid SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid ListItemId { get; set; }
    public string ListItemCode { get; set; }
    public string ListItemName { get; set; }
    public Guid AllocationObjectId { get; set; }
    public double AllocationRate { get; set; }
    public int AllocationAmount { get; set; }
    public int SortOrder { get; set; }
    public string CostAccount { get; set; }
    public Guid ExpenseItemId { get; set; }
    public int TotalAllocationAmount { get; set; }
    public int State { get; set; }
    public string ExpenseItemCode { get; set; }
    public string ExpenseItemName { get; set; }
    public string AllocationObjectCode { get; set; }
    public string AllocationObjectName { get; set; }
    public string AllocationAccount { get; set; }
    public int AllocationObjectType { get; set; }
}

public class SUAllocationDetailPostCreateRequest
{
    public string Description { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public double? Amount { get; set; }
    public string ListItemCode { get; set; }
    public Guid? DebitAccountObjectId { get; set; }
    public string DebitAccountObjectName { get; set; }
    public Guid? CreditAccountObjectId { get; set; }
    public string CreditAccountObjectName { get; set; }
    public string DebitAccountObjectCode { get; set; }
    public string CreditAccountObjectCode { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public Guid? JobId { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public string ProjectWorkCode { get; set; }
    public string ProjectWorkName { get; set; }
    public Guid? OrderId { get; set; }
    public string OrderCode { get; set; }
    public Guid? ContractId { get; set; }
    public string ContractCode { get; set; }
    public Guid? ListItemId { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public int SortOrder { get; set; }
    public bool UnReasonableCost { get; set; }
    public int State { get; set; } = 0;
    public string ExpenseItemCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public string JobName { get; set; }
    public string ExpenseItemName { get; set; }
    public string ListItemName { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string JobCode { get; set; }
    public string ContractSubject { get; set; }
    public Guid? AllocationObjectId { get; set; }
    public string AllocationObjectCode { get; set; }
    public string AllocationObjectName { get; set; }
    public int? AllocationObjectType { get; set; }
}

#endregion

#region Update

public class SUAllocationUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid RefId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
    public int? DisplayOnBook { get; set; }
    public int? RefOrder { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? PostedDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedManagement { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsGetSupplyAllocated { get; set; }
    public double? TotalAmount { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; } = 0;
    public string BranchName { get; set; }
    public int? EditVersion { get; set; }
    public string AttachmentIdList { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];
    public SUAllocationDto OldData { get; set; }
}

public class SUAllocationDetailExpenseUpdateRequest
{
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public string SupplyCategoryCode { get; set; }
    public string SupplyCategoryName { get; set; }
    public Guid? TenantId { get; set; }
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid? SupplyId { get; set; }
    public Guid? SupplyCategoryId { get; set; }
    public int SortOrder { get; set; }
    public double? TotalAllocationAmount { get; set; }
    public double? AllocationAmount { get; set; }
    public double? RemainingAmount { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public SUAllocationDetailExpenseDto OldData { get; set; }
}

public class SUAllocationDetailTableUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? AllocationObjectId { get; set; }
    public double? AllocationRate { get; set; }
    public double? AllocationAmount { get; set; }
    public int SortOrder { get; set; }
    public string CostAccount { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public double? TotalAllocationAmount { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public string ExpenseItemCode { get; set; }
    public string ExpenseItemName { get; set; }
    public string AllocationObjectCode { get; set; }
    public string AllocationObjectName { get; set; }
    public string AllocationAccount { get; set; }
    public int? AllocationObjectType { get; set; }
    public Guid? ListItemId { get; set; }
    public string ListItemCode { get; set; }
    public string ListItemName { get; set; }
    public SUAllocationDetailTableDto OldData { get; set; }
}

public class SUAllocationDetailPostUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public string Description { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public double? Amount { get; set; }
    public string ListItemCode { get; set; }
    public Guid? DebitAccountObjectId { get; set; }
    public string DebitAccountObjectName { get; set; }
    public Guid? CreditAccountObjectId { get; set; }
    public string CreditAccountObjectName { get; set; }
    public string DebitAccountObjectCode { get; set; }
    public string CreditAccountObjectCode { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public Guid? JobId { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public string ProjectWorkCode { get; set; }
    public string ProjectWorkName { get; set; }
    public Guid? OrderId { get; set; }
    public string OrderCode { get; set; }
    public Guid? ContractId { get; set; }
    public string ContractCode { get; set; }
    public Guid? ListItemId { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public int SortOrder { get; set; }
    public bool UnReasonableCost { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public string ExpenseItemCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public string JobName { get; set; }
    public string ExpenseItemName { get; set; }
    public string ListItemName { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string JobCode { get; set; }

    public string ContractSubject { get; set; }
    public Guid? AllocationObjectId { get; set; }
    public string AllocationObjectCode { get; set; }
    public string AllocationObjectName { get; set; }
    public int? AllocationObjectType { get; set; }
    public SUAllocationDetailPostDto OldData { get; set; }
}

#endregion