using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_GraceTime
{
    public Guid Id { get; set; }
    public string? GraceTimeCode { get; set; }
    public string? GraceTimeType { get; set; }
    public double? AllowLate { get; set; }
    public double? AllowEarly { get; set; }
    public string? CodeWorkInformation { get; set; }
    public string? CodeUnit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}
