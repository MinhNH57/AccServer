namespace Catalog.SGas.Entities;
public class CatalogContractFileAttach
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? ViewFile { get; set; }
    public string? NumberContract { get; set; }
    public Guid IdData { get; set; }
    public string? FileNames { get; set; }
    public string? FilePath { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public int IdAsc { get; set; }
    public string? Notes { get; set; }
}
