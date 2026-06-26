using SmartAccCloud.Application.Models.Identities;

namespace SmartAccCloud.Application.Interfaces.Identities;

public interface IAuthService
{
    Task<Result<SignInResponse>> Login(LoginModel loginModel, CancellationToken token);
    Task<Result<SignInResponse>> RefreshToken(RefreshAccessTokenRequest model, CancellationToken token);
    Task<Result<bool>> Logout(LogoutRequest request, CancellationToken token);

    Task<Result<bool>> ChangePassword(ChangePasswordRequsest requsest, CancellationToken token);

    Task<Result<bool>> ResetPassword(string codeUser, CancellationToken token);

    //Task<Result<bool>> CreateUser(CreateUserModel model);
    //Task<Result<bool>> Edit(ApplicationUser model);
    //Task<Result<bool>> RemoveUser(string id);
}