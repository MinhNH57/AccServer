namespace FixedAsset.Domain.AggregatesModel.FATransferAggregate;

public class FATransferDetail
    : Entity
{
    public Guid? FixedAssetId { get; private set; }
    public string FixedAssetName { get; private set; }
    public Guid? FromOrganizationUnitId { get; private set; }
    public Guid? ToOrganizationUnitId { get; private set; }
    public Guid? ListItemId { get; private set; }
    public Guid? ContractId { get; private set; }
    public Guid? OrderId { get; private set; }
    public Guid? ProjectWorkId { get; private set; }
    public Guid? ExpenseItemId { get; private set; }
    public Guid? JobId { get; private set; }
    public int SortOrder { get; private set; }
    public string CostAccount { get; private set; }
    public string ContractCode { get; private set; }
    public string ExpenseItemCode { get; private set; }
    public string JobCode { get; private set; }
    public string ListItemCode { get; private set; }
    public string OrderCode { get; private set; }
    public string ProjectWorkCode { get; private set; }
    public string ExpenseItemName { get; private set; }
    public string JobName { get; private set; }
    public string ListItemName { get; private set; }
    public string ProjectWorkName { get; private set; }
    public string FromOrganizationUnitCode { get; private set; }
    public string ToOrganizationUnitCode { get; private set; }
    public string FromOrganizationUnitName { get; private set; }
    public string ToOrganizationUnitName { get; private set; }
    public string FixedAssetCode { get; private set; }
    public int State { get; private set; }
    public int EditVersion { get; private set; }

    protected FATransferDetail() { }

    public FATransferDetail(
        Guid? fixedAssetId,
        string fixedAssetName,
        Guid? fromOrganizationUnitId,
        Guid? toOrganizationUnitId,
        Guid? listItemId,
        Guid? contractId,
        Guid? orderId,
        Guid? projectWorkId,
        Guid? expenseItemId,
        Guid? jobId,
        int sortOrder,
        string costAccount,
        string contractCode,
        string expenseItemCode,
        string jobCode,
        string listItemCode,
        string orderCode,
        string projectWorkCode,
        string expenseItemName,
        string jobName,
        string listItemName,
        string projectWorkName,
        string fromOrganizationUnitCode,
        string toOrganizationUnitCode,
        string fromOrganizationUnitName,
        string toOrganizationUnitName,
        string fixedAssetCode,
        int state,
        int editVersion) : this()
    {
        //Id = Guid.NewGuid();
        FixedAssetId = fixedAssetId;
        FixedAssetName = fixedAssetName;
        FromOrganizationUnitId = fromOrganizationUnitId;
        ToOrganizationUnitId = toOrganizationUnitId;
        ListItemId = listItemId;
        ContractId = contractId;
        OrderId = orderId;
        ProjectWorkId = projectWorkId;
        ExpenseItemId = expenseItemId;
        JobId = jobId;
        SortOrder = sortOrder;
        CostAccount = costAccount;
        ContractCode = contractCode;
        ExpenseItemCode = expenseItemCode;
        JobCode = jobCode;
        ListItemCode = listItemCode;
        OrderCode = orderCode;
        ProjectWorkCode = projectWorkCode;
        ExpenseItemName = expenseItemName;
        JobName = jobName;
        ListItemName = listItemName;
        ProjectWorkName = projectWorkName;
        FromOrganizationUnitCode = fromOrganizationUnitCode;
        ToOrganizationUnitCode = toOrganizationUnitCode;
        FromOrganizationUnitName = fromOrganizationUnitName;
        ToOrganizationUnitName = toOrganizationUnitName;
        FixedAssetCode = fixedAssetCode;
        State = state;
        EditVersion = editVersion;
    }

    public FATransferDetail Update(
        Guid? fixedAssetId, string fixedAssetName, Guid? fromOrganizationUnitId, Guid? toOrganizationUnitId,
        Guid? listItemId, Guid? contractId, Guid? orderId, Guid? projectWorkId, Guid? expenseItemId, Guid? jobId,
        int sortOrder, string costAccount, string contractCode, string expenseItemCode, string jobCode,
        string listItemCode, string orderCode, string projectWorkCode, string expenseItemName, string jobName,
        string listItemName, string projectWorkName, string fromOrganizationUnitCode, string toOrganizationUnitCode,
        string fromOrganizationUnitName, string toOrganizationUnitName, string fixedAssetCode, int state, int editVersion)
    {
        FixedAssetId = fixedAssetId;
        FixedAssetName = fixedAssetName;
        FromOrganizationUnitId = fromOrganizationUnitId;
        ToOrganizationUnitId = toOrganizationUnitId;
        ListItemId = listItemId;
        ContractId = contractId;
        OrderId = orderId;
        ProjectWorkId = projectWorkId;
        ExpenseItemId = expenseItemId;
        JobId = jobId;
        SortOrder = sortOrder;
        CostAccount = costAccount;
        ContractCode = contractCode;
        ExpenseItemCode = expenseItemCode;
        JobCode = jobCode;
        ListItemCode = listItemCode;
        OrderCode = orderCode;
        ProjectWorkCode = projectWorkCode;
        ExpenseItemName = expenseItemName;
        JobName = jobName;
        ListItemName = listItemName;
        ProjectWorkName = projectWorkName;
        FromOrganizationUnitCode = fromOrganizationUnitCode;
        ToOrganizationUnitCode = toOrganizationUnitCode;
        FromOrganizationUnitName = fromOrganizationUnitName;
        ToOrganizationUnitName = toOrganizationUnitName;
        FixedAssetCode = fixedAssetCode;
        State = state;
        EditVersion = editVersion;

        return this;
    }



}
