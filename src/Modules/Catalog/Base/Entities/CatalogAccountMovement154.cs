using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Base.Entities;
public class CatalogAccountMovement154
{
    public string? AccountSymbol { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public int? Ordinal { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid Id { get; set; }

}
