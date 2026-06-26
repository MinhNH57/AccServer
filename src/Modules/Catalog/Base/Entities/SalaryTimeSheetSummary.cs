using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class SalaryTimeSheetSummary
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid IdSalarySummaryOfTimekeeping { get; set; }
    public int MonthText { get; set; }
    public int YearText { get; set; }
    public string AttendanceType { get; set; }
    public string SummaryTableName { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public bool IsBaseDetailSalaryTimeSheet { get; set; }
}
