namespace FixedAsset.Domain.AggregatesModel.FADecrementAggregate;

public class FADecrementDetail
    : Entity
{
    public Guid? FixedAssetId { get; private set; }
    public string FixedAssetCode { get; private set; }
    public string FixedAssetName { get; private set; }
    public Guid? OrganizationUnitId { get; private set; }
    public int SortOrder { get; private set; }
    public double OrgPrice { get; private set; }
    public double DepreciationAmount { get; private set; }
    public double AccumDepreciationAmount { get; private set; }
    public double RemainingAmount { get; private set; }
    public double DepreciationAmountInMonth { get; private set; }
    public string OrgPriceAccount { get; private set; }
    public string DepreciationAccount { get; private set; }
    public string RemainingAccount { get; private set; }
    public string OrganizationUnitCode { get; private set; }
    public string OrganizationUnitName { get; private set; }
    public int EditVersion { get; private set; }
    public int State { get; private set; }

    protected FADecrementDetail() { }

    public FADecrementDetail(
        Guid? fixedAssetId,
        string fixedAssetCode,
        string fixedAssetName,
        Guid? organizationUnitId,
        int sortOrder,
        double orgPrice,
        double depreciationAmount,
        double accumDepreciationAmount,
        double remainingAmount,
        double depreciationAmountInMonth,
        string orgPriceAccount,
        string depreciationAccount,
        string remainingAccount,
        string organizationUnitCode,
        string organizationUnitName,
        int editVersion,
        int state) : this()
    {
        //Id = Guid.NewGuid();
        FixedAssetId = fixedAssetId;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        OrganizationUnitId = organizationUnitId;
        SortOrder = sortOrder;
        OrgPrice = orgPrice;
        DepreciationAmount = depreciationAmount;
        AccumDepreciationAmount = accumDepreciationAmount;
        RemainingAmount = remainingAmount;
        DepreciationAmountInMonth = depreciationAmountInMonth;
        OrgPriceAccount = orgPriceAccount;
        DepreciationAccount = depreciationAccount;
        RemainingAccount = remainingAccount;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        EditVersion = editVersion;
        State = state;
    }
    public FADecrementDetail Update(
        Guid? fixedAssetId,
        string fixedAssetCode,
        string fixedAssetName,
        Guid? organizationUnitId,
        int sortOrder,
        double orgPrice,
        double depreciationAmount,
        double accumDepreciationAmount,
        double remainingAmount,
        double depreciationAmountInMonth,
        string orgPriceAccount,
        string depreciationAccount,
        string remainingAccount,
        string organizationUnitCode,
        string organizationUnitName,
        int editVersion,
        int state)
    {
        FixedAssetId = fixedAssetId;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        OrganizationUnitId = organizationUnitId;
        SortOrder = sortOrder;
        OrgPrice = orgPrice;
        DepreciationAmount = depreciationAmount;
        AccumDepreciationAmount = accumDepreciationAmount;
        RemainingAmount = remainingAmount;
        DepreciationAmountInMonth = depreciationAmountInMonth;
        OrgPriceAccount = orgPriceAccount;
        DepreciationAccount = depreciationAccount;
        RemainingAccount = remainingAccount;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        EditVersion = editVersion;
        State = state;

        return this;
    }

}
