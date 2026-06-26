using Finbuckle.MultiTenant.Abstractions;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Infrastructure.Caching;
using SmartAccCloud.Infrastructure.Constants.Policy;
using SmartAccCloud.Infrastructure.Persistence;

namespace SmartAccCloud.Infrastructure.Permissions;

public class PermissionnService(
    ApplicationDbContext context,
    IDistributedCache cache,
    IMultiTenantContextAccessor tenantContextAccessor,
    ICurrentUser currentUser) : IPermissionnService
{
    /// <summary>
    /// lấy danh sách chức năng đc truy cập theo User
    /// </summary>
    /// <param name="codeUser"></param>
    /// <returns></returns>
    public Result<List<RuleUser>> GetPermissionsAsync(string codeUser)
    {
        var lstPermis = context.Database.SqlQueryRaw<RuleUser>("EXEC CreateRuleFucnAndReport {0}, {1}, {2}", "RuleFunctionv2", codeUser, 888).ToList();
        // var lstPermis = context.RuleUser.Where(c => c.CodeUser == codeUser).OrderBy(c => c.Notes).ToList();

        return Result<List<RuleUser>>.Success(lstPermis);
    }

    public async Task<Result<List<RuleUser>>> GetPermissionsForCreateRule(CancellationToken token)
    {
        var listMenuQuery = context.WSmartMenu
            .AsNoTracking()
            .Where(c => c.IsActive == true)
            .Select(c => new RuleUser
            {
                KeyFuntion = c.MenuName,
                NameFuntion = c.MenuCaption,
                Notes = c.MenuLevel
            });

        var catalogFunQuery = context.CatalogFuntion
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

        return Result<List<RuleUser>>.Success(unionData);
    }

    public async Task<bool> HasPermission(string? codeUser, string policy)
    {
        if (codeUser is null) return false;
        string resource = string.Empty;
        string action = string.Empty;

        string[] subStringPolicy = policy.Split(new char[] { '-' });
        if (subStringPolicy.Length > 2 && subStringPolicy[0].Equals("Permission", StringComparison.OrdinalIgnoreCase))
        {
            resource = subStringPolicy[1];
            action = subStringPolicy[2];
        }
        var tenantInfo = tenantContextAccessor.MultiTenantContext.TenantInfo;
        if (tenantInfo is null) return false;
        var listPermission = await cache.GetOrCreateAsync(GetPermissionCacheKey(tenantInfo.Identifier!, codeUser), () =>
             {
                 var lst = context.RuleUser
                     .Where(c => c.CodeUser == codeUser)
                     .ToHashSet();
                 return Task.FromResult(lst);
             },
            new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) }
            );

        var permissionFunction = listPermission.FirstOrDefault(c => c.KeyFuntion == resource)!;

        return action switch
        {
            CustomAction.AllowInsert => permissionFunction.AllowInsert,
            CustomAction.AllowEdit => permissionFunction.AllowEdit,
            CustomAction.AllowDelete => permissionFunction.AllowDelete,
            CustomAction.AllowView => permissionFunction.AllowView,
            CustomAction.AllowPrint => permissionFunction.AllowPrint,

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<Result<RuleUser>> GetPermisssion(string keyFunction, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(keyFunction);
        string? codeUser = currentUser.CodeUser;
        var tenantInfo = tenantContextAccessor.MultiTenantContext.TenantInfo;
        if (string.IsNullOrEmpty(codeUser) || tenantInfo is null)
            return Result<RuleUser>.Failure(new Error("400", "Fail"));
        if (codeUser == "ADMIN")
        {
            return Result<RuleUser>.Success(new(){AllowEdit = true,AllowInsert = true,AllowPrint = true,AllowDelete = true});
        }

        var listPermission = await cache.GetOrCreateAsync(GetPermissionCacheKey(tenantInfo.Identifier!, codeUser), () =>
            {
                var lst = context.RuleUser
                    .Where(c => c.CodeUser == codeUser && c.AllowView)
                    .ToHashSet();
                return Task.FromResult(lst);
            },
            new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) }, token: token);

        var ruleUser = listPermission.FirstOrDefault(c => c.KeyFuntion == keyFunction);
        if (ruleUser is null)
            return Result<RuleUser>.Failure(new Error("404", "Not found rules"));

        return Result<RuleUser>.Success(ruleUser);
    }

    public async Task<Result<bool>> UpdateRuleFunction(CreateUpdateUserRequest request, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        var listRule = await context.RuleUser
            .Where(c => c.CodeUser == request.CodeUser)
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

        context.RuleUser.UpdateRange(listRule);
        await context.SaveChangesAsync(token).ConfigureAwait(false);

        string? codeUser = currentUser.CodeUser;
        var tenantInfo = tenantContextAccessor.MultiTenantContext.TenantInfo;
        if (string.IsNullOrEmpty(codeUser) || tenantInfo is null)
            return Result<bool>.Failure(new Error("400", "Fail"));

        await cache.RemoveCacheAsync(GetPermissionCacheKey(tenantInfo.Identifier!, codeUser), token: token);

        return Result<bool>.Success(true);
    }



    private string GetPermissionCacheKey(string tenantId, string codeuser)
    {
        return $"KT:permission:{tenantId}:{codeuser}";
    }
}