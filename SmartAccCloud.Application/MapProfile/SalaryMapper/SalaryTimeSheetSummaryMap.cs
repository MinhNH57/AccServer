using SmartAccCloud.Application.Models.Salary.SalaryTimeSheetSummary;

namespace SmartAccCloud.Application.MapProfile.SalaryMapper;
public class SalaryTimeSheetSummaryMap :Profile
{
    public SalaryTimeSheetSummaryMap()
    {
        CreateMap<SalaryTimeSheetSummary, SalaryTimeSheetSummaryDto>().ReverseMap();
    }
}
