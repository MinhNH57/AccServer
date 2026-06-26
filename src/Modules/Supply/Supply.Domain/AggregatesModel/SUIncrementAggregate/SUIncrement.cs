namespace Supply.Domain.AggregatesModel.SUIncrementAggregate;

public class SUIncrement :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; private set; }
    public Guid? BranchId { get; private set; }
    public Guid? SupplyCategoryId { get; private set; }
    public Guid? SUAuditRefId { get; private set; }
    public Guid? SupplyOtherBookId { get; private set; }
    public Guid? FADecrementRefId { get; private set; }
    public int RefType { get; private set; }
    public int? AllocationTime { get; private set; }
    public int? RemainingAllocationTime { get; private set; }
    public int? DisplayOnBook { get; private set; }
    public int? RefOrder { get; private set; }
    public DateTime? RefDate { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsPostedManagement { get; private set; }
    public bool IsPostedFinance { get; private set; }
    public bool SuspendAllocate { get; private set; }
    public double? Quantity { get; private set; }
    public double? UnitPrice { get; private set; }
    public double? Amount { get; private set; }
    public double? AllocatedAmount { get; private set; }
    public double? RemainingAmount { get; private set; }
    public double? TermlyAllocationAmount { get; private set; }
    public string SupplyCode { get; private set; }
    public string SupplyName { get; private set; }
    public string RefNo { get; private set; }
    public string Unit { get; private set; }
    public string AllocationAccount { get; private set; }
    public string CreatedBy { get; private set; }
    public string ModifiedBy { get; private set; }
    public Guid? InPuRefDetailId { get; private set; }
    public string ReasonIncrement { get; private set; }
    public string SupplyGroup { get; private set; }
    public string SupplyCategoryCode { get; private set; }
    public string SupplyCategoryName { get; private set; }
    public int? State { get; private set; }
    public int? EditVersion { get; private set; }
    public string BranchName { get; private set; }
    public string ReasonInactive { get; private set; }

    private readonly List<SUIncrementDetailDepartment> _sUIncrementDetailDepartments;

    public IReadOnlyCollection<SUIncrementDetailDepartment> SUIncrementDetailDepartments =>
        _sUIncrementDetailDepartments.AsReadOnly();

    private readonly List<SUIncrementDetailAllocation> _sUIncrementDetailAllocations;

    public IReadOnlyCollection<SUIncrementDetailAllocation> SUIncrementDetailAllocations =>
        _sUIncrementDetailAllocations.AsReadOnly();

    private readonly List<SUIncrementDetail> _sUIncrementDetails;

    public IReadOnlyCollection<SUIncrementDetail> SUIncrementDetails => _sUIncrementDetails.AsReadOnly();


    private readonly List<SUIncrementDetailSource> _sUIncrementDetailSources;

    public IReadOnlyCollection<SUIncrementDetailSource> SUIncrementDetailSources => _sUIncrementDetailSources.AsReadOnly();

    protected SUIncrement()
    {
        _sUIncrementDetailDepartments = [];
        _sUIncrementDetailAllocations = [];
        _sUIncrementDetails = [];
        _sUIncrementDetailSources = [];
    }

    public SUIncrement(
        Guid? tenantId,
        Guid? branchId,
        Guid? supplyCategoryId,
        Guid? sUAuditRefId,
        Guid? supplyOtherBookId,
        Guid? fADecrementRefId,
        int refType,
        int? allocationTime,
        int? remainingAllocationTime,
        int? displayOnBook,
        int? refOrder,
        DateTime? refDate,
        DateTime? createdDate,
        DateTime? modifiedDate,
        bool isPostedManagement,
        bool isPostedFinance,
        bool suspendAllocate,
        double? quantity,
        double? unitPrice,
        double? amount,
        double? allocatedAmount,
        double? remainingAmount,
        double? termlyAllocationAmount,
        string supplyCode,
        string supplyName,
        string refNo,
        string unit,
        string allocationAccount,
        string createdBy,
        string modifiedBy,
        Guid? inPuRefDetailId,
        string reasonIncrement,
        string supplyGroup,
        string supplyCategoryCode,
        string supplyCategoryName,
        int? state,
        int? editVersion,
        string branchName,
        string reasonInactive) : this()
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        BranchId = branchId;
        SupplyCategoryId = supplyCategoryId;
        SUAuditRefId = sUAuditRefId;
        SupplyOtherBookId = supplyOtherBookId;
        FADecrementRefId = fADecrementRefId;
        RefType = refType;
        AllocationTime = allocationTime;
        RemainingAllocationTime = remainingAllocationTime;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        IsPostedManagement = isPostedManagement;
        IsPostedFinance = isPostedFinance;
        SuspendAllocate = suspendAllocate;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Amount = amount;
        AllocatedAmount = allocatedAmount;
        RemainingAmount = remainingAmount;
        TermlyAllocationAmount = termlyAllocationAmount;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        RefNo = refNo;
        Unit = unit;
        AllocationAccount = allocationAccount;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        InPuRefDetailId = inPuRefDetailId;
        ReasonIncrement = reasonIncrement;
        SupplyGroup = supplyGroup;
        SupplyCategoryCode = supplyCategoryCode;
        SupplyCategoryName = supplyCategoryName;
        State = state;
        EditVersion = editVersion;
        BranchName = branchName;
        ReasonInactive = reasonInactive;

        AddSUIncrementCreatedDomainEvent();
    }

    public SUIncrement Update(
        Guid? tenantId,
        Guid? branchId,
        Guid? supplyCategoryId,
        Guid? sUAuditRefId,
        Guid? supplyOtherBookId,
        Guid? fADecrementRefId,
        int? allocationTime,
        int? remainingAllocationTime,
        int? displayOnBook,
        int? refOrder,
        DateTime? refDate,
        DateTime? modifiedDate,
        bool isPostedManagement,
        bool isPostedFinance,
        bool suspendAllocate,
        double? quantity,
        double? unitPrice,
        double? amount,
        double? allocatedAmount,
        double? remainingAmount,
        double? termlyAllocationAmount,
        string supplyCode,
        string supplyName,
        string refNo,
        string unit,
        string allocationAccount,
        string modifiedBy,
        Guid? inPuRefDetailId,
        string reasonIncrement,
        string supplyGroup,
        string supplyCategoryCode,
        string supplyCategoryName,
        int? state,
        int? editVersion,
        string branchName,
        string reasonInactive)
    {
        TenantId = tenantId;
        BranchId = branchId;
        SupplyCategoryId = supplyCategoryId;
        SUAuditRefId = sUAuditRefId;
        SupplyOtherBookId = supplyOtherBookId;
        FADecrementRefId = fADecrementRefId;
        AllocationTime = allocationTime;
        RemainingAllocationTime = remainingAllocationTime;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        RefDate = refDate;
        ModifiedDate = modifiedDate;
        IsPostedManagement = isPostedManagement;
        IsPostedFinance = isPostedFinance;
        SuspendAllocate = suspendAllocate;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Amount = amount;
        AllocatedAmount = allocatedAmount;
        RemainingAmount = remainingAmount;
        TermlyAllocationAmount = termlyAllocationAmount;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        RefNo = refNo;
        Unit = unit;
        AllocationAccount = allocationAccount;
        ModifiedBy = modifiedBy;
        InPuRefDetailId = inPuRefDetailId;
        ReasonIncrement = reasonIncrement;
        SupplyGroup = supplyGroup;
        SupplyCategoryCode = supplyCategoryCode;
        SupplyCategoryName = supplyCategoryName;
        State = state;
        EditVersion = editVersion;
        BranchName = branchName;
        ReasonInactive = reasonInactive;

        AddSUIncrementUpdatedDomainEvent();

        return this;
    }

    public SUIncrementDetailDepartment AddSUIncrementDetailDepartment(
        Guid? tenantId,
        Guid? organizationUnitId,
        int sortOrder,
        int? allocationTime,
        int? remainingAllocationTime,
        double? quantity,
        double? unitPrice,
        double? amount,
        double? allocatedAmount,
        string organizationUnitCode,
        string organizationUnitName,
        int? organizationUnitType,
        int state,
        int? editVersion)
    {
        var department = new SUIncrementDetailDepartment(
            tenantId,
            organizationUnitId,
            sortOrder,
            allocationTime,
            remainingAllocationTime,
            quantity,
            unitPrice,
            amount,
            allocatedAmount,
            organizationUnitCode,
            organizationUnitName,
            organizationUnitType,
            state,
            editVersion);

        _sUIncrementDetailDepartments.Add(department);

        return department;
    }

    public SUIncrementDetailAllocation AddSUIncrementDetailAllocation(
        Guid? tenantId,
        Guid? objectId,
        Guid? expenseItemId,
        int sortOrder,
        int? objectType,
        double? allocationRate,
        string costAccount,
        string objectCode,
        string objectName,
        int state,
        int? editVersion,
        string expenseItemCode,
        Guid? listItemId,
        string listItemCode)
    {
        var allocation = new SUIncrementDetailAllocation(
            tenantId,
            objectId,
            expenseItemId,
            sortOrder,
            objectType,
            allocationRate,
            costAccount,
            objectCode,
            objectName,
            state,
            editVersion,
            expenseItemCode,
            listItemId,
            listItemCode);

        _sUIncrementDetailAllocations.Add(allocation);

        return allocation;
    }


    public SUIncrementDetail AddSUIncrementDetail(
        int sortOrder,
        string description,
        string numberNo,
        int state,
        int? editVersion)
    {
        var detail = new SUIncrementDetail(
            sortOrder,
            description,
            numberNo,
            state,
            editVersion);

        _sUIncrementDetails.Add(detail);

        return detail;
    }

    public SUIncrementDetailSource AddSUIncrementDetailSource(
        Guid? tenantId,
        Guid refId,
        Guid? refDetailId,
        Guid? organizationUnitId,
        Guid? fixedAssetId,
        int refType,
        int sortOrder,
        string journalMemo,
        string creditAccount,
        string debitAccount,
        string refNo,
        double? amount,
        DateTime? refDate,
        int state,
        int? editVersion,
        int? detailPostOrder)
    {
        var source = new SUIncrementDetailSource(
            tenantId,
            refId,
            refDetailId,
            organizationUnitId,
            fixedAssetId,
            refType,
            sortOrder,
            journalMemo,
            creditAccount,
            debitAccount,
            refNo,
            amount,
            refDate,
            state,
            editVersion,
            detailPostOrder);

        _sUIncrementDetailSources.Add(source);

        return source;
    }


    public void ClearDetailData()
    {
        _sUIncrementDetailAllocations.Clear();
        _sUIncrementDetailSources.Clear();
        _sUIncrementDetails.Clear();
        _sUIncrementDetailDepartments.Clear();
    }

    private void AddSUIncrementCreatedDomainEvent()
    {
        var @event = new SUIncrementCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddSUIncrementUpdatedDomainEvent()
    {
        var @event = new SUIncrementUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
