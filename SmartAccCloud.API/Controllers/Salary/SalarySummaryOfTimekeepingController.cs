using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Salary.SalarySummaryOfTimekeeping;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Domain.Entity.Salary;
using SmartAccCloud.Infrastructure.Caching;

namespace SmartAccCloud.API.Controllers.Salary;
[ApiVersion(1)]
[Route("api/salary-summary-of-timekeeping")]
[ApiController]
[Authorize]
public class SalarySummaryOfTimekeepingController(
    RedisCacheService redisCache,
        IMapper mapper,
        IApplicationDbContext context,
        ICrudServicesAsync services,
    IMultiTenantContextAccessor tenantContextAccessor)
{

    TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;

    [HttpGet]
    [Route("get-all-salary-summary-by-{type}")]
    [HasPermission(CustomAction.AllowView, Resource.SalarySummaryOfTimekeeping)]

    public IResult GetAllSalarySummary(string type)
    {
        var lstData = services.ReadManyNoTracked<SalarySummaryOfTimekeeping>()
            .Where(x => x.AttendanceType == type).OrderByDescending(x => x.IdAsc).Take(12).ToList();
        return Results.Ok(lstData);
    }


    [HttpGet]
    [Route("get-salary-summary-of-timekeeping/{id}")]
    [HasPermission(CustomAction.AllowView, Resource.SalarySummaryOfTimekeeping)]

    public async Task<IResult> GetSalarySummaryOfTimekeeping(Guid id)
    {
        var result = await services.ReadSingleAsync<SalarySummaryOfTimekeeping>(id);
        return Results.Ok(result);
    }


    [HttpPost]
    [Route("add-salary-summary-of-timekeeping")]
    [HasPermission(CustomAction.AllowInsert, Resource.SalarySummaryOfTimekeeping)]
    public async Task<IResult> AddSalarySummaryOfTimekeeping(SalarySummaryOfTimekeeping param)
    {
        param.TrimStrings();

        var result = await services.CreateAndSaveAsync(param);
        if (result != null)
        {
            await redisCache.RemoveByPatternAsync(GetCatalogCacheKey("SalarySummaryOfTimekeeping"));
            return Results.Ok(Result<bool>.Success(true));

        }
        return Results.BadRequest(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Thêm thất bại")));
    }

    [HttpPut]
    [Route("edit-salary-summary-of-timekeeping")]
    [HasPermission(CustomAction.AllowEdit, Resource.SalarySummaryOfTimekeeping)]
    public async Task<IResult> EditSalarySummaryOfTimekeeping(SalarySummaryOfTimekeepingDto param)
    {
        param.TrimStrings();
        var existsSalarySummaryOfTimekeeping = await services.ReadSingleAsync<SalarySummaryOfTimekeeping>(param.Id).ConfigureAwait(false);
        if (existsSalarySummaryOfTimekeeping != null)
        {
            existsSalarySummaryOfTimekeeping = mapper.Map(param, existsSalarySummaryOfTimekeeping);
            await services.UpdateAndSaveAsync(existsSalarySummaryOfTimekeeping);
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.Duplicate).ToString(),
            $"Sửa thất bại do mã  không đã tồn tại")));
    }

    [HttpDelete]
    [Route("delete-salary-summary-of-timekeeping/{id}")]
    [HasPermission(CustomAction.AllowDelete, Resource.SalarySummaryOfTimekeeping)]

    public async Task<IResult> DeleteSalarySummaryOfTimekeeping(Guid id)
    {
        var existsSalary = await context.SalarySummaryOfTimekeeping
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        if (existsSalary is null) return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Xóa thất bại do mã không tồn tại")));
        var existSalaryTimeSheet = await context.SalaryTimeSheet
            .AsNoTracking()
            .Where(x => x.IdSalaySummaryOfTimekeeping == existsSalary.Id).ToListAsync();
        context.SalarySummaryOfTimekeeping.Remove(existsSalary);
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
