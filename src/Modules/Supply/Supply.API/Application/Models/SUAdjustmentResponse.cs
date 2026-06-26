namespace Supply.API.Application.Models;

public class SUAdjustmentDto
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
}

public class SUAdjustmentDetailDto
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
}

public class SUAdjustmentDetailVoucherDto
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
}

public class SUAdjustmentSupplyAvailable
{
    public Guid SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public int AllocationTime { get; set; }
    public double Quantity { get; set; }
    public double Amount { get; set; }
    public string AllocationAccount { get; set; }
}

public class SUAdjustmentResponse
{
    public List<SUAdjustmentDto> SUAdjustment { get; set; }
    public List<SUAdjustmentDetailDto> SUAdjustmentDetail { get; set; }
    public List<SUAdjustmentDetailVoucherDto> SUAdjustmentDetailVoucher { get; set; }
}

public class SUAdjustmentCreateResponse
{
    public List<SUAdjustmentSaveFullResponse> SUAdjustment { get; set; }
    public List<SUAdjustmentDetailSaveFullResponse> SUAdjustmentDetail { get; set; }
    public List<SUAdjustmentDetailSaveFullResponse> SUAdjustmentDetailVoucher { get; set; }
}

public class SUAdjustmentUpdateResponse
{
    public List<SUAdjustmentSaveFullResponse> SUAdjustment { get; set; } = [];
    public List<SUAdjustmentDetailSaveFullResponse> SUAdjustmentDetail { get; set; } = [];
    public List<SUAdjustmentDetailSaveFullResponse> SUAdjustmentDetailVoucher { get; set; } = [];
}

public class SUAdjustmentSaveFullResponse
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

public class SUAdjustmentDetailSaveFullResponse
{
    public Guid RefDetailId { get; set; }
    public int SortOrder { get; set; }
}