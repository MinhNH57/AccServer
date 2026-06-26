using Finbuckle.MultiTenant.Abstractions;
using SmartAccCloud.Application.Caching;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Fund;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Application.Services.SmartFund
{
    public class SmartFundServices(IDataServices dataServices,
        IMultiTenantContextAccessor tenantContextAccessor,
        IRedisCacheService cache,
        IApplicationDbContext context, ICurrentUser currentUser) : ISmartFundServices
    {
        private readonly TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
        public async Task<List<object>> GetRuleUser(RuleUserQuery param)
        {
            var values = new { param.Parameter, param.MaUser, param.CodeUnit };
            var lstDataRule = await dataServices.GetListObject<object>(param.StoreName, _tenant.ConnectionString(), values).ConfigureAwait(false) ?? new();
            return lstDataRule;
        }

        public async Task<Result<bool>> UpdateRuleUnionFunction(CreateUpdateUserUnionRequest request, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(request);

            var listRule = await context.RuleBaseUnion
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

            context.RuleBaseUnion.UpdateRange(listRule);
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
}
