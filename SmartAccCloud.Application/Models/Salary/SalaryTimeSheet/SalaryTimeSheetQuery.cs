namespace SmartAccCloud.Application.Models.Salary.SalaryTimeSheet;
public class SalaryTimeSheetQuery
{
    public Guid IdSalaySummaryOfTimekeeping { get; set; }
    public List<SalaryTimeSheetDto> LstSalaryTimeSheet { get; set; } = new();

}
