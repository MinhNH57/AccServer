namespace SmartAccCloud.Application.Models.Salary.SalaryTimeSheetSummaryDetails;
public class SalaryTimeSheetSummaryDetailsQuery
{
    public Guid IdSalaryTimeSheetSummary { get; set; }
    public List<SalaryTimeSheetSummaryDetailsDto> LstSalaryTimeSheetSummaryDetails { get; set; } = new();


}
