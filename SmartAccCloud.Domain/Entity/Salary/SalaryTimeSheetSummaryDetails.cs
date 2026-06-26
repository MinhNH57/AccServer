using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SmartAccCloud.Domain.Entity.Salary;
public class SalaryTimeSheetSummaryDetails
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid IdSalaryTimeSheetSummary { get; set; }
    public string CodeRoom { get; set; }
    public string NameRoom { get; set; }
    public string ObjCode { get; set; }
    public string ObjName { get; set; }
    [Precision(8, 2)]
    public decimal FullSalaryDays { get; set; }
    [Precision(8, 2)]
    public decimal NonFullSalaryDays { get; set; }
    [Precision(8, 2)]
    public decimal FullDaySalary { get; set; }
    [Precision(8, 2)]
    public decimal HalfDaySalary { get; set; }
    [Precision(8, 2)]
    public decimal DayOvertimeHours { get; set; }
    [Precision(8, 2)]
    public decimal DayWeekendOvertimeHour { get; set; }
    [Precision(8, 2)]
    public decimal DayHolidayOvertimeHour { get; set; }
    [Precision(8, 2)]
    public decimal NightOvertimeHours { get; set; }
    [Precision(8, 2)]
    public decimal NightWeekendOvertimeHour { get; set; }
    [Precision(8, 2)]
    public decimal NightHolidayOvertimeHour { get; set; }
    [Precision(8, 2)]
    public decimal TotalOvertimeHours { get; set; }
    public string? NameSalaryType { get; set; }

    public string? YearText { get; set; }
    public string? MonthText { get; set; }
    public string? CodeSalaryType { get; set; }
    public int? CodeUnit { get; set; } = 100;
}
