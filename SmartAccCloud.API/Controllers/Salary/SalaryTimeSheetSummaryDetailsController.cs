using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Models.Salary.SalaryTimeSheetSummaryDetails;
using SmartAccCloud.Application.Services.Salary.SalaryTimeSheetSummaryDetails;
using SmartAccCloud.Domain.Entity.Salary;

namespace SmartAccCloud.API.Controllers.Salary;
[Route("api/salary-time-sheet-summary-details")]
[ApiController]
[Authorize]
public class SalaryTimeSheetSummaryDetailsController(
    IMapper mapper,
    IApplicationDbContext context,
    ICrudServicesAsync services,
    ISalaryTimeSheetSummaryDetailsServices salaryTimeSheetSummaryDetails) : ControllerBase
{
    [HttpGet]
    [Route("get-all/{id}")]
    [HasPermission(CustomAction.AllowView, Resource.SalarySummaryOfTimekeeping)]

    public async Task<IResult> GetAllSalaryTime(Guid id)
    {
        var lstData = await salaryTimeSheetSummaryDetails.GetSalaryTimeSheetSummaryDetailsById(id);
        return Results.Ok(lstData);
    }
    [HttpGet]
    [Route("get-salary-time-sheet-summary-details/{id}")]
    [HasPermission(CustomAction.AllowView, Resource.SalarySummaryOfTimekeeping)]
    public async Task<IResult> GetSalaryTimeSheet(Guid id)
    {
        var result = await services.ReadSingleAsync<SalaryTimeSheetSummaryDetails>(id);
        return Results.Ok(result);
    }

    [HttpPost]
    [Route("add-salary-time-sheet-summary-details")]
    [HasPermission(CustomAction.AllowInsert, Resource.SalaryTimeSheet)]
    public async Task<IResult> AddSalaryTimeSheet(List<SalaryTimeSheetSummaryDetailsDto> param)
    {
        var result = await salaryTimeSheetSummaryDetails.CreateSalaryTimeSheetSummaryDetails(param);
        return Results.Ok(result);
    }
    [HttpPut]
    [Route("edit-salary-time-sheet-summary-details")]
    [HasPermission(CustomAction.AllowEdit, Resource.SalarySummaryOfTimekeeping)]
    public async Task<IResult> EditSalaryTimeSheet(SalaryTimeSheetSummaryDetailsQuery param)
    {
        var result = await salaryTimeSheetSummaryDetails.EditSalaryTimeSheetSummaryDetails(param);
        return Results.Ok(result);

    }
    [HttpDelete]
    [Route("delete-salary-time-sheet-summary-details/{id}")]
    [HasPermission(CustomAction.AllowDelete, Resource.SalaryTimeSheet)]

    public async Task<IResult> DeleteSalaryTimeSheet(Guid id)
    {
        var existsSalaryTimeSheet = await services.ReadSingleAsync<SalaryTimeSheet>(id);
        if (existsSalaryTimeSheet != null)
        {
            await services.DeleteAndSaveAsync<SalaryTimeSheet>(id);
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Xóa thất bại do mã không tồn tại")));
    }
}
