using SmartAccCloud.Application.Models.Salary.SalaryTimeSheet;

namespace SmartAccCloud.Application.Services.Salary.SalaryTimeSheet;
public interface ISalaryTimeSheetServices
{
    Task<Result<bool>> CreateSalaryTimeSheet(List<SalaryTimeSheetDto> query);
    Task<List<SalaryTimeSheetDto>> GetSalaryTimeSheetById(Guid id);
    Task<Result<bool>> EditSalaryTimeSheet(SalaryTimeSheetQuery query);
}
