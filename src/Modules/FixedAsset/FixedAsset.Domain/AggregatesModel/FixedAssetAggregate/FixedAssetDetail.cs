namespace FixedAsset.Domain.AggregatesModel.FixedAssetAggregate;

public class FixedAssetDetail : Entity
{
    public int SortOrder { get; private set; }

    public double? Quantity { get; private set; }

    public string Description { get; private set; }

    public string WarrantyTime { get; private set; }

    public string Unit { get; private set; }

    public int EditVersion { get; private set; }

    public int State { get; private set; }

    protected FixedAssetDetail() { }

    public FixedAssetDetail(
        int sortOrder,
        double? quantity,
        string description,
        string warrantyTime,
        string unit,
        int editVersion) : this()
    {
        if (quantity <= 0)
        {
            throw new FixedAssetDomainException("Số lượng phải lớn hơn 0.");
        }

        //Id = Guid.NewGuid();
        SortOrder = sortOrder;
        Quantity = quantity;
        Description = description;
        WarrantyTime = warrantyTime;
        Unit = unit;
        EditVersion = editVersion;
    }

    public FixedAssetDetail Update(
        int sortOrder,
        double? quantity,
        string description,
        string warrantyTime,
        string unit,
        int editVersion)
    {
        if (quantity <= 0)
        {
            throw new FixedAssetDomainException("Số lượng phải lớn hơn 0.");
        }

        SortOrder = sortOrder;
        Quantity = quantity;
        Description = description;
        WarrantyTime = warrantyTime;
        Unit = unit;
        EditVersion = editVersion;

        return this;
    }
}
