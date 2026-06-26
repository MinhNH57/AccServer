using Finbuckle.MultiTenant.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.API.Controllers.Rules;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RulesUsersController(IPermissionnService permissionnService, IDistributedCache cache,IMultiTenantContextAccessor tenantContextAccessor) : ResultControllerBase
{
    private readonly string _tenantId = tenantContextAccessor.MultiTenantContext.TenantInfo!.Identifier!;
    [HttpGet]
    [Route("get-rules-by-key/{keyFunction}")]
    public async Task<IActionResult> GetRulesUserByKey(string keyFunction, CancellationToken token)
    {
        var result = await permissionnService.GetPermisssion(keyFunction, token);

        return Ok(result);
    }

    [HttpGet]
    [HasPermission(CustomAction.AllowView, Resource.RuleFunction)]
    [Route("get-all-rules/{codeUser}")]
    public IActionResult GetAllRulesByUser(string codeUser)
    {
        var result = permissionnService.GetPermissionsAsync(codeUser);

        return Ok(result);
    }

    [HttpGet]
    [Route("get-all-function")]
    public async Task<IActionResult> GetAllFunction(CancellationToken token)
    {
        var rsl = await permissionnService.GetPermissionsForCreateRule(token);

        return Ok(rsl);
    }


    [HttpPut]
    [Route("update-rule-user")]
    public async Task<IActionResult> UpdateRuleFunUser(CreateUpdateUserRequest request, CancellationToken token)
    {
        var rsl = await permissionnService.UpdateRuleFunction(request, token);
        await cache.RemoveAsync($"menu-{request.CodeUser}-{_tenantId}", token);
        return Ok(rsl);
    }
}
