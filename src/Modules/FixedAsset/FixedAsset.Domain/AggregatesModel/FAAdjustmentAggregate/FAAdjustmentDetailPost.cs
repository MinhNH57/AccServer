namespace FixedAsset.Domain.AggregatesModel.FAAdjustmentAggregate;

public class FAAdjustmentDetailPost
        : Entity
{
    public Guid? TenantId { get; set; }
    public Guid? AccountObjectId { get; set; }
    public string AccountObjectCode { get; set; }
    public string AccountObjectName { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public string ExpenseItemCode { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public string OrganizationUnitCode { get; set; }
    public Guid? JobId { get; set; }
    public string JobCode { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public string ProjectWorkCode { get; set; }
    public Guid? OrderId { get; set; }
    public string OrderCode { get; set; }
    public Guid? ContractId { get; set; }
    public string ContractCode { get; set; }
    public Guid? ListItemId { get; set; }
    public string ListItemCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public string JobName { get; set; }
    public string ProjectWorkName { get; set; }
    public string ExpenseItemName { get; set; }
    public string ListItemName { get; set; }
    public int SortOrder { get; set; }
    public bool UnReasonableCost { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public int State { get; set; }
    public int EditVersion { get; set; }

    protected FAAdjustmentDetailPost() { }

    public FAAdjustmentDetailPost(Guid? tenantId, Guid? accountObjectId, string accountObjectCode, string accountObjectName, Guid? expenseItemId, string expenseItemCode, Guid? organizationUnitId, string organizationUnitCode, Guid? jobId, string jobCode, Guid? projectWorkId, string projectWorkCode, Guid? orderId, string orderCode, Guid? contractId, string contractCode, Guid? listItemId, string listItemCode, string organizationUnitName, string jobName, string projectWorkName, string expenseItemName, string listItemName, int sortOrder, bool unReasonableCost, double amount, string description, string debitAccount, string creditAccount, int state, int editVersion)
        : this()
    {
        TenantId = tenantId;
        AccountObjectId = accountObjectId;
        AccountObjectCode = accountObjectCode;
        AccountObjectName = accountObjectName;
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
        SortOrder = sortOrder;
        UnReasonableCost = unReasonableCost;
        Amount = amount;
        Description = description;
        DebitAccount = debitAccount;
        CreditAccount = creditAccount;
        State = state;
        EditVersion = editVersion;
    }

    public FAAdjustmentDetailPost Update(Guid? tenantId, Guid? accountObjectId, string accountObjectCode, string accountObjectName, Guid? expenseItemId, string expenseItemCode, Guid? organizationUnitId, string organizationUnitCode, Guid? jobId, string jobCode, Guid? projectWorkId, string projectWorkCode, Guid? orderId, string orderCode, Guid? contractId, string contractCode, Guid? listItemId, string listItemCode, string organizationUnitName, string jobName, string projectWorkName, string expenseItemName, string listItemName, int sortOrder, bool unReasonableCost, double amount, string description, string debitAccount, string creditAccount, int state, int editVersion)
    {
        TenantId = tenantId;
        AccountObjectId = accountObjectId;
        AccountObjectCode = accountObjectCode;
        AccountObjectName = accountObjectName;
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
        SortOrder = sortOrder;
        UnReasonableCost = unReasonableCost;
        Amount = amount;
        Description = description;
        DebitAccount = debitAccount;
        CreditAccount = creditAccount;
        State = state;
        EditVersion = editVersion;

        return this;
    }
}
