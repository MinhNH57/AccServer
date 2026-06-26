namespace Supply.Domain.AggregatesModel.SUAuditAggregate;

public class SUAuditDetail
        : Entity
{
    public Guid? SupplyId { get; private set; }
    public string SupplyCode { get; private set; }
    public string SupplyName { get; private set; }
    public Guid? OrganizationUnitId { get; private set; }
    public int SortOrder { get; private set; }
    public int? Action { get; private set; }
    public double? QuantityOnBook { get; private set; }
    public double? QuantityInventory { get; private set; }
    public double? DiffQuantity { get; private set; }
    public double? GoodQuantity { get; private set; }
    public double? DamageQuantity { get; private set; }
    public double? ExecuteQuantity { get; private set; }
    public string Note { get; private set; }
    public string OrganizationUnitCode { get; private set; }
    public string OrganizationUnitName { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }
    public string Unit { get; private set; }

    protected SUAuditDetail() { }

    public SUAuditDetail(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        Guid? organizationUnitId,
        int sortOrder,
        int? action,
        double? quantityOnBook,
        double? quantityInventory,
        double? diffQuantity,
        double? goodQuantity,
        double? damageQuantity,
        double? executeQuantity,
        string note,
        string organizationUnitCode,
        string organizationUnitName,
        int state,
        int? editVersion,
        string unit)
    {
        SupplyId = supplyId;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        OrganizationUnitId = organizationUnitId;
        SortOrder = sortOrder;
        Action = action;
        QuantityOnBook = quantityOnBook;
        QuantityInventory = quantityInventory;
        DiffQuantity = diffQuantity;
        GoodQuantity = goodQuantity;
        DamageQuantity = damageQuantity;
        ExecuteQuantity = executeQuantity;
        Note = note;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        State = state;
        EditVersion = editVersion;
        Unit = unit;
    }
}
