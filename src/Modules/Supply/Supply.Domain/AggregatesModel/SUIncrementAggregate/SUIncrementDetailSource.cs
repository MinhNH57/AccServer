namespace Supply.Domain.AggregatesModel.SUIncrementAggregate;

public class SUIncrementDetailSource : Entity
{
    public Guid? TenantId { get; private set; }

    public Guid RefId { get; private set; }

    public Guid? RefDetailId { get; private set; }

    public Guid? OrganizationUnitId { get; private set; }

    public Guid? FixedAssetId { get; private set; }

    public int RefType { get; private set; }

    public int SortOrder { get; private set; }

    public string JournalMemo { get; private set; }

    public string CreditAccount { get; private set; }

    public string DebitAccount { get; private set; }

    public string RefNo { get; private set; }

    public double? Amount { get; private set; }

    public DateTime? RefDate { get; private set; }

    public int State { get; private set; } = 0;

    public int? EditVersion { get; private set; }

    public int? DetailPostOrder { get; private set; }

    protected SUIncrementDetailSource() { }

    public SUIncrementDetailSource(
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
        int? detailPostOrder) : this()
    {
        //Id = Guid.NewGuid();
        TenantId = tenantId;
        RefId = refId;
        RefDetailId = refDetailId;
        OrganizationUnitId = organizationUnitId;
        FixedAssetId = fixedAssetId;
        RefType = refType;
        SortOrder = sortOrder;
        JournalMemo = journalMemo;
        CreditAccount = creditAccount;
        DebitAccount = debitAccount;
        RefNo = refNo;
        Amount = amount;
        RefDate = refDate;
        State = state;
        EditVersion = editVersion;
        DetailPostOrder = detailPostOrder;
    }

    public SUIncrementDetailSource Update(
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
        TenantId = tenantId;
        RefId = refId;
        RefDetailId = refDetailId;
        OrganizationUnitId = organizationUnitId;
        FixedAssetId = fixedAssetId;
        RefType = refType;
        SortOrder = sortOrder;
        JournalMemo = journalMemo;
        CreditAccount = creditAccount;
        DebitAccount = debitAccount;
        RefNo = refNo;
        Amount = amount;
        RefDate = refDate;
        State = state;
        EditVersion = editVersion;
        DetailPostOrder = detailPostOrder;

        return this;
    }
}
