namespace Supply.API.Application.Models;

public class SUIncrementGetDataFromPUStock
{
    public bool? Selected { get; set; }
    public Guid? RefDetailId { get; set; }
    public string InventoryItemCode { get; set; }
    public string InventoryItemName { get; set; }
    public DateTime? PostedDate { get; set; }
    public DateTime? RefDate { get; set; }
    public string RefNo { get; set; }
    public double? Quantity { get; set; }
    public string UnitName { get; set; }
    public double? UnitPrice { get; set; }
    public double? Amount { get; set; }
    public double? TotalInwardAmount { get; set; }
    public string DebitAccount { get; set; }
    public string CreditAccount { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public string OrganizationUnitCode { get; set; }
    public string OrganizationUnitName { get; set; }
    public Guid? RefId { get; set; }
    public int? RefType { get; set; }
    public double? PUUnitPrice { get; set; }
    public double? TotalExpendituresPuAmount { get; set; }
    public double? PuAmount { get; set; }
}