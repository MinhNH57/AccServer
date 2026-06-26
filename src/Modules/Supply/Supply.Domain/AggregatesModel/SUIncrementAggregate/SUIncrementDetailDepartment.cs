namespace Supply.Domain.AggregatesModel.SUIncrementAggregate;

public class SUIncrementDetailDepartment : Entity
{
    public Guid? TenantId { get; private set; }

    public Guid? OrganizationUnitId { get; private set; }

    public int SortOrder { get; private set; }

    public int? AllocationTime { get; private set; }

    public int? RemainingAllocationTime { get; private set; }

    public double? Quantity { get; private set; }

    public double? UnitPrice { get; private set; }

    public double? Amount { get; private set; }

    public double? AllocatedAmount { get; private set; }

    public string OrganizationUnitCode { get; private set; }

    public string OrganizationUnitName { get; private set; }
    public int? OrganizationUnitType { get; private set; }

    public int State { get; private set; } = 0;

    public int? EditVersion { get; private set; }

    protected SUIncrementDetailDepartment() { }

    public SUIncrementDetailDepartment(
        Guid? tenantId,
        Guid? organizationUnitId,
        int sortOrder,
        int? allocationTime,
        int? remainingAllocationTime,
        double? quantity,
        double? unitPrice,
        double? amount,
        double? allocatedAmount,
        string organizationUnitCode,
        string organizationUnitName,
        int? organizationUnitType,
        int state,
        int? editVersion) : this()
    {
        //Id = Guid.NewGuid();
        TenantId = tenantId;
        OrganizationUnitId = organizationUnitId;
        SortOrder = sortOrder;
        AllocationTime = allocationTime;
        RemainingAllocationTime = remainingAllocationTime;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Amount = amount;
        AllocatedAmount = allocatedAmount;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        OrganizationUnitType = organizationUnitType;
        State = state;
        EditVersion = editVersion;
    }

    public SUIncrementDetailDepartment Update(
        Guid? tenantId,
        Guid? organizationUnitId,
        int sortOrder,
        int? allocationTime,
        int? remainingAllocationTime,
        double? quantity,
        double? unitPrice,
        double? amount,
        double? allocatedAmount,
        string organizationUnitCode,
        string organizationUnitName,
        int? organizationUnitType,
        int state,
        int? editVersion)
    {
        TenantId = tenantId;
        OrganizationUnitId = organizationUnitId;
        SortOrder = sortOrder;
        AllocationTime = allocationTime;
        RemainingAllocationTime = remainingAllocationTime;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Amount = amount;
        AllocatedAmount = allocatedAmount;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        OrganizationUnitType = organizationUnitType;
        State = state;
        EditVersion = editVersion;

        return this;
    }
}
