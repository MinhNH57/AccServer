namespace Supply.Domain.AggregatesModel.SUAdjustmentAggregate;

public class SUAdjustmentDetailVoucher
        : Entity
{
    public Guid? TenantId { get; private set; }
    public Guid? VoucherRefId { get; private set; }
    public Guid VoucherRefDetailId { get; private set; }
    public string CreditAccount { get; private set; }
    public string DebitAccount { get; private set; }
    public int? VoucherRefType { get; private set; }
    public int SortOrder { get; private set; }
    public string RefNo { get; private set; }
    public double? Amount { get; private set; }
    public DateTime? RefDate { get; private set; }
    public string RefTypeName { get; private set; }
    public string Description { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }
    public int? DetailPostOrder { get; private set; }

    protected SUAdjustmentDetailVoucher() { }

    public SUAdjustmentDetailVoucher(
        Guid? tenantId,
        Guid? voucherRefId,
        Guid voucherRefDetailId,
        string creditAccount,
        string debitAccount,
        int? voucherRefType,
        int sortOrder,
        string refNo,
        double? amount,
        DateTime? refDate,
        string refTypeName,
        string description,
        int state,
        int? editVersion,
        int? detailPostOrder) : this()
    {
        //Id = Guid.NewGuid();
        TenantId = tenantId;
        VoucherRefId = voucherRefId;
        VoucherRefDetailId = voucherRefDetailId;
        CreditAccount = creditAccount;
        DebitAccount = debitAccount;
        VoucherRefType = voucherRefType;
        SortOrder = sortOrder;
        RefNo = refNo;
        Amount = amount;
        RefDate = refDate;
        RefTypeName = refTypeName;
        Description = description;
        State = state;
        EditVersion = editVersion;
        DetailPostOrder = detailPostOrder;
    }

    public SUAdjustmentDetailVoucher Update(
        Guid? tenantId,
        Guid? voucherRefId,
        Guid voucherRefDetailId,
        string creditAccount,
        string debitAccount,
        int? voucherRefType,
        int sortOrder,
        string refNo,
        double? amount,
        DateTime? refDate,
        string refTypeName,
        string description,
        int state,
        int? editVersion,
        int? detailPostOrder)
    {
        TenantId = tenantId;
        VoucherRefId = voucherRefId;
        VoucherRefDetailId = voucherRefDetailId;
        CreditAccount = creditAccount;
        DebitAccount = debitAccount;
        VoucherRefType = voucherRefType;
        SortOrder = sortOrder;
        RefNo = refNo;
        Amount = amount;
        RefDate = refDate;
        RefTypeName = refTypeName;
        Description = description;
        State = state;
        EditVersion = editVersion;
        DetailPostOrder = detailPostOrder;

        return this;
    }
}
