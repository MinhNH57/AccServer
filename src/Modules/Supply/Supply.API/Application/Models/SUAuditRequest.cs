namespace Supply.API.Application.Models;

#region Create

public class SUAuditCreateRequest
{
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime RefTime { get; set; }
    public DateTime BalanceDate { get; set; }
    public bool IsExecuted { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public string Summary { get; set; }
    public int State { get; set; }
    public string AuditMember { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; }
}

public class SUAuditDetailCreateRequest
{
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public int SortOrder { get; set; }
    public int Action { get; set; }
    public double QuantityOnBook { get; set; }
    public double QuantityInventory { get; set; }
    public double DiffQuantity { get; set; }
    public double GoodQuantity { get; set; }
    public double DamageQuantity { get; set; }
    public double ExecuteQuantity { get; set; }
    public string Note { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int State { get; set; }
    public string Unit { get; set; }
}

#endregion

#region Update

public class SUAuditUpdateRequest
{
    public Guid? TenantId { get; set; }
    public Guid RefId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int? DisplayOnBook { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? RefTime { get; set; }
    public DateTime? BalanceDate { get; set; }
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
    public string BranchName { get; set; }
    public string AttachmentIdList { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];
    public SUAudit OldData { get; set; }
}

public class SUAuditDetailUpdateRequest
{
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public int SortOrder { get; set; }
    public int? Action { get; set; }
    public double? QuantityOnBook { get; set; }
    public double? QuantityInventory { get; set; }
    public double? DiffQuantity { get; set; }
    public double? GoodQuantity { get; set; }
    public double? DamageQuantity { get; set; }
    public double? ExecuteQuantity { get; set; }
    public string Note { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }
    public string Unit { get; set; }
    public SUAuditDetail OldData { get; set; }
}

#endregion