namespace Supply.Domain.AggregatesModel.SUTransferAggregate;

public class SUTransferDetail
    : Entity
{
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? FromOrganizationUnitId { get; set; }
    public string FromOrganizationUnitCode { get; set; }
    public string FromOrganizationUnitName { get; set; }
    public Guid? ToOrganizationUnitId { get; set; }
    public string ToOrganizationUnitCode { get; set; }
    public string ToOrganizationUnitName { get; set; }
    public Guid? ListItemId { get; set; }
    public string ListItemCode { get; set; }
    public string ListItemName { get; set; }
    public Guid? ContractId { get; set; }
    public string ContractCode { get; set; }
    public Guid? OrderId { get; set; }
    public string OrderCode { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public string ProjectWorkCode { get; set; }
    public string ProjectWorkName { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public string ExpenseItemCode { get; set; }
    public string ExpenseItemName { get; set; }
    public Guid? JobId { get; set; }
    public string JobCode { get; set; }
    public string JobName { get; set; }
    public int SortOrder { get; set; }
    public double? UseQuantity { get; set; }
    public double? TransferQuantity { get; set; }
    public string CostAccount { get; set; }

    protected SUTransferDetail() { }

    public SUTransferDetail(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        Guid? fromOrganizationUnitId,
        string fromOrganizationUnitCode,
        string fromOrganizationUnitName,
        Guid? toOrganizationUnitId,
        string toOrganizationUnitCode,
        string toOrganizationUnitName,
        Guid? listItemId,
        string listItemCode,
        string listItemName,
        Guid? contractId,
        string contractCode,
        Guid? orderId,
        string orderCode,
        Guid? projectWorkId,
        string projectWorkCode,
        string projectWorkName,
        Guid? expenseItemId,
        string expenseItemCode,
        string expenseItemName,
        Guid? jobId,
        string jobCode,
        string jobName,
        int sortOrder,
        double? useQuantity,
        double? transferQuantity,
        string costAccount) : this()
    {
        //Id = Guid.NewGuid();
        SupplyId = supplyId;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        FromOrganizationUnitId = fromOrganizationUnitId;
        FromOrganizationUnitCode = fromOrganizationUnitCode;
        FromOrganizationUnitName = fromOrganizationUnitName;
        ToOrganizationUnitId = toOrganizationUnitId;
        ToOrganizationUnitCode = toOrganizationUnitCode;
        ToOrganizationUnitName = toOrganizationUnitName;
        ListItemId = listItemId;
        ListItemCode = listItemCode;
        ListItemName = listItemName;
        ContractId = contractId;
        ContractCode = contractCode;
        OrderId = orderId;
        OrderCode = orderCode;
        ProjectWorkId = projectWorkId;
        ProjectWorkCode = projectWorkCode;
        ProjectWorkName = projectWorkName;
        ExpenseItemId = expenseItemId;
        ExpenseItemCode = expenseItemCode;
        ExpenseItemName = expenseItemName;
        JobId = jobId;
        JobCode = jobCode;
        JobName = jobName;
        SortOrder = sortOrder;
        UseQuantity = useQuantity;
        TransferQuantity = transferQuantity;
        CostAccount = costAccount;
    }

    public SUTransferDetail Update(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        Guid? fromOrganizationUnitId,
        string fromOrganizationUnitCode,
        string fromOrganizationUnitName,
        Guid? toOrganizationUnitId,
        string toOrganizationUnitCode,
        string toOrganizationUnitName,
        Guid? listItemId,
        string listItemCode,
        string listItemName,
        Guid? contractId,
        string contractCode,
        Guid? orderId,
        string orderCode,
        Guid? projectWorkId,
        string projectWorkCode,
        string projectWorkName,
        Guid? expenseItemId,
        string expenseItemCode,
        string expenseItemName,
        Guid? jobId,
        string jobCode,
        string jobName,
        int sortOrder,
        double? useQuantity,
        double? transferQuantity,
        string costAccount)
    {
        SupplyId = supplyId;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        FromOrganizationUnitId = fromOrganizationUnitId;
        FromOrganizationUnitCode = fromOrganizationUnitCode;
        FromOrganizationUnitName = fromOrganizationUnitName;
        ToOrganizationUnitId = toOrganizationUnitId;
        ToOrganizationUnitCode = toOrganizationUnitCode;
        ToOrganizationUnitName = toOrganizationUnitName;
        ListItemId = listItemId;
        ListItemCode = listItemCode;
        ListItemName = listItemName;
        ContractId = contractId;
        ContractCode = contractCode;
        OrderId = orderId;
        OrderCode = orderCode;
        ProjectWorkId = projectWorkId;
        ProjectWorkCode = projectWorkCode;
        ProjectWorkName = projectWorkName;
        ExpenseItemId = expenseItemId;
        ExpenseItemCode = expenseItemCode;
        ExpenseItemName = expenseItemName;
        JobId = jobId;
        JobCode = jobCode;
        JobName = jobName;
        SortOrder = sortOrder;
        UseQuantity = useQuantity;
        TransferQuantity = transferQuantity;
        CostAccount = costAccount;

        return this;
    }
}
