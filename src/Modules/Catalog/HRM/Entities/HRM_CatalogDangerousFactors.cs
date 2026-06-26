using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CatalogDangerousFactors
{
    public string? JobPositionCode { get; set; }
    public string? JobPositionName { get; set; }
    public string? DangerMayOccur { get; set; }
    public string? Consequences { get; set; }
    public string? PreventiveMeasures { get; set; }
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public Guid Id { get; set; }
    public int? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
    public string? Notes { get; set; }
    public string? CodeJobPosition { get; set; }
    public string? NameJobPosition { get; set; }
}
