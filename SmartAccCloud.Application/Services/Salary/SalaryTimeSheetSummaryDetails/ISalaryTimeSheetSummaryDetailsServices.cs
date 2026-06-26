using SmartAccCloud.Application.Models.Salary.SalaryTimeSheetSummaryDetails;

namespace SmartAccCloud.Application.Services.Salary.SalaryTimeSheetSummaryDetails;
public interface ISalaryTimeSheetSummaryDetailsServices
{
    Task<Result<bool>> CreateSalaryTimeSheetSummaryDetails(List<SalaryTimeSheetSummaryDetailsDto> query);
    Task<List<SalaryTimeSheetSummaryDetailsDto>> GetSalaryTimeSheetSummaryDetailsById(Guid id);
    Task<Result<bool>> EditSalaryTimeSheetSummaryDetails(SalaryTimeSheetSummaryDetailsQuery query);
}
