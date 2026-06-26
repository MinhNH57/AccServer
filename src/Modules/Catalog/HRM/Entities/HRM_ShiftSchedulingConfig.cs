using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_ShiftSchedulingConfig
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? ShiftSchedulingAndWorkCode { get; set; }
    public string? CodeShiftSchedulingPolicy { get; set; }
    public string? NameShiftSchedulingPolicy { get; set; }
    public string? Notes { get; set; }
    public bool? IsActive { get; set; }
}
