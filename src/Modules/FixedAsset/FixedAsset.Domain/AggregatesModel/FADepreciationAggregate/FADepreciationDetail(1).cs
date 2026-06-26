namespace FixedAsset.Domain.AggregatesModel.FADepreciationAggregate;

public class FADepreciationDetail
    : Entity
{
    public Guid? FixedAssetId { get; private set; }
    public Guid? OrganizationUnitId { get; private set; }
    public int SortOrder { get; private set; }
    public double MonthlyDepreciationAmount { get; private set; }
    public double AmountReasonableCost { get; private set; }
    public double AmountUnReasonableCost { get; private set; }
    public string OrganizationUnitCode { get; private set; }
    public string OrganizationUnitName { get; private set; }
    public string FixedAssetCode { get; private set; }
    public string FixedAssetName { get; private set; }
    public string FixedAssetCategoryName { get; private set; }
    public Guid FixedAssetCategoryId { get; private set; }
    public string FixedAssetCategoryCode { get; private set; }
    public int State { get; private set; }
    public int EditVersion { get; private set; }

    protected FADepreciationDetail() { }

    public FADepreciationDetail(
        Guid? fixedAssetId,
        Guid? organizationUnitId,
        int sortOrder,
        double monthlyDepreciationAmount,
        double amountReasonableCost,
        double amountUnReasonableCost,
        string organizationUnitCode,
        string organizationUnitName,
        string fixedAssetCode,
        string fixedAssetName,
        string fixedAssetCategoryName,
        Guid fixedAssetCategoryId,
        string fixedAssetCategoryCode,
        int state,
        int editVersion) : this()
    {
        //Id = Guid.NewGuid();
        FixedAssetId = fixedAssetId;
        OrganizationUnitId = organizationUnitId;
        SortOrder = sortOrder;
        MonthlyDepreciationAmount = monthlyDepreciationAmount;
        AmountReasonableCost = amountReasonableCost;
        AmountUnReasonableCost = amountUnReasonableCost;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        FixedAssetCategoryName = fixedAssetCategoryName;
        FixedAssetCategoryId = fixedAssetCategoryId;
        FixedAssetCategoryCode = fixedAssetCategoryCode;
        State = state;
        EditVersion = editVersion;
    }
}
