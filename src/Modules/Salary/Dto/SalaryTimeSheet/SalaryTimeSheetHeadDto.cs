namespace Salary.Dto.SalaryTimeSheet;

public class SalaryTimeSheetHeadDto
{
    public Guid Id { get; set; }
    public int? CodeUnit { get; set; }
    public int PeriodMonth { get; set; }
    public int PeriodYear { get; set; }
    public string SalarySheetName { get; set; } = string.Empty;
    public string? CodeRoom { get; set; } = string.Empty;
    public string? NameRoom { get; set; } = string.Empty;
    public string? TypeSalary { get; set; } = string.Empty;
    public string? CodeTypeSalary { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public bool IsBaseOnOldSalarySheet { get; set; }
    public decimal TotalNetIncomeAmount { get; set; }
}
