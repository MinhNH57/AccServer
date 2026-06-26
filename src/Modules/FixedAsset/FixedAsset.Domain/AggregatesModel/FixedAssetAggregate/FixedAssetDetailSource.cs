namespace FixedAsset.Domain.AggregatesModel.FixedAssetAggregate;

public class FixedAssetDetailSource : Entity
{
    public Guid? RefId { get; private set; }

    public Guid? RefDetailId { get; private set; }

    public int SortOrder { get; private set; }

    public int RefType { get; private set; }

    public string JournalMemo { get; private set; }

    public string CreditAccount { get; private set; }

    public string DebitAccount { get; private set; }

    public string RefNo { get; private set; }

    public double? Amount { get; private set; }

    public DateTime? RefDate { get; private set; }

    public DateTime? PostedDate { get; private set; }

    public int? DetailPostOrder { get; private set; }

    public int EditVersion { get; private set; }

    public int State { get; private set; } = 0;

    protected FixedAssetDetailSource() { }

    public FixedAssetDetailSource(
        Guid? refId,
        Guid? refDetailId,
        int sortOrder,
        int refType,
        string journalMemo,
        string creditAccount,
        string debitAccount,
        string refNo,
        double? amount,
        DateTime? refDate,
        DateTime? postedDate,
        int? detailPostOrder,
        int editVersion) : this()
    {
        //Id = Guid.NewGuid();
        RefId = refId;
        RefDetailId = refDetailId;
        SortOrder = sortOrder;
        RefType = refType;
        JournalMemo = journalMemo;
        CreditAccount = creditAccount;
        DebitAccount = debitAccount;
        RefNo = refNo;
        Amount = amount;
        RefDate = refDate;
        PostedDate = postedDate;
        DetailPostOrder = detailPostOrder;
        EditVersion = editVersion;
    }

    public FixedAssetDetailSource Update(
        Guid? refId,
        Guid? refDetailId,
        int sortOrder,
        int refType,
        string journalMemo,
        string creditAccount,
        string debitAccount,
        string refNo,
        double? amount,
        DateTime? refDate,
        DateTime? postedDate,
        int? detailPostOrder,
        int editVersion)
    {
        RefId = refId;
        RefDetailId = refDetailId;
        SortOrder = sortOrder;
        RefType = refType;
        JournalMemo = journalMemo;
        CreditAccount = creditAccount;
        DebitAccount = debitAccount;
        RefNo = refNo;
        Amount = amount;
        RefDate = refDate;
        PostedDate = postedDate;
        DetailPostOrder = detailPostOrder;
        EditVersion = editVersion;

        return this;
    }
}
