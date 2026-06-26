namespace Supply.Domain.AggregatesModel.SUAdjustmentAggregate;

public class SUAdjustmentDetail
        : Entity
{
    public Guid? SupplyId { get; private set; }
    public string SupplyCode { get; private set; }
    public string SupplyName { get; private set; }
    public double? Quantity { get; private set; }
    public string AllocationAccount { get; private set; }
    public double? CurrentRemainingAmount { get; private set; }
    public double? NewRemainingAmount { get; private set; }
    public double? DiffRemainingAmount { get; private set; }
    public double? CurrentRemainingAllocationTime { get; private set; }
    public double? NewRemainingAllocationTime { get; private set; }
    public double? DiffAllocationTime { get; private set; }
    public double? TermlyAllocationAmount { get; private set; }
    public string Note { get; private set; }
    public int SortOrder { get; private set; }
    public int State { get; private set; } = 0;
    public int? EditVersion { get; private set; }

    protected SUAdjustmentDetail() { }

    public SUAdjustmentDetail(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        double? quantity,
        string allocationAccount,
        double? currentRemainingAmount,
        double? newRemainingAmount,
        double? diffRemainingAmount,
        double? currentRemainingAllocationTime,
        double? newRemainingAllocationTime,
        double? diffAllocationTime,
        double? termlyAllocationAmount,
        string note,
        int sortOrder,
        int state,
        int? editVersion) : this()
    {
        //Id = Guid.NewGuid();
        SupplyId = supplyId;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        Quantity = quantity;
        AllocationAccount = allocationAccount;
        CurrentRemainingAmount = currentRemainingAmount;
        NewRemainingAmount = newRemainingAmount;
        DiffRemainingAmount = diffRemainingAmount;
        CurrentRemainingAllocationTime = currentRemainingAllocationTime;
        NewRemainingAllocationTime = newRemainingAllocationTime;
        DiffAllocationTime = diffAllocationTime;
        TermlyAllocationAmount = termlyAllocationAmount;
        Note = note;
        SortOrder = sortOrder;
        State = state;
        EditVersion = editVersion;
    }

    public SUAdjustmentDetail Update(
        Guid? supplyId,
        string supplyCode,
        string supplyName,
        double? quantity,
        string allocationAccount,
        double? currentRemainingAmount,
        double? newRemainingAmount,
        double? diffRemainingAmount,
        double? currentRemainingAllocationTime,
        double? newRemainingAllocationTime,
        double? diffAllocationTime,
        double? termlyAllocationAmount,
        string note,
        int sortOrder,
        int state,
        int? editVersion)
    {
        SupplyId = supplyId;
        SupplyCode = supplyCode;
        SupplyName = supplyName;
        Quantity = quantity;
        AllocationAccount = allocationAccount;
        CurrentRemainingAmount = currentRemainingAmount;
        NewRemainingAmount = newRemainingAmount;
        DiffRemainingAmount = diffRemainingAmount;
        CurrentRemainingAllocationTime = currentRemainingAllocationTime;
        NewRemainingAllocationTime = newRemainingAllocationTime;
        DiffAllocationTime = diffAllocationTime;
        TermlyAllocationAmount = termlyAllocationAmount;
        Note = note;
        SortOrder = sortOrder;
        State = state;
        EditVersion = editVersion;

        return this;
    }
}
