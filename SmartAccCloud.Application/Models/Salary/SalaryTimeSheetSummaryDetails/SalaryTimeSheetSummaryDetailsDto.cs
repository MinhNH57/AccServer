namespace SmartAccCloud.Application.Models.Salary.SalaryTimeSheetSummaryDetails;
public class SalaryTimeSheetSummaryDetailsDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdSalaryTimeSheetSummary { get; set; }
    public string CodeRoom { get; set; }
    public string NameRoom { get; set; }
    public string ObjCode { get; set; }
    public string ObjName { get; set; }
    public decimal FullSalaryDays { get; set; }
    public decimal NonFullSalaryDays { get; set; }
    public decimal FullDaySalary { get; set; }
    public decimal HalfDaySalary { get; set; }
    public decimal DayOvertimeHours { get; set; }
    public decimal DayWeekendOvertimeHour { get; set; }
    public decimal DayHolidayOvertimeHour { get; set; }
    public decimal NightOvertimeHours { get; set; }
    public decimal NightWeekendOvertimeHour { get; set; }
    public decimal NightHolidayOvertimeHour { get; set; }
    public decimal TotalOvertimeHours { get; set; }
    public string? NameSalaryType { get; set; }
    public string? LstObjectSalary { get; set; }
    public string? YearText { get; set; }
    public string? MonthText { get; set; }
    public string? CodeSalaryType { get; set; }
}
