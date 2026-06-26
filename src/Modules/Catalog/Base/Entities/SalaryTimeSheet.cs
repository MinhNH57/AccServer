using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class SalaryTimeSheet
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdSalaySummaryOfTimekeeping { get; set; }

    public string? RoomCode { get; set; }
    public string? RoomName { get; set; }
    public string? ObjectCode { get; set; }
    public string? ObjectName { get; set; }
    public string? Position { get; set; }
    public string? N01 { get; set; }
    public string? N02 { get; set; }
    public string? N03 { get; set; }
    public string? N04 { get; set; }
    public string? N05 { get; set; }
    public string? N06 { get; set; }
    public string? N07 { get; set; }
    public string? N08 { get; set; }
    public string? N09 { get; set; }
    public string? N10 { get; set; }
    public string? N11 { get; set; }
    public string? N12 { get; set; }
    public string? N13 { get; set; }
    public string? N14 { get; set; }
    public string? N15 { get; set; }
    public string? N16 { get; set; }
    public string? N17 { get; set; }
    public string? N18 { get; set; }
    public string? N19 { get; set; }
    public string? N20 { get; set; }
    public string? N21 { get; set; }
    public string? N22 { get; set; }
    public string? N23 { get; set; }
    public string? N24 { get; set; }
    public string? N25 { get; set; }
    public string? N26 { get; set; }
    public string? N27 { get; set; }
    public string? N28 { get; set; }
    public string? N29 { get; set; }
    public string? N30 { get; set; }
    public string? N31 { get; set; }
    public double? TotalHoursWork { get; set; }
    public double? TotalStandardWorkHours { get; set; }
    public int? CodeUnit { get; set; } = 100;
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public string? Notes { get; set; }
    public string? CodeSalaryType { get; set; }
    public string? MonthText { get; set; }
    public string? YearText { get; set; }
    public string? NameSalaryType { get; set; }
    public double? TotalNonHoursWork { get; set; }
    public double? DayTimeRegularDays { get; set; }
    public double? DayTimeSaturdaySunday { get; set; }
    public double? NightTimeRegularDays { get; set; }
    public double? NightTimeSaturdaySunday { get; set; }
    public double? TotalOvertime { get; set; }
    public double? DayTimeHoliday { get; set; }
    public double? NightTimeHoliday { get; set; }
}
