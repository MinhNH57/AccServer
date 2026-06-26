namespace Supply.Domain.AggregatesModel.SUAllocationAggregate;

public class SUAllocation :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; private set; }
    public Guid? BranchId { get; private set; }
    public int RefType { get; private set; }
    public int? Month { get; private set; }
    public int? Year { get; private set; }
    public int? DisplayOnBook { get; private set; }
    public int? RefOrder { get; private set; }
    public DateTime? RefDate { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsPostedManagement { get; private set; }
    public bool IsPostedFinance { get; private set; }
    public bool IsGetSupplyAllocated { get; private set; }
    public double? TotalAmount { get; private set; }
    public string RefNo { get; private set; }
    public string JournalMemo { get; private set; }
    public string CreatedBy { get; private set; }
    public string ModifiedBy { get; private set; }
    public int State { get; private set; } = 0;
    public string BranchName { get; private set; }
    public int? EditVersion { get; private set; }
    public string AttachmentIdList { get; private set; }

    private readonly List<SUAllocationDetailExpense> _sUAllocationDetailExpenses;

    public IReadOnlyCollection<SUAllocationDetailExpense> SUAllocationDetailExpenses => _sUAllocationDetailExpenses.AsReadOnly();

    private readonly List<SUAllocationDetailTable> _sUAllocationDetailTables;

    public IReadOnlyCollection<SUAllocationDetailTable> SUAllocationDetailTables => _sUAllocationDetailTables.AsReadOnly();

    private readonly List<SUAllocationDetailPost> _sUAllocationDetailPosts;

    public IReadOnlyCollection<SUAllocationDetailPost> SUAllocationDetailPosts => _sUAllocationDetailPosts.AsReadOnly();

    protected SUAllocation()
    {
        _sUAllocationDetailExpenses = [];
        _sUAllocationDetailTables = [];
        _sUAllocationDetailPosts = [];
    }

    public SUAllocation(
        Guid? tenantId,
        Guid? branchId,
        int refType,
        int? month,
        int? year,
        int? displayOnBook,
        int? refOrder,
        DateTime? refDate,
        DateTime? postedDate,
        DateTime? createdDate,
        DateTime? modifiedDate,
        bool isPostedManagement,
        bool isPostedFinance,
        bool isGetSupplyAllocated,
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
        IsPostedManagement = isPostedManagement;
        IsPostedFinance = isPostedFinance;
        IsGetSupplyAllocated = isGetSupplyAllocated;
        TotalAmount = totalAmount;
        RefNo = refNo;
        JournalMemo = journalMemo;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        State = state;
        BranchName = branchName;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;

        AddCreatedDomainEvent();
    }

    public SUAllocation Update(
        Guid? tenantId,
        Guid? branchId,
        int? month,
        int? year,
        int? displayOnBook,
        int? refOrder,
        DateTime? refDate,
        DateTime? postedDate,
        DateTime? modifiedDate,
        bool isPostedManagement,
        bool isPostedFinance,
        bool isGetSupplyAllocated,
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
        Month = month;
        Year = year;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        PostedDate = postedDate;
        ModifiedDate = modifiedDate;
        IsPostedManagement = isPostedManagement;
        IsPostedFinance = isPostedFinance;
        IsGetSupplyAllocated = isGetSupplyAllocated;
        TotalAmount = totalAmount;
        RefNo = refNo;
        JournalMemo = journalMemo;
        ModifiedBy = modifiedBy;
        State = state;
        BranchName = branchName;
        EditVersion = editVersion;
        AttachmentIdList = attachmentIdList;

        AddUpdatedDomainEvent();

        return this;
    }

    public SUAllocationDetailExpense AddSUAllocationDetailExpense(
        string supplyCode,
        string supplyName,
        string supplyCategoryCode,
        string supplyCategoryName,
        Guid? tenantId,
        Guid? supplyId,
        Guid? supplyCategoryId,
        int sortOrder,
        double? totalAllocationAmount,
        double? allocationAmount,
        double? remainingAmount,
        int state,
        int? editVersion)
    {
        var expense = new SUAllocationDetailExpense(
            supplyCode,
            supplyName,
            supplyCategoryCode,
            supplyCategoryName,
            tenantId,
            supplyId,
            supplyCategoryId,
            sortOrder,
            totalAllocationAmount,
            allocationAmount,
            remainingAmount,
            state,
            editVersion);

        _sUAllocationDetailExpenses.Add(expense);

        return expense;
    }

    public SUAllocationDetailTable AddSUAllocationDetailTable(
        Guid? tenantId,
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        Guid? allocationObjectId,
        double? allocationRate,
        double? allocationAmount,
        int sortOrder,
        string costAccount,
        Guid? expenseItemId,
        double? totalAllocationAmount,
        int state,
        int? editVersion,
        string expenseItemCode,
        string expenseItemName,
        string allocationObjectCode,
        string allocationObjectName,
        string allocationAccount,
        int? allocationObjectType,
        Guid? listItemId,
        string listItemCode,
        string listItemName)
    {
        var table = new SUAllocationDetailTable(
            tenantId,
            supplyId,
            supplyCode,
            supplyName,
            allocationObjectId,
            allocationRate,
            allocationAmount,
            sortOrder,
            costAccount,
            expenseItemId,
            totalAllocationAmount,
            state,
            editVersion,
            expenseItemCode,
            expenseItemName,
            allocationObjectCode,
            allocationObjectName,
            allocationAccount,
            allocationObjectType,
            listItemId,
            listItemCode,
            listItemName);

        _sUAllocationDetailTables.Add(table);

        return table;
    }

    public SUAllocationDetailPost AddSUAllocationDetailPost(
        Guid? tenantId,
        string description,
        string debitAccount,
        string creditAccount,
        double? amount,
        string listItemCode,
        Guid? debitAccountObjectId,
        string debitAccountObjectName,
        Guid? creditAccountObjectId,
        string creditAccountObjectName,
        string debitAccountObjectCode,
        string creditAccountObjectCode,
        Guid? organizationUnitId,
        Guid? jobId,
        Guid? projectWorkId,
        string projectWorkCode,
        string projectWorkName,
        Guid? orderId,
        string orderCode,
        Guid? contractId,
        string contractCode,
        Guid? listItemId,
        Guid? expenseItemId,
        int sortOrder,
        bool unReasonableCost,
        int state,
        int? editVersion,
        string expenseItemCode,
        string organizationUnitName,
        string jobName,
        string expenseItemName,
        string listItemName,
        string organizationUnitCode,
        string jobCode,
        string contractSubject,
        Guid? allocationObjectId,
        string allocationObjectCode,
        string allocationObjectName,
        int? allocationObjectType)
    {

        var post = new SUAllocationDetailPost(
            tenantId,
            description,
            debitAccount,
            creditAccount,
            amount,
            listItemCode,
            debitAccountObjectId,
            debitAccountObjectName,
            creditAccountObjectId,
            creditAccountObjectName,
            debitAccountObjectCode,
            creditAccountObjectCode,
            organizationUnitId,
            jobId,
            projectWorkId,
            projectWorkCode,
            projectWorkName,
            orderId,
            orderCode,
            contractId,
            contractCode,
            listItemId,
            expenseItemId,
            sortOrder,
            unReasonableCost,
            state,
            editVersion,
            expenseItemCode,
            organizationUnitName,
            jobName,
            expenseItemName,
            listItemName,
            organizationUnitCode,
            jobCode,
            contractSubject,
            allocationObjectId,
            allocationObjectCode,
            allocationObjectName,
            allocationObjectType);

        _sUAllocationDetailPosts.Add(post);

        return post;
    }

    public void ClearDetailData()
    {
        _sUAllocationDetailExpenses.Clear();
        _sUAllocationDetailTables.Clear();
        _sUAllocationDetailPosts.Clear();
    }

    private void AddCreatedDomainEvent()
    {
        var @event = new SUAllocationCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddUpdatedDomainEvent()
    {
        var @event = new SUAllocationUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
