using System.ComponentModel.DataAnnotations;

namespace FixedAsset.Domain.AggregatesModel.FixedAssetAggregate;

public class FixedAssetDetailAllocation : Entity
{
    [Required]
    public Guid ObjectId { get; private set; }

    public Guid? ExpenseItemId { get; private set; }

    public Guid? ListItemId { get; private set; }

    [Required]
    public int SortOrder { get; private set; }

    [Required]
    public int ObjectType { get; private set; }

    [Required]
    public double AllocationRate { get; private set; }

    [Required]
    public string CostAccount { get; private set; }

    [Required]
    public string ObjectCode { get; private set; }

    [Required]
    public string ObjectName { get; private set; }

    public string ExpenseItemCode { get; private set; }

    public string ListItemCode { get; private set; }

    public int EditVersion { get; private set; }

    public int State { get; private set; }

    protected FixedAssetDetailAllocation() { }

    public FixedAssetDetailAllocation(
        Guid objectId,
        Guid? expenseItemId,
        Guid? listItemId,
        int sortOrder,
        int objectType,
        double allocationRate,
        string costAccount,
        string objectCode,
        string objectName,
        string expenseItemCode,
        string listItemCode,
        int editVersion) : this()
    {
        //Id = Guid.NewGuid();
        ObjectId = objectId;
        ExpenseItemId = expenseItemId;
        ListItemId = listItemId;
        SortOrder = sortOrder;
        ObjectType = objectType;
        AllocationRate = allocationRate;
        CostAccount = costAccount;
        ObjectCode = objectCode;
        ObjectName = objectName;
        ExpenseItemCode = expenseItemCode;
        ListItemCode = listItemCode;
        EditVersion = editVersion;
    }

    public FixedAssetDetailAllocation Update(
        Guid objectId,
        Guid? expenseItemId,
        Guid? listItemId,
        int sortOrder,
        int objectType,
        double allocationRate,
        string costAccount,
        string objectCode,
        string objectName,
        string expenseItemCode,
        string listItemCode,
        int editVersion)
    {
        ObjectId = objectId;
        ExpenseItemId = expenseItemId;
        ListItemId = listItemId;
        SortOrder = sortOrder;
        ObjectType = objectType;
        AllocationRate = allocationRate;
        CostAccount = costAccount;
        ObjectCode = objectCode;
        ObjectName = objectName;
        ExpenseItemCode = expenseItemCode;
        ListItemCode = listItemCode;
        EditVersion = editVersion;

        return this;
    }
}
