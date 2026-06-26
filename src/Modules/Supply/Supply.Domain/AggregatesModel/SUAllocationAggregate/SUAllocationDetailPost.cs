namespace Supply.Domain.AggregatesModel.SUAllocationAggregate;

public class SUAllocationDetailPost
    : Entity
{
    public Guid? TenantId { get; private set; }
    public string Description { get; private set; }
    public string DebitAccount { get; private set; }
    public string CreditAccount { get; private set; }
    public double? Amount { get; private set; }
    public string ListItemCode { get; private set; }
    public Guid? DebitAccountObjectId { get; private set; }
    public string DebitAccountObjectName { get; private set; }
    public Guid? CreditAccountObjectId { get; private set; }
    public string CreditAccountObjectName { get; private set; }
    public string DebitAccountObjectCode { get; private set; }
    public string CreditAccountObjectCode { get; private set; }
    public Guid? OrganizationUnitId { get; private set; }
    public Guid? JobId { get; private set; }
    public Guid? ProjectWorkId { get; private set; }
    public string ProjectWorkCode { get; private set; }
    public string ProjectWorkName { get; private set; }
    public Guid? OrderId { get; private set; }
    public string OrderCode { get; private set; }
    public Guid? ContractId { get; private set; }
    public string ContractCode { get; private set; }
    public Guid? ListItemId { get; private set; }
    public Guid? ExpenseItemId { get; private set; }
    public int SortOrder { get; private set; }
    public bool UnReasonableCost { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }
    public string ExpenseItemCode { get; private set; }
    public string OrganizationUnitName { get; private set; }
    public string JobName { get; private set; }
    public string ExpenseItemName { get; private set; }
    public string ListItemName { get; private set; }
    public string OrganizationUnitCode { get; private set; }
    public string JobCode { get; private set; }
    public string ContractSubject { get; private set; }
    public Guid? AllocationObjectId { get; private set; }
    public string AllocationObjectCode { get; private set; }
    public string AllocationObjectName { get; private set; }
    public int? AllocationObjectType { get; private set; }

    protected SUAllocationDetailPost() { }

    public SUAllocationDetailPost(
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
        int? allocationObjectType) : this()
    {
        TenantId = tenantId;
        Description = description;
        DebitAccount = debitAccount;
        CreditAccount = creditAccount;
        Amount = amount;
        ListItemCode = listItemCode;
        DebitAccountObjectId = debitAccountObjectId;
        DebitAccountObjectName = debitAccountObjectName;
        CreditAccountObjectId = creditAccountObjectId;
        CreditAccountObjectName = creditAccountObjectName;
        DebitAccountObjectCode = debitAccountObjectCode;
        CreditAccountObjectCode = creditAccountObjectCode;
        OrganizationUnitId = organizationUnitId;
        JobId = jobId;
        ProjectWorkId = projectWorkId;
        ProjectWorkCode = projectWorkCode;
        ProjectWorkName = projectWorkName;
        OrderId = orderId;
        OrderCode = orderCode;
        ContractId = contractId;
        ContractCode = contractCode;
        ListItemId = listItemId;
        ExpenseItemId = expenseItemId;
        SortOrder = sortOrder;
        UnReasonableCost = unReasonableCost;
        State = state;
        EditVersion = editVersion;
        ExpenseItemCode = expenseItemCode;
        OrganizationUnitName = organizationUnitName;
        JobName = jobName;
        ExpenseItemName = expenseItemName;
        ListItemName = listItemName;
        OrganizationUnitCode = organizationUnitCode;
        JobCode = jobCode;
        ContractSubject = contractSubject;
        AllocationObjectId = allocationObjectId;
        AllocationObjectCode = allocationObjectCode;
        AllocationObjectName = allocationObjectName;
        AllocationObjectType = allocationObjectType;
    }
}
