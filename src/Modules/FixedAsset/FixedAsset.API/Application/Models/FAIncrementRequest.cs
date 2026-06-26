namespace FixedAsset.API.Application.Models;

#region Get

public class GetFAArisingQuery
{
    public Guid FixedAssetId { get; set; }
    public int DisplayOnBook { get; set; }
}

#endregion

#region Create

public class FixedAssetCreateRequest
{
    public Guid FixedAssetCategoryId { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public Guid? AccountObjectId { get; set; }
    public int RefType { get; set; }
    public int? ProductionYear { get; set; }
    public int State { get; set; }
    public int? Quality { get; set; }
    public int? LifeTimeUnit { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime? DeliveryRecordDate { get; set; }
    public DateTime DepreciationDate { get; set; }
    public bool? IsNotDepreciation { get; set; }
    public bool? IsLimitDepreciationAmount { get; set; }
    public bool? IsFixedAssetOfStateBudget { get; set; }
    public double? OrgPrice { get; set; }
    public double? DepreciationAmount { get; set; }
    public double? LifeTime { get; set; }
    public double? LifeTimeRemaining { get; set; }
    public double? LifeTimeInMonth { get; set; }
    public double? LifeTimeRemainingInMonth { get; set; }
    public double? DepreciationRateMonth { get; set; }
    public double? MonthlyDepreciationAmountByIncomeTax { get; set; }
    public double? DepreciationRateYear { get; set; }
    public double? MonthlyDepreciationAmount { get; set; }
    public double? YearlyDepreciationAmount { get; set; }
    public double? AccumDepreciationAmount { get; set; }
    public double? RemainingAmount { get; set; }
    public double? DepreciationAmountByIncomeTax { get; set; }
    public string RefNo { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public string Manufacturer { get; set; }
    public string MadeIn { get; set; }
    public string OrgPriceAccount { get; set; }
    public string DepreciationAccount { get; set; }
    public string Source { get; set; }
    public string CapacityMachine { get; set; }
    public string SerialNumber { get; set; }
    public string VendorName { get; set; }
    public string GuaranteeDuration { get; set; }
    public string GuaranteeCondition { get; set; }
    public string DeliveryRecordNo { get; set; }
    public string OrganizationUnitName { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string FixedAssetCategoryName { get; set; }
    public int FixedAssetState { get; set; }
    public string FixedAssetCategoryCode { get; set; }
    public string AttachmentIdList { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];
    public string AccountObjectCode { get; set; }
    public string ReasonNotDepreciation { get; set; }
    public Guid? LocationWarehouseId { get; set; }
    public string LocationWarehouseCode { get; set; }
    public string LocationWarehouseName { get; set; }
}

public class FixedAssetDetailAllocationCreateRequest
{
    public Guid ObjectId { get; set; }
    public Guid? ExpenseItemId { get; set; }
    public Guid? ListItemId { get; set; }
    public int SortOrder { get; set; }
    public int ObjectType { get; set; }
    public int AllocationRate { get; set; }
    public string CostAccount { get; set; }
    public string ObjectCode { get; set; }
    public string ObjectName { get; set; }
    public int State { get; set; }
    public string ListItemCode { get; set; }
    public string ExpenseItemCode { get; set; }
    public Guid? LocationWarehouseId { get; set; }
    public string LocationWarehouseCode { get; set; }
    public string LocationWarehouseName { get; set; }
}

public class FixedAssetDetailSourceCreateRequest
{
    public Guid RefId { get; set; }
    public Guid RefDetailId { get; set; }
    public int SortOrder { get; set; }
    public int RefType { get; set; }
    public string JournalMemo { get; set; }
    public string CreditAccount { get; set; }
    public string DebitAccount { get; set; }
    public string RefNo { get; set; }
    public int Amount { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime PostedDate { get; set; }
    public int State { get; set; }
    public int DetailPostOrder { get; set; }
}

public class FixedAssetDetailCreateRequest
{
    public int SortOrder { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string WarrantyTime { get; set; }
    public string Unit { get; set; }
    public int State { get; set; }
}

public class FixedAssetDetailAccessoryCreateRequest
{
    public int SortOrder { get; set; }
    public int Quantity { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; }
    public string Unit { get; set; }
    public int State { get; set; }
}

#endregion

#region Update

public class FixedAssetUpdateRequest
{
    public Guid FixedAssetId { get; set; }
    public Guid FixedAssetCategoryId { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public Guid BranchId { get; set; }
    public Guid AccountObjectId { get; set; }
    public string AccountObjectCode { get; set; }
    public Guid RefId { get; set; }
    public int ProductionYear { get; set; }
    public int? Quality { get; set; }
    public int? LifeTimeUnit { get; set; }
    public int? LifeTimeRemainingUnit { get; set; }
    public long? RefOrder { get; set; }
    public int? Inactive { get; set; }
    public int? DisplayOnBook { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime DeliveryRecordDate { get; set; }
    public DateTime DepreciationDate { get; set; }
    public bool? IsNotDepreciation { get; set; }
    public bool? IsLimitDepreciationAmount { get; set; }
    public bool? IsEnoughVoucher { get; set; }
    public bool? IsPostedManagement { get; set; }
    public bool? IsPostedFinance { get; set; }
    public bool? IsFixedAssetOfStateBudget { get; set; }
    public int? Quantity { get; set; }
    public double? OrgPrice { get; set; }
    public double? DepreciationAmount { get; set; }
    public double? LifeTime { get; set; }
    public double? LifeTimeRemaining { get; set; }
    public double? DepreciationRateMonth { get; set; }
    public double? RemainingAmountByIncomeTax { get; set; }
    public double? MonthlyDepreciationAmountByIncomeTax { get; set; }
    public double? MonthlyDepreciationAmount { get; set; }
    public double? YearlyDepreciationAmount { get; set; }
    public double? AccumDepreciationAmount { get; set; }
    public double? RemainingAmount { get; set; }
    public double? DepreciationAmountByIncomeTax { get; set; }
    public string RefNo { get; set; }
    public string FixedAssetCode { get; set; }
    public string FixedAssetName { get; set; }
    public string Manufacturer { get; set; }
    public string OrgPriceAccount { get; set; }
    public string MadeIn { get; set; }
    public string DepreciationAccount { get; set; }
    public string Source { get; set; }
    public string CapacityMachine { get; set; }
    public string SerialNumber { get; set; }
    public string VendorName { get; set; }
    public string GuaranteeDuration { get; set; }
    public string GuaranteeCondition { get; set; }
    public string DeliveryRecordNo { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public string FixedAssetCategoryName { get; set; }
    public double? LifeTimeInMonth { get; set; }
    public double? LifeTimeRemainingInMonth { get; set; }
    public double? DepreciationRateYear { get; set; }
    public int? FixedAssetState { get; set; }
    public string FixedAssetCategoryCode { get; set; }
    public string AttachmentIdList { get; set; }
    public string BranchName { get; set; }
    public string ReasonNotDepreciation { get; set; }
    public bool? IsMappingAms { get; set; }
    public int? ExcelRowIndex { get; set; }
    public bool? IsValid { get; set; }
    public int RefType { get; set; }
    public List<AttachmentIdListData> AttachmentIdListData { get; set; } = [];
    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    public bool? AutoRefNo { get; set; }
    public bool? ForceUpdate { get; set; }
    public int EditVersion { get; set; }
    public int State { get; set; }
    public Guid? LocationWarehouseId { get; set; }
    public string LocationWarehouseCode { get; set; }
    public string LocationWarehouseName { get; set; }

    public FixedAssetDto OldData { get; set; }
}

public class FixedAssetDetailAllocationUpdateRequest
{
    public Guid FixedAssetDetailId { get; set; }
    public Guid FixedAssetId { get; set; }
    public Guid ObjectId { get; set; }
    public Guid ExpenseItemId { get; set; }
    public Guid ListItemId { get; set; }
    public int SortOrder { get; set; }
    public int ObjectType { get; set; }
    public double AllocationRate { get; set; }
    public string CostAccount { get; set; }
    public string ObjectCode { get; set; }
    public string ObjectName { get; set; }
    public string ExpenseItemCode { get; set; }
    public string ListItemCode { get; set; }
    public int EditVersion { get; set; }
    public int State { get; set; }
    public FixedAssetDetailAllocationDto OldData { get; set; }

}

public class FixedAssetDetailSourceUpdateRequest
{
    public Guid FixedAssetDetailId { get; set; }
    public Guid FixedAssetId { get; set; }
    public Guid RefId { get; set; }
    public Guid RefDetailId { get; set; }
    public int SortOrder { get; set; }
    public int RefType { get; set; }
    public string JournalMemo { get; set; }
    public string CreditAccount { get; set; }
    public string DebitAccount { get; set; }
    public string RefNo { get; set; }
    public double Amount { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime PostedDate { get; set; }
    public int? DetailPostOrder { get; set; }
    public int EditVersion { get; set; }
    public int State { get; set; }
    public FixedAssetDetailSourceDto OldData { get; set; }
}

public class FixedAssetDetailUpdateRequest
{
    public Guid FixedAssetDetailId { get; set; }
    public Guid FixedAssetId { get; set; }
    public int SortOrder { get; set; }
    public double? Quantity { get; set; }
    public string Description { get; set; }
    public string WarrantyTime { get; set; }
    public string Unit { get; set; }
    public int EditVersion { get; set; }
    public int State { get; set; }
    public FixedAssetDetailDto OldData { get; set; }

}

public class FixedAssetDetailAccessoryUpdateRequest
{
    public Guid FixedAssetDetailId { get; set; }
    public Guid FixedAssetId { get; set; }
    public int SortOrder { get; set; }
    public double? Quantity { get; set; }
    public double? Amount { get; set; }
    public string Description { get; set; }
    public string Unit { get; set; }
    public int EditVersion { get; set; }
    public int State { get; set; }
    public FixedAssetDetailAccessoryDto OldData { get; set; }
}

#endregion
