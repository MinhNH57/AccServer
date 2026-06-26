namespace FixedAsset.Domain.AggregatesModel.FADepreciationAggregate;

public class FADepreciationDetailAllocation
    : Entity
{
    public Guid? TenantId { get; private set; }
    public Guid? FixedAssetId { get; private set; }
    public Guid? OrganizationUnitId { get; private set; }
    public Guid? AllocationObjectId { get; private set; }
    public Guid? ExpenseItemId { get; private set; }
    public Guid? ListItemId { get; private set; }
    public int SortOrder { get; private set; }
    public double? MonthlyDepreciationAmount { get; private set; }
    public double? AllocationRate { get; private set; }
    public double? AllocationAmount { get; private set; }
    public string CostAccount { get; private set; }
    public string OrganizationUnitName { get; private set; }
    public string AllocationObjectCode { get; private set; }
    public string AllocationObjectName { get; private set; }
    public int? AllocationObjectType { get; private set; }
    public int State { get; private set; }
    public int EditVersion { get; private set; }
    public string FixedAssetCode { get; private set; }
    public string FixedAssetName { get; private set; }
    public string ListItemCode { get; private set; }
    public string ListItemName { get; private set; }
    public string ExpenseItemCode { get; private set; }
    public string ExpenseItemName { get; private set; }
    public string DepreciationAccount { get; private set; }
    public string OrganizationUnitCode { get; private set; }

    protected FADepreciationDetailAllocation() { }

    public FADepreciationDetailAllocation(
        Guid? tenantId,
        Guid? fixedAssetId,
        Guid? organizationUnitId,
        Guid? allocationObjectId,
        Guid? expenseItemId,
        Guid? listItemId,
        int sortOrder,
        double? monthlyDepreciationAmount,
        double? allocationRate,
        double? allocationAmount,
        string costAccount,
        string organizationUnitName,
        string allocationObjectCode,
        string allocationObjectName,
        int? allocationObjectType,
        int state,
        int editVersion,
        string fixedAssetCode,
        string fixedAssetName,
        string listItemCode,
        string listItemName,
        string expenseItemCode,
        string expenseItemName,
        string depreciationAccount,
        string organizationUnitCode) : this()
    {
        //Id = Guid.NewGuid();
        TenantId = tenantId;
        FixedAssetId = fixedAssetId;
        OrganizationUnitId = organizationUnitId;
        AllocationObjectId = allocationObjectId;
        ExpenseItemId = expenseItemId;
        ListItemId = listItemId;
        SortOrder = sortOrder;
        MonthlyDepreciationAmount = monthlyDepreciationAmount;
        AllocationRate = allocationRate;
        AllocationAmount = allocationAmount;
        CostAccount = costAccount;
        OrganizationUnitName = organizationUnitName;
        AllocationObjectCode = allocationObjectCode;
        AllocationObjectName = allocationObjectName;
        AllocationObjectType = allocationObjectType;
        State = state;
        EditVersion = editVersion;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        ListItemCode = listItemCode;
        ListItemName = listItemName;
        ExpenseItemCode = expenseItemCode;
        ExpenseItemName = expenseItemName;
        DepreciationAccount = depreciationAccount;
        OrganizationUnitCode = organizationUnitCode;
    }
}
