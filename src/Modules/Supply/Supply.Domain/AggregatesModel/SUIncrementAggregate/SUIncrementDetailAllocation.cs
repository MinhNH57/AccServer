namespace Supply.Domain.AggregatesModel.SUIncrementAggregate;

public class SUIncrementDetailAllocation : Entity
{
    public Guid? TenantId { get; private set; }

    public Guid? ObjectId { get; private set; }

    public Guid? ExpenseItemId { get; private set; }

    public int SortOrder { get; private set; }

    public int? ObjectType { get; private set; }

    public double? AllocationRate { get; private set; }

    public string CostAccount { get; private set; }

    public string ObjectCode { get; private set; }

    public string ObjectName { get; private set; }

    public int State { get; private set; } = 0;

    public int? EditVersion { get; private set; }

    public string ExpenseItemCode { get; private set; }

    public Guid? ListItemId { get; private set; }

    public string ListItemCode { get; private set; }

    protected SUIncrementDetailAllocation() { }

    public SUIncrementDetailAllocation(
        Guid? tenantId,
        Guid? objectId,
        Guid? expenseItemId,
        int sortOrder,
        int? objectType,
        double? allocationRate,
        string costAccount,
        string objectCode,
        string objectName,
        int state,
        int? editVersion,
        string expenseItemCode,
        Guid? listItemId,
        string listItemCode) : this()
    {
        //Id = Guid.NewGuid();
        TenantId = tenantId;
        ObjectId = objectId;
        ExpenseItemId = expenseItemId;
        SortOrder = sortOrder;
        ObjectType = objectType;
        AllocationRate = allocationRate;
        CostAccount = costAccount;
        ObjectCode = objectCode;
        ObjectName = objectName;
        State = state;
        EditVersion = editVersion;
        ExpenseItemCode = expenseItemCode;
        ListItemId = listItemId;
        ListItemCode = listItemCode;
    }

    public SUIncrementDetailAllocation Update(
        Guid? tenantId,
        Guid? objectId,
        Guid? expenseItemId,
        int sortOrder,
        int? objectType,
        double? allocationRate,
        string costAccount,
        string objectCode,
        string objectName,
        int state,
        int? editVersion,
        string expenseItemCode,
        Guid? listItemId,
        string listItemCode)
    {
        TenantId = tenantId;
        ObjectId = objectId;
        ExpenseItemId = expenseItemId;
        SortOrder = sortOrder;
        ObjectType = objectType;
        AllocationRate = allocationRate;
        CostAccount = costAccount;
        ObjectCode = objectCode;
        ObjectName = objectName;
        State = state;
        EditVersion = editVersion;
        ExpenseItemCode = expenseItemCode;
        ListItemId = listItemId;
        ListItemCode = listItemCode;

        return this;
    }
}
