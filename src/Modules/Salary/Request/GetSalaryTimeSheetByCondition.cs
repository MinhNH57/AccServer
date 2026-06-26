namespace Salary.Request;

public class GetSalaryTimeSheetByCondition
{
    public string CodeRoom { get; set; } = string.Empty;
    public string CodeTypeSalary { get; set; } = string.Empty; 
    public int PeriodYear { get; set; } = DateTime.Now.Year;
    public int PeriodMonth { get; set; } = DateTime.Now.Month;  
}
