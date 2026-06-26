using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Catalogs;
public class CatalogProductTank
{
    public string? TankCode { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public Guid Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}
