namespace Supply.Domain.AggregatesModel.SUTransferAggregate;

public class SUTransfer :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; private set; }
    public Guid? BranchId { get; private set; }
    public int RefType { get; private set; }
    public int? DisplayOnBook { get; private set; }
    public int? RefOrder { get; private set; }
    public DateTime? RefDate { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsPostedManagement { get; private set; }
    public bool IsPostedFinance { get; private set; }
    public double? TotalQuantity { get; private set; }
    public string RefNo { get; private set; }
    public string DeliveryName { get; private set; }
    public string ReceiptName { get; private set; }
    public string JournalMemo { get; private set; }
    public string CreatedBy { get; private set; }
    public string ModifiedBy { get; private set; }
    public int State { get; private set; } = 0;
    public string BranchName { get; private set; }
    public int? EditVersion { get; private set; }
    public string AttachmentIdList { get; private set; }

    private readonly List<SUTransferDetail> _sUTransferDetails;

    public IReadOnlyCollection<SUTransferDetail> SUTransferDetails => _sUTransferDetails.AsReadOnly();

    protected SUTransfer()
    {
        _sUTransferDetails = [];
    }

    public SUTransfer(
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
        double? totalQuantity,
        string refNo,
        string deliveryName,
        string receiptName,
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
        TotalQuantity = totalQuantity;
        RefNo = refNo;
        DeliveryName = deliveryName;
        ReceiptName = receiptName;
        JournalMemo = journalMemo;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        State = state;
        BranchName = branchName;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;

        AddSUTransferCreatedDomainEvent();
    }

    public SUTransfer Update(
        Guid? tenantId,
        Guid? branchId,
        int? displayOnBook,
        int? refOrder,
        DateTime? refDate,
        DateTime? modifiedDate,
        bool isPostedManagement,
        bool isPostedFinance,
        double? totalQuantity,
        string refNo,
        string deliveryName,
        string receiptName,
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
        TotalQuantity = totalQuantity;
        RefNo = refNo;
        DeliveryName = deliveryName;
        ReceiptName = receiptName;
        JournalMemo = journalMemo;
        ModifiedBy = modifiedBy;
        State = state;
        BranchName = branchName;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;

        AddSUTransferUpdatedDomainEvent();

        return this;
    }

    public SUTransferDetail AddSUTransferDetail(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        Guid? fromOrganizationUnitId,
        string fromOrganizationUnitCode,
        string fromOrganizationUnitName,
        Guid? toOrganizationUnitId,
        string toOrganizationUnitCode,
        string toOrganizationUnitName,
        Guid? listItemId,
        string listItemCode,
        string listItemName,
        Guid? contractId,
        string contractCode,
        Guid? orderId,
        string orderCode,
        Guid? projectWorkId,
        string projectWorkCode,
        string projectWorkName,
        Guid? expenseItemId,
        string expenseItemCode,
        string expenseItemName,
        Guid? jobId,
        string jobCode,
        string jobName,
        int sortOrder,
        double? useQuantity,
        double? transferQuantity,
        string costAccount)
    {

        var detail = new SUTransferDetail(
            supplyId,
            supplyCode,
            supplyName,
            fromOrganizationUnitId,
            fromOrganizationUnitCode,
            fromOrganizationUnitName,
            toOrganizationUnitId,
            toOrganizationUnitCode,
            toOrganizationUnitName,
            listItemId,
            listItemCode,
            listItemName,
            contractId,
            contractCode,
            orderId,
            orderCode,
            projectWorkId,
            projectWorkCode,
            projectWorkName,
            expenseItemId,
            expenseItemCode,
            expenseItemName,
            jobId,
            jobCode,
            jobName,
            sortOrder,
            useQuantity,
            transferQuantity,
            costAccount);

        _sUTransferDetails.Add(detail);

        return detail;
    }

    public void ClearDetailData()
    {
        _sUTransferDetails.Clear();
    }

    private void AddSUTransferCreatedDomainEvent()
    {
        var @event = new SUTransferCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddSUTransferUpdatedDomainEvent()
    {
        var @event = new SUTransferUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
