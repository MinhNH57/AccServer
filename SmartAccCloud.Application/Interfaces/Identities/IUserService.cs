using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Application.Interfaces.Identities;

public interface IUserService
{
    Task<Result<List<UserVm>>> GetUserVm(CancellationToken token);
    Task<Response.Result<Guid>> CreateUser(CreateUpdateUserRequest request, CancellationToken token);
    Task<Response.Result<UserVm>> GetByCode(string codeUser, CancellationToken token);
    Task<Response.Result<bool>> UpdateUser(CreateUpdateUserRequest request, CancellationToken token);
}