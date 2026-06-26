using SmartAccCloud.Application.Models.Salary.SalarySummaryOfTimekeeping;

namespace SmartAccCloud.Application.MapProfile.SalaryMapper;
internal class SalarySummaryOfTimekeepingMap : Profile
{
    public SalarySummaryOfTimekeepingMap()
    {
        CreateMap<SalarySummaryOfTimekeeping, SalarySummaryOfTimekeepingDto>().ReverseMap();
    }
}
