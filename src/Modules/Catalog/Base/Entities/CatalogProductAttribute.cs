using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Base.Entities;
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
