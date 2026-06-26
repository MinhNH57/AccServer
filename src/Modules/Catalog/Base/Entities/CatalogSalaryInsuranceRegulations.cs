using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;

public class CatalogSalaryInsuranceRegulations
{
    public DateTime ApplyFromDate { get; set; }
    public DateTime? ApplyToDate { get; set; }
    [Precision(18, 2)]
    public decimal? BaseSalary { get; set; }
    [Precision(18, 2)]
    public decimal? MaxSalaryForInsurance { get; set; }
    [Precision(18, 2)]
    public decimal? PersonalDeduction { get; set; }
    [Precision(18, 2)]
    public decimal? DependentDeduction { get; set; }
    [Precision(5, 2)]
    public decimal? WorkingHoursPerDay { get; set; }
    public bool IsWorkSaturdayMorning { get; set; }
    public bool IsWorkSaturdayAfternoon { get; set; }
    public bool IsWorkSundayMorning { get; set; }
    public bool IsWorkSundayAfternoon { get; set; }
    [Precision(5, 2)]
    public decimal? DayOvertimeRate { get; set; }
    [Precision(5, 2)]
    public decimal? HolidayOvertimeRate { get; set; }
    [Precision(5, 2)]
    public decimal? WeekendOvertimeRate { get; set; }
    [Precision(5, 2)]
    public decimal? NightDayOvertimeRate { get; set; }
    [Precision(5, 2)]
    public decimal? NightHolidayOvertimeRate { get; set; }
    [Precision(5, 2)]
    public decimal? NightWeekendOvertimeRate { get; set; }
    [Precision(5, 2)]
    public decimal? CompanySocialInsuranceRate { get; set; }
    [Precision(5, 2)]
    public decimal? CompanyUnemploymentInsuranceRate { get; set; }
    [Precision(5, 2)]
    public decimal? CompanyUnionFeeRate { get; set; }
    [Precision(5, 2)]
    public decimal? CompanyHealthInsuranceRate { get; set; }
    [Precision(5, 2)]
    public decimal? CompanyAccidentInsuranceRate { get; set; }
    [Precision(5, 2)]
    public decimal? EmployeeSocialInsuranceRate { get; set; }
    [Precision(5, 2)]
    public decimal? EmployeeUnemploymentInsuranceRate { get; set; }
    [Precision(5, 2)]
    public decimal? EmployeeUnionFeeRate { get; set; }
    [Precision(5, 2)]
    public decimal? EmployeeHealthInsuranceRate { get; set; }
    [Precision(5, 2)]
    public decimal? EmployeeAccidentInsuranceRate { get; set; }
    [Precision(18, 2)]
    public decimal? MaxUnionFee { get; set; }
    [Precision(5, 2)]
    public decimal? WorkingDaysInMonth { get; set; }
    [Precision(5, 2)]
    public decimal? NonResidentTaxRate { get; set; }
    [Precision(5, 2)]
    public decimal? ResidentTaxRateShortContract { get; set; }
    public int? CodeUnit { get; set; } = 100;
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public bool IsActive { get; set; }
}