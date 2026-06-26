namespace FixedAsset.Domain.AggregatesModel.FAChangeFinancialLeasingToOwnerAggregate;

public class FAChangeFinancialLeasingToOwner :
    Entity, IAggregateRoot
{
    public Guid? TenantId { get; private set; }
    public Guid? BranchId { get; private set; }
    public Guid? FixedAssetId { get; private set; }
    public string FixedAssetName { get; private set; }
    public int? DisplayOnBook { get; private set; }
    public int RefType { get; private set; }
    public int? RefOrder { get; private set; }
    public string JournalMemo { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public DateTime? RefDate { get; private set; }
    public string RefNo { get; private set; }
    public string FixedAssetCode { get; private set; }
    public double? TotalAmount { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsPostedFinance { get; private set; }
    public bool IsPostedManagement { get; private set; }
    public string OldOrgPriceAccount { get; private set; }
    public string NewOrgPriceAccount { get; private set; }
    public string OldDepreciationAccount { get; private set; }
    public string NewDepreciationAccount { get; private set; }
    public double OldOrgPrice { get; private set; }
    public double NewOrgPrice { get; private set; }
    public double OldDepreciationAmount { get; private set; }
    public double NewDepreciationAmount { get; private set; }
    public double OldAccumDepreciationAmount { get; private set; }
    public double NewAccumDepreciationAmount { get; private set; }
    public double OldRemainingAmount { get; private set; }
    public double NewRemainingAmount { get; private set; }
    public double OldLifeTime { get; private set; }
    public double NewLifeTime { get; private set; }
    public double OldLifeTimeRemaining { get; private set; }
    public double NewLifeTimeRemaining { get; private set; }
    public double OldDepreciationRateMonth { get; private set; }
    public double NewDepreciationRateMonth { get; private set; }
    public double OldMonthlyDepreciationAmount { get; private set; }
    public double NewMonthlyDepreciationAmount { get; private set; }
    public double OldDepreciationRateYear { get; private set; }
    public double NewDepreciationRateYear { get; private set; }
    public double OldYearlyDepreciationAmount { get; private set; }
    public double NewYearlyDepreciationAmount { get; private set; }
    public bool OldIsLimitDepreciationAmount { get; private set; }
    public bool NewIsLimitDepreciationAmount { get; private set; }
    public double OldDepreciationAmountByIncomeTax { get; private set; }
    public double NewDepreciationAmountByIncomeTax { get; private set; }
    public double OldRemainingAmountByIncomeTax { get; private set; }
    public double NewRemainingAmountByIncomeTax { get; private set; }
    public double OldMonthlyDepreciationAmountByIncomeTax { get; private set; }
    public double NewMonthlyDepreciationAmountByIncomeTax { get; private set; }
    public string CreatedBy { get; private set; }
    public string ModifiedBy { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }
    public double DifferentOrgPrice { get; private set; }
    public double DifferentDepreciationAmount { get; private set; }
    public double DifferentAccumDepreciationAmount { get; private set; }
    public double DifferentRemainingAmount { get; private set; }
    public double DifferentLifeTime { get; private set; }
    public double DifferentLifeTimeRemaining { get; private set; }
    public double DifferentDepreciationRateMonth { get; private set; }
    public double DifferentMonthlyDepreciationAmount { get; private set; }
    public double DifferentDepreciationRateYear { get; private set; }
    public double DifferentYearlyDepreciationAmount { get; private set; }
    public double DifferentDepreciationAmountByIncomeTax { get; private set; }
    public double DifferentRemainingAmountByIncomeTax { get; private set; }
    public double DifferentMonthlyDepreciationAmountByIncomeTax { get; private set; }
    public string AttachmentIdList { get; private set; }
    public string BranchName { get; private set; }

    private readonly List<FAChangeFinancialLeasingToOwnerDetail> _fAChangeFinancialLeasingToOwnerDetails;

    public IReadOnlyCollection<FAChangeFinancialLeasingToOwnerDetail> FAChangeFinancialLeasingToOwnerDetails => _fAChangeFinancialLeasingToOwnerDetails.AsReadOnly();

    protected FAChangeFinancialLeasingToOwner()
    {
        _fAChangeFinancialLeasingToOwnerDetails = [];
    }

    public FAChangeFinancialLeasingToOwner(
        Guid? tenantId,
        Guid? branchId,
        Guid? fixedAssetId,
        string fixedAssetName,
        int? displayOnBook,
        int refType,
        int? refOrder,
        string journalMemo,
        DateTime? postedDate,
        DateTime? refDate,
        string refNo,
        string fixedAssetCode,
        double? totalAmount,
        DateTime? createdDate,
        DateTime? modifiedDate,
        string oldOrgPriceAccount,
        string newOrgPriceAccount,
        string oldDepreciationAccount,
        string newDepreciationAccount,
        double oldOrgPrice,
        double newOrgPrice,
        double oldDepreciationAmount,
        double newDepreciationAmount,
        double oldAccumDepreciationAmount,
        double newAccumDepreciationAmount,
        double oldRemainingAmount,
        double newRemainingAmount,
        double oldLifeTime,
        double newLifeTime,
        double oldLifeTimeRemaining,
        double newLifeTimeRemaining,
        double oldDepreciationRateMonth,
        double newDepreciationRateMonth,
        double oldMonthlyDepreciationAmount,
        double newMonthlyDepreciationAmount,
        double oldDepreciationRateYear,
        double newDepreciationRateYear,
        double oldYearlyDepreciationAmount,
        double newYearlyDepreciationAmount,
        bool oldIsLimitDepreciationAmount,
        bool newIsLimitDepreciationAmount,
        double oldDepreciationAmountByIncomeTax,
        double newDepreciationAmountByIncomeTax,
        double oldRemainingAmountByIncomeTax,
        double newRemainingAmountByIncomeTax,
        double oldMonthlyDepreciationAmountByIncomeTax,
        double newMonthlyDepreciationAmountByIncomeTax,
        string createdBy,
        string modifiedBy,
        int state,
        int? editVersion,
        double differentOrgPrice,
        double differentDepreciationAmount,
        double differentAccumDepreciationAmount,
        double differentRemainingAmount,
        double differentLifeTime,
        double differentLifeTimeRemaining,
        double differentDepreciationRateMonth,
        double differentMonthlyDepreciationAmount,
        double differentDepreciationRateYear,
        double differentYearlyDepreciationAmount,
        double differentDepreciationAmountByIncomeTax,
        double differentRemainingAmountByIncomeTax,
        double differentMonthlyDepreciationAmountByIncomeTax,
        string attachmentIdList,
        string branchName) : this()
    {
        TenantId = tenantId;
        BranchId = branchId;
        FixedAssetId = fixedAssetId;
        FixedAssetName = fixedAssetName;
        DisplayOnBook = displayOnBook;
        RefType = refType;
        RefOrder = refOrder;
        JournalMemo = journalMemo;
        PostedDate = postedDate;
        RefDate = refDate;
        RefNo = refNo;
        FixedAssetCode = fixedAssetCode;
        TotalAmount = totalAmount;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        OldOrgPriceAccount = oldOrgPriceAccount;
        NewOrgPriceAccount = newOrgPriceAccount;
        OldDepreciationAccount = oldDepreciationAccount;
        NewDepreciationAccount = newDepreciationAccount;
        OldOrgPrice = oldOrgPrice;
        NewOrgPrice = newOrgPrice;
        OldDepreciationAmount = oldDepreciationAmount;
        NewDepreciationAmount = newDepreciationAmount;
        OldAccumDepreciationAmount = oldAccumDepreciationAmount;
        NewAccumDepreciationAmount = newAccumDepreciationAmount;
        OldRemainingAmount = oldRemainingAmount;
        NewRemainingAmount = newRemainingAmount;
        OldLifeTime = oldLifeTime;
        NewLifeTime = newLifeTime;
        OldLifeTimeRemaining = oldLifeTimeRemaining;
        NewLifeTimeRemaining = newLifeTimeRemaining;
        OldDepreciationRateMonth = oldDepreciationRateMonth;
        NewDepreciationRateMonth = newDepreciationRateMonth;
        OldMonthlyDepreciationAmount = oldMonthlyDepreciationAmount;
        NewMonthlyDepreciationAmount = newMonthlyDepreciationAmount;
        OldDepreciationRateYear = oldDepreciationRateYear;
        NewDepreciationRateYear = newDepreciationRateYear;
        OldYearlyDepreciationAmount = oldYearlyDepreciationAmount;
        NewYearlyDepreciationAmount = newYearlyDepreciationAmount;
        OldIsLimitDepreciationAmount = oldIsLimitDepreciationAmount;
        NewIsLimitDepreciationAmount = newIsLimitDepreciationAmount;
        OldDepreciationAmountByIncomeTax = oldDepreciationAmountByIncomeTax;
        NewDepreciationAmountByIncomeTax = newDepreciationAmountByIncomeTax;
        OldRemainingAmountByIncomeTax = oldRemainingAmountByIncomeTax;
        NewRemainingAmountByIncomeTax = newRemainingAmountByIncomeTax;
        OldMonthlyDepreciationAmountByIncomeTax = oldMonthlyDepreciationAmountByIncomeTax;
        NewMonthlyDepreciationAmountByIncomeTax = newMonthlyDepreciationAmountByIncomeTax;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        DifferentOrgPrice = differentOrgPrice;
        DifferentDepreciationAmount = differentDepreciationAmount;
        DifferentAccumDepreciationAmount = differentAccumDepreciationAmount;
        DifferentRemainingAmount = differentRemainingAmount;
        DifferentLifeTime = differentLifeTime;
        DifferentLifeTimeRemaining = differentLifeTimeRemaining;
        DifferentDepreciationRateMonth = differentDepreciationRateMonth;
        DifferentMonthlyDepreciationAmount = differentMonthlyDepreciationAmount;
        DifferentDepreciationRateYear = differentDepreciationRateYear;
        DifferentYearlyDepreciationAmount = differentYearlyDepreciationAmount;
        DifferentDepreciationAmountByIncomeTax = differentDepreciationAmountByIncomeTax;
        DifferentRemainingAmountByIncomeTax = differentRemainingAmountByIncomeTax;
        DifferentMonthlyDepreciationAmountByIncomeTax = differentMonthlyDepreciationAmountByIncomeTax;
        AttachmentIdList = attachmentIdList;
        BranchName = branchName;

        AddCreatedDomainEvent();
    }
    public FAChangeFinancialLeasingToOwner Update(
        Guid? tenantId,
        Guid? branchId,
        Guid? fixedAssetId,
        string fixedAssetName,
        int? displayOnBook,
        int? refOrder,
        string journalMemo,
        DateTime? postedDate,
        DateTime? refDate,
        string refNo,
        string fixedAssetCode,
        double? totalAmount,
        DateTime? modifiedDate,
        string oldOrgPriceAccount,
        string newOrgPriceAccount,
        string oldDepreciationAccount,
        string newDepreciationAccount,
        double oldOrgPrice,
        double newOrgPrice,
        double oldDepreciationAmount,
        double newDepreciationAmount,
        double oldAccumDepreciationAmount,
        double newAccumDepreciationAmount,
        double oldRemainingAmount,
        double newRemainingAmount,
        double oldLifeTime,
        double newLifeTime,
        double oldLifeTimeRemaining,
        double newLifeTimeRemaining,
        double oldDepreciationRateMonth,
        double newDepreciationRateMonth,
        double oldMonthlyDepreciationAmount,
        double newMonthlyDepreciationAmount,
        double oldDepreciationRateYear,
        double newDepreciationRateYear,
        double oldYearlyDepreciationAmount,
        double newYearlyDepreciationAmount,
        bool oldIsLimitDepreciationAmount,
        bool newIsLimitDepreciationAmount,
        double oldDepreciationAmountByIncomeTax,
        double newDepreciationAmountByIncomeTax,
        double oldRemainingAmountByIncomeTax,
        double newRemainingAmountByIncomeTax,
        double oldMonthlyDepreciationAmountByIncomeTax,
        double newMonthlyDepreciationAmountByIncomeTax,
        string modifiedBy,
        int state,
        int? editVersion,
        double differentOrgPrice,
        double differentDepreciationAmount,
        double differentAccumDepreciationAmount,
        double differentRemainingAmount,
        double differentLifeTime,
        double differentLifeTimeRemaining,
        double differentDepreciationRateMonth,
        double differentMonthlyDepreciationAmount,
        double differentDepreciationRateYear,
        double differentYearlyDepreciationAmount,
        double differentDepreciationAmountByIncomeTax,
        double differentRemainingAmountByIncomeTax,
        double differentMonthlyDepreciationAmountByIncomeTax,
        string attachmentIdList,
        string branchName)
    {
        TenantId = tenantId;
        BranchId = branchId;
        FixedAssetId = fixedAssetId;
        FixedAssetName = fixedAssetName;
        DisplayOnBook = displayOnBook;
        RefOrder = refOrder;
        JournalMemo = journalMemo;
        PostedDate = postedDate;
        RefDate = refDate;
        RefNo = refNo;
        FixedAssetCode = fixedAssetCode;
        TotalAmount = totalAmount;
        ModifiedDate = modifiedDate;
        OldOrgPriceAccount = oldOrgPriceAccount;
        NewOrgPriceAccount = newOrgPriceAccount;
        OldDepreciationAccount = oldDepreciationAccount;
        NewDepreciationAccount = newDepreciationAccount;
        OldOrgPrice = oldOrgPrice;
        NewOrgPrice = newOrgPrice;
        OldDepreciationAmount = oldDepreciationAmount;
        NewDepreciationAmount = newDepreciationAmount;
        OldAccumDepreciationAmount = oldAccumDepreciationAmount;
        NewAccumDepreciationAmount = newAccumDepreciationAmount;
        OldRemainingAmount = oldRemainingAmount;
        NewRemainingAmount = newRemainingAmount;
        OldLifeTime = oldLifeTime;
        NewLifeTime = newLifeTime;
        OldLifeTimeRemaining = oldLifeTimeRemaining;
        NewLifeTimeRemaining = newLifeTimeRemaining;
        OldDepreciationRateMonth = oldDepreciationRateMonth;
        NewDepreciationRateMonth = newDepreciationRateMonth;
        OldMonthlyDepreciationAmount = oldMonthlyDepreciationAmount;
        NewMonthlyDepreciationAmount = newMonthlyDepreciationAmount;
        OldDepreciationRateYear = oldDepreciationRateYear;
        NewDepreciationRateYear = newDepreciationRateYear;
        OldYearlyDepreciationAmount = oldYearlyDepreciationAmount;
        NewYearlyDepreciationAmount = newYearlyDepreciationAmount;
        OldIsLimitDepreciationAmount = oldIsLimitDepreciationAmount;
        NewIsLimitDepreciationAmount = newIsLimitDepreciationAmount;
        OldDepreciationAmountByIncomeTax = oldDepreciationAmountByIncomeTax;
        NewDepreciationAmountByIncomeTax = newDepreciationAmountByIncomeTax;
        OldRemainingAmountByIncomeTax = oldRemainingAmountByIncomeTax;
        NewRemainingAmountByIncomeTax = newRemainingAmountByIncomeTax;
        OldMonthlyDepreciationAmountByIncomeTax = oldMonthlyDepreciationAmountByIncomeTax;
        NewMonthlyDepreciationAmountByIncomeTax = newMonthlyDepreciationAmountByIncomeTax;
        ModifiedBy = modifiedBy;
        State = state;
        EditVersion = editVersion;
        DifferentOrgPrice = differentOrgPrice;
        DifferentDepreciationAmount = differentDepreciationAmount;
        DifferentAccumDepreciationAmount = differentAccumDepreciationAmount;
        DifferentRemainingAmount = differentRemainingAmount;
        DifferentLifeTime = differentLifeTime;
        DifferentLifeTimeRemaining = differentLifeTimeRemaining;
        DifferentDepreciationRateMonth = differentDepreciationRateMonth;
        DifferentMonthlyDepreciationAmount = differentMonthlyDepreciationAmount;
        DifferentDepreciationRateYear = differentDepreciationRateYear;
        DifferentYearlyDepreciationAmount = differentYearlyDepreciationAmount;
        DifferentDepreciationAmountByIncomeTax = differentDepreciationAmountByIncomeTax;
        DifferentRemainingAmountByIncomeTax = differentRemainingAmountByIncomeTax;
        DifferentMonthlyDepreciationAmountByIncomeTax = differentMonthlyDepreciationAmountByIncomeTax;
        AttachmentIdList = attachmentIdList;
        BranchName = branchName;

        AddUpdatedDomainEvent();

        return this;
    }

    public FAChangeFinancialLeasingToOwnerDetail AddFAChangeFinancialLeasingToOwnerDetail(
        int sortOrder,
        string description,
        string debitAccount,
        string creditAccount,
        double? amount,
        Guid? listItemId,
        string listItemCode,
        string listItemName,
        int state,
        int? editVersion)
    {
        var detail = new FAChangeFinancialLeasingToOwnerDetail(
            sortOrder,
            description,
            debitAccount,
            creditAccount,
            amount,
            listItemId,
            listItemCode,
            listItemName,
            state,
            editVersion);

        _fAChangeFinancialLeasingToOwnerDetails.Add(detail);

        return detail;
    }

    public void ClearDetailData()
    {
        _fAChangeFinancialLeasingToOwnerDetails.Clear();
    }

    private void AddCreatedDomainEvent()
    {
        var @event = new FAChangeFinancialLeasingToOwnerCreatedDomainEvent(this);

        AddDomainEvent(@event);
    }

    private void AddUpdatedDomainEvent()
    {
        var @event = new FAChangeFinancialLeasingToOwnerUpdatedDomainEvent(this);

        AddDomainEvent(@event);
    }
}
