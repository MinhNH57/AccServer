using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_LeaveOnPolicy
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? LeavepolicyCode { get; set; }
    public string? LeaveOnName { get; set; }
    public string? LeaveOnCode { get; set; }
    public string? CodeObj { get; set; }
    public int? TotalLeave { get; set; }
    public int? Leaved { get; set; }
    public int? Remaining { get; set; }
    public bool? IsActive { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? CodeUnit { get; set; }
}
