namespace FixedAsset.Domain.AggregatesModel.FADecrementAggregate;

public class FADecrement :
    Entity, IAggregateRoot
{
    public Guid? BranchId { get; private set; }
    public int RefType { get; private set; }
    public int DisplayOnBook { get; private set; }
    public int RefOrder { get; private set; }
    public DateTime RefDate { get; private set; }
    public DateTime PostedDate { get; private set; }
    public bool IsPostedFinance { get; private set; }
    public bool IsPostedManagement { get; private set; }
    public double TotalAmount { get; private set; }
    public string RefNo { get; private set; }
    public string JournalMemo { get; private set; }
    public string BranchName { get; private set; }
    public string AttachmentIdList { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime ModifiedDate { get; private set; }
    public string ModifiedBy { get; private set; }
    public bool AutoRefNo { get; private set; }
    public bool ForceUpdate { get; private set; }
    public int EditVersion { get; private set; }
    public int State { get; private set; }

    private readonly List<FADecrementDetail> _fADecrementDetails;

    public IReadOnlyCollection<FADecrementDetail> FADecrementDetails => _fADecrementDetails.AsReadOnly();

    private readonly List<FADecrementDetailPost> _fADecrementDetailPosts;

    public IReadOnlyCollection<FADecrementDetailPost> FADecrementDetailPosts => _fADecrementDetailPosts.AsReadOnly();

    protected FADecrement()
    {
        _fADecrementDetails = [];
        _fADecrementDetailPosts = [];
    }

    public FADecrement(
        Guid? branchId,
        int refType,
        int displayOnBook,
        int refOrder,
        DateTime refDate,
        DateTime postedDate,
        double totalAmount,
        string refNo,
        string journalMemo,
        string branchName,
        string attachmentIdList,
        string createdBy,
        bool autoRefNo,
        bool forceUpdate,
        int editVersion,
        int state) : this()
    {
        Id = Guid.NewGuid();
        BranchId = branchId;
        RefType = refType;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        PostedDate = postedDate;
        TotalAmount = totalAmount;
        RefNo = refNo;
        JournalMemo = journalMemo;
        BranchName = branchName;
        AttachmentIdList = attachmentIdList;
        CreatedDate = DateTime.Now;
        CreatedBy = createdBy;
        ModifiedDate = DateTime.Now;
        ModifiedBy = createdBy;
        AutoRefNo = autoRefNo;
        ForceUpdate = forceUpdate;
        EditVersion = editVersion;
        State = state;

        AddCreatedDomainEvent();
    }

    public FADecrement Update(
        Guid? branchId,
        int displayOnBook,
        int refOrder,
        DateTime refDate,
        DateTime postedDate,
        double totalAmount,
        string refNo,
        string journalMemo,
        string branchName,
        string attachmentIdList,
        string modifiedBy,
        bool autoRefNo,
        bool forceUpdate,
        int editVersion,
        int state)
    {
        BranchId = branchId;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        PostedDate = postedDate;
        TotalAmount = totalAmount;
        RefNo = refNo;
        JournalMemo = journalMemo;
        BranchName = branchName;
        AttachmentIdList = attachmentIdList;
        ModifiedDate = DateTime.Now;
        ModifiedBy = modifiedBy;
        AutoRefNo = autoRefNo;
        ForceUpdate = forceUpdate;
        EditVersion = editVersion;
        State = state;

        AddUpdatedDomainEvent();

        return this;
    }

    public FADecrementDetail AddFADecrementDetail(
        Guid? fixedAssetId,
        string fixedAssetCode,
        string fixedAssetName,
        Guid? organizationUnitId,
        int sortOrder,
        double orgPrice,
        double depreciationAmount,
        double accumDepreciationAmount,
        double remainingAmount,
        double depreciationAmountInMonth,
        string orgPriceAccount,
        string depreciationAccount,
        string remainingAccount,
        string organizationUnitCode,
        string organizationUnitName,
        int editVersion,
        int state)
    {
        var existingFADecrementDetail = _fADecrementDetails.SingleOrDefault(f => f.FixedAssetId == fixedAssetId || f.FixedAssetCode == fixedAssetCode);
        if (existingFADecrementDetail != null)
        {
            throw new FixedAssetDomainException($"TSCĐ {existingFADecrementDetail.FixedAssetCode} đã được chọn trong chứng từ này.");
        }
        else
        {
            var fADecrementDetail = new FADecrementDetail(
                fixedAssetId,
                fixedAssetCode,
                fixedAssetName,
                organizationUnitId,
                sortOrder,
                orgPrice,
                depreciationAmount,
                accumDepreciationAmount,
                remainingAmount,
                depreciationAmountInMonth,
                orgPriceAccount,
                depreciationAccount,
                remainingAccount,
                organizationUnitCode,
                organizationUnitName,
                editVersion,
                state);

            _fADecrementDetails.Add(fADecrementDetail);

            return fADecrementDetail;
        }
    }

    public FADecrementDetail UpdateFADecrementDetail(
        Guid refDetailId,
        Guid? fixedAssetId,
        string fixedAssetCode,
        string fixedAssetName,
        Guid? organizationUnitId,
        int sortOrder,
        double orgPrice,
        double depreciationAmount,
        double accumDepreciationAmount,
        double remainingAmount,
        double depreciationAmountInMonth,
        string orgPriceAccount,
        string depreciationAccount,
        string remainingAccount,
        string organizationUnitCode,
        string organizationUnitName,
        int editVersion,
        int state)
    {
        var fADecrementDetail = _fADecrementDetails.FirstOrDefault(f => f.Id == refDetailId);
        if (fADecrementDetail == null)
        {
            throw new FixedAssetDomainException($"Không tìm thấy tài sản với Id = '{refDetailId}'.");
        }

        var existingFADecrementDetail = _fADecrementDetails.SingleOrDefault(f => f.FixedAssetId != fixedAssetId && f.FixedAssetCode == fixedAssetCode);
        if (existingFADecrementDetail != null)
        {
            throw new FixedAssetDomainException($"TSCĐ {existingFADecrementDetail.FixedAssetCode} đã được chọn trong chứng từ này.");
        }

        return fADecrementDetail.Update(
            fixedAssetId,
            fixedAssetCode,
            fixedAssetName,
            organizationUnitId,
            sortOrder,
            orgPrice,
            depreciationAmount,
            accumDepreciationAmount,
            remainingAmount,
            depreciationAmountInMonth,
            orgPriceAccount,
            depreciationAccount,
            remainingAccount,
            organizationUnitCode,
            organizationUnitName,
            editVersion,
            state);
    }

    public FADecrementDetailPost AddFADecrementDetailPost(
        Guid? expenseItemId,
        string expenseItemCode,
        Guid? organizationUnitId,
        string organizationUnitCode,
        Guid? jobId,
        string jobCode,
        Guid? projectWorkId,
        string projectWorkCode,
        Guid? orderId,
        string orderCode,
        Guid? contractId,
        string contractCode,
        Guid? listItemId,
        string listItemCode,
        string organizationUnitName,
        string jobName,
        string projectWorkName,
        string expenseItemName,
        string listItemName,
        Guid? accountObjectId,
        string accountObjectCode,
        string accountObjectName,
        int sortOrder,
        bool unReasonableCost,
        double amount,
        string description,
        string debitAccount,
        string creditAccount,
        int editVersion,
        int state)
    {
        var fADecrementDetailPost = new FADecrementDetailPost(
            expenseItemId,
            expenseItemCode,
            organizationUnitId,
            organizationUnitCode,
            jobId,
            jobCode,
            projectWorkId,
            projectWorkCode,
            orderId,
            orderCode,
            contractId,
            contractCode,
            listItemId,
            listItemCode,
            organizationUnitName,
            jobName,
            projectWorkName,
            expenseItemName,
            listItemName,
            accountObjectId,
            accountObjectCode,
            accountObjectName,
            sortOrder,
            unReasonableCost,
            amount,
            description,
            debitAccount,
            creditAccount,
            editVersion,
            state);

        _fADecrementDetailPosts.Add(fADecrementDetailPost);

        return fADecrementDetailPost;
    }

    public FADecrementDetailPost UpdateFADecrementDetailPost(
        Guid refDetailId,
        Guid? expenseItemId,
        string expenseItemCode,
        Guid? organizationUnitId,
        string organizationUnitCode,
        Guid? jobId,
        string jobCode,
        Guid? projectWorkId,
        string projectWorkCode,
        Guid? orderId,
        string orderCode,
        Guid? contractId,
        string contractCode,
        Guid? listItemId,
        string listItemCode,
        string organizationUnitName,
        string jobName,
        string projectWorkName,
        string expenseItemName,
        string listItemName,
        Guid? accountObjectId,
        string accountObjectCode,
        string accountObjectName,
        int sortOrder,
        bool unReasonableCost,
        double amount,
        string description,
        string debitAccount,
        string creditAccount,
        int editVersion,
        int state)
    {
        var fADecrementDetailPost = _fADecrementDetailPosts.FirstOrDefault(f => f.Id == refDetailId);
        if (fADecrementDetailPost == null)
        {
            throw new FixedAssetDomainException($"Không tìm thấy hạch toán với Id = '{refDetailId}'.");
        }

        return fADecrementDetailPost.Update(
            expenseItemId,
            expenseItemCode,
            organizationUnitId,
            organizationUnitCode,
            jobId,
            jobCode,
            projectWorkId,
            projectWorkCode,
            orderId,
            orderCode,
            contractId,
            contractCode,
            listItemId,
            listItemCode,
            organizationUnitName,
            jobName,
            projectWorkName,
            expenseItemName,
            listItemName,
            accountObjectId,
            accountObjectCode,
            accountObjectName,
            sortOrder,
            unReasonableCost,
            amount,
            description,
            debitAccount,
            creditAccount,
            editVersion,
            state);
    }

    public void ClearDetailData()
    {
        _fADecrementDetails.Clear();
        _fADecrementDetailPosts.Clear();
    }

    private void AddCreatedDomainEvent()
    {
        var @event = new FADecrementCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddUpdatedDomainEvent()
    {
        var @event = new FADecrementUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
