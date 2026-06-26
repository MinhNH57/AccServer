using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Application.Interfaces.Identities;

public interface IPermissionnService
{
    Response.Result<List<RuleUser>> GetPermissionsAsync(string codeUser);
    Task<bool> HasPermission(string? codeUser, string policy);
    //Task<bool> ChangePermission();
    Task<Response.Result<RuleUser>> GetPermisssion(string keyFunction, CancellationToken token);
    Task<Response.Result<List<RuleUser>>> GetPermissionsForCreateRule(CancellationToken token);
    Task<Response.Result<bool>> UpdateRuleFunction(CreateUpdateUserRequest request, CancellationToken token);
}