namespace FixedAsset.Domain.AggregatesModel.FAAuditAggregate;

public class FAAudit :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; private set; }
    public Guid? BranchId { get; private set; }
    public int RefType { get; private set; }
    public int DisplayOnBook { get; private set; }
    public DateTime? RefDate { get; private set; }
    public DateTime? RefTime { get; private set; }
    public DateTime? InventoryDate { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsExecuted { get; private set; }
    public string RefNo { get; private set; }
    public string JournalMemo { get; private set; }
    public string Summary { get; private set; }
    public string CreatedBy { get; private set; }
    public string ModifiedBy { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }
    public string AuditMember { get; private set; }
    public string AttachmentIdList { get; private set; }
    public string BranchName { get; private set; }

    private readonly List<FAAuditDetail> _fAAuditDetails;

    public IReadOnlyCollection<FAAuditDetail> FAAuditDetails => _fAAuditDetails.AsReadOnly();

    protected FAAudit()
    {
        _fAAuditDetails = [];
    }

    public FAAudit(
        Guid? tenantId,
        Guid? branchId,
        int refType,
        int displayOnBook,
        DateTime? refDate,
        DateTime? refTime,
        DateTime? inventoryDate,
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
        string attachmentIdList,
        string branchName) : this()
    {
        TenantId = tenantId;
        BranchId = branchId;
        RefType = refType;
        DisplayOnBook = displayOnBook;
        RefDate = refDate;
        RefTime = refTime;
        InventoryDate = inventoryDate;
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
        AttachmentIdList = attachmentIdList;
        BranchName = branchName;

        AddCreatedDomainEvent();
    }

    public FAAudit Update(
        Guid? tenantId,
        Guid? branchId,
        int displayOnBook,
        DateTime? refDate,
        DateTime? refTime,
        DateTime? inventoryDate,
        DateTime? modifiedDate,
        bool isExecuted,
        string refNo,
        string journalMemo,
        string summary,
        string modifiedBy,
        int state,
        int? editVersion,
        string auditMember,
        string attachmentIdList,
        string branchName) 
    {
        TenantId = tenantId;
        BranchId = branchId;
        DisplayOnBook = displayOnBook;
        RefDate = refDate;
        RefTime = refTime;
        InventoryDate = inventoryDate;
        ModifiedDate = modifiedDate;
        IsExecuted = isExecuted;
        RefNo = refNo;
        JournalMemo = journalMemo;
        Summary = summary;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        AuditMember = auditMember;
        AttachmentIdList = attachmentIdList;
        BranchName = branchName;

        AddUpdatedDomainEvent();

        return this;
    }

    public FAAuditDetail AddFAAuditDetail(
        Guid? fixedAssetId,
        Guid? organizationUnitId,
        int existInStock,
        int quality,
        int recommendation,
        int sortOrder,
        double orgPrice,
        double depreciationAmount,
        double accumDepreciationAmount,
        double remainingAmount,
        string note,
        string organizationUnitCode,
        string organizationUnitName,
        string fixedAssetCode,
        string fixedAssetName,
        int state,
        int? editVersion)
    {
        var fAAuditDetail = new FAAuditDetail(
            fixedAssetId,
            organizationUnitId,
            existInStock,
            quality,
            recommendation,
            sortOrder,
            orgPrice,
            depreciationAmount,
            accumDepreciationAmount,
            remainingAmount,
            note,
            organizationUnitCode,
            organizationUnitName,
            fixedAssetCode,
            fixedAssetName,
            state,
            editVersion);

        _fAAuditDetails.Add(fAAuditDetail);

        return fAAuditDetail;
    }

    public void ClearDetailData()
    {
        _fAAuditDetails.Clear();
    }

    private void AddCreatedDomainEvent()
    {
        var @event = new FAAuditCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddUpdatedDomainEvent()
    {
        var @event = new FAAuditUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
