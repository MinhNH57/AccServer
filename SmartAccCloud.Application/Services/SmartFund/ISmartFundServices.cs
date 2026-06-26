using SmartAccCloud.Application.Models.Fund;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Application.Services.SmartFund
{
    public interface ISmartFundServices
    {
        Task<List<object>> GetRuleUser(RuleUserQuery param);

        Task<Response.Result<bool>> UpdateRuleUnionFunction(CreateUpdateUserUnionRequest request, CancellationToken token);

    }
}
