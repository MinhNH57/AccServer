namespace FixedAsset.Domain.AggregatesModel.FAAdjustmentAggregate;

public class FAAdjustment :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; private set; }
    public Guid? BranchId { get; private set; }
    public string BranchName { get; private set; }
    public int RefType { get; private set; }
    public int DisplayOnBook { get; private set; }
    public int RefOrder { get; private set; }
    public DateTime? RefDate { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public DateTime? DecisionDate { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsPostedFinance { get; private set; }
    public bool IsPostedManagement { get; private set; }
    public string RefNo { get; private set; }
    public string DecisionNo { get; private set; }
    public string Reason { get; private set; }
    public string JournalMemo { get; private set; }
    public string CreatedBy { get; private set; }
    public string ModifiedBy { get; private set; }
    public int State { get; private set; }
    public int EditVersion { get; private set; }
    public string Members { get; private set; }
    public double TotalAmount { get; private set; }
    public string AttachmentIdList { get; private set; }

    private readonly List<FAAdjustmentDetail> _fAAdjustmentDetails;

    public IReadOnlyCollection<FAAdjustmentDetail> FAAdjustmentDetails => _fAAdjustmentDetails.AsReadOnly();

    private readonly List<FAAdjustmentDetailPost> _fAAdjustmentDetailPosts;

    public IReadOnlyCollection<FAAdjustmentDetailPost> FAAdjustmentDetailPosts => _fAAdjustmentDetailPosts.AsReadOnly();

    protected FAAdjustment()
    {
        _fAAdjustmentDetails = [];
        _fAAdjustmentDetailPosts = [];
    }

    public FAAdjustment(
        Guid? tenantId,
        Guid? branchId,
        string branchName,
        int refType,
        int displayOnBook,
        int refOrder,
        DateTime? refDate,
        DateTime? postedDate,
        DateTime? decisionDate,
        DateTime? createdDate,
        DateTime? modifiedDate,
        string refNo,
        string decisionNo,
        string reason,
        string journalMemo,
        string createdBy,
        string modifiedBy,
        int state,
        int editVersion,
        string members,
        double totalAmount,
        string attachmentIdList)
        : this()
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        BranchId = branchId;
        BranchName = branchName;
        RefType = refType;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        PostedDate = postedDate;
        DecisionDate = decisionDate;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        RefNo = refNo;
        DecisionNo = decisionNo;
        Reason = reason;
        JournalMemo = journalMemo;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        Members = members;
        TotalAmount = totalAmount;
        AttachmentIdList = attachmentIdList;

        AddCreatedDomainEvent();
    }

    public FAAdjustment Update(
        Guid? tenantId,
        Guid? branchId,
        string branchName,
        int displayOnBook,
        int refOrder,
        DateTime? refDate,
        DateTime? postedDate,
        DateTime? decisionDate,
        DateTime? modifiedDate,
        string refNo,
        string decisionNo,
        string reason,
        string journalMemo,
        string modifiedBy,
        int state,
        int editVersion,
        string members,
        double totalAmount,
        string attachmentIdList)
    {
        TenantId = tenantId;
        BranchId = branchId;
        BranchName = branchName;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        PostedDate = postedDate;
        DecisionDate = decisionDate;
        ModifiedDate = modifiedDate;
        RefNo = refNo;
        DecisionNo = decisionNo;
        Reason = reason;
        JournalMemo = journalMemo;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        Members = members;
        TotalAmount = totalAmount;
        AttachmentIdList = attachmentIdList;

        AddUpdatedDomainEvent();

        return this;
    }

    public FAAdjustmentDetail AddFAAdjustmentDetail(Guid? fixedAssetId, string fixedAssetCode, string fixedAssetName, Guid? organizationUnitId, int sortOrder, double currentRemainingAmount, double newRemainingAmount, double diffRemainingAmount, double currentLifeTime, double newLifeTime, double currentAccumDepreciationAmount, double newMonthlyDepreciationAmountByIncomeTax, double diffLifeTime, double diffMonthlyDepreciationAmount, double newAccumDepreciationAmount, double diffAccumDepreciationAmount, double currentDepreciationAmount, double newDepreciationAmount, double diffDepreciationAmount, double newMonthlyDepreciationAmount, string costAccount, string adjustmentAccount, string organizationUnitCode, string organizationUnitName, int state, int editVersion)
    {
        var fAAdjustmentDetail = new FAAdjustmentDetail(fixedAssetId, fixedAssetCode, fixedAssetName, organizationUnitId, sortOrder, currentRemainingAmount, newRemainingAmount, diffRemainingAmount, currentLifeTime, newLifeTime, currentAccumDepreciationAmount, newMonthlyDepreciationAmountByIncomeTax, diffLifeTime, diffMonthlyDepreciationAmount, newAccumDepreciationAmount, diffAccumDepreciationAmount, currentDepreciationAmount, newDepreciationAmount, diffDepreciationAmount, newMonthlyDepreciationAmount, costAccount, adjustmentAccount, organizationUnitCode, organizationUnitName, state, editVersion);

        _fAAdjustmentDetails.Add(fAAdjustmentDetail);

        return fAAdjustmentDetail;
    }

    public FAAdjustmentDetailPost AddFAAdjustmentDetailPost(Guid? tenantId, Guid? accountObjectId, string accountObjectCode, string accountObjectName, Guid? expenseItemId, string expenseItemCode, Guid? organizationUnitId, string organizationUnitCode, Guid? jobId, string jobCode, Guid? projectWorkId, string projectWorkCode, Guid? orderId, string orderCode, Guid? contractId, string contractCode, Guid? listItemId, string listItemCode, string organizationUnitName, string jobName, string projectWorkName, string expenseItemName, string listItemName, int sortOrder, bool unReasonableCost, double amount, string description, string debitAccount, string creditAccount, int state, int editVersion)
    {
        var fAAdjustmentDetailPost = new FAAdjustmentDetailPost(tenantId, accountObjectId, accountObjectCode, accountObjectName, expenseItemId, expenseItemCode, organizationUnitId, organizationUnitCode, jobId, jobCode, projectWorkId, projectWorkCode, orderId, orderCode, contractId, contractCode, listItemId, listItemCode, organizationUnitName, jobName, projectWorkName, expenseItemName, listItemName, sortOrder, unReasonableCost, amount, description, debitAccount, creditAccount, state, editVersion);

        _fAAdjustmentDetailPosts.Add(fAAdjustmentDetailPost);

        return fAAdjustmentDetailPost;
    }

    public void ClearDetailData()
    {
        _fAAdjustmentDetails.Clear();
        _fAAdjustmentDetailPosts.Clear();
    }

    private void AddCreatedDomainEvent()
    {
        var @event = new FAAdjustmentCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddUpdatedDomainEvent()
    {
        var @event = new FAAdjustmentUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
