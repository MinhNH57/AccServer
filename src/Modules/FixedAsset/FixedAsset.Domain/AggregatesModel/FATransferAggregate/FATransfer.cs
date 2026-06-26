namespace FixedAsset.Domain.AggregatesModel.FATransferAggregate;

public class FATransfer :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; private set; }
    public Guid? BranchId { get; private set; }
    public int RefType { get; private set; }
    public int DisplayOnBook { get; private set; }
    public int RefOrder { get; private set; }
    public DateTime? RefDate { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsPostedFinance { get; private set; }
    public bool IsPostedManagement { get; private set; }
    public string RefNo { get; private set; }
    public string HandOverName { get; private set; }
    public string RecipientName { get; private set; }
    public string JournalMemo { get; private set; }
    public string CreatedBy { get; private set; }
    public string ModifiedBy { get; private set; }
    public int State { get; private set; }
    public int EditVersion { get; private set; }
    public string AttachmentIdList { get; private set; }
    public string BranchName { get; private set; }
    public bool AutoRefNo { get; private set; }
    public bool ForceUpdate { get; private set; }

    private readonly List<FATransferDetail> _fATransferDetails;

    public IReadOnlyCollection<FATransferDetail> FATransferDetails => _fATransferDetails.AsReadOnly();

    protected FATransfer()
    {
        _fATransferDetails = [];
    }

    public FATransfer(
        Guid? tenantId,
        Guid? branchId,
        int refType,
        int displayOnBook,
        int refOrder,
        DateTime? refDate,
        DateTime? postedDate,
        string refNo,
        string handOverName,
        string recipientName,
        string journalMemo,
        string createdBy,
        int state,
        int editVersion,
        string attachmentIdList,
        string branchName,
        bool autoRefNo,
        bool forceUpdate) : this()
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        BranchId = branchId;
        RefType = refType;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        CreatedDate = DateTime.Now;
        PostedDate = postedDate;
        ModifiedDate = DateTime.Now;
        RefNo = refNo;
        HandOverName = handOverName;
        RecipientName = recipientName;
        JournalMemo = journalMemo;
        CreatedBy = createdBy;
        ModifiedBy = createdBy;
        State = state;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;
        BranchName = branchName;
        AutoRefNo = autoRefNo;
        ForceUpdate = forceUpdate;

        AddCreatedDomainEvent();
    }

    public FATransfer Update(
        Guid? tenantId,
        Guid? branchId,
        int displayOnBook,
        int refOrder,
        DateTime? refDate,
        DateTime? postedDate,
        string refNo,
        string handOverName,
        string recipientName,
        string journalMemo,
        string modifiedBy,
        int state,
        int editVersion,
        string attachmentIdList,
        string branchName,
        bool autoRefNo,
        bool forceUpdate)
    {
        TenantId = tenantId;
        BranchId = branchId;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        PostedDate = postedDate;
        ModifiedDate = DateTime.Now;
        RefNo = refNo;
        HandOverName = handOverName;
        RecipientName = recipientName;
        JournalMemo = journalMemo;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;
        BranchName = branchName;
        AutoRefNo = autoRefNo;
        ForceUpdate = forceUpdate;

        AddUpdatedDomainEvent();

        return this;
    }

    public FATransferDetail AddFATransferDetail(
        Guid? fixedAssetId,
        string fixedAssetName,
        Guid? fromOrganizationUnitId,
        Guid? toOrganizationUnitId,
        Guid? listItemId,
        Guid? contractId,
        Guid? orderId,
        Guid? projectWorkId,
        Guid? expenseItemId,
        Guid? jobId,
        int sortOrder,
        string costAccount,
        string contractCode,
        string expenseItemCode,
        string jobCode,
        string listItemCode,
        string orderCode,
        string projectWorkCode,
        string expenseItemName,
        string jobName,
        string listItemName,
        string projectWorkName,
        string fromOrganizationUnitCode,
        string toOrganizationUnitCode,
        string fromOrganizationUnitName,
        string toOrganizationUnitName,
        string fixedAssetCode,
        int state,
        int editVersion)
    {
        var existingFATransferDetail = _fATransferDetails.SingleOrDefault(f => f.FixedAssetId == fixedAssetId || f.FixedAssetCode == fixedAssetCode);
        if (existingFATransferDetail != null)
        {
            throw new FixedAssetDomainException($"TSCĐ {existingFATransferDetail.FixedAssetCode} đã được chọn trong chứng từ này.");
        }
        else
        {
            var fATransferDetail = new FATransferDetail(
                fixedAssetId,
                fixedAssetName,
                fromOrganizationUnitId,
                toOrganizationUnitId,
                listItemId,
                contractId,
                orderId,
                projectWorkId,
                expenseItemId,
                jobId,
                sortOrder,
                costAccount,
                contractCode,
                expenseItemCode,
                jobCode,
                listItemCode,
                orderCode,
                projectWorkCode,
                expenseItemName,
                jobName,
                listItemName,
                projectWorkName,
                fromOrganizationUnitCode,
                toOrganizationUnitCode,
                fromOrganizationUnitName,
                toOrganizationUnitName,
                fixedAssetCode,
                state,
                editVersion);

            _fATransferDetails.Add(fATransferDetail);

            return fATransferDetail;
        }
    }

    public void ClearDetailData()
    {
        _fATransferDetails.Clear();
    }

    private void AddCreatedDomainEvent()
    {
        var @event = new FATransferCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddUpdatedDomainEvent()
    {
        var @event = new FATransferUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
