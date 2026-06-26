using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_ShiftSchedulingPolicy
{
    public Guid? Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? ShiftSchedulingPolicyCode { get; set; }
    public string? ShiftSchedulingPolicyName { get; set; }
    public string? Description { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
