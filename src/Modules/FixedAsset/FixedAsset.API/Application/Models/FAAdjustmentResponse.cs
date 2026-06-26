namespace FixedAsset.API.Application.Models;

public class FAAdjustmentDto
{
    public Guid? TenantId { get; set; }
    public Guid RefId { get; set; }
    public Guid? BranchId { get; set; }
    public string BranchName { get; set; }
    public int RefType { get; set; }
    public int DisplayOnBook { get; set; }
    public int RefOrder { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime? DecisionDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsPostedFinance { get; set; }
    public bool IsPostedManagement { get; set; }
    public string RefNo { get; set; }
    public string DecisionNo { get; set; }
    public string Reason { get; set; }
    public string JournalMemo { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public int State { get; set; }
    public int EditVersion { get; set; }
    public string Members { get; set; }
    public double TotalAmount { get; set; }
    public string AttachmentIdList { get; set; }
}

public class FAAdjustmentDetailDto
{
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
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
}

public class FAAdjustmentDetailPostDto
{
    public Guid? TenantId { get; set; }
    public Guid RefDetailId { get; set; }
    public Guid RefId { get; set; }
    public Guid? AccountObjectId { get; set; }
    public string AccountObjectCode { get; set; }
    public string AccountObjectName { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public string ExpenseItemCode { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public string OrganizationUnitCode { get; set; }
    public Guid? JobId { get; set; }
    public string JobCode { get; set; }
    public Guid? ProjectWorkId { get; set; }
    public string ProjectWorkCode { get; set; }
    public Guid? OrderId { get; set; }
    public string OrderCode { get; set; }
    public Guid? ContractId { get; set; }
    public string ContractCode { get; set; }
    public Guid? ListItemId { get; set; }
    public string ListItemCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public string JobName { get; set; }
    public string ProjectWorkName { get; set; }
    public string ExpenseItemName { get; set; }
    public string ListItemName { get; set; }
    public int SortOrder { get; set; }
    public bool UnReasonableCost { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public int State { get; set; }
    public int EditVersion { get; set; }
}

public class FAAdjustmentResponse
{
    public List<FAAdjustmentDto> FAAdjustment { get; set; }
    public List<FAAdjustmentDetailDto> FAAdjustmentDetail { get; set; }
    public List<FAAdjustmentDetailPostDto> FAAdjustmentDetailPost { get; set; }
}

public class FAAdjustmentCreateResponse
{
    public List<FAAdjustmentSaveFullResponse> FAAdjustment { get; set; }
    public List<FAAdjustmentDetailSaveFullResponse> FAAdjustmentDetail { get; set; }
    public List<FAAdjustmentDetailSaveFullResponse> FAAdjustmentDetailPost { get; set; }
}

public class FAAdjustmentUpdateResponse
{
    public List<FAAdjustmentSaveFullResponse> FAAdjustment { get; set; } = [];
    public List<FAAdjustmentDetailSaveFullResponse> FAAdjustmentDetail { get; set; } = [];
    public List<FAAdjustmentDetailSaveFullResponse> FAAdjustmentDetailPost { get; set; } = [];
}

public class FAAdjustmentAvailableFixedAsset
{
    public Guid FixedAssetId { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public DateTime DepreciationDate { get; set; }
    public string DepreciationAccount { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public Guid FixedAssetCategoryId { get; set; }
    public bool IsFixedAssetOfStateBudget { get; set; }
    public bool IsNotDepreciation { get; set; }
    public bool HasDepreciationInMonth { get; set; }
    public double DepreciationAmountByIncomeTax { get; set; }
    public double SumAmountReasonableCost { get; set; }
    public double MonthlyDepreciationAmountByIncomeTax { get; set; }
    public double AccumDepreciationAmount { get; set; }
}

public class FAAdjustmentCustomFixedAsset
{
    public Guid FixedAssetId { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public DateTime IncrementDate { get; set; }
    public double OrgPrice { get; set; }
    public string OrgPriceAccount { get; set; }
    public double DepreciationAmount { get; set; }
    public string DepreciationAccount { get; set; }
    public double RealAccumDepreciationAmount { get; set; }
    public double AccumDepreciationAmount { get; set; }
    public double DepreciationAmountInMonth { get; set; }
    public double MonthlyDepreciationAmount { get; set; }
    public double LifeTimeRemainingInMonth { get; set; }
}
public class FAAdjustmentSaveFullResponse
{
    public Guid RefId { get; set; }
    public int EditVersion { get; set; }
    public Guid BranchId { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string RefNo { get; set; }
    public int RefType { get; set; }

}

public class FAAdjustmentDetailSaveFullResponse
{
    public Guid? RefDetailId { get; set; }
    public int SortOrder { get; set; }
}