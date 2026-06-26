using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Models.Salary.SalaryTimeSheet;
using SmartAccCloud.Application.Services.Salary.SalaryTimeSheet;
using SmartAccCloud.Domain.Entity.Salary;

namespace SmartAccCloud.API.Controllers.Salary;
[Route("api/salary-time-sheet")]
[ApiController]
[Authorize]
public class SalaryTimeSheetController(
    IMapper mapper,
    IApplicationDbContext context,
    ICrudServicesAsync services,
    ISalaryTimeSheetServices salaryTimeSheetServices) : ControllerBase
{
    [HttpGet]
    [Route("get-all/{id}")]
    [HasPermission(CustomAction.AllowView, Resource.SalarySummaryOfTimekeeping)]

    public async Task<IResult> GetAllSalaryTime(Guid id)
    {
        var lstData = await salaryTimeSheetServices.GetSalaryTimeSheetById(id);
        return Results.Ok(lstData);
    }
    [HttpGet]
    [Route("get-salary-time-sheet/{id}")]
    [HasPermission(CustomAction.AllowView, Resource.SalarySummaryOfTimekeeping)]
    public async Task<IResult> GetSalaryTimeSheet(Guid id)
    {
        var result = await services.ReadSingleAsync<SalaryTimeSheet>(id);
        return Results.Ok(result);
    }

    [HttpPost]
    [Route("add-salary-time-sheet")]
    [HasPermission(CustomAction.AllowInsert, Resource.SalaryTimeSheet)]
    public async Task<IResult> AddSalaryTimeSheet(List<SalaryTimeSheetDto> param)
    {
        var result = await salaryTimeSheetServices.CreateSalaryTimeSheet(param);
        return Results.Ok(result);
    }
    [HttpPut]
    [Route("edit-salary-time-sheet")]
    [HasPermission(CustomAction.AllowEdit, Resource.SalarySummaryOfTimekeeping)]
    public async Task<IResult> EditSalaryTimeSheet(SalaryTimeSheetQuery param)
    {
        var result = await salaryTimeSheetServices.EditSalaryTimeSheet(param);
        return Results.Ok(result);

    }
    [HttpDelete]
    [Route("delete-salary-time-sheet/{id}")]
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
