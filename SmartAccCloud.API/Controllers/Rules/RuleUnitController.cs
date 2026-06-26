using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.RuleUnit;

namespace SmartAccCloud.API.Controllers.Rules;

[Route("api/[controller]")]
[ApiController]
public class RuleUnitController(IRuleUnitService ruleUnitService) : ControllerBase
{
    [HttpGet]
    [Route("get-user-by-code-unit/{codeUnit}")]
    public async Task<IActionResult> GetUserByUnit(int codeUnit, CancellationToken token)
    {
        var rsl = await ruleUnitService.GetUserByUnit(codeUnit, token);
        return Ok(rsl);
    }

    [HttpGet]
    [Route("get-by-user")]
    public async Task<IActionResult> GetRuleUnitByUser(string codeUser, string codeUnit, CancellationToken token)
    {
        var rsl = await ruleUnitService.GetRuleUnitByUser(codeUser, codeUnit, token);

        return Ok(rsl);
    }

    [HttpPut]
    [Route("update-rule-unit")]
    public async Task<IActionResult> UpdateRuleUnit(RulesUnitUpdate request, CancellationToken token)
    {
        var rsl = await ruleUnitService.UpdateRuleUnit(request, token);

        return Ok(rsl);
    }
}
