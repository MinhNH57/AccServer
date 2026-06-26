using BuildingBlocks.Web;
using Serilog;
using Systems.Infrastructure;
using Systems.Models.LockRequest;
using Lock = Systems.Infrastructure.Entities.Lock;

namespace Systems.Apis;

public class LockApi : ICarterModule
{

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Systems");
        var api = vApi.MapGroup("lock-system").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPost("/get-system-lock", GetLockByUnit)
            .WithName("get-lock-unit")
            .WithSummary("")
            .WithDescription("Lấy thông tin khoá sổ đơn vị/Khoá sổ tất cả (quản trị)")
            .WithTags("Systems");

        api.MapPost("/update-system-lock", UpdateLockLst)
            .WithName("update-system-lock")
            .WithSummary("")
            .WithDescription("Cập nhật thông tin khoá sổ")
            .WithTags("Systems");

        api.MapGet("/get-lock-detail-by-user", GetLockDetailByCodeUnit)
            .WithName("get-lock-detail-by-user")
            .WithSummary("")
            .WithDescription("Lấy thông tin khoá sổ khi đăng nhập")
            .WithTags("Systems");
        //GetLockDetailByCodeUnit
    }

    public static async Task<IResult> GetLockByUnit(
        [AsParameters] SystemService service,
        LockRequestStore request,
        SystemDbcontext dbcontext,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        string queryStr =
            $"exec Lock_CreateLockSystem '{request.Parameter}', '{request.UserCode}', '{request.CodeUnit}', '{request.YearTxt}'";
        var data = await service.DbContext
            .Database
            .SqlQueryRaw<Lock>(queryStr)
            .ToListAsync(token);
        return Results.Ok(Result.Success(data));
    }
    public static async Task<IResult> UpdateLockLst(
        [AsParameters] SystemService service,
        LockUpdateRequest request,
        ICurrentUser currentUser,
        SystemDbcontext dbcontext,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        var lstLockUpdate = await dbcontext.Locks.AsNoTracking()
            .Where(x => x.YearText == request.Query.YearTxt && x.LockType == request.Query.TypeLock &&
                        x.CodeUnit == request.Query.CodeUnit)
            .ToListAsync(cancellationToken: token);
        if (lstLockUpdate.Any())
        {
            lstLockUpdate.ForEach(x =>
            {
                var updated = request.LstLockEdit.FirstOrDefault(y => y.Id == x.Id);
                if (updated != null)
                {
                    x.Locks = updated.Locks;
                }
            });
            dbcontext.Locks.UpdateRange(lstLockUpdate);
            var count = await dbcontext.SaveChangesAsync(token);
            if (count > 1)
            {
                Log.Information($"[LockVoucher] User: {currentUser.CodeUser} has edited the data in the table Locks:{request.Query.TypeLock}-{request.Query.YearTxt}-{request.Query.CodeUnit}");
                return Results.Ok(Result.Success(true));
            }
        }
        return Results.BadRequest(Result.Failure(new Error("400", "Có lỗi khi cất giữ khoá sổ.")));

    }
    public static async Task<IResult> GetLockDetailByCodeUnit(
        [AsParameters] SystemService service,
        ICurrentUser user,
        SystemDbcontext dbcontext,
        CancellationToken token)
    {
        string queryStr =
            $"exec Lock_CreateLockSystem 'GetRuleLock', '{user.CodeUser}', '{user.CodeUnit}', '{DateTime.Now.Year}'";
        //var data = await service.DbContext
        //    .Database
        //    .SqlQueryRaw<Lock>(queryStr)
        //    .ToListAsync(token);

        var data = await dbcontext.Locks
            .AsNoTracking()
            .Where(x =>
                (x.CodeUnit == user.CodeUnit || x.CodeUnit == 9999) &&
                (x.LockType == "LockAll" || x.LockType == "LockUnit") &&
                x.Locks == true)
            .ToListAsync(token);

        return Results.Ok(Result.Success(data));
    }

}