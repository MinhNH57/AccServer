namespace FixedAsset.Domain.AggregatesModel.FixedAssetAggregate;

public class FixedAsset :
    Entity, IAggregateRoot
{
    public Guid? FixedAssetCategoryId { get; private set; }

    public Guid? OrganizationUnitId { get; private set; }

    public Guid? BranchId { get; private set; }

    public Guid? AccountObjectId { get; private set; }

    public string AccountObjectCode { get; private set; }

    public Guid RefId { get; private set; }

    public int? ProductionYear { get; private set; }

    public int? Quality { get; private set; }

    public int? LifeTimeUnit { get; private set; }

    public int? LifeTimeRemainingUnit { get; private set; }

    public int RefOrder { get; private set; }

    public int? Inactive { get; private set; }

    public int? DisplayOnBook { get; private set; }

    public DateTime RefDate { get; private set; }

    public DateTime? DeliveryRecordDate { get; private set; }

    public DateTime DepreciationDate { get; private set; }

    public bool? IsNotDepreciation { get; private set; }

    public bool? IsLimitDepreciationAmount { get; private set; }

    public bool? IsEnoughVoucher { get; private set; }

    public bool? IsPostedManagement { get; private set; }

    public bool? IsPostedFinance { get; private set; }

    public bool? IsFixedAssetOfStateBudget { get; private set; }

    public int? Quantity { get; private set; }

    public double? OrgPrice { get; private set; }

    public double? DepreciationAmount { get; private set; }

    public double? LifeTime { get; private set; }

    public double? LifeTimeRemaining { get; private set; }

    public double? DepreciationRateMonth { get; private set; }

    public double? RemainingAmountByIncomeTax { get; private set; }

    public double? MonthlyDepreciationAmountByIncomeTax { get; private set; }

    public double? MonthlyDepreciationAmount { get; private set; }

    public double? YearlyDepreciationAmount { get; private set; }

    public double? AccumDepreciationAmount { get; private set; }

    public double? RemainingAmount { get; private set; }

    public double? DepreciationAmountByIncomeTax { get; private set; }

    public string RefNo { get; private set; }

    public string FixedAssetCode { get; private set; }

    public string FixedAssetName { get; private set; }

    public string Manufacturer { get; private set; }

    public string OrgPriceAccount { get; private set; }

    public string MadeIn { get; private set; }

    public string DepreciationAccount { get; private set; }

    public string Source { get; private set; }

    public string CapacityMachine { get; private set; }

    public string SerialNumber { get; private set; }

    public string VendorName { get; private set; }

    public string GuaranteeDuration { get; private set; }

    public string GuaranteeCondition { get; private set; }

    public string DeliveryRecordNo { get; private set; }

    public string OrganizationUnitCode { get; private set; }

    public string OrganizationUnitName { get; private set; }

    public string FixedAssetCategoryName { get; private set; }

    public double? LifeTimeInMonth { get; private set; }

    public double? LifeTimeRemainingInMonth { get; private set; }

    public double? DepreciationRateYear { get; private set; }

    public int? FixedAssetState { get; private set; }

    public string FixedAssetCategoryCode { get; private set; }

    public string BranchName { get; private set; }

    public string ReasonNotDepreciation { get; private set; }

    public bool? IsMappingSmart { get; private set; }

    public int? ExcelRowIndex { get; private set; }

    public bool? IsValid { get; private set; }

    public int RefType { get; private set; }

    public string AttachmentIdList { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public string CreatedBy { get; private set; }

    public DateTime ModifiedDate { get; private set; }

    public string ModifiedBy { get; private set; }

    public bool? AutoRefNo { get; private set; }

    public bool? ForceUpdate { get; private set; }

    public int EditVersion { get; private set; }

    public int State { get; private set; }

    public Guid? LocationWarehouseId { get; set; }

    public string LocationWarehouseCode { get; set; }

    public string LocationWarehouseName { get; set; }


    private readonly List<FixedAssetDetailAllocation> _fixedAssetDetailAllocations;

    public IReadOnlyCollection<FixedAssetDetailAllocation> FixedAssetDetailAllocations =>
        _fixedAssetDetailAllocations.AsReadOnly();

    private readonly List<FixedAssetDetailSource> _fixedAssetDetailSources;

    public IReadOnlyCollection<FixedAssetDetailSource> FixedAssetDetailSources => _fixedAssetDetailSources.AsReadOnly();

    private readonly List<FixedAssetDetail> _fixedAssetDetails;

    public IReadOnlyCollection<FixedAssetDetail> FixedAssetDetails => _fixedAssetDetails.AsReadOnly();

    private readonly List<FixedAssetDetailAccessory> _fixedAssetDetailAccessories;

    public IReadOnlyCollection<FixedAssetDetailAccessory> FixedAssetDetailAccessories =>
        _fixedAssetDetailAccessories.AsReadOnly();

    protected FixedAsset()
    {
        _fixedAssetDetailAllocations = [];
        _fixedAssetDetailSources = [];
        _fixedAssetDetails = [];
        _fixedAssetDetailAccessories = [];
    }

    public FixedAsset(
        Guid fixedAssetCategoryId,
        Guid organizationUnitId,
        Guid? branchId,
        Guid? accountObjectId,
        string accountObjectCode,
        int? productionYear,
        int? quality,
        int? lifeTimeUnit,
        int? lifeTimeRemainingUnit,
        int? inactive,
        int? displayOnBook,
        DateTime refDate,
        DateTime? deliveryRecordDate,
        DateTime depreciationDate,
        bool? isNotDepreciation,
        bool? isLimitDepreciationAmount,
        bool? isEnoughVoucher,
        bool? isPostedManagement,
        bool? isPostedFinance,
        bool? isFixedAssetOfStateBudget,
        int? quantity,
        double? orgPrice,
        double? depreciationAmount,
        double? lifeTime,
        double? lifeTimeRemaining,
        double? depreciationRateMonth,
        double? remainingAmountByIncomeTax,
        double? monthlyDepreciationAmountByIncomeTax,
        double? monthlyDepreciationAmount,
        double? yearlyDepreciationAmount,
        double? accumDepreciationAmount,
        double? remainingAmount,
        double? depreciationAmountByIncomeTax,
        string refNo,
        string fixedAssetCode,
        string fixedAssetName,
        string manufacturer,
        string orgPriceAccount,
        string madeIn,
        string depreciationAccount,
        string source,
        string capacityMachine,
        string serialNumber,
        string vendorName,
        string guaranteeDuration,
        string guaranteeCondition,
        string deliveryRecordNo,
        string organizationUnitCode,
        string organizationUnitName,
        string fixedAssetCategoryName,
        double? lifeTimeInMonth,
        double? lifeTimeRemainingInMonth,
        double? depreciationRateYear,
        int? fixedAssetState,
        string fixedAssetCategoryCode,
        string branchName,
        string reasonNotDepreciation,
        bool? isMappingSmart,
        int? excelRowIndex,
        bool? isValid,
        int refType,
        string attachmentIdList,
        string createdBy,
        bool? autoRefNo,
        bool? forceUpdate,
        int editVersion,
        Guid? locationWarehouseId,
        string locationWarehouseCode,
        string locationWarehouseName) : this()
    {
        Id = Guid.NewGuid();
        FixedAssetCategoryId = fixedAssetCategoryId;
        OrganizationUnitId = organizationUnitId;
        BranchId = branchId;
        AccountObjectId = accountObjectId;
        AccountObjectCode = accountObjectCode;
        RefId = Guid.NewGuid();
        ProductionYear = productionYear;
        Quality = quality;
        LifeTimeUnit = lifeTimeUnit;
        LifeTimeRemainingUnit = lifeTimeRemainingUnit;
        Inactive = inactive;
        DisplayOnBook = displayOnBook;
        RefDate = refDate;
        DeliveryRecordDate = deliveryRecordDate;
        DepreciationDate = depreciationDate;
        IsNotDepreciation = isNotDepreciation;
        IsLimitDepreciationAmount = isLimitDepreciationAmount;
        IsEnoughVoucher = isEnoughVoucher;
        IsPostedManagement = isPostedManagement;
        IsPostedFinance = isPostedFinance;
        IsFixedAssetOfStateBudget = isFixedAssetOfStateBudget;
        Quantity = quantity;
        OrgPrice = orgPrice;
        DepreciationAmount = depreciationAmount;
        LifeTime = lifeTime;
        LifeTimeRemaining = lifeTimeRemaining;
        DepreciationRateMonth = depreciationRateMonth;
        RemainingAmountByIncomeTax = remainingAmountByIncomeTax;
        MonthlyDepreciationAmountByIncomeTax = monthlyDepreciationAmountByIncomeTax;
        MonthlyDepreciationAmount = monthlyDepreciationAmount;
        YearlyDepreciationAmount = yearlyDepreciationAmount;
        AccumDepreciationAmount = accumDepreciationAmount;
        RemainingAmount = remainingAmount;
        DepreciationAmountByIncomeTax = depreciationAmountByIncomeTax;
        RefNo = refNo;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        Manufacturer = manufacturer;
        OrgPriceAccount = orgPriceAccount;
        MadeIn = madeIn;
        DepreciationAccount = depreciationAccount;
        Source = source;
        CapacityMachine = capacityMachine;
        SerialNumber = serialNumber;
        VendorName = vendorName;
        GuaranteeDuration = guaranteeDuration;
        GuaranteeCondition = guaranteeCondition;
        DeliveryRecordNo = deliveryRecordNo;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        FixedAssetCategoryName = fixedAssetCategoryName;
        LifeTimeInMonth = lifeTimeInMonth;
        LifeTimeRemainingInMonth = lifeTimeRemainingInMonth;
        DepreciationRateYear = depreciationRateYear;
        FixedAssetState = fixedAssetState;
        FixedAssetCategoryCode = fixedAssetCategoryCode;
        BranchName = branchName;
        ReasonNotDepreciation = reasonNotDepreciation;
        IsMappingSmart = isMappingSmart;
        ExcelRowIndex = excelRowIndex;
        IsValid = isValid;
        RefType = refType;
        AttachmentIdList = attachmentIdList;
        CreatedDate = DateTime.Now;
        CreatedBy = createdBy;
        ModifiedDate = DateTime.Now;
        ModifiedBy = createdBy;
        AutoRefNo = autoRefNo;
        ForceUpdate = forceUpdate;
        EditVersion = editVersion;
        LocationWarehouseId = locationWarehouseId;
        LocationWarehouseCode = locationWarehouseCode;
        LocationWarehouseName = locationWarehouseName;

        AddCreatedDomainEvent();
    }

    public FixedAsset Update(
        Guid fixedAssetCategoryId,
        Guid organizationUnitId,
        Guid? branchId,
        Guid? accountObjectId,
        string accountObjectCode,
        int? productionYear,
        int? quality,
        int? lifeTimeUnit,
        int? lifeTimeRemainingUnit,
        int? inactive,
        int? displayOnBook,
        DateTime refDate,
        DateTime? deliveryRecordDate,
        DateTime depreciationDate,
        bool? isNotDepreciation,
        bool? isLimitDepreciationAmount,
        bool? isEnoughVoucher,
        bool? isPostedManagement,
        bool? isPostedFinance,
        bool? isFixedAssetOfStateBudget,
        int? quantity,
        double? orgPrice,
        double? depreciationAmount,
        double? lifeTime,
        double? lifeTimeRemaining,
        double? depreciationRateMonth,
        double? remainingAmountByIncomeTax,
        double? monthlyDepreciationAmountByIncomeTax,
        double? monthlyDepreciationAmount,
        double? yearlyDepreciationAmount,
        double? accumDepreciationAmount,
        double? remainingAmount,
        double? depreciationAmountByIncomeTax,
        string refNo,
        string fixedAssetCode,
        string fixedAssetName,
        string manufacturer,
        string orgPriceAccount,
        string madeIn,
        string depreciationAccount,
        string source,
        string capacityMachine,
        string serialNumber,
        string vendorName,
        string guaranteeDuration,
        string guaranteeCondition,
        string deliveryRecordNo,
        string organizationUnitCode,
        string organizationUnitName,
        string fixedAssetCategoryName,
        double? lifeTimeInMonth,
        double? lifeTimeRemainingInMonth,
        double? depreciationRateYear,
        int? fixedAssetState,
        string fixedAssetCategoryCode,
        string branchName,
        string reasonNotDepreciation,
        bool? isMappingSmart,
        int? excelRowIndex,
        bool? isValid,
        string attachmentIdList,
        string modifiedBy,
        bool? autoRefNo,
        bool? forceUpdate,
        int editVersion,
        Guid? locationWarehouseId,
        string locationWarehouseCode,
        string locationWarehouseName)
    {
        FixedAssetCategoryId = fixedAssetCategoryId;
        OrganizationUnitId = organizationUnitId;
        BranchId = branchId;
        AccountObjectId = accountObjectId;
        AccountObjectCode = accountObjectCode;
        ProductionYear = productionYear;
        Quality = quality;
        LifeTimeUnit = lifeTimeUnit;
        LifeTimeRemainingUnit = lifeTimeRemainingUnit;
        Inactive = inactive;
        DisplayOnBook = displayOnBook;
        RefDate = refDate;
        DeliveryRecordDate = deliveryRecordDate;
        DepreciationDate = depreciationDate;
        IsNotDepreciation = isNotDepreciation;
        IsLimitDepreciationAmount = isLimitDepreciationAmount;
        IsEnoughVoucher = isEnoughVoucher;
        IsPostedManagement = isPostedManagement;
        IsPostedFinance = isPostedFinance;
        IsFixedAssetOfStateBudget = isFixedAssetOfStateBudget;
        Quantity = quantity;
        OrgPrice = orgPrice;
        DepreciationAmount = depreciationAmount;
        LifeTime = lifeTime;
        LifeTimeRemaining = lifeTimeRemaining;
        DepreciationRateMonth = depreciationRateMonth;
        RemainingAmountByIncomeTax = remainingAmountByIncomeTax;
        MonthlyDepreciationAmountByIncomeTax = monthlyDepreciationAmountByIncomeTax;
        MonthlyDepreciationAmount = monthlyDepreciationAmount;
        YearlyDepreciationAmount = yearlyDepreciationAmount;
        AccumDepreciationAmount = accumDepreciationAmount;
        RemainingAmount = remainingAmount;
        DepreciationAmountByIncomeTax = depreciationAmountByIncomeTax;
        RefNo = refNo;
        FixedAssetCode = fixedAssetCode;
        FixedAssetName = fixedAssetName;
        Manufacturer = manufacturer;
        OrgPriceAccount = orgPriceAccount;
        MadeIn = madeIn;
        DepreciationAccount = depreciationAccount;
        Source = source;
        CapacityMachine = capacityMachine;
        SerialNumber = serialNumber;
        VendorName = vendorName;
        GuaranteeDuration = guaranteeDuration;
        GuaranteeCondition = guaranteeCondition;
        DeliveryRecordNo = deliveryRecordNo;
        OrganizationUnitCode = organizationUnitCode;
        OrganizationUnitName = organizationUnitName;
        FixedAssetCategoryName = fixedAssetCategoryName;
        LifeTimeInMonth = lifeTimeInMonth;
        LifeTimeRemainingInMonth = lifeTimeRemainingInMonth;
        DepreciationRateYear = depreciationRateYear;
        FixedAssetState = fixedAssetState;
        FixedAssetCategoryCode = fixedAssetCategoryCode;
        BranchName = branchName;
        ReasonNotDepreciation = reasonNotDepreciation;
        IsMappingSmart = isMappingSmart;
        ExcelRowIndex = excelRowIndex;
        IsValid = isValid;
        AttachmentIdList = attachmentIdList;
        ModifiedDate = DateTime.Now;
        ModifiedBy = modifiedBy;
        AutoRefNo = autoRefNo;
        ForceUpdate = forceUpdate;
        EditVersion = editVersion;
        LocationWarehouseId = locationWarehouseId;
        LocationWarehouseCode = locationWarehouseCode;
        LocationWarehouseName = locationWarehouseName;

        AddUpdatedDomainEvent();

        return this;
    }

    public FixedAssetDetailAllocation AddFixedAssetDetailAllocation(
        Guid objectId,
        Guid? expenseItemId,
        Guid? listItemId,
        int sortOrder,
        int objectType,
        double allocationRate,
        string costAccount,
        string objectCode,
        string objectName,
        string expenseItemCode,
        string listItemCode,
        int editVersion)
    {
        var existingFixedAssetDetailAllocation = _fixedAssetDetailAllocations.SingleOrDefault(f => f.ObjectId == objectId && f.ExpenseItemId == expenseItemId && f.CostAccount == costAccount && f.ListItemId == listItemId);
        if (existingFixedAssetDetailAllocation != null)
        {
            throw new FixedAssetDomainException("Không được tồn tại 2 dòng trùng nhau có cùng Đối tượng phân bổ, Tài khoản chi phí, Khoản mục chi phí và Mã thống kê.");
        }
        else
        {
            var fixedAssetDetailAllocation = new FixedAssetDetailAllocation(
                objectId,
                expenseItemId,
                listItemId,
                sortOrder,
                objectType,
                allocationRate,
                costAccount,
                objectCode,
                objectName,
                expenseItemCode,
                listItemCode,
                editVersion);

            _fixedAssetDetailAllocations.Add(fixedAssetDetailAllocation);

            return fixedAssetDetailAllocation;
        }
    }

    public FixedAssetDetailAllocation UpdateFixedAssetDetailAllocation(
        Guid fixedAssetDetailId,
        Guid objectId,
        Guid? expenseItemId,
        Guid? listItemId,
        int sortOrder,
        int objectType,
        double allocationRate,
        string costAccount,
        string objectCode,
        string objectName,
        string expenseItemCode,
        string listItemCode,
        int editVersion)
    {
        var fixedAssetDetailAllocation = _fixedAssetDetailAllocations.FirstOrDefault(f => fixedAssetDetailId == f.Id) ?? throw new FixedAssetDomainException($"Không tìm thấy thiết lập phân bổ với Id = '{fixedAssetDetailId}'.");
        var existingFixedAssetDetailAllocation = _fixedAssetDetailAllocations.SingleOrDefault(f => fixedAssetDetailId != f.Id && f.ObjectId == objectId && f.ExpenseItemId == expenseItemId && f.CostAccount == costAccount && f.ListItemId == listItemId);

        return existingFixedAssetDetailAllocation != null
            ? throw new FixedAssetDomainException("Không được tồn tại 2 dòng trùng nhau có cùng Đối tượng phân bổ, Tài khoản chi phí, Khoản mục chi phí và Mã thống kê.")
            : fixedAssetDetailAllocation.Update(
                objectId,
                expenseItemId,
                listItemId,
                sortOrder,
                objectType,
                allocationRate,
                costAccount,
                objectCode,
                objectName,
                expenseItemCode,
                listItemCode,
                editVersion);
    }

    public FixedAssetDetailSource AddFixedAssetDetailSource(
        Guid refId,
        Guid refDetailId,
        int sortOrder,
        int refType,
        string journalMemo,
        string creditAccount,
        string debitAccount,
        string refNo,
        double amount,
        DateTime refDate,
        DateTime postedDate,
        int? detailPostOrder,
        int editVersion)
    {
        var existingFixedAssetDetailSource = _fixedAssetDetailSources.SingleOrDefault(f => f.RefId == refId);

        if (existingFixedAssetDetailSource != null)
        {
            throw new FixedAssetDomainException("Không được tồn tại 2 dòng trùng nhau có cùng Số chứng từ.");
        }
        else
        {
            var fixedAssetDetailSource = new FixedAssetDetailSource(
                refId,
                refDetailId,
                sortOrder,
                refType,
                journalMemo,
                creditAccount,
                debitAccount,
                refNo,
                amount,
                refDate,
                postedDate,
                detailPostOrder,
                editVersion);

            _fixedAssetDetailSources.Add(fixedAssetDetailSource);

            return fixedAssetDetailSource;
        }
    }

    public FixedAssetDetailSource UpdateFixedAssetDetailSource(
        Guid fixedAssetDetailId,
        Guid refId,
        Guid refDetailId,
        int sortOrder,
        int refType,
        string journalMemo,
        string creditAccount,
        string debitAccount,
        string refNo,
        double amount,
        DateTime refDate,
        DateTime postedDate,
        int? detailPostOrder,
        int editVersion)
    {
        var fixedAssetDetailSource = _fixedAssetDetailSources.FirstOrDefault(f => fixedAssetDetailId == f.Id) ?? throw new FixedAssetDomainException($"Không tìm thấy nguồn gốc hình thành với Id = '{fixedAssetDetailId}'.");
        var existingFixedAssetDetailSource = _fixedAssetDetailSources.SingleOrDefault(f => fixedAssetDetailId != f.Id && f.RefId == refId);

        return existingFixedAssetDetailSource != null
            ? throw new FixedAssetDomainException("Không được tồn tại 2 dòng trùng nhau có cùng Số chứng từ.")
            : fixedAssetDetailSource.Update(
                refId,
                refDetailId,
                sortOrder,
                refType,
                journalMemo,
                creditAccount,
                debitAccount,
                refNo,
                amount,
                refDate,
                postedDate,
                detailPostOrder,
                editVersion);
    }

    public FixedAssetDetail AddFixedAssetDetail(
        int sortOrder,
        double? quantity,
        string description,
        string warrantyTime,
        string unit,
        int editVersion)
    {
        var fixedAssetDetail = new FixedAssetDetail(
            sortOrder,
            quantity,
            description,
            warrantyTime,
            unit,
            editVersion);

        _fixedAssetDetails.Add(fixedAssetDetail);

        return fixedAssetDetail;
    }

    public FixedAssetDetail UpdateFixedAssetDetail(
        Guid fixedAssetDetailId,
        int sortOrder,
        double? quantity,
        string description,
        string warrantyTime,
        string unit,
        int editVersion)
    {
        var fixedAssetDetail = _fixedAssetDetails.FirstOrDefault(f => fixedAssetDetailId == f.Id);
        return fixedAssetDetail == null
            ? throw new FixedAssetDomainException($"Không tìm thấy bộ phận cấu thành với Id = '{fixedAssetDetailId}'.")
            : fixedAssetDetail.Update(
                sortOrder,
                quantity,
                description,
                warrantyTime,
                unit,
                editVersion);
    }

    public FixedAssetDetailAccessory AddFixedAssetDetailAccessory(
        int sortOrder,
        double? quantity,
        double? amount,
        string description,
        string unit,
        int editVersion)
    {
        var fixedAssetDetailAccessory = new FixedAssetDetailAccessory(
            sortOrder,
            quantity,
            amount,
            description,
            unit,
            editVersion);

        _fixedAssetDetailAccessories.Add(fixedAssetDetailAccessory);

        return fixedAssetDetailAccessory;
    }

    public FixedAssetDetailAccessory UpdateFixedAssetDetailAccessory(
        Guid fixedAssetDetailId,
        int sortOrder,
        double? quantity,
        double? amount,
        string description,
        string unit,
        int editVersion)
    {
        var fixedAssetDetailAccessory = _fixedAssetDetailAccessories.FirstOrDefault(f => fixedAssetDetailId == f.Id);
        return fixedAssetDetailAccessory == null
            ? throw new FixedAssetDomainException($"Không tìm thấy dụng cụ, phụ tùng kèm theo với Id = '{fixedAssetDetailId}'.")
            : fixedAssetDetailAccessory.Update(
                sortOrder,
                quantity,
                amount,
                description,
                unit,
                editVersion);
    }

    public void ClearDetailData()
    {
        _fixedAssetDetailAllocations.Clear();
        _fixedAssetDetailSources.Clear();
        _fixedAssetDetails.Clear();
        _fixedAssetDetailAccessories.Clear();
    }

    private void AddCreatedDomainEvent()
    {
        var @event = new FixedAssetCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddUpdatedDomainEvent()
    {
        var @event = new FixedAssetUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
