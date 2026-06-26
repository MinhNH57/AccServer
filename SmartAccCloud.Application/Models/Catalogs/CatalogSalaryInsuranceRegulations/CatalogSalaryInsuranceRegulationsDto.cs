using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogSalaryInsuranceRegulations;
public class CatalogSalaryInsuranceRegulationsDto
{
    public DateTime? ApplyFromDate { get; set; }
    public DateTime? ApplyToDate { get; set; }
    public decimal? BaseSalary { get; set; }
    public decimal? MaxSalaryForInsurance { get; set; }
    public decimal? PersonalDeduction { get; set; }
    public decimal? DependentDeduction { get; set; }
    public decimal? WorkingHoursPerDay { get; set; }
    public bool IsWorkSaturdayMorning { get; set; }
    public bool IsWorkSaturdayAfternoon { get; set; }
    public bool IsWorkSundayMorning { get; set; }
    public bool IsWorkSundayAfternoon { get; set; }
    public decimal? DayOvertimeRate { get; set; }
    public decimal? HolidayOvertimeRate { get; set; }
    public decimal? WeekendOvertimeRate { get; set; }
    public decimal? NightDayOvertimeRate { get; set; }
    public decimal? NightHolidayOvertimeRate { get; set; }
    public decimal? NightWeekendOvertimeRate { get; set; }
    public decimal? CompanySocialInsuranceRate { get; set; }
    public decimal? CompanyUnemploymentInsuranceRate { get; set; }
    public decimal? CompanyUnionFeeRate { get; set; }
    public decimal? CompanyHealthInsuranceRate { get; set; }
    public decimal? CompanyAccidentInsuranceRate { get; set; }
    public decimal? EmployeeSocialInsuranceRate { get; set; }
    public decimal? EmployeeUnemploymentInsuranceRate { get; set; }
    public decimal? EmployeeUnionFeeRate { get; set; }
    public decimal? EmployeeHealthInsuranceRate { get; set; }
    public decimal? EmployeeAccidentInsuranceRate { get; set; }
    public decimal? MaxUnionFee { get; set; }
    public decimal? WorkingDaysInMonth { get; set; }
    public decimal? NonResidentTaxRate { get; set; }
    public decimal? ResidentTaxRateShortContract { get; set; }
    public int? CodeUnit { get; set; } = 100;
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public bool IsActive { get; set; }
}
