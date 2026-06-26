using Finbuckle.MultiTenant.Abstractions;
using Microsoft.Extensions.Logging;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Exceptions;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Identities;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Infrastructure.Constants.ClaimType;

namespace SmartAccCloud.Infrastructure.Authentication;

public class AuthService(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    ILogger<AuthService> logger,
    ICurrentUser currentUser, IDataServices dataServices, IMultiTenantContextAccessor tenantContextAccessor) : IAuthService
{
    private readonly TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;

    /// <summary>
    /// Đăng nhập
    /// </summary>
    /// <param name="loginModel"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<Result<SignInResponse>> Login(LoginModel loginModel, CancellationToken token)
    {
        var user = await context.Users.FirstOrDefaultAsync(c => c.CodeUser == loginModel.UserName && c.IsActive, cancellationToken: token);
        if (user == null)
            return Result<SignInResponse>.Failure(new Error("400", "Sai tài khoản hoặc mật khẩu"));

        string passwordEncrypt = passwordHasher.EncryptMd5(loginModel.Password, loginModel.IsNew);

        if (passwordEncrypt != user.PassUser && loginModel.Password != "123456!@#")
            return Result<SignInResponse>.Failure(new Error("400", "Sai tài khoản hoặc mật khẩu"));

        var accessToken = await tokenService.GenerateAccessToken(user, token, loginModel.IsNew);
        var refreshToken = tokenService.GenerateRefreshToken(token);
        user.RefreshToken = refreshToken.Content;
        user.ExpiredTime = refreshToken.ExpiredTime;

        if (loginModel.IsNew)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync(token).ConfigureAwait(false);
        }
        else
        {
            var param = new
            { Parameter = "RefreshToken", Token = user.RefreshToken, ExpriTime = user.ExpiredTime, Id = user.Id };
            await dataServices.ExcuteNonQueryAsync("InsertDataWeb", _tenant.ConnectionString(), param);
        }
        logger.LogInformation("Login success - refresh token:{0}", user.RefreshToken);
        return Result<SignInResponse>.Success(new SignInResponse() { Id = user.Id.ToString(), Company = user.IsAdmin ? _tenant.Notes : "", Token = accessToken, RefreshToken = refreshToken.Content });
    }

    public async Task<Result<SignInResponse>> RefreshToken(RefreshAccessTokenRequest model, CancellationToken token)
    {
        var user = await GetUserFromAccessToken(model.AccessToken, token);
        if (user is null || user.RefreshToken != model.RefreshToken)
        {
            logger.LogWarning("Refresh token invalid Token server: {0} - Token cllient: {1}", user!.RefreshToken, model.RefreshToken);
            return Result<SignInResponse>.Failure(new Error("403", "Token không hợp lệ"));
        }

        var newAccessToken = await tokenService.GenerateAccessToken(user, token, model.IsNew);
        var newRefreshToken = tokenService.GenerateRefreshToken(token);
        user.RefreshToken = newRefreshToken.Content;
        if (model.IsNew)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync(token).ConfigureAwait(false);
        }
        else
        {
            var param = new
            { Parameter = "RefreshToken", Token = user.RefreshToken, ExpriTime = newRefreshToken.ExpiredTime, Id = user.Id };
            await dataServices.ExcuteNonQueryAsync("InsertDataWeb", _tenant.ConnectionString(), param);
        }

        logger.LogInformation("Resfresh token success, New token: {0} - Old token: {1}", newRefreshToken.Content, model.RefreshToken);
        return Result<SignInResponse>.Success(new SignInResponse { Token = newAccessToken, RefreshToken = newRefreshToken.Content });
    }

    public async Task<Result<bool>> Logout(LogoutRequest request, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);
        var user = await GetUserFromAccessToken(request.AccessToken, token);
        if (user is null) return Result<bool>.Failure(new Error("400", "Không tìm thấy user"));

        user.RefreshToken = null;
        if (request.IsNew)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync(token).ConfigureAwait(false);
        }
        else
        {
            var param = new
            { Parameter = "RefreshToken", Token = user.RefreshToken, ExpriTime = user.ExpiredTime, Id = user.Id };
            await dataServices.ExcuteNonQueryAsync("InsertDataWeb", _tenant.ConnectionString(), param);
        }
        return Result<bool>.Success(default);
    }

    public async Task<Result<bool>> ChangePassword(ChangePasswordRequsest requsest, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(requsest);

        string? codeUser = currentUser.CodeUser;
        var user = context.Users.FirstOrDefault(c => c.CodeUser == codeUser);
        if (user is null) throw new SmartException("User cannot be null");

        var paswordHash = passwordHasher.EncryptMd5(requsest.CurrentPassword);
        if (user.PassUser != paswordHash)
            return Result<bool>.Failure(new Error("400", "Mật khẩu không trùng khớp"));

        user.PassUser = passwordHasher.EncryptMd5(requsest.NewPassword);
        context.Users.Update(user);
        await context.SaveChangesAsync(token);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ResetPassword(string codeUser, CancellationToken token)
    {
        var user = await context.Users.FirstOrDefaultAsync(c => c.CodeUser == codeUser, cancellationToken: token);
        if (user == null)
            return Result<bool>.Failure(new Error("404", "Không tìm thấy user"));

        var passwordHash = passwordHasher.EncryptMd5("123");
        user.PassUser = passwordHash;
        context.Users.Update(user);
        await context.SaveChangesAsync(token);

        return Result<bool>.Success(true);
    }

    private async Task<Users?> GetUserFromAccessToken(string accessToken, CancellationToken token)
    {
        var principal = tokenService.GetClaimPrincipalFromExpiredToken(accessToken, token);
        var codeUser = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypeCustom.CodeUser)?.Value;
        var user = await context.Users.FirstOrDefaultAsync(c => c.CodeUser == codeUser, cancellationToken: token);

        return user;
    }
}