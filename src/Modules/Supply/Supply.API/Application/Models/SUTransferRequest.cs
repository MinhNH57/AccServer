namespace Supply.API.Application.Models;

# region Get

public class GetOrganizationUnitSupplyTransferQuery
{
    public Guid? SupplyId { get; set; }
    public DateTime RefDate { get; set; }
}

#endregion

#region Create

public class SUTransferCreateRequest
{
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int? DisplayOnBook { get; set; }
    public DateTime? RefDate { get; set; }
    public bool IsPostedManagement { get; set; }
    public bool IsPostedFinance { get; set; }
    public string RefNo { get; set; }
    public string DeliveryName { get; set; }
    public string ReceiptName { get; set; }
    public string JournalMemo { get; set; }
    public int State { get; set; } = 0;
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }
}

public class SUTransferDetailCreateRequest
{
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

#endregion

#region Update

public class SUTransferUpdateRequest
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

    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];

    public SUTransferDto OldData { get; set; }
}

public class SUTransferDetailUpdateRequest
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
    public SUTransferDetailDto OldData { get; set; }
}

#endregion