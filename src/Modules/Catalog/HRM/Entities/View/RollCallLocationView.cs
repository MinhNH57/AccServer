using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.View;
public class RollCallLocationView
{
    public Guid? Id { get; set; }
    public string? LocationCode { get; set; }
    public string? LocationName { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? DistanceAllow { get; set; }
    public int? CodeUnit { get; set; }
    public string? Notes { get; set; }
}
