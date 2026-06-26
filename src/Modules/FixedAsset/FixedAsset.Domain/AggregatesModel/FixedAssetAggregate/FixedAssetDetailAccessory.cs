namespace FixedAsset.Domain.AggregatesModel.FixedAssetAggregate;

public class FixedAssetDetailAccessory : Entity
{
    public int SortOrder { get; private set; }

    public double? Quantity { get; private set; }

    public double? Amount { get; private set; }

    public string Description { get; private set; }

    public string Unit { get; private set; }

    public int EditVersion { get; private set; }

    public int State { get; private set; }

    protected FixedAssetDetailAccessory() { }

    public FixedAssetDetailAccessory(
        int sortOrder,
        double? quantity,
        double? amount,
        string description,
        string unit,
        int editVersion) : this()
    {
        //Id = Guid.NewGuid();
        SortOrder = sortOrder;
        Quantity = quantity;
        Amount = amount;
        Description = description;
        Unit = unit;
        EditVersion = editVersion;
    }

    public FixedAssetDetailAccessory Update(
        int sortOrder,
        double? quantity,
        double? amount,
        string description,
        string unit,
        int editVersion)
    {
        SortOrder = sortOrder;
        Quantity = quantity;
        Amount = amount;
        Description = description;
        Unit = unit;
        EditVersion = editVersion;

        return this;
    }
}
