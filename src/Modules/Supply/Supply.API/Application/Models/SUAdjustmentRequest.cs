namespace Supply.API.Application.Models;

#region Get

public class GetSUAdjustmentSupplyAvailableQuery
{
    public DateTime RefDate { get; set; }
    public Guid? RefId { get; set; }
}

#endregion

#region Create

public class SUAdjustmentCreateRequest
{
    public Guid? TenantId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int? DisplayOnBook { get; set; }
    public int? RefOrder { get; set; }
    public string JournalMemo { get; set; }
    public DateTime? RefDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public string RefNo { get; set; }
    public int State { get; set; } = 0;
    public double? TotalAmount { get; set; }
    public string BranchName { get; set; }
    public string AttachmentIdList { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }
}

public class SUAdjustmentDetailCreateRequest
{
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public double? Quantity { get; set; }
    public string AllocationAccount { get; set; }
    public double? CurrentRemainingAmount { get; set; }
    public double? NewRemainingAmount { get; set; }
    public double? DiffRemainingAmount { get; set; }
    public double? CurrentRemainingAllocationTime { get; set; }
    public double? NewRemainingAllocationTime { get; set; }
    public double? DiffAllocationTime { get; set; }
    public double? TermlyAllocationAmount { get; set; }
    public string Note { get; set; }
    public int SortOrder { get; set; }
    public int State { get; set; } = 0;
}

public class SUAdjustmentDetailVoucherCreateRequest
{
    public Guid? TenantId { get; set; }
    public Guid? VoucherRefId { get; set; }
    public Guid VoucherRefDetailId { get; set; }
    public string CreditAccount { get; set; }
    public string DebitAccount { get; set; }
    public int? VoucherRefType { get; set; }
    public int SortOrder { get; set; }
    public string RefNo { get; set; }
    public double? Amount { get; set; }
    public DateTime? RefDate { get; set; }
    public string RefTypeName { get; set; }
    public string Description { get; set; }
    public int State { get; set; } = 0;
    public int? DetailPostOrder { get; set; }
}

#endregion

#region Update

public class SUAdjustmentUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid RefId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int? DisplayOnBook { get; set; }
    public int? RefOrder { get; set; }
    public string JournalMemo { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public string RefNo { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public double? TotalAmount { get; set; }
    public string BranchName { get; set; }
    public string AttachmentIdList { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];
    public SUAdjustmentDto OldData { get; set; }
}

public class SUAdjustmentDetailUpdateRequest
{
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public double? Quantity { get; set; }
    public string AllocationAccount { get; set; }
    public double? CurrentRemainingAmount { get; set; }
    public double? NewRemainingAmount { get; set; }
    public double? DiffRemainingAmount { get; set; }
    public double? CurrentRemainingAllocationTime { get; set; }
    public double? NewRemainingAllocationTime { get; set; }
    public double? DiffAllocationTime { get; set; }
    public double? TermlyAllocationAmount { get; set; }
    public string Note { get; set; }
    public int SortOrder { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public SUAdjustmentDetailDto OldData { get; set; }
}

public class SUAdjustmentDetailVoucherUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid? VoucherRefId { get; set; }
    public Guid VoucherRefDetailId { get; set; }
    public string CreditAccount { get; set; }
    public string DebitAccount { get; set; }
    public int? VoucherRefType { get; set; }
    public int SortOrder { get; set; }
    public string RefNo { get; set; }
    public double? Amount { get; set; }
    public DateTime? RefDate { get; set; }
    public string RefTypeName { get; set; }
    public string Description { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public int? DetailPostOrder { get; set; }
    public SUAdjustmentDetailVoucherDto OldData { get; set; }
}

#endregion