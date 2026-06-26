namespace Supply.API.Application.Models;

public class SUTransferDto
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
    public double? TotalQuantity { get; set; }
    public string RefNo { get; set; }
    public string DeliveryName { get; set; }
    public string ReceiptName { get; set; }
    public string JournalMemo { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; } = 0;
    public string BranchName { get; set; }
    public int? EditVersion { get; set; }
    public string AttachmentIdList { get; set; }
}

public class SUTransferDetailDto
{
    public Guid? RefDetailId { get; set; }
    public Guid? RefId { get; set; }
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? FromOrganizationUnitId { get; set; }
    public string FromOrganizationUnitCode { get; set; }
    public string FromOrganizationUnitName { get; set; }
    public Guid? ToOrganizationUnitId { get; set; }
    public string ToOrganizationUnitCode { get; set; }
    public string ToOrganizationUnitName { get; set; }
    public Guid? ListItemId { get; set; }
    public string ListItemCode { get; set; }
    public string ListItemName { get; set; }
    public Guid? ContractId { get; set; }
    public string ContractCode { get; set; }
    public Guid? OrderId { get; set; }
    public string OrderCode { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public string ProjectWorkCode { get; set; }
    public string ProjectWorkName { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public string ExpenseItemCode { get; set; }
    public string ExpenseItemName { get; set; }
    public Guid? JobId { get; set; }
    public string JobCode { get; set; }
    public string JobName { get; set; }
    public int SortOrder { get; set; }
    public double? UseQuantity { get; set; }
    public double? TransferQuantity { get; set; }
    public string CostAccount { get; set; }
}


public class OrganizationUnitSupplyTransfer
{
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int OrganizationUnitTypeId { get; set; }
    public Guid? ParentId { get; set; }
    public int Grade { get; set; }
    public Guid? BranchId { get; set; }
    public bool IsParent { get; set; }
    public bool Inactive { get; set; }
    public double Quantity { get; set; }
}

public class SupplyTransfer
{
    public Guid SupplyId { get; set; }
    public int DisplayOnBook { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public bool IsReferenced { get; set; }
    public bool IsMappingAms { get; set; }
    public int ExcelRowIndex { get; set; }
    public bool IsValid { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool AutoRefNo { get; set; }
    public bool ForceUpdate { get; set; }
    public int State { get; set; }
}

public class SUTransferResponse
{
    public List<SUTransferDto> SUTransfer { get; set; }
    public List<SUTransferDetailDto> SUTransferDetail { get; set; }
}

public class SUTransferCreateResponse
{
    public List<SUTransferSaveFullResponse> SUTransfer { get; set; }
    public List<SUTransferDetailSaveFullResponse> SUTransferDetail { get; set; }
}

public class SUTransferUpdateResponse
{
    public List<SUTransferSaveFullResponse> SUTransfer { get; set; } = [];
    public List<SUTransferDetailSaveFullResponse> SUTransferDetail { get; set; } = [];
}

public class SUTransferSaveFullResponse
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

public class SUTransferDetailSaveFullResponse
{
    public Guid RefDetailId { get; set; }
    public int SortOrder { get; set; }
}