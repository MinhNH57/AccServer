using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carter;

namespace Catalog.HRM.Features;
public class HrmSyncApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("CatalogTest");

        var api = vApi.MapGroup("HRMSyncConfig/").HasApiVersion(1.0).RequireAuthorization();
        api.MapPost("sync-config", SyncConfig)
           .WithName("HRMSyncConfig1")
           .WithSummary("Đồng bộ cấu hình thông báo")
           .WithDescription("Gọi store sp_HRMSyncConfig để đồng bộ cấu hình")
           .WithTags("HRMcatalog1");

        api.MapPost("update-missing-work", UpdateMissingWork)
           .WithName("HRMUpdateMissingWork")
           .WithSummary("Chuyển các công việc chưa hoàn thành của ngày hôm trước sang ngày hôm nay")
           .WithDescription("Gọi store usp_UpdateWorkDateForPendingTasks để chuyển ngày làm việc")
           .WithTags("HRMcatalog1");

        api.MapPost("writting-log", WrittingActivityLog)
           .WithName("ActivityLog")
           .WithSummary("Ghi lại các thay đổi trong công việc")
           .WithDescription("Gọi store SP_WriteHistoryActivityLog để ghi lại nhật ký hoạt động")
           .WithTags("HRMcatalog1");
    }

    public async Task<bool> SyncConfig(
    [AsParameters] HrmCatalogService service,
    SyncConfigRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var obj = new
        {
            request.Param,
            request.CodeUnit,
            request.CodeUser
        };

        var isSuccess = await service.SmartDataServices.ExcuteNonQueryAsync(
            "sp_HRMSyncConfig",
            service.DbContext.Database.GetConnectionString(),
            obj
        );

        return isSuccess;
    }

    public async Task<bool> UpdateMissingWork([AsParameters] HrmCatalogService service)
    {
        var rsl = await service.SmartDataServices.ExcuteNonQueryAsync("usp_UpdateWorkDateForPendingTasks", service.DbContext.Database.GetConnectionString());
        return rsl;
    }

    public async Task<bool> WrittingActivityLog([AsParameters] HrmCatalogService service , LogRequest request)
    {
        var obj = new
        {
            request.RecordId,
            request.TableName,
            request.ActionType,
            request.UserCode,
            request.OldValue,
            request.NewValue,
            request.Contents,
        };
        var rsl = await service.SmartDataServices.ExcuteNonQueryAsync("SP_WriteHistoryActivityLog", service.DbContext.Database.GetConnectionString(),obj);
        return rsl;
    }
}
