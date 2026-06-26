namespace FixedAsset.Domain.AggregatesModel.FADecrementAggregate;

public class FADecrementDetailPost
    : Entity
{
    public Guid? ExpenseItemId { get; private set; }
    public string ExpenseItemCode { get; private set; }
    public Guid? OrganizationUnitId { get; private set; }
    public string OrganizationUnitCode { get; private set; }
    public Guid? JobId { get; private set; }
    public string JobCode { get; private set; }
    public Guid? ProjectWorkId { get; private set; }
    public string ProjectWorkCode { get; private set; }
    public Guid? OrderId { get; private set; }
    public string OrderCode { get; private set; }
    public Guid? ContractId { get; private set; }
    public string ContractCode { get; private set; }
    public Guid? ListItemId { get; private set; }
    public string ListItemCode { get; private set; }
    public string OrganizationUnitName { get; private set; }
    public string JobName { get; private set; }
    public string ProjectWorkName { get; private set; }
    public string ExpenseItemName { get; private set; }
    public string ListItemName { get; private set; }
    public Guid? AccountObjectId { get; private set; }
    public string AccountObjectCode { get; private set; }
    public string AccountObjectName { get; private set; }
    public int SortOrder { get; private set; }
    public bool UnReasonableCost { get; private set; }
    public double Amount { get; private set; }
    public string Description { get; private set; }
    public string DebitAccount { get; private set; }
    public string CreditAccount { get; private set; }
    public int EditVersion { get; private set; }
    public int State { get; private set; }

    protected FADecrementDetailPost() { }

    public FADecrementDetailPost(
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
        int state) : this()
    {
        //Id = Guid.NewGuid();
        ExpenseItemId = expenseItemId;
        ExpenseItemCode = expenseItemCode;
        OrganizationUnitId = organizationUnitId;
        OrganizationUnitCode = organizationUnitCode;
        JobId = jobId;
        JobCode = jobCode;
        ProjectWorkId = projectWorkId;
        ProjectWorkCode = projectWorkCode;
        OrderId = orderId;
        OrderCode = orderCode;
        ContractId = contractId;
        ContractCode = contractCode;
        ListItemId = listItemId;
        ListItemCode = listItemCode;
        OrganizationUnitName = organizationUnitName;
        JobName = jobName;
        ProjectWorkName = projectWorkName;
        ExpenseItemName = expenseItemName;
        ListItemName = listItemName;
        AccountObjectId = accountObjectId;
        AccountObjectCode = accountObjectCode;
        AccountObjectName = accountObjectName;
        SortOrder = sortOrder;
        UnReasonableCost = unReasonableCost;
        Amount = amount;
        Description = description;
        DebitAccount = debitAccount;
        CreditAccount = creditAccount;
        EditVersion = editVersion;
        State = state;
    }

    public FADecrementDetailPost Update(
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
        ExpenseItemId = expenseItemId;
        ExpenseItemCode = expenseItemCode;
        OrganizationUnitId = organizationUnitId;
        OrganizationUnitCode = organizationUnitCode;
        JobId = jobId;
        JobCode = jobCode;
        ProjectWorkId = projectWorkId;
        ProjectWorkCode = projectWorkCode;
        OrderId = orderId;
        OrderCode = orderCode;
        ContractId = contractId;
        ContractCode = contractCode;
        ListItemId = listItemId;
        ListItemCode = listItemCode;
        OrganizationUnitName = organizationUnitName;
        JobName = jobName;
        ProjectWorkName = projectWorkName;
        ExpenseItemName = expenseItemName;
        ListItemName = listItemName;
        AccountObjectId = accountObjectId;
        AccountObjectCode = accountObjectCode;
        AccountObjectName = accountObjectName;
        SortOrder = sortOrder;
        UnReasonableCost = unReasonableCost;
        Amount = amount;
        Description = description;
        DebitAccount = debitAccount;
        CreditAccount = creditAccount;
        EditVersion = editVersion;
        State = state;

        return this;
    }
}
