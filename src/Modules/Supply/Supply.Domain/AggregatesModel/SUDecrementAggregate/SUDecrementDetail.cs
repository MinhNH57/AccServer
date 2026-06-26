namespace Supply.Domain.AggregatesModel.SUDecrementAggregate;

public class SUDecrementDetail
    : Entity
{
    public Guid? SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public Guid? SUAllocationId { get; set; }
    public Guid? SUAuditRefId { get; set; }
    public int SortOrder { get; set; }
    public double? UseQuantity { get; set; }
    public double? DecrementQuantity { get; set; }
    public double? DecrementAmount { get; set; }
    public double? RemainingDecrementAmount { get; set; }
    public string Reason { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int State { get; set; } = 0;
    public int? EditVersion { get; set; }

    protected SUDecrementDetail() { }

    public SUDecrementDetail(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        Guid? organizationUnitId,
        Guid? sUAllocationId,
        Guid? sUAuditRefId,
        int sortOrder,
        double? useQuantity,
        double? decrementQuantity,
        double? decrementAmount,
        double? remainingDecrementAmount,
        string reason,
        string organizationUnitCode,
        string organizationUnitName,
        int state,
        int? editVersion) : this()
    {
        //Id = Guid.NewGuid();
        SupplyId = supplyId;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        OrganizationUnitId = organizationUnitId;
        SUAllocationId = sUAllocationId;
        SUAuditRefId = sUAuditRefId;
        SortOrder = sortOrder;
        UseQuantity = useQuantity;
        DecrementQuantity = decrementQuantity;
        DecrementAmount = decrementAmount;
        RemainingDecrementAmount = remainingDecrementAmount;
        Reason = reason;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        State = state;
        EditVersion = editVersion;
    }

    public SUDecrementDetail Update(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        Guid? organizationUnitId,
        Guid? sUAllocationId,
        Guid? sUAuditRefId,
        int sortOrder,
        double? useQuantity,
        double? decrementQuantity,
        double? decrementAmount,
        double? remainingDecrementAmount,
        string reason,
        string organizationUnitCode,
        string organizationUnitName,
        int state,
        int? editVersion)
    {
        SupplyId = supplyId;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        OrganizationUnitId = organizationUnitId;
        SUAllocationId = sUAllocationId;
        SUAuditRefId = sUAuditRefId;
        SortOrder = sortOrder;
        UseQuantity = useQuantity;
        DecrementQuantity = decrementQuantity;
        DecrementAmount = decrementAmount;
        RemainingDecrementAmount = remainingDecrementAmount;
        Reason = reason;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        State = state;
        EditVersion = editVersion;

        return this;
    }
}
