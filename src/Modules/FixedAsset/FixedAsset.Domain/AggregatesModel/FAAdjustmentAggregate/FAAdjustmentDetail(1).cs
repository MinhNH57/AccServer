namespace FixedAsset.Domain.AggregatesModel.FAAdjustmentAggregate;

public class FAAdjustmentDetail
        : Entity
{
    public Guid? FixedAssetId { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public int SortOrder { get; set; }
    public double CurrentRemainingAmount { get; set; }
    public double NewRemainingAmount { get; set; }
    public double DiffRemainingAmount { get; set; }
    public double CurrentLifeTime { get; set; }
    public double NewLifeTime { get; set; }
    public double CurrentAccumDepreciationAmount { get; set; }
    public double NewMonthlyDepreciationAmountByIncomeTax { get; set; }
    public double DiffLifeTime { get; set; }
    public double DiffMonthlyDepreciationAmount { get; set; }
    public double NewAccumDepreciationAmount { get; set; }
    public double DiffAccumDepreciationAmount { get; set; }
    public double CurrentDepreciationAmount { get; set; }
    public double NewDepreciationAmount { get; set; }
    public double DiffDepreciationAmount { get; set; }
    public double NewMonthlyDepreciationAmount { get; set; }
    public string CostAccount { get; set; }
    public string AdjustmentAccount { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public int State { get; set; } = 0;
    public int EditVersion { get; set; }

    protected FAAdjustmentDetail() { }

    public FAAdjustmentDetail(Guid? fixedAssetId, string fixedAssetCode, string fixedAssetName, Guid? organizationUnitId, int sortOrder, double currentRemainingAmount, double newRemainingAmount, double diffRemainingAmount, double currentLifeTime, double newLifeTime, double currentAccumDepreciationAmount, double newMonthlyDepreciationAmountByIncomeTax, double diffLifeTime, double diffMonthlyDepreciationAmount, double newAccumDepreciationAmount, double diffAccumDepreciationAmount, double currentDepreciationAmount, double newDepreciationAmount, double diffDepreciationAmount, double newMonthlyDepreciationAmount, string costAccount, string adjustmentAccount, string organizationUnitCode, string organizationUnitName, int state, int editVersion)
        :this()
    {
        FixedAssetId = fixedAssetId;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        OrganizationUnitId = organizationUnitId;
        SortOrder = sortOrder;
        CurrentRemainingAmount = currentRemainingAmount;
        NewRemainingAmount = newRemainingAmount;
        DiffRemainingAmount = diffRemainingAmount;
        CurrentLifeTime = currentLifeTime;
        NewLifeTime = newLifeTime;
        CurrentAccumDepreciationAmount = currentAccumDepreciationAmount;
        NewMonthlyDepreciationAmountByIncomeTax = newMonthlyDepreciationAmountByIncomeTax;
        DiffLifeTime = diffLifeTime;
        DiffMonthlyDepreciationAmount = diffMonthlyDepreciationAmount;
        NewAccumDepreciationAmount = newAccumDepreciationAmount;
        DiffAccumDepreciationAmount = diffAccumDepreciationAmount;
        CurrentDepreciationAmount = currentDepreciationAmount;
        NewDepreciationAmount = newDepreciationAmount;
        DiffDepreciationAmount = diffDepreciationAmount;
        NewMonthlyDepreciationAmount = newMonthlyDepreciationAmount;
        CostAccount = costAccount;
        AdjustmentAccount = adjustmentAccount;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        State = state;
        EditVersion = editVersion;
    }

    public FAAdjustmentDetail Update(Guid? fixedAssetId, string fixedAssetCode, string fixedAssetName, Guid? organizationUnitId, int sortOrder, double currentRemainingAmount, double newRemainingAmount, double diffRemainingAmount, double currentLifeTime, double newLifeTime, double currentAccumDepreciationAmount, double newMonthlyDepreciationAmountByIncomeTax, double diffLifeTime, double diffMonthlyDepreciationAmount, double newAccumDepreciationAmount, double diffAccumDepreciationAmount, double currentDepreciationAmount, double newDepreciationAmount, double diffDepreciationAmount, double newMonthlyDepreciationAmount, string costAccount, string adjustmentAccount, string organizationUnitCode, string organizationUnitName, int state, int editVersion)
    {
        FixedAssetId = fixedAssetId;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        OrganizationUnitId = organizationUnitId;
        SortOrder = sortOrder;
        CurrentRemainingAmount = currentRemainingAmount;
        NewRemainingAmount = newRemainingAmount;
        DiffRemainingAmount = diffRemainingAmount;
        CurrentLifeTime = currentLifeTime;
        NewLifeTime = newLifeTime;
        CurrentAccumDepreciationAmount = currentAccumDepreciationAmount;
        NewMonthlyDepreciationAmountByIncomeTax = newMonthlyDepreciationAmountByIncomeTax;
        DiffLifeTime = diffLifeTime;
        DiffMonthlyDepreciationAmount = diffMonthlyDepreciationAmount;
        NewAccumDepreciationAmount = newAccumDepreciationAmount;
        DiffAccumDepreciationAmount = diffAccumDepreciationAmount;
        CurrentDepreciationAmount = currentDepreciationAmount;
        NewDepreciationAmount = newDepreciationAmount;
        DiffDepreciationAmount = diffDepreciationAmount;
        NewMonthlyDepreciationAmount = newMonthlyDepreciationAmount;
        CostAccount = costAccount;
        AdjustmentAccount = adjustmentAccount;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        State = state;
        EditVersion = editVersion;

        return this;
    }
}
