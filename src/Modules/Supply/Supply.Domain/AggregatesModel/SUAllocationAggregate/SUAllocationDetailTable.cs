namespace Supply.Domain.AggregatesModel.SUAllocationAggregate;

public class SUAllocationDetailTable
    : Entity
{
    public Guid? TenantId { get; private set; }
    public Guid? SupplyId { get; private set; }
    public string SupplyCode { get; private set; }
    public string SupplyName { get; private set; }
    public Guid? AllocationObjectId { get; private set; }
    public double? AllocationRate { get; private set; }
    public double? AllocationAmount { get; private set; }
    public int SortOrder { get; private set; }
    public string CostAccount { get; private set; }
    public Guid? ExpenseItemId { get; private set; }
    public double? TotalAllocationAmount { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }
    public string ExpenseItemCode { get; private set; }
    public string ExpenseItemName { get; private set; }
    public string AllocationObjectCode { get; private set; }
    public string AllocationObjectName { get; private set; }
    public string AllocationAccount { get; private set; }
    public int? AllocationObjectType { get; private set; }
    public Guid? ListItemId { get; private set; }
    public string ListItemCode { get; private set; }
    public string ListItemName { get; private set; }

    protected SUAllocationDetailTable() { }

    public SUAllocationDetailTable(
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
        string listItemName) : this()
    {
        TenantId = tenantId;
        SupplyId = supplyId;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        AllocationObjectId = allocationObjectId;
        AllocationRate = allocationRate;
        AllocationAmount = allocationAmount;
        SortOrder = sortOrder;
        CostAccount = costAccount;
        ExpenseItemId = expenseItemId;
        TotalAllocationAmount = totalAllocationAmount;
        State = state;
        EditVersion = editVersion;
        ExpenseItemCode = expenseItemCode;
        ExpenseItemName = expenseItemName;
        AllocationObjectCode = allocationObjectCode;
        AllocationObjectName = allocationObjectName;
        AllocationAccount = allocationAccount;
        AllocationObjectType = allocationObjectType;
        ListItemId = listItemId;
        ListItemCode = listItemCode;
        ListItemName = listItemName;
    }
}
