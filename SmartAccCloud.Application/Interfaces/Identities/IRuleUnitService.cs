using SmartAccCloud.Application.Models.RuleUnit;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Application.Interfaces.Identities;

public interface IRuleUnitService
{
    Task<Response.Result<List<UserVm>>> GetUserByUnit(int codeUnit, CancellationToken token);
    Task<Response.Result<List<RuleUnit>>> GetRuleUnitByUser(string codeUser, string codeUnit, CancellationToken token);
    Task<Response.Result<bool>> UpdateRuleUnit(RulesUnitUpdate request, CancellationToken token);
}