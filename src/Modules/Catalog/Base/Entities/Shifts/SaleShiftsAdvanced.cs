using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Shifts;
public class SaleShiftsAdvanced
{
    public Guid Id { get; set; }
    //public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public string? ShiftCode { get; set; }
    public string? ShiftName { get; set; }
    public bool? AllowLateEarly { get; set; }
    public double? LateArrivalMinutes { get; set; }
    public double? AllowedEarlyMinutes { get; set; }
    public bool? IsPenalizeLateArrival { get; set; }
    public bool? IsPenalizeEarlyLeave { get; set; }
    public bool? IsOverTime { get; set; }
    public bool? IsBeforeShift { get; set; }
    public bool? IsAfterShift { get; set; }
    public bool? IsBeforeWorkShift { get; set; }
    public bool? IsBeforeTheSpecificTime { get; set; }
    public TimeSpan? BeforeTheSpecificTime { get; set; }
    public bool? IsBeforeRequireOvertimeCheckInAndCheckOut { get; set; }
    public TimeSpan? OverCheckinBegin { get; set; }
    public TimeSpan? OverCheckOutEnd { get; set; }
    public bool? IsAfterWorkShift { get; set; }
    public bool? IsAfterTheSpecificTime { get; set; }
    public TimeSpan? AfterTheSpecificTime { get; set; }
    public bool? IsAfterRequireOvertimeAttendance { get; set; }
    public bool? IsRestBetweenShifts { get; set; }
    public bool? IsShiftMeal { get; set; }
    public string? CodeSalaryPaid { get; set; }
    public string? NameSalaryPaid { get; set; }
    public bool? IsBasedOnPayrollDays { get; set; }
    public bool? IsBasedOnActualDays { get; set; }
    public bool? IsManeuveringWork { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModdifyDate { get; set; }
}
