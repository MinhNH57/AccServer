using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Salary.SalaryTimeSheetSummary;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Domain.Entity.Salary;
using SmartAccCloud.Infrastructure.Caching;

namespace SmartAccCloud.API.Controllers.Salary;
[Route("api/salary-time-sheet-summary")]
[ApiController]
[Authorize]
public class SalaryTimeSheetSummaryController(
    RedisCacheService redisCache,
        IMapper mapper,
        IApplicationDbContext context,
        ICrudServicesAsync services,
    IMultiTenantContextAccessor tenantContextAccessor)
{

    TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;

    [HttpGet]
    [Route("get-all-salary-summary-by-{type}")]
    [HasPermission(CustomAction.AllowView, Resource.SalaryTimeSheetSummary)]
    public IResult GetAllSalarySummary(string type)
    {
        var lstData = services.ReadManyNoTracked<SalaryTimeSheetSummary>()
            .Where(x => x.AttendanceType == type).OrderByDescending(x => x.IdAsc).Take(12).ToList();
        return Results.Ok(lstData);
    }

    [HttpGet]
    [Route("get-salary-time-sheet-summary/{id}")]
    [HasPermission(CustomAction.AllowView, Resource.SalaryTimeSheetSummary)]
    public async Task<IResult> GetSalaryTimeSheetSummary(Guid id)
    {
        var result = await services.ReadSingleAsync<SalaryTimeSheetSummary>(id);
        return Results.Ok(result);
    }

    [HttpPost]
    [Route("add-salary-time-sheet-summary")]
    [HasPermission(CustomAction.AllowInsert, Resource.SalaryTimeSheetSummary)]
    public async Task<IResult> AddSalaryTimeSheetSummary(SalaryTimeSheetSummary param)
    {
        param.TrimStrings();

        var result = await services.CreateAndSaveAsync(param);
        if (result != null)
        {
            await redisCache.RemoveByPatternAsync(GetCatalogCacheKey("SalaryTimeSheetSummary"));
            return Results.Ok(Result<bool>.Success(true));

        }
        return Results.BadRequest(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Thêm thất bại")));
    }
    [HttpPut]
    [Route("edit-salary-time-sheet-summary")]
    [HasPermission(CustomAction.AllowEdit, Resource.SalaryTimeSheetSummary)]
    public async Task<IResult> EditSalaryTimeSheetSummary(SalaryTimeSheetSummaryDto param)
    {
        param.TrimStrings();
        var existsSalaryTimeSheetSummary = await services.ReadSingleAsync<SalaryTimeSheetSummary>(param.Id).ConfigureAwait(false);
        if (existsSalaryTimeSheetSummary != null)
        {
            existsSalaryTimeSheetSummary = mapper.Map(param, existsSalaryTimeSheetSummary);
            await services.UpdateAndSaveAsync(existsSalaryTimeSheetSummary);
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.Duplicate).ToString(),
            $"Sửa thất bại do mã  không đã tồn tại")));
    }
    [HttpDelete]
    [Route("delete-salary-time-sheet-summary/{id}")]
    [HasPermission(CustomAction.AllowDelete, Resource.SalaryTimeSheetSummary)]

    public async Task<IResult> DeleteSalaryTimeSheetSummary(Guid id)
    {
        var existsSalary = await context.SalaryTimeSheetSummary
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        if (existsSalary is null) return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Xóa thất bại do mã không tồn tại")));
        var existSalaryTimeSheet = await context.SalaryTimeSheet
            .AsNoTracking()
            .Where(x => x.IdSalaySummaryOfTimekeeping == existsSalary.Id).ToListAsync();
        context.SalaryTimeSheetSummary.Remove(existsSalary);
        context.SalaryTimeSheet.RemoveRange(existSalaryTimeSheet);

        var count = await context.SaveChangesAsync(CancellationToken.None);

        if (count > 0)
        {
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Xóa thất bại do mã không tồn tại")));
    }
    private string GetCatalogCacheKey(string id)
    {
        return $"catalog:{_tenant.Identifier}:{id}";
    }
}
