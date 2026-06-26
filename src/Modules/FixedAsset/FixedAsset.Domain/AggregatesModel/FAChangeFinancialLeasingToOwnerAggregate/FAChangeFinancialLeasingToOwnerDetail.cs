namespace FixedAsset.Domain.AggregatesModel.FAChangeFinancialLeasingToOwnerAggregate;

public class FAChangeFinancialLeasingToOwnerDetail
    : Entity
{
    public int SortOrder { get; private set; }
    public string Description { get; private set; }
    public string DebitAccount { get; private set; }
    public string CreditAccount { get; private set; }
    public double? Amount { get; private set; }
    public Guid? ListItemId { get; private set; }
    public string ListItemCode { get; private set; }
    public string ListItemName { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }

    protected FAChangeFinancialLeasingToOwnerDetail() { }

    public FAChangeFinancialLeasingToOwnerDetail(
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
        SortOrder = sortOrder;
        Description = description;
        DebitAccount = debitAccount;
        CreditAccount = creditAccount;
        Amount = amount;
        ListItemId = listItemId;
        ListItemCode = listItemCode;
        ListItemName = listItemName;
        State = state;
        EditVersion = editVersion;
    }

    public FAChangeFinancialLeasingToOwnerDetail Update(
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
        SortOrder = sortOrder;
        Description = description;
        DebitAccount = debitAccount;
        CreditAccount = creditAccount;
        Amount = amount;
        ListItemId = listItemId;
        ListItemCode = listItemCode;
        ListItemName = listItemName;
        State = state;
        EditVersion = editVersion;

        return this;
    }
}
