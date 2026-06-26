using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.Shifts;
public class CatalogSaleShifts
{
    public string ShiftCode { get; set; } = string.Empty;
    public string? ShiftName { get; set; }
    public DateTime? BeginTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? CodeWarehose { get; set; }
    public string? NameWarehose { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public int? CodeUnit { get; set; }
    public bool IsCheckInDotting { get; set; } = false;
    public bool IsCheckOutDotting { get; set; } = false;

    // Khung giờ check-in
    public DateTime? BeginCheckIn { get; set; } 
    public DateTime? EndCheckIn { get; set; } 

    // Khung giờ check-out
    public DateTime? BeginCheckOut { get; set; }
    public DateTime? EndCheckOut { get; set; } 

    // Cài đặt tính công
    public bool AllowPartialCheckIn { get; set; } = false;
    public bool SplitByWorkingHours { get; set; } = false;

    // Thời gian tính lương
    public DateTime? PayRollBeginTime { get; set; } 
    public DateTime? PayRollEndTime { get; set; } 
    public double PayRollHoursOfWork { get; set; } = 0;
    public double PayRollDaysOfWork { get; set; } = 0;

    public DateTime? PayRollBeginTimeEx { get; set; }
    public DateTime? PayRollEndTimeEx { get; set; }
    public double PayRollHoursOfWorkEx { get; set; } = 0;
    public double PayRollDaysOfWorkEx { get; set; } = 0;

    // Hệ số lương
    public double PayrollRegularRate { get; set; } = 0;
    public double PayrollHolidayRate { get; set; } = 0;
    public double PayrollDayOffRate { get; set; } = 0;
    public double CompensationForHolidays { get; set; } = 0;

    public double PayrollRegularRateEx { get; set; } = 0;
    public double PayrollHolidayRateEx { get; set; } = 0;
    public double PayrollDayOffRateEx { get; set; } = 0;

    // Phạt thiếu giờ
    public bool IsPenalizeNoCheckIn { get; set; } = false;
    public bool IsPenalizeNoCheckOut { get; set; } = false;

    // Định mức phạt
    public float? DeductedDaysCaseCheckIn { get; set; } = 0;
    public float? DeductedHoursCaseCheckIn { get; set; } = 0;

    public float? DeductedDaysCaseCheckOut { get; set; } = 0;
    public float? DeductedHoursCaseCheckOut { get; set; } = 0;

    // Phòng ban
    public string? CodeRoom { get; set; }
    public string? NameRooom { get; set; }

    // Yêu cầu chấm v=công vào /ra khi nghỉ giữa giờ
    public bool? RequireBreakCheckIn { get; set; }
    public bool? IsRequireBreakCheckOut { get; set; }
    // Khung giờ nghỉ giữa giờ 
    public DateTime? BreakEndTime { get; set; }
    public DateTime? BreakStartTime { get; set; }

    // Nghỉ giữa các ca (shift transition)
    public bool IsRestBetweenShift { get; set; } = false; // Có nghỉ giữa giờ 
    public DateTime? BeginRestBetween { get; set; } // Thời gian bắt đầu nghỉ giữa các ca
    public DateTime? EndRestBetween { get; set; } // Thời gian kết thúc nghỉ giữa giờ 

    // Khung giờ chấm công vào khi kết thúc nghỉ giữa ca
    public DateTime? CheckInBreakStartTime { get; set; }
    public DateTime? CheckInBreakEndTime { get; set; }

    // Khung giờ chấm công ra khi bắt đầu nghỉ giữa ca
    public DateTime? CheckOutBreakStartTime { get; set; }
    public DateTime? CheckOutBreakEndTime { get; set; }
}
