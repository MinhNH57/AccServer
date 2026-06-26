namespace Supply.Domain.AggregatesModel.SUDecrementAggregate;

public class SUDecrement :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; set; }
    public Guid? BranchId { get; set; }
    public int RefType { get; set; }
    public int? DisplayOnBook { get; set; }
    public int? RefOrder { get; set; }
    public DateTime? RefDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedManagement { get; set; }
    public bool IsPostedFinance { get; set; }
    public double? TotalAmount { get; set; }
    public string RefNo { get; set; }
    public string JournalMemo { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; } = 0;
    public string BranchName { get; set; }
    public int? EditVersion { get; set; }
    public string AttachmentIdList { get; set; }

    private readonly List<SUDecrementDetail> _sUDecrementDetails;

    public IReadOnlyCollection<SUDecrementDetail> SUDecrementDetails => _sUDecrementDetails.AsReadOnly();

    protected SUDecrement()
    {
        _sUDecrementDetails = new List<SUDecrementDetail>();
    }

    public SUDecrement(
        Guid? tenantId,
        Guid? branchId,
        int refType,
        int? displayOnBook,
        int? refOrder,
        DateTime? refDate,
        DateTime? createdDate,
        DateTime? modifiedDate,
        bool isPostedManagement,
        bool isPostedFinance,
        double? totalAmount,
        string refNo,
        string journalMemo,
        string createdBy,
        string modifiedBy,
        int state,
        string branchName,
        int? editVersion,
        string attachmentIdList) : this()
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        BranchId = branchId;
        RefType = refType;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        IsPostedManagement = isPostedManagement;
        IsPostedFinance = isPostedFinance;
        TotalAmount = totalAmount;
        RefNo = refNo;
        JournalMemo = journalMemo;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        State = state;
        BranchName = branchName;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;

        AddSUDecrementCreatedDomainEvent();
    }

    public SUDecrement Update(
        Guid? tenantId,
        Guid? branchId,
        int? displayOnBook,
        int? refOrder,
        DateTime? refDate,
        DateTime? modifiedDate,
        bool isPostedManagement,
        bool isPostedFinance,
        double? totalAmount,
        string refNo,
        string journalMemo,
        string modifiedBy,
        int state,
        string branchName,
        int? editVersion,
        string attachmentIdList)
    {
        TenantId = tenantId;
        BranchId = branchId;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        ModifiedDate = modifiedDate;
        IsPostedManagement = isPostedManagement;
        IsPostedFinance = isPostedFinance;
        TotalAmount = totalAmount;
        RefNo = refNo;
        JournalMemo = journalMemo;
        ModifiedBy = modifiedBy;
        State = state;
        BranchName = branchName;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;

        AddSUDecrementUpdatedDomainEvent();

        return this;
    }

    public SUDecrementDetail AddSUDecrementDetail(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        Guid? organizationUnitId,
        Guid? sUAllocationId,
        Guid? sUAuditRefId,
        int sortOrder,
        double? useQuantity,
        double? decrementQuantity,
        double? decrementAmount,
        double? remainingDecrementAmount,
        string reason,
        string organizationUnitCode,
        string organizationUnitName,
        int state,
        int? editVersion)
    {
            var detail = new SUDecrementDetail(
                supplyId,
                supplyCode,
                supplyName,
                organizationUnitId,
                sUAllocationId,
                sUAuditRefId,
                sortOrder,
                useQuantity,
                decrementQuantity,
                decrementAmount,
                remainingDecrementAmount,
                reason,
                organizationUnitCode,
                organizationUnitName,
                state,
                editVersion);

    _sUDecrementDetails.Add(detail);

            return detail;
    }
    public void ClearDetailData()
    {
        _sUDecrementDetails.Clear();
    }

    private void AddSUDecrementCreatedDomainEvent()
    {
        var @event = new SUDecrementCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddSUDecrementUpdatedDomainEvent()
    {
        var @event = new SUDecrementUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
