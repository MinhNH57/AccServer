using Microsoft.Extensions.Caching.Distributed;
using Systems.Infrastructure.Entities;
using Systems.Models.Fund;

namespace Systems.Apis;

public class SystemApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("System");

        var api = vApi.MapGroup("systems").HasApiVersion(1.0);

        api.RequireAuthorization();

        api.MapPut("/update-rule-user", UpdateRuleFunction)
            .WithName("UpdateRuleUser")
            .WithSummary("Tạo mới quyền cho User")
            .WithDescription("Tạo mới quyền cho User.")
            .WithTags("Systems");

        api.MapGet("/get-all-rules/{codeUser}", GetRuleUser)
            .WithName("get-all-rules")
            .WithSummary("Lấy quyền chức năng của User")
            .WithDescription("Lấy quyền chức năng của User")
            .WithTags("Systems");

        api.MapGet("/get-all-function", GetAllFunction)
            .WithName("GetAllFunction")
            .WithSummary("Tất lấy danh sách chức năng")
            .WithDescription("Tất lấy danh sách chức năng.")
            .WithTags("Systems");

        api.MapGet("/get-rules-by-key/{keyFunction}", GetPermisssion)
            .WithName("Get-rules-by-key")
            .WithSummary("Lấy ra quyền chức năng của User")
            .WithDescription("Lấy ra quyền chức năng của User")
            .WithTags("Systems");

        api.MapPut("/update-rule-user-union-fund", UpdateRuleUnionFunction)
            .WithName("UpdateRuleUnionFunction")
            .WithSummary("Phân quyền theo nhóm khách hàng")
            .WithDescription("Phân quyền theo nhóm khách hàng")
            .WithTags("Systems");

        api.MapPut("/get-rule-user-union-fund", GetRuleUserBaseUnion)
            .WithName("GetRuleUserBaseUnion")
            .WithSummary("Lấy quyền tổ nhóm")
            .WithDescription("Lấy quyền tổ nhóm")
            .WithTags("Systems");

        api.MapGet("/get-unit-info", GetUnitInfo)
            .WithName("GetUnitInfo")
            .WithSummary("Lấy Thông tin đơn vị")
            .WithDescription("Lấy Thông tin đơn vị")
            .WithTags("Systems");

        api.MapGet("/get-rule-approve", GetRuleApprove)
            .WithDescription("Lấy luồng trình ký")
            .WithSummary("Lấy luồng trình ký")
            .WithTags("Systems");

        api.MapGet("/get-rule-approve-summary", GetRuleApproveSummary)
            .WithDescription("Lấy các luồng trình ký theo người dùng")
            .WithSummary("Lấy các luồng trình ký theo người dùng")
            .WithTags("Systems");

        api.MapPost("/add-rule-approve", AddRuleApprove)
            .WithDescription("Thêm cấu hình trình ký")
            .WithSummary("Thêm cấu hình trình ký")
            .WithTags("Systems");

        api.MapDelete("/delete-rule-approve", DeleteRuleApprove)
            .WithDescription("Xoa cấu hình trình ký")
            .WithSummary("Xoa cấu hình trình ký")
            .WithTags("Systems");
    }

    public async Task<IResult> AddRuleApprove(List<CatalogRecipientOfMessage> request,
        [AsParameters] SystemService service, string ownerId, string dataType,
        string job, CancellationToken token)
    {
        //await service.DbContext.CatalogRecipientOfMessage
        //    .Where(c => c.DataType == dataType)
        //    .ExecuteDeleteAsync(cancellationToken: token);

        var lstVatTaxList = await service.DbContext.CatalogRecipientOfMessage
            .AsNoTracking()
            .Where(c => c.OwnerId == ownerId && c.DataType == dataType && c.JobCode == job)
            .ToListAsync(token);

        var lstUpdateVatTaxList = request
            .Where(c => lstVatTaxList.Any(r => r.Id == c.Id))
            .ToList();

        var lstCreateVatTaxList = request
            .Where(x => lstVatTaxList.All(y => y.Id != x.Id))
            .ToList();

        var lstRemoveVatTaxList = lstVatTaxList
            .Where(y => request.All(x => x.Id != y.Id))
            .ToList();

        lstCreateVatTaxList.ForEach(c => c.DataType = dataType);
        if (lstCreateVatTaxList.Count > 0)
            await service.DbContext.CatalogRecipientOfMessage.AddRangeAsync(lstCreateVatTaxList, token);

        if (lstUpdateVatTaxList.Count > 0)
            service.DbContext.CatalogRecipientOfMessage.UpdateRange(lstUpdateVatTaxList);

        if (lstRemoveVatTaxList.Count > 0)
            service.DbContext.CatalogRecipientOfMessage.RemoveRange(lstRemoveVatTaxList);

        await service.DbContext.SaveChangesAsync(token);

        return Results.Ok();
    }

    public async Task<IResult> DeleteRuleApprove([AsParameters] SystemService service, string ownerId, string dataType,
        string job, CancellationToken token)
    {
        var lstVatTaxList = await service.DbContext.CatalogRecipientOfMessage
            .AsNoTracking()
            .Where(c => c.OwnerId == ownerId && c.DataType == dataType && c.JobCode == job)
            .ToListAsync(token);

        if (lstVatTaxList.Count > 0)
            service.DbContext.CatalogRecipientOfMessage.RemoveRange(lstVatTaxList);

        await service.DbContext.SaveChangesAsync(token);

        return Results.Ok();
    }

    public async Task<IResult> GetRuleApprove([AsParameters] SystemService service, string ownerId, string dataType,
        string job, CancellationToken token)
    {
        var listData = await service.DbContext.CatalogRecipientOfMessage
            .AsNoTracking()
            .Where(c => c.OwnerId == ownerId && c.DataType == dataType && c.JobCode == job)
            .OrderBy(c => c.Ordinal)
            .ToListAsync(token);

        return Results.Ok(Result.Success(listData));
    }

    public async Task<IResult> GetRuleApproveSummary([AsParameters] SystemService service, string ownerId,
        CancellationToken token)
    {
        var listData = await service.DbContext.ViewRecipientOfMessageByOwners
            .AsNoTracking()
            .Where(c => c.OwnerId == ownerId)
            .ToListAsync(token);

        return Results.Ok(Result.Success(listData));
    }


    private static async Task<IResult> GetAllFunction([AsParameters] SystemService service, CancellationToken token)
    {
        var listMenuQuery = service.DbContext.WSmartMenu
            .AsNoTracking()
            .Where(c => c.IsActive == true)
            .Select(c => new RuleUser
            {
                KeyFuntion = c.MenuName,
                NameFuntion = c.MenuCaption,
                Notes = c.MenuLevel
            });

        var catalogFunQuery = service.DbContext.CatalogFuntion
            .AsNoTracking()
            .Where(c => c.IsActive && c.Module == "Account")
            .Select(c => new RuleUser()
            {
                KeyFuntion = c.FuntionCode,
                NameFuntion = c.FuntionName,
                Notes = c.FuntionLevel
            });

        var unionData = await listMenuQuery.Concat(catalogFunQuery)
            .OrderBy(c => c.Notes)
            .ToListAsync(cancellationToken: token);

        return Results.Ok(Result.Success(unionData));
    }

    private static IResult GetRuleUser(
        [AsParameters] SystemService service,
        string codeUser)
    {
        var lstPermis = service.DbContext.Database
            .SqlQueryRaw<RuleUser>("EXEC CreateRuleFucnAndReport {0}, {1}, {2}", "RuleFunctionv2", codeUser, 888)
            .ToList();

        return Results.Ok(Result.Success(lstPermis));
    }

    private static async Task<IResult> UpdateRuleFunction(
        [AsParameters] SystemService service,
        CreateUpdateUserRequest request,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        var listRule = await service.DbContext.RuleUser
            .Where(c => c.CodeUser == request.CodeUser && !string.IsNullOrEmpty(c.NameFuntion) &&
                        !string.IsNullOrEmpty(c.Notes))
            .ToListAsync(cancellationToken: token)
            .ConfigureAwait(false);

        foreach (var item in request.LstRules!)
        {
            var existingRules = listRule.FirstOrDefault(c => c.KeyFuntion == item.KeyFuntion);
            if (existingRules is not null)
            {
                existingRules.AllowInsert = item.AllowInsert;
                existingRules.AllowEdit = item.AllowEdit;
                existingRules.AllowView = item.AllowView;
                existingRules.AllowDelete = item.AllowDelete;
                existingRules.AllowPrint = item.AllowPrint;
            }
        }

        service.DbContext.RuleUser.UpdateRange(listRule);
        await service.DbContext.SaveChangesAsync(token).ConfigureAwait(false);

        //string? codeUser = currentUser.CodeUser;
        var tenantInfo = service.TenantContextAccessor.MultiTenantContext.TenantInfo;
        //if (string.IsNullOrEmpty(codeUser) || tenantInfo is null)
        //if (tenantInfo is null)
        //    return Results.BadRequest(Result<bool>.Failure(new Error("400", "Fail")));

        await service.Cache.RemoveCacheAsync(GetPermissionCacheKey(tenantInfo.Identifier!, request.CodeUser),
            token: token);

        return Results.Ok(Result<bool>.Success(true));
    }

    public static async Task<IResult> GetPermisssion(
        [AsParameters] SystemService service,
        string keyFunction, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(keyFunction);
        string? codeUser = service.CurrentUser.CodeUser;
        var tenantInfo = service.TenantContextAccessor.MultiTenantContext.TenantInfo;
        if (string.IsNullOrEmpty(codeUser) || tenantInfo is null)
            return Results.BadRequest(Result<RuleUser>.Failure(new Error("400", "Fail")));
        if (codeUser == "ADMIN")
        {
            return Results.Ok(Result.Success<RuleUser>(new()
                { AllowEdit = true, AllowInsert = true, AllowPrint = true, AllowDelete = true }));
        }

        var listPermission = await service.Cache.GetOrCreateAsync(
            GetPermissionCacheKey(tenantInfo.Identifier!, codeUser), () =>
            {
                var lst = service.DbContext.RuleUser
                    .Where(c => c.CodeUser == codeUser && c.AllowView)
                    .ToHashSet();
                return Task.FromResult(lst);
            },
            new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) },
            token: token);

        var ruleUser = listPermission.FirstOrDefault(c => c.KeyFuntion == keyFunction);
        if (ruleUser is null)
            return Results.BadRequest(Result<RuleUser>.Failure(new Error("404", "Not found rules")));

        return Results.Ok(Result<RuleUser>.Success(ruleUser));
    }

    private static string GetPermissionCacheKey(string tenantId, string codeuser)
    {
        return $"KT:{tenantId}:permission:{codeuser}";
        //await cache.RemoveAsync($"menu-{request.CodeUser}-{_tenantId}", token);
    }

    private static async Task<IResult> UpdateRuleUnionFunction(
        [AsParameters] SystemService service,
        CreateUpdateUserUnionRequest request,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        var listRule = await service.DbContext.RuleBaseUnion
            .Where(c => c.CodeUser == request.CodeUser)
            .ToListAsync(cancellationToken: token)
            .ConfigureAwait(false);

        foreach (var item in request.LstRules!)
        {
            var existingRules = listRule.FirstOrDefault(c => c.CodeObject == item.CodeObject);
            if (existingRules is not null)
            {
                existingRules.IsAllow = item.IsAllow;
            }
        }

        service.DbContext.RuleBaseUnion.UpdateRange(listRule);
        await service.DbContext.SaveChangesAsync(token).ConfigureAwait(false);


        var tenantInfo = service.TenantContextAccessor.MultiTenantContext.TenantInfo;


        if (tenantInfo is null)
            return Results.BadRequest(Result<bool>.Failure(new Error("400", "Có lỗi trong quá trình phân quyền")));

        await service.Cache.RemoveCacheAsync(
            GetPermissionCacheKey(tenantInfo.Identifier!, service.CurrentUser.CodeUser), token: token);
        return Results.Ok(Result<bool>.Success(true));
    }

    public static async Task<IResult> GetRuleUserBaseUnion(
        [AsParameters] SystemService service,
        RuleUserQuery param, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(param);
        string? codeUser = service.CurrentUser.CodeUser;
        var tenantInfo = service.TenantContextAccessor.MultiTenantContext.TenantInfo;
        var values = new { param.Parameter, param.MaUser, param.CodeUnit };
        var lstDataRule = await service.SmartDataServices
            .GetListObject<object>(param.StoreName, service.DbContext.Database.GetConnectionString()!, values)
            .ConfigureAwait(false) ?? new();

        return Results.Ok(Result.Success(lstDataRule));
    }

    public static async Task<IResult> GetUnitInfo(
        [AsParameters] SystemService service,
        int codeUnit, CancellationToken token)
    {
        string? codeUser = service.CurrentUser.CodeUser;
        var data = await service.DbContext.UnitInfo.FirstOrDefaultAsync(
            c => c.UnitCode == codeUnit && c.UserCode == codeUser, cancellationToken: token);
        if (data is not null)
        {
            return Results.Ok(Result.Success(data));
        }

        return Results.Ok(Result.Success(new UnitInfo()));
    }
}