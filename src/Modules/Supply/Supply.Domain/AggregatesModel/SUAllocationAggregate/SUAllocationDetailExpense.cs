namespace Supply.Domain.AggregatesModel.SUAllocationAggregate;

public class SUAllocationDetailExpense
    : Entity
{
    public string SupplyCode { get; private set; }
    public string SupplyName { get; private set; }
    public string SupplyCategoryCode { get; private set; }
    public string SupplyCategoryName { get; private set; }
    public Guid? TenantId { get; private set; }
    public Guid? SupplyId { get; private set; }
    public Guid? SupplyCategoryId { get; private set; }
    public int SortOrder { get; private set; }
    public double? TotalAllocationAmount { get; private set; }
    public double? AllocationAmount { get; private set; }
    public double? RemainingAmount { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }

    protected SUAllocationDetailExpense() { }

    public SUAllocationDetailExpense(
        string supplyCode,
        string supplyName,
        string supplyCategoryCode,
        string supplyCategoryName,
        Guid? tenantId,
        Guid? supplyId,
        Guid? supplyCategoryId,
        int sortOrder,
        double? totalAllocationAmount,
        double? allocationAmount,
        double? remainingAmount,
        int state,
        int? editVersion) : this()
    {
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        SupplyCategoryCode = supplyCategoryCode;
        SupplyCategoryName = supplyCategoryName;
        TenantId = tenantId;
        SupplyId = supplyId;
        SupplyCategoryId = supplyCategoryId;
        SortOrder = sortOrder;
        TotalAllocationAmount = totalAllocationAmount;
        AllocationAmount = allocationAmount;
        RemainingAmount = remainingAmount;
        State = state;
        EditVersion = editVersion;
    }
}
