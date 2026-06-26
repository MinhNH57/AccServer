using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Base.Entities;
public class TardinessPolicy
{
    public Guid Id { get; set; }
    public string? DataType { get; set; }
    public string? CodeShift { get; set; }
    public string? TardinessPolicyCode { get; set; }
    public string? TardinessPolicyName { get; set; }
    public string? CodeTardiness { get; set; }
    public string? NameTardiness { get; set; }
    public double? BeginMinute { get; set; }
    public double? EndMinute { get; set; }
    public string? Notes { get; set; }
    public float? ExedTime { get; set; }
    public float? ExedDate { get; set; }
    public bool? IsSpecTime { get;set; }
}
