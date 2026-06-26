namespace Catalog.SGas.Entities.Sgas;
public class CatalogPumpNozzle
{
    public string? StationId { get; set; }
    public string? NozzleId { get; set; }
    public string? PumpNozzleCode { get; set; }
    public string? PumpNozzleName { get; set; }
    public string? PumpColumnCode { get; set; }
    public string? PumpColumnName { get; set; }
    public string? CodeWarehose { get; set; }
    public string? NameWarehose { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public Guid IdPumpColumn { get; set; } = Guid.NewGuid(); 
    public string? CreatedBy { get; set; }
    public double? Wattage { get; set; }
    public string? Notes { get; set; }
    public string? CompanyId { get; set; }
    public string? CommodityCode { get; set; }
    public string? Serial { get; set; }
    public string? TypeSign { get; set; }
    public string? BankOfAmount { get; set; }
    public string? BankOfName { get; set; }
    public string? BankOfCode { get; set; }
    public string? AccountOwner { get; set; }
    public bool NoPublishInv { get; set; }
}
