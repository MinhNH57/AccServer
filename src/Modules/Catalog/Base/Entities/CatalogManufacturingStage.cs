using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogManufacturingStage
{
    public string StageCode { get; set; }
    public string? StageName { get; set; }
    public int? Sequential { get; set; }
    public string? StageType { get; set; }
    public string? Notes { get; set; }
    public bool IsUse { get; set; }
    public bool IsActive { get; set; } = true;
    public int? CodeUnit { get; set; } = 100;
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public string? ManufacturingStageBelong { get; set; }
    public string? ManufacturingStageBelongName { get; set; }
}
