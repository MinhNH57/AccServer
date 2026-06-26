namespace Supply.Domain.AggregatesModel.SUAuditAggregate;

public class SUAudit :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; set; }
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

    private readonly List<SUAuditDetail> _sUAuditDetails;

    public IReadOnlyCollection<SUAuditDetail> SUAuditDetails => _sUAuditDetails.AsReadOnly();

    protected SUAudit()
    {
        _sUAuditDetails = [];
    }

    public SUAudit(
        Guid? tenantId,
        Guid? branchId,
        int refType,
        int? displayOnBook,
        DateTime? refDate,
        DateTime? refTime,
        DateTime? balanceDate,
        DateTime? createdDate,
        DateTime? modifiedDate,
        bool isExecuted,
        string refNo,
        string journalMemo,
        string summary,
        string createdBy,
        string modifiedBy,
        int state,
        int? editVersion,
        string auditMember,
        string branchName,
        string attachmentIdList) : this()
    {
        TenantId = tenantId;
        BranchId = branchId;
        RefType = refType;
        DisplayOnBook = displayOnBook;
        RefDate = refDate;
        RefTime = refTime;
        BalanceDate = balanceDate;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        IsExecuted = isExecuted;
        RefNo = refNo;
        JournalMemo = journalMemo;
        Summary = summary;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        AuditMember = auditMember;
        BranchName = branchName;
        AttachmentIdList = attachmentIdList;

        AddCreatedDomainEvent();
    }
    public SUAudit Update(
        Guid? tenantId,
        Guid? branchId,
        int? displayOnBook,
        DateTime? refDate,
        DateTime? refTime,
        DateTime? balanceDate,
        DateTime? modifiedDate,
        bool isExecuted,
        string refNo,
        string journalMemo,
        string summary,
        string modifiedBy,
        int state,
        int? editVersion,
        string auditMember,
        string branchName,
        string attachmentIdList)
    {
        TenantId = tenantId;
        BranchId = branchId;
        DisplayOnBook = displayOnBook;
        RefDate = refDate;
        RefTime = refTime;
        BalanceDate = balanceDate;
        ModifiedDate = modifiedDate;
        IsExecuted = isExecuted;
        RefNo = refNo;
        JournalMemo = journalMemo;
        Summary = summary;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        AuditMember = auditMember;
        BranchName = branchName;
        AttachmentIdList = attachmentIdList;

        AddUpdatedDomainEvent();

        return this;
    }

    public SUAuditDetail AddSUAuditDetail(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        Guid? organizationUnitId,
        int sortOrder,
        int? action,
        double? quantityOnBook,
        double? quantityInventory,
        double? diffQuantity,
        double? goodQuantity,
        double? damageQuantity,
        double? executeQuantity,
        string note,
        string organizationUnitCode,
        string organizationUnitName,
        int state,
        int? editVersion,
        string unit)
    {
        var detail = new SUAuditDetail(
            supplyId,
            supplyCode,
            supplyName,
            organizationUnitId,
            sortOrder,
            action,
            quantityOnBook,
            quantityInventory,
            diffQuantity,
            goodQuantity,
            damageQuantity,
            executeQuantity,
            note,
            organizationUnitCode,
            organizationUnitName,
            state,
            editVersion,
            unit);

        _sUAuditDetails.Add(detail);

        return detail;
    }

    public void ClearDetailData()
    {
        _sUAuditDetails.Clear();
    }

    private void AddCreatedDomainEvent()
    {
        var @event = new SUAuditCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddUpdatedDomainEvent()
    {
        var @event = new SUAuditUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
