using Salary.Model.Contracts;

namespace Salary.Model;

public class SalaryTimeSheetHead :IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int? CodeUnit { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public int PeriodMonth { get; set; }
    public int PeriodYear { get; set; }
    public string SalarySheetName { get; set; } = string.Empty;
    public string? TypeSalary { get; set; } = string.Empty;
    public string? CodeRoom { get; set; } = string.Empty;
    public string? NameRoom { get; set; } = string.Empty;
    public string? CodeTypeSalary { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public bool IsBaseOnOldSalarySheet { get; set; }
    public decimal TotalNetIncomeAmount { get; set; }
    public ICollection<SalaryTimeSheetDetail> SalaryTimeSheetDetails { get; set; } = null!;

}
