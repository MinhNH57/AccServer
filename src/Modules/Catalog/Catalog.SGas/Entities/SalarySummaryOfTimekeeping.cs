using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.SGas.Entities;
public class SalarySummaryOfTimekeeping
{
    public int MonthText { get; set; }
    public int YearText { get; set; }
    public string AttendanceType { get; set; }
    public string SummaryTableName { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public string? CodeRoom { get; set; }
    public string? NameRoom { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public bool IsBaseOnOldSalarySheet { get; set; }
    //public bool IsAutoAddNewEmployee { get; set; }
    public bool IsIncludedInactiveEmployee { get; set; }
    public DateTime FromDate { get; set; } 
    public DateTime ToDate { get; set; } 
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
}
