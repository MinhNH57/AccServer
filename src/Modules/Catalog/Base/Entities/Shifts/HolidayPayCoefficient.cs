using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Base.Entities.Shifts;
public class HolidayPayCoefficient
{
    public Guid Id { get; set; }
    public int IdAsc { get; set; }
    public string? NameHoliday { get; set; }
    public double? LeaveDay { get; set; }
    public string? CodeObj { get; set; }
    public string? NameObj { get; set; }
    public double? Coefficient { get; set; }
    public int? YearOfApplication { get; set; }
    public bool? FixedBetweenYears { get; set; }
    public bool? IsAllCompany { get; set; }
    public string? CodeShift { get;set; }   
}
