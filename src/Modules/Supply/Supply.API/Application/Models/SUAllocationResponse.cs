namespace Supply.API.Application.Models;

public class SUAllocationDto
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
}

public class SUAllocationDetailExpenseDto
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
}

public class SUAllocationDetailTableDto
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
}

public class SUAllocationDetailPostDto
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
}

public class SUAllocationResponse
{
    public List<SUAllocationDto> SUAllocation { get; set; }
    public List<SUAllocationDetailExpenseDto> SUAllocationDetailExpense { get; set; }
    public List<SUAllocationDetailTableDto> SUAllocationDetailTable { get; set; }
    public List<SUAllocationDetailPostDto> SUAllocationDetailPost { get; set; }
}

public class SUAllocationExpenseResponse
{
    public List<SUAllocationDetailExpenseDto> ListExpenseData { get; set; }
    public List<SUAllocationDetailTableDto> ListDataAllocationTable { get; set; }
    public List<SUAllocationDetailPostDto> ListDataAllocationDetailPost { get; set; }
}

public class SUAllocationCreateResponse
{
    public List<SUAllocationSaveFullResponse> SUAllocation { get; set; }
    public List<SUAllocationDetailSaveFullResponse> SUAllocationDetailExpense { get; set; }
    public List<SUAllocationDetailSaveFullResponse> SUAllocationDetailTable { get; set; }
    public List<SUAllocationDetailSaveFullResponse> SUAllocationDetailPost { get; set; }
}

public class SUAllocationUpdateResponse
{
    public List<SUAllocationSaveFullResponse> SUAllocation { get; set; } = [];
    public List<SUAllocationDetailSaveFullResponse> SUAllocationDetailExpense { get; set; } = [];
    public List<SUAllocationDetailSaveFullResponse> SUAllocationDetailTable { get; set; } = [];
    public List<SUAllocationDetailSaveFullResponse> SUAllocationDetailPost { get; set; } = [];
}

public class SUAllocationSaveFullResponse
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

public class SUAllocationDetailSaveFullResponse
{
    public Guid? RefDetailId { get; set; }
    public int SortOrder { get; set; }
}