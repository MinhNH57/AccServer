using SmartAccCloud.Application.Models.Salary.SalaryTimeSheetSummaryDetails;

namespace SmartAccCloud.Application.MapProfile.SalaryMapper;
public class SalaryTimeSheetSummaryDetailsMap :Profile
{
    public SalaryTimeSheetSummaryDetailsMap()
    {
        CreateMap<SalaryTimeSheetSummaryDetails, SalaryTimeSheetSummaryDetailsDto>().ReverseMap();
    }
}
