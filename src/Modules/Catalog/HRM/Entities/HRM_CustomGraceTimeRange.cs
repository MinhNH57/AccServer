using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CustomGraceTimeRange
{
    public Guid Id { get; set; }
    //public int? IdAsc { get; set; }
    public string? Type { get; set; }
    public string? CustomGraceTimeRangeCode { get; set; }
    public DateTime? AllowFromDate { get; set; }
    public DateTime? AllowToDate { get; set; }
    public double? TotalMinute { get; set; }
    public string? GraceTimeCode { get; set; }
    public string? CodeWorkInformation { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
