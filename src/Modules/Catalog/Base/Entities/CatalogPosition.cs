namespace Catalog.Base.Entities;
public class CatalogPosition
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CodePosition { get; set; }

    public string? NamePosition { get; set; }
     
    public int? CodeUnit { get; set; }

    public string? Notes { get; set; }
}
