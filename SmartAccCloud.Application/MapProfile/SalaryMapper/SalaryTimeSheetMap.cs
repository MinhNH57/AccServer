using SmartAccCloud.Application.Models.Salary.SalaryTimeSheet;

namespace SmartAccCloud.Application.MapProfile.SalaryMapper;
public class SalaryTimeSheetMap : Profile
{
    public SalaryTimeSheetMap()
    {
        CreateMap<SalaryTimeSheet, SalaryTimeSheetDto>().ReverseMap();
    }
}
