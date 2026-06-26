namespace FixedAsset.Domain.AggregatesModel.FADepreciationAggregate;

public class FADepreciationDetailPost
    : Entity
{
    public Guid? TenantId { get; private set; }
    public Guid? FixedAssetId { get; private set; }
    public Guid? DebitAccountObjectId { get; private set; }
    public Guid? CreditAccountObjectId { get; private set; }
    public Guid? ExpenseItemId { get; private set; }
    public Guid? OrganizationUnitId { get; private set; }
    public Guid? JobId { get; private set; }
    public Guid? ProjectWorkId { get; private set; }
    public Guid? OrderId { get; private set; }
    public Guid? ContractId { get; private set; }
    public Guid? ListItemId { get; private set; }
    public int SortOrder { get; private set; }
    public bool UnReasonableCost { get; private set; }
    public double Amount { get; private set; }
    public string Description { get; private set; }
    public string DebitAccount { get; private set; }
    public string CreditAccount { get; private set; }
    public string OrganizationUnitName { get; private set; }
    public string ListItemCode { get; private set; }
    public string ExpenseItemCode { get; private set; }
    public string OrganizationUnitCode { get; private set; }
    public string JobCode { get; private set; }
    public string JobName { get; private set; }
    public string ExpenseItemName { get; private set; }
    public string ListItemName { get; private set; }
    public string ProjectWorkCode { get; private set; }
    public string CreditAccountObjectCode { get; private set; }
    public string DebitAccountObjectCode { get; private set; }
    public string CreditAccountObjectName { get; private set; }
    public string DebitAccountObjectName { get; private set; }
    public string OrderCode { get; private set; }
    public string ContractCode { get; private set; }
    public string ContractSubject { get; private set; }
    public string ProjectWorkName { get; private set; }
    public string AccountName { get; private set; }
    public DateTime? ContractSignDate { get; private set; }
    public int State { get; private set; }
    public int EditVersion { get; private set; }
    public int? AllocationObjectType { get; private set; }

    protected FADepreciationDetailPost() { }

    public FADepreciationDetailPost(
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
        int? allocationObjectType) : this()
    {
        //Id = Guid.NewGuid();
        TenantId = tenantId;
        FixedAssetId = fixedAssetId;
        DebitAccountObjectId = debitAccountObjectId;
        CreditAccountObjectId = creditAccountObjectId;
        ExpenseItemId = expenseItemId;
        OrganizationUnitId = organizationUnitId;
        JobId = jobId;
        ProjectWorkId = projectWorkId;
        OrderId = orderId;
        ContractId = contractId;
        ListItemId = listItemId;
        SortOrder = sortOrder;
        UnReasonableCost = unReasonableCost;
        Amount = amount;
        Description = description;
        DebitAccount = debitAccount;
        CreditAccount = creditAccount;
        OrganizationUnitName = organizationUnitName;
        ListItemCode = listItemCode;
        ExpenseItemCode = expenseItemCode;
        OrganizationUnitCode = organizationUnitCode;
        JobCode = jobCode;
        JobName = jobName;
        ExpenseItemName = expenseItemName;
        ListItemName = listItemName;
        ProjectWorkCode = projectWorkCode;
        CreditAccountObjectCode = creditAccountObjectCode;
        DebitAccountObjectCode = debitAccountObjectCode;
        CreditAccountObjectName = creditAccountObjectName;
        DebitAccountObjectName = debitAccountObjectName;
        OrderCode = orderCode;
        ContractCode = contractCode;
        ContractSubject = contractSubject;
        ProjectWorkName = projectWorkName;
        AccountName = accountName;
        ContractSignDate = contractSignDate;
        State = state;
        EditVersion = editVersion;
        AllocationObjectType = allocationObjectType;
    }
}
