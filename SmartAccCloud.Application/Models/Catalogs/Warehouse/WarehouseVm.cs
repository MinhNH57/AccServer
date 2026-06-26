namespace SmartAccCloud.Application.Models.Catalogs.Warehouse;

public class WarehouseVm
{
    public string CodeWarehose { get; set; }
    public string? NameWarehose { get; set; }
    public string? AddressWarehose { get; set; }
    public string? CodeStocker { get; set; }
    public string? NameStocker { get; set; }
    public bool? CostPrice { get; set; }
    public bool IsStore { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
}