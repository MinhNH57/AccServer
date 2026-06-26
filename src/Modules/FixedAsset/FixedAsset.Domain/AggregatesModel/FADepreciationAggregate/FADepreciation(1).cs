namespace FixedAsset.Domain.AggregatesModel.FADepreciationAggregate;

public class FADepreciation :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; private set; }
    public Guid? BranchId { get; private set; }
    public int RefType { get; private set; }
    public int Month { get; private set; }
    public int? Year { get; private set; }
    public int DisplayOnBook { get; private set; }
    public int RefOrder { get; private set; }
    public DateTime? RefDate { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsPostedFinance { get; private set; }
    public bool IsPostedManagement { get; private set; }
    public double? TotalAmount { get; private set; }
    public string RefNo { get; private set; }
    public string JournalMemo { get; private set; }
    public string CreatedBy { get; private set; }
    public string ModifiedBy { get; private set; }
    public int State { get; private set; }
    public int EditVersion { get; private set; }
    public string AttachmentIdList { get; private set; }
    public string BranchName { get; private set; }

    private readonly List<FADepreciationDetail> _fADepreciationDetails;

    public IReadOnlyCollection<FADepreciationDetail> FADepreciationDetails => _fADepreciationDetails.AsReadOnly();

    private readonly List<FADepreciationDetailAllocation> _fADepreciationDetailAllocations;

    public IReadOnlyCollection<FADepreciationDetailAllocation> FADepreciationDetailAllocations => _fADepreciationDetailAllocations.AsReadOnly();

    private readonly List<FADepreciationDetailPost> _fADepreciationDetailPosts;

    public IReadOnlyCollection<FADepreciationDetailPost> FADepreciationDetailPosts => _fADepreciationDetailPosts.AsReadOnly();

    protected FADepreciation()
    {
        _fADepreciationDetails = [];
        _fADepreciationDetailAllocations = [];
        _fADepreciationDetailPosts = [];
    }

    public FADepreciation(
        Guid? tenantId,
        Guid? branchId,
        int refType,
        int month,
        int? year,
        int displayOnBook,
        int refOrder,
        DateTime? refDate,
        DateTime? postedDate,
        DateTime? createdDate,
        DateTime? modifiedDate,
        double? totalAmount,
        string refNo,
        string journalMemo,
        string createdBy,
        string modifiedBy,
        int state,
        int editVersion,
        string attachmentIdList,
        string branchName) : this()
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        BranchId = branchId;
        RefType = refType;
        Month = month;
        Year = year;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        PostedDate = postedDate;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        TotalAmount = totalAmount;
        RefNo = refNo;
        JournalMemo = journalMemo;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;
        BranchName = branchName;

        AddCreatedDomainEvent();
    }

    public FADepreciation Update(
        Guid? tenantId,
        Guid? branchId,
        int month,
        int? year,
        int displayOnBook,
        int refOrder,
        DateTime? refDate,
        DateTime? postedDate,
        DateTime? modifiedDate,
        double? totalAmount,
        string refNo,
        string journalMemo,
        string modifiedBy,
        int state,
        int editVersion,
        string attachmentIdList,
        string branchName)
    {
        TenantId = tenantId;
        BranchId = branchId;
        Month = month;
        Year = year;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        PostedDate = postedDate;
        ModifiedDate = modifiedDate;
        TotalAmount = totalAmount;
        RefNo = refNo;
        JournalMemo = journalMemo;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;
        BranchName = branchName;

        AddUpdatedDomainEvent();

        return this;
    }

    public FADepreciationDetail AddFADepreciationDetail(
        Guid? fixedAssetId,
        Guid? organizationUnitId,
        int sortOrder,
        double monthlyDepreciationAmount,
        double amountReasonableCost,
        double amountUnReasonableCost,
        string organizationUnitCode,
        string organizationUnitName,
        string fixedAssetCode,
        string fixedAssetName,
        string fixedAssetCategoryName,
        Guid fixedAssetCategoryId,
        string fixedAssetCategoryCode,
        int state,
        int editVersion)
    {
        var existingFADepreciationDetail = _fADepreciationDetails.SingleOrDefault(f => f.FixedAssetId == fixedAssetId || f.FixedAssetCode == fixedAssetCode);
        if (existingFADepreciationDetail != null)
        {
            throw new FixedAssetDomainException($"TSCĐ {existingFADepreciationDetail.FixedAssetCode} đã được chọn trong chứng từ này.");
        }
        else
        {
            var fADepreciationDetail = new FADepreciationDetail(
                fixedAssetId,
                organizationUnitId,
                sortOrder,
                monthlyDepreciationAmount,
                amountReasonableCost,
                amountUnReasonableCost,
                organizationUnitCode,
                organizationUnitName,
                fixedAssetCode,
                fixedAssetName,
                fixedAssetCategoryName,
                fixedAssetCategoryId,
                fixedAssetCategoryCode,
                state,
                editVersion);

            _fADepreciationDetails.Add(fADepreciationDetail);

            return fADepreciationDetail;
        }
    }

    public FADepreciationDetailAllocation AddFADepreciationDetailAllocation(
        Guid? tenantId,
        Guid? fixedAssetId,
        Guid? organizationUnitId,
        Guid? allocationObjectId,
        Guid? expenseItemId,
        Guid? listItemId,
        int sortOrder,
        double? monthlyDepreciationAmount,
        double? allocationRate,
        double? allocationAmount,
        string costAccount,
        string organizationUnitName,
        string allocationObjectCode,
        string allocationObjectName,
        int? allocationObjectType,
        int state,
        int editVersion,
        string fixedAssetCode,
        string fixedAssetName,
        string listItemCode,
        string listItemName,
        string expenseItemCode,
        string expenseItemName,
        string depreciationAccount,
        string organizationUnitCode)
    {
        var fADepreciationDetail = new FADepreciationDetailAllocation(
            tenantId,
            fixedAssetId,
            organizationUnitId,
            allocationObjectId,
            expenseItemId,
            listItemId,
            sortOrder,
            monthlyDepreciationAmount,
            allocationRate,
            allocationAmount,
            costAccount,
            organizationUnitName,
            allocationObjectCode,
            allocationObjectName,
            allocationObjectType,
            state,
            editVersion,
            fixedAssetCode,
            fixedAssetName,
            listItemCode,
            listItemName,
            expenseItemCode,
            expenseItemName,
            depreciationAccount,
            organizationUnitCode);

        _fADepreciationDetailAllocations.Add(fADepreciationDetail);

        return fADepreciationDetail;
    }

    public FADepreciationDetailPost AddFADepreciationDetailPost(
        Guid? tenantId,
        Guid? fixedAssetId,
        Guid? debitAccountObjectId,
        Guid? creditAccountObjectId,
        Guid? expenseItemId,
        Guid? organizationUnitId,
        Guid? jobId,
        Guid? projectWorkId,
        Guid? orderId,
        Guid? contractId,
        Guid? listItemId,
        int sortOrder,
        bool unReasonableCost,
        double amount,
        string description,
        string debitAccount,
        string creditAccount,
        string organizationUnitName,
        string listItemCode,
        string expenseItemCode,
        string organizationUnitCode,
        string jobCode,
        string jobName,
        string expenseItemName,
        string listItemName,
        string projectWorkCode,
        string creditAccountObjectCode,
        string debitAccountObjectCode,
        string creditAccountObjectName,
        string debitAccountObjectName,
        string orderCode,
        string contractCode,
        string contractSubject,
        string projectWorkName,
        string accountName,
        DateTime? contractSignDate,
        int state,
        int editVersion,
        int? allocationObjectType)
    {

        var fADepreciationDetailPost = new FADepreciationDetailPost(
            tenantId,
            fixedAssetId,
            debitAccountObjectId,
            creditAccountObjectId,
            expenseItemId,
            organizationUnitId,
            jobId,
            projectWorkId,
            orderId,
            contractId,
            listItemId,
            sortOrder,
            unReasonableCost,
            amount,
            description,
            debitAccount,
            creditAccount,
            organizationUnitName,
            listItemCode,
            expenseItemCode,
            organizationUnitCode,
            jobCode,
            jobName,
            expenseItemName,
            listItemName,
            projectWorkCode,
            creditAccountObjectCode,
            debitAccountObjectCode,
            creditAccountObjectName,
            debitAccountObjectName,
            orderCode,
            contractCode,
            contractSubject,
            projectWorkName,
            accountName,
            contractSignDate,
            state,
            editVersion,
            allocationObjectType);

        _fADepreciationDetailPosts.Add(fADepreciationDetailPost);

        return fADepreciationDetailPost;
    }

    public void ClearDetailData()
    {
        _fADepreciationDetails.Clear();
        _fADepreciationDetailAllocations.Clear();
        _fADepreciationDetailPosts.Clear();
    }

    private void AddCreatedDomainEvent()
    {
        var @event = new FADepreciationCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddUpdatedDomainEvent()
    {
        var @event = new FADepreciationUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
