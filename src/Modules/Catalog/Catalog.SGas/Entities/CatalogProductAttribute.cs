using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities;
public class CatalogProductAttribute
{
    public string AttributeCode { get; set; } = "";
    public string? AttributeName { get; set; }
    public string? Notes { get; set; }
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public string? TypeAttribute { get; set; }
    public string? DataType { get; set; }
}
