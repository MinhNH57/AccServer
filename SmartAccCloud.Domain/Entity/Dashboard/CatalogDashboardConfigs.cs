using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Domain.Entity.Dashboard;
public class CatalogDashboardConfigs
{
    public int ColumnNumber { get; set; }
    public int RowNumber { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public string? ContentConfigs { get; set; }
    public string? NameDashboard { get; set; }
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public int CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
}
