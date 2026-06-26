namespace FixedAsset.Domain.AggregatesModel.FAAuditAggregate;

public class FAAuditDetail
        : Entity
{
    public Guid? FixedAssetId { get; private set; }
    public Guid? OrganizationUnitId { get; private set; }
    public int ExistInStock { get; private set; }
    public int Quality { get; private set; }
    public int Recommendation { get; private set; }
    public int SortOrder { get; private set; }
    public double OrgPrice { get; private set; }
    public double DepreciationAmount { get; private set; }
    public double AccumDepreciationAmount { get; private set; }
    public double RemainingAmount { get; private set; }
    public string Note { get; private set; }
    public string OrganizationUnitCode { get; private set; }
    public string OrganizationUnitName { get; private set; }
    public string FixedAssetCode { get; private set; }
    public string FixedAssetName { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }

    protected FAAuditDetail() { }

    public FAAuditDetail(
        Guid? fixedAssetId,
        Guid? organizationUnitId,
        int existInStock,
        int quality,
        int recommendation,
        int sortOrder,
        double orgPrice,
        double depreciationAmount,
        double accumDepreciationAmount,
        double remainingAmount,
        string note,
        string organizationUnitCode,
        string organizationUnitName,
        string fixedAssetCode,
        string fixedAssetName,
        int state,
        int? editVersion) : this()
    {
        FixedAssetId = fixedAssetId;
        OrganizationUnitId = organizationUnitId;
        ExistInStock = existInStock;
        Quality = quality;
        Recommendation = recommendation;
        SortOrder = sortOrder;
        OrgPrice = orgPrice;
        DepreciationAmount = depreciationAmount;
        AccumDepreciationAmount = accumDepreciationAmount;
        RemainingAmount = remainingAmount;
        Note = note;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        State = state;
        EditVersion = editVersion;
    }

    public FAAuditDetail Update(
        Guid? fixedAssetId,
        Guid? organizationUnitId,
        int existInStock,
        int quality,
        int recommendation,
        int sortOrder,
        double orgPrice,
        double depreciationAmount,
        double accumDepreciationAmount,
        double remainingAmount,
        string note,
        string organizationUnitCode,
        string organizationUnitName,
        string fixedAssetCode,
        string fixedAssetName,
        int state,
        int? editVersion)
    {
        FixedAssetId = fixedAssetId;
        OrganizationUnitId = organizationUnitId;
        ExistInStock = existInStock;
        Quality = quality;
        Recommendation = recommendation;
        SortOrder = sortOrder;
        OrgPrice = orgPrice;
        DepreciationAmount = depreciationAmount;
        AccumDepreciationAmount = accumDepreciationAmount;
        RemainingAmount = remainingAmount;
        Note = note;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        State = state;
        EditVersion = editVersion;

        return this;
    }

}
