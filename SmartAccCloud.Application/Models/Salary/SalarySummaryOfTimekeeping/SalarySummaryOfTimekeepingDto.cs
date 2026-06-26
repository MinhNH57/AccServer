namespace SmartAccCloud.Application.Models.Salary.SalarySummaryOfTimekeeping;
public class SalarySummaryOfTimekeepingDto
{
    public int MonthText { get; set; }
    public int YearText { get; set; }
    public string AttendanceType { get; set; }
    public string SummaryTableName { get; set; }
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsBaseOnOldSalarySheet { get; set; }
    public bool IsAutoAddNewEmployee { get; set; }
    public bool IsIncludedInactiveEmployee { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
