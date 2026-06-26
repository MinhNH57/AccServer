namespace Supply.Infrastructure.Entities;

public class SULedger
{
    public string RefNo { get; set; }
    public int RefType { get; set; }
    public Guid SupplyId { get; set; }
    public string SupplyCode { get; set; }
    public string SupplyName { get; set; }
    public Guid? SupplyCategoryId { get; set; }
    public string SupplyCategoryCode { get; set; }
    public string SupplyCategoryName { get; set; }
    public string SupplyGroup { get; set; }
    public DateTime? RefDate { get; set; }
    public double Amount { get; set; }
    public double? AllocatedAmount { get; set; }
    public double? RemainingAmount { get; set; }
    public string Unit { get; set; }
    public double? Quantity { get; set; }
    public double? DecrementQuantity { get; set; }
    public double? RemainingQuantity { get; set; }
    public int? AllocationTime { get; set; }
    public int? RemainingAllocationTime { get; set; }
    public string AllocationAccount { get; set; }
    public double UnitPrice { get; set; }
    public double TermlyAllocationAmount { get; set; }
    public bool? SuspendAllocate { get; set; }
    public string ReasonIncrement { get; set; }
    public Guid? BranchId { get; set; }
    public string BranchName { get; set; }
}