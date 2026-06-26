using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.RollCall;
public class CatalogRollCallLocation
{
    public Guid? Id { get; set; }
    public string? LocationCode { get; set; }
    public string? LocationName { get; set; }
    public int? CodeUnit { get; set; }
    public bool? IsActive { get; set; }
    public string? Notes {get; set;}
    //public int? IdAsc {get; set;}
    public double? DistanceAllow { get; set; }
}
