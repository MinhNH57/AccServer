using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Base.Entities;
public class TardinessPenalty
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public bool? IsLate { get; set; }
    public float? ExedTime { get; set; }
    public float? ExedDate { get; set; }
    public string? TardinessCode { get; set; }
    public string? TardinessName { get; set; }
    public double? DeductByHour { get; set; }
    public string? Notes { get; set; }
    public bool? IsSpecTime { get; set; }
}
