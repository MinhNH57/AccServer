namespace Catalog.SGas.Entities;
public class SalesRemainingDebts
{
    public string? AccountSymbol { get; set; }

    public decimal? OpeningDebit { get; set; }

    public decimal? OpeningCredit { get; set; }

    public string? ObjectCode { get; set; }

    public string? ObjectName { get; set; }

    public string? WarehoseCode { get; set; }

    public string? WarehoseName { get; set; }

    public string? ContractCode { get; set; }

    public string? ContractName { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid(); 
    public int? CodeUnit { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; }
}
