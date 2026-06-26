using Finbuckle.MultiTenant.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using SmartAccCloud.Application.Models.Fund;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Application.Services.SmartFund;
using SmartAccCloud.Infrastructure.Dynamic;
using SmartAccCloud.Infrastructure.Permissions;

namespace SmartAccCloud.API.Controllers.Fund
{
    [Route("api/rule-smart-fund")]
    [ApiController]
    public class RuleSmartFundController(ISmartFundServices smartFundServices, IDistributedCache cache, IMultiTenantContextAccessor tenantContextAccessor) : ControllerBase
    {
        private readonly string _tenantId = tenantContextAccessor.MultiTenantContext.TenantInfo!.Identifier!;

        [HttpPut]
        [Route("get-rule-user")]
        [Authorize]
        public async Task<IResult> GetRuleUser(RuleUserQuery param)
        {
            var result = await smartFundServices.GetRuleUser(param);
            return Results.Ok(result);
        }

        [HttpPut]
        [Route("update-rule-user-union")]
        public async Task<IActionResult> UpdateRuleUnionUser(CreateUpdateUserUnionRequest request, CancellationToken token)
        {
            var rsl = await smartFundServices.UpdateRuleUnionFunction(request, token);
            await cache.RemoveAsync($"menu-{request.CodeUser}-{_tenantId}", token);
            return Ok(rsl);
        }
    }
}
