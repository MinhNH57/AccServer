using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork;
public class HRM_WorkDailyContents
{
    public Guid Id { get; set; }
    //public int IdAsc { get; set; }
    public Guid IdContents { get; set; }
    public string? WorkContents { get; set; }
    public string? Notes { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public bool? IsDone { get; set; }
    public DateTime? CompleteAt { get; set; }
    public double? Duration { get; set; }
}
