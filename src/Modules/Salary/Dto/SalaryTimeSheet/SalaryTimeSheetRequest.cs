using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Salary.Dto.SalaryTimeSheet;

public class SalaryTimeSheetRequest : ICommand<Result>
{
    public SalaryTimeSheetHeadDto? SalaryHead { get; set; }
    public List<SalaryTimeSheetDetailDto>? SalaryDetail { get; set; }
}
