namespace Supply.Domain.AggregatesModel.SUAdjustmentAggregate;

public class SUAdjustment :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; private set; }
    public Guid? BranchId { get; private set; }
    public int RefType { get; private set; }
    public int? DisplayOnBook { get; private set; }
    public int? RefOrder { get; private set; }
    public string JournalMemo { get; private set; }
    public DateTime? RefDate { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsPostedFinance { get; private set; }
    public bool IsPostedManagement { get; private set; }
    public string RefNo { get; private set; }
    public string CreatedBy { get; private set; }
    public string ModifiedBy { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }
    public double? TotalAmount { get; private set; }
    public string BranchName { get; private set; }
    public string AttachmentIdList { get; private set; }

    private readonly List<SUAdjustmentDetail> _sUAdjustmentDetails;

    public IReadOnlyCollection<SUAdjustmentDetail> SUAdjustmentDetails => _sUAdjustmentDetails.AsReadOnly();

    private readonly List<SUAdjustmentDetailVoucher> _sUAdjustmentDetailVouchers;

    public IReadOnlyCollection<SUAdjustmentDetailVoucher> SUAdjustmentDetailVouchers => _sUAdjustmentDetailVouchers.AsReadOnly();

    protected SUAdjustment()
    {
        _sUAdjustmentDetails = [];
        _sUAdjustmentDetailVouchers = [];
    }

    public SUAdjustment(
        Guid? tenantId,
        Guid? branchId,
        int refType,
        int? displayOnBook,
        int? refOrder,
        string journalMemo,
        DateTime? refDate,
        DateTime? createdDate,
        DateTime? modifiedDate,
        bool isPostedFinance,
        bool isPostedManagement,
        string refNo,
        string createdBy,
        string modifiedBy,
        int state,
        int? editVersion,
        double? totalAmount,
        string branchName,
        string attachmentIdList) : this()
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        BranchId = branchId;
        RefType = refType;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        JournalMemo = journalMemo;
        RefDate = refDate;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        IsPostedFinance = isPostedFinance;
        IsPostedManagement = isPostedManagement;
        RefNo = refNo;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        TotalAmount = totalAmount;
        BranchName = branchName;
        AttachmentIdList = attachmentIdList;

        AddSUAdjustmentCreatedDomainEvent();
    }

    public SUAdjustment Update(
        Guid? tenantId,
        Guid? branchId,
        int? displayOnBook,
        int? refOrder,
        string journalMemo,
        DateTime? refDate,
        DateTime? modifiedDate,
        bool isPostedFinance,
        bool isPostedManagement,
        string refNo,
        string modifiedBy,
        int state,
        int? editVersion,
        double? totalAmount,
        string branchName,
        string attachmentIdList)
    {
        TenantId = tenantId;
        BranchId = branchId;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        JournalMemo = journalMemo;
        RefDate = refDate;
        ModifiedDate = modifiedDate;
        IsPostedFinance = isPostedFinance;
        IsPostedManagement = isPostedManagement;
        RefNo = refNo;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        TotalAmount = totalAmount;
        BranchName = branchName;
        AttachmentIdList = attachmentIdList;

        AddSUAdjustmentUpdatedDomainEvent();

        return this;
    }

    public SUAdjustmentDetail AddSUAdjustmentDetail(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        double? quantity,
        string allocationAccount,
        double? currentRemainingAmount,
        double? newRemainingAmount,
        double? diffRemainingAmount,
        double? currentRemainingAllocationTime,
        double? newRemainingAllocationTime,
        double? diffAllocationTime,
        double? termlyAllocationAmount,
        string note,
        int sortOrder,
        int state,
        int? editVersion)
    {
        var detail = new SUAdjustmentDetail(
            supplyId,
            supplyCode,
            supplyName,
            quantity,
            allocationAccount,
            currentRemainingAmount,
            newRemainingAmount,
            diffRemainingAmount,
            currentRemainingAllocationTime,
            newRemainingAllocationTime,
            diffAllocationTime,
            termlyAllocationAmount,
            note,
            sortOrder,
            state,
            editVersion);

        _sUAdjustmentDetails.Add(detail);

        return detail;
    }

    public SUAdjustmentDetailVoucher AddSUAdjustmentDetailVoucher(
        Guid? tenantId,
        Guid? voucherRefId,
        Guid voucherRefDetailId,
        string creditAccount,
        string debitAccount,
        int? voucherRefType,
        int sortOrder,
        string refNo,
        double? amount,
        DateTime? refDate,
        string refTypeName,
        string description,
        int state,
        int? editVersion,
        int? detailPostOrder)
    {
        var voucher = new SUAdjustmentDetailVoucher(
            tenantId,
            voucherRefId,
            voucherRefDetailId,
            creditAccount,
            debitAccount,
            voucherRefType,
            sortOrder,
            refNo,
            amount,
            refDate,
            refTypeName,
            description,
            state,
            editVersion,
            detailPostOrder);

        _sUAdjustmentDetailVouchers.Add(voucher);

        return voucher;
    }

    public void ClearDetailData()
    {
        _sUAdjustmentDetails.Clear();
        _sUAdjustmentDetailVouchers.Clear();
    }

    private void AddSUAdjustmentCreatedDomainEvent()
    {
        var @event = new SUAdjustmentCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddSUAdjustmentUpdatedDomainEvent()
    {
        var @event = new SUAdjustmentUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
