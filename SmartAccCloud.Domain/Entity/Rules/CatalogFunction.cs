namespace SmartAccCloud.Domain.Entity.Rules;

public class CatalogFunction
{
    public string Module { get; set; }
    public string? FuntionLevel { get; set; }
    public string FuntionCode { get; set; }
    public string? FuntionName { get; set; }
    public string? Notes { get; set; }
    public Guid Id { get; set; }
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
}