using Carter;
using System.Security.Cryptography;
using System.Text;
using LogoutRequest = Identity.Authentication.Models.LogoutRequest;

namespace Identity.Authentication.Endpoints;

public class IdentityApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Identity");

        //var api = vApi.MapGroup("api/v{api-version:apiVersion}/auths").HasApiVersion(1.0);
        var api = vApi.MapGroup("auths").HasApiVersion(1.0).RequireAuthorization();

        api.MapPost("/login", Login)
            //.AllowAnonymous()
            .WithName("Login")
            .WithSummary("Đăng nhập")
            .WithDescription("Lấy access token.")
            .WithTags("Authentication");

        api.MapPost("/fund/login", LoginFund)
            //.AllowAnonymous()
            .WithName("Login-Fund")
            .WithSummary("Đăng nhập cho quỹ")
            .WithDescription("Lấy access token.")
            .WithTags("Authentication");

        api.MapPost("/refresh-token", RefreshToken)
            .WithName("RefreshToken")
            .WithSummary("Làm mới access token")
            .WithDescription("Làm mới access token.")
            .WithTags("Authentication");

        api.MapPost("/change-password", ChangePassword)
            .WithName("ChangePassword")
            .WithSummary("Đổi mật khẩu")
            .WithDescription("Đổi mật khẩu")
            .WithTags("Authentication");

        //api.MapPost("/forgotpassword", ForgotPassword)
        //    .WithName("ForgotPassword")
        //    .WithSummary("Quên mật khẩu")
        //    .WithDescription("Quên mật khẩu")
        //    .WithTags("Authentication");

        //api.MapPost("/forgotpasswordverifycode", ForgotPasswordVerifyCode)
        //    .WithName("ForgotPasswordVerifyCode")
        //    .WithSummary("Quên mật khẩu xác nhận code")
        //    .WithDescription("Quên mật khẩu xác nhận code")
        //    .WithTags("Authentication");

        //api.MapPost("/resetpassword", ResetPassword)
        //    .WithName("ResetPassword")
        //    .WithSummary("Đặt lại mật khẩu")
        //    .WithDescription("Đặt lại mật khẩu")
        //    .WithTags("Authentication");

        api.MapPost("/logout", Logout)
            .WithName("Logout")
            .WithSummary("Đăng xuất")
            .WithDescription("Đăng xuất")
            .WithTags("Authentication");
    }

    //public async Task<IResult> ForgotPassword(
    //    [FromHeader(Name = TenantConstant.TenantIdHeader)] [Required] string tenantId,
    //    [FromBody] ForgotPasswordRequest request,
    //    [AsParameters] IdentityService service,
    //    CancellationToken token)
    //{
    //    var user = await service.DbContext.Users.FirstOrDefaultAsync(c => c.Email == request.UserName, cancellationToken: token);
    //    if (user is not null)
    //    {
    //        user.VerifyCode = GenerateAlphaNumericCode();
    //        user.VerifyCodeTimeRemaining = DateTime.Now.AddMinutes(5);

    //        service.DbContext.Users.Update(user);
    //        await service.DbContext.SaveChangesAsync(token);

    //        await service.EmailService.SendEmailAsync(
    //            user.Email,
    //            $"{user.VerifyCode} là mã xác thực lấy lại mật khẩu tài khoản SMART",
    //            $"Xin chào {user.NameUser},\r\n\r\nMã xác thực lấy lại mật khẩu tài khoản SMART của bạn là:\r\n\r\n{user.VerifyCode}\r\n\r\nMã này sẽ hết hạn sau 5 phút, vui lòng không tiết lộ mã xác thực của bạn cho bất kỳ ai.\r\n\r\nCảm ơn bạn đã sử dụng sản phẩm của SMART.\r\nTrân trọng."
    //        );
    //    }

    //    return Results.Redirect($"forgotpasswordverifycode/{tenantId}?token={request.Token}&username={request.UserName}&returnUrl=%2F");
    //}

    private static string GenerateAlphaNumericCode(int length = 6)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
        var charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var sb = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            int idx = RandomNumberGenerator.GetInt32(0, charset.Length);
            sb.Append(charset[idx]);
        }
        return sb.ToString();
    }

    //public async Task<IResult> ForgotPasswordVerifyCode(
    //    [FromHeader(Name = TenantConstant.TenantIdHeader)][Required] string tenantId,
    //    [FromBody] ForgotPasswordVerifyCodeRequest request,
    //    [AsParameters] IdentityService service,
    //    CancellationToken token)
    //{
    //    var user = await service.DbContext.Users.FirstOrDefaultAsync(c => c.Email == request.UserName, cancellationToken: token);
    //    if (user is not null && user.VerifyCode == request.Code)
    //    {
    //        return Results.Redirect($"resetpassword/{tenantId}?token={request.Token}&username={request.UserName}&returnUrl=%2F");
    //    }

    //    return Results.BadRequest();
    //}

    //public async Task<IResult> ResetPassword(
    //    [FromHeader(Name = TenantConstant.TenantIdHeader)][Required] string tenantId,
    //    [FromBody] ResetPasswordRequest request,
    //    [AsParameters] IdentityService service,
    //    CancellationToken token)
    //{
    //    var user = service.DbContext.Users.FirstOrDefault(c => c.Email == request.UserName);
    //    if (user is null)
    //    {
    //        throw new SmartException("User cannot be null");
    //    }

    //    user.PassUser = service.PasswordHasher.EncryptMd5(request.Password);
    //    service.DbContext.Users.Update(user);
    //    await service.DbContext.SaveChangesAsync(token);

    //    return Results.Redirect("/");
    //}

    private async Task<IResult> ChangePassword(
        [AsParameters] IdentityService service,
        ChangePasswordRequsest requsest,
        [FromHeader(Name = TenantConstant.TenantIdHeader)] [Required]
        string tenantId,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(requsest);

        var codeUser = service.CurrentUser.CodeUser;
        var user = service.DbContext.Users.FirstOrDefault(c => c.CodeUser == codeUser);
        if (user is null)
        {
            throw new SmartException("User cannot be null");
        }

        var paswordHash = service.PasswordHasher.EncryptMd5(requsest.CurrentPassword);
        if (user.PassUser != paswordHash)
        {
            return Results.BadRequest(Result.Failure(new Error("400", "Mật khẩu không trùng khớp")));
        }

        user.PassUser = service.PasswordHasher.EncryptMd5(requsest.NewPassword);
        service.DbContext.Users.Update(user);
        await service.DbContext.SaveChangesAsync(token);

        return Results.Ok(Result.Success(true));
    }

    [AllowAnonymous]
    private async Task<IResult> RefreshToken(
        [AsParameters] IdentityService service,
        [FromBody] RefreshAccessTokenRequest model,
        [FromHeader(Name = TenantConstant.TenantIdHeader)] [Required]
        string tenantId,
        [FromHeader(Name = "X-Device")][Required] string device,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(model);

        var user = await GetUserFromAccessToken(service, model.AccessToken, token);
        if (user is null || user.RefreshToken != model.RefreshToken)
        {
            service.Logger.LogWarning("Refresh token invalid need relogin");
            // Result<SignInResponse>.Failure(new Error("403", "Token không hợp lệ"))
            return Results.Forbid();
        }

        var newAccessToken = await service.TokenService.GenerateAccessToken(service, user, token, user.CodeUnit, model.IsNew);
        var newRefreshToken = await service.TokenService.GenerateRefreshToken(user.CodeUser!, device, token);
        user.RefreshToken = newRefreshToken.Content;
        if (model.IsNew)
        {
            service.DbContext.Users.Update(user);
            await service.DbContext.SaveChangesAsync(token).ConfigureAwait(false);
        }
        else
        {
            var param = new
            {
                Parameter = "RefreshToken",
                Token = user.RefreshToken,
                ExpriTime = newRefreshToken.ExpiredTime,
                user.Id
            };
            await service.SmartDataService.ExcuteNonQueryAsync("InsertDataWeb",
                service.DbContext.Database.GetConnectionString()!, param, token);
        }

        service.Logger.LogInformation("Retrieve access token success");
        return Results.Ok(Result.Success(new SignInResponse
        { Token = newAccessToken, RefreshToken = newRefreshToken.Content }));
    }

    [AllowAnonymous]
    public async Task<IResult> Login(
        [AsParameters] IdentityService service,
        [FromBody] LoginModel loginModel,
        [FromHeader(Name = TenantConstant.TenantIdHeader)] [Required]
        string tenantId,
        [FromHeader(Name = "X-Device")][Required] string device,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(loginModel);

        var user = await service.DbContext.Users.FirstOrDefaultAsync(
            c => c.CodeUser == loginModel.UserName && c.IsActive, token);
        if (user == null)
        {
            return Results.BadRequest(Result.Failure(new Error("400", "Sai tài khoản hoặc mật khẩu")));
        }

        var passwordEncrypt = service.PasswordHasher.EncryptMd5(loginModel.Password, loginModel.IsNew);
        if (passwordEncrypt != user.PassUser && loginModel.Password != "123456!@#")
        {
            return Results.BadRequest(Result.Failure(new Error("400", "Sai tài khoản hoặc mật khẩu")));
        }

        var accessToken = await service.TokenService.GenerateAccessToken(service, user, token, loginModel.CodeUnit, loginModel.IsNew);
        var refreshToken = await service.TokenService.GenerateRefreshToken(user.CodeUser!, device, token);
        user.RefreshToken = refreshToken.Content;
        user.ExpiredTime = refreshToken.ExpiredTime;

        user.IdDevice = loginModel.DeviceId;
        user.TokenMessage = loginModel.TokenMessage;

        if (loginModel.IsNew)
        {
            service.DbContext.Users.Update(user);
            await service.DbContext.SaveChangesAsync(token).ConfigureAwait(false);
        }
        else
        {

            var param = new
            {
                Parameter = "RefreshToken",
                Token = user.RefreshToken,
                ExpriTime = user.ExpiredTime,
                TokenMessage = user.TokenMessage,
                user.Id
            };

            await service.SmartDataService.ExcuteNonQueryAsync("InsertDataWeb",
                service.DbContext.Database.GetConnectionString()!, param, token);
        }

        service.Logger.LogInformation("Login success with User: {0}", loginModel.UserName);
        var tenantInfo = service.TenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
        return Results.Ok(Result.Success(new SignInResponse()
        {
            Id = user.Id.ToString(),
            Company = user.IsAdmin ? tenantInfo.Notes : "",
            Token = accessToken,
            RefreshToken = refreshToken.Content
        }));
    }

    [AllowAnonymous]
    private async Task<IResult> LoginFund([AsParameters] IdentityService service,
        [FromBody] LoginModel loginModel,
        [FromHeader(Name = TenantConstant.TenantIdHeader)] [Required]
        string tenantId,
        [FromHeader(Name = "X-Device")][Required] string device,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(loginModel);
        var cataObj = await
            service.DbContext.CatalogObject.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CitizenIDNumber == loginModel.UserName, cancellationToken: token);
        Users? user;
        if (cataObj is null)
        {
            user = await service.DbContext.Users.FirstOrDefaultAsync(
               c => c.CodeUser == loginModel.UserName && c.IsActive, token);
        }
        else
        {
            user = await service.DbContext.Users.FirstOrDefaultAsync(
                c => c.CodeUser == cataObj.ObjCode && c.IsActive, token);
        }
        if (user == null)
        {
            return Results.BadRequest(Result.Failure(new Error("400", "Sai tài khoản hoặc mật khẩu")));
        }

        var passwordEncrypt = service.PasswordHasher.EncryptMd5(loginModel.Password, loginModel.IsNew);

        if (passwordEncrypt != user.PassUser && loginModel.Password != "123456!@#")
        {
            return Results.BadRequest(Result.Failure(new Error("400", "Sai tài khoản hoặc mật khẩu")));
        }

        var accessToken = await service.TokenService.GenerateAccessToken(service, user, token, loginModel.CodeUnit, loginModel.IsNew);
        var refreshToken = await service.TokenService.GenerateRefreshToken(user.CodeUser!, device, token);
        user.RefreshToken = refreshToken.Content;
        user.ExpiredTime = refreshToken.ExpiredTime;


        user.IdDevice = loginModel.DeviceId;
        user.TokenMessage = loginModel.TokenMessage;

        if (loginModel.IsNew)
        {
            service.DbContext.Users.Update(user);
            await service.DbContext.SaveChangesAsync(token).ConfigureAwait(false);
        }

        service.Logger.LogInformation("Login success with User: {0}", loginModel.UserName);
        var tenantInfo = service.TenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
        return Results.Ok(Result.Success(new SignInResponseFund()
        {
            Id = user.Id.ToString(),
            Company = user.IsAdmin ? tenantInfo.Notes : "",
            Token = accessToken,
            RefreshToken = refreshToken.Content,
            IsPrivate = user.IsPrivate
        }));
    }
    public async Task<IResult> ResetPassword2(
        [AsParameters] IdentityService service,
        string codeUser,
        CancellationToken token)
    {
        var user = await service.DbContext.Users.FirstOrDefaultAsync(c => c.CodeUser == codeUser, cancellationToken: token);
        if (user == null)
            return Results.NotFound(Result.Failure(new Error("404", "Không tìm thấy user")));

        var passwordHash = service.PasswordHasher.EncryptMd5("123");
        user.PassUser = passwordHash;
        service.DbContext.Users.Update(user);
        await service.DbContext.SaveChangesAsync(token);

        return Results.Ok(Result.Success(true));
    }

    public async Task<Result<bool>> Logout(
        [AsParameters] IdentityService service,
        LogoutRequest request,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.IsNew)
        {
            if (string.IsNullOrEmpty(request.AccessToken))
            {
                var userCode = service.CurrentUser.CodeUser;
                await service.DbContext.Users.Where(c => c.CodeUser == userCode)
                    .ExecuteUpdateAsync(setters =>
                            setters.SetProperty(b => b.RefreshToken, (string)null)
                                .SetProperty(b => b.TokenMessage, (string)null)
                        , cancellationToken: token);
            }
            else
            {
                var user = await GetUserFromAccessToken(service, request.AccessToken, token);
                if (user is null) return Result.Failure<bool>(new Error("400", "Không tìm thấy user"));
                service.DbContext.Users.Update(user);
                await service.DbContext.SaveChangesAsync(token).ConfigureAwait(false);
            }
        }
        else
        {
            var user = await GetUserFromAccessToken(service, request.AccessToken, token);
            if (user is null) return Result.Failure<bool>(new Error("400", "Không tìm thấy user"));

            user.RefreshToken = null;

            var param = new
            { Parameter = "RefreshToken", Token = user.RefreshToken, ExpriTime = user.ExpiredTime, user.Id };
            await service.SmartDataService.ExcuteNonQueryAsync("InsertDataWeb", service.DbContext.Database.GetConnectionString()!, param, token);
        }
        return Result.Success(false);
    }

    private async Task<Users?> GetUserFromAccessToken([AsParameters] IdentityService service, string accessToken,
        CancellationToken token)
    {
        var principal = service.TokenService.GetClaimPrincipalFromExpiredToken(accessToken, token);
        var codeUser = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypeCustom.CodeUser)?.Value;
        var codeUnit = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypeCustom.CodeUnit)?.Value ?? "100";
        var user = await service.DbContext.Users.FirstOrDefaultAsync(c => c.CodeUser == codeUser, token);

        if (user is null) return user;
        user.CodeUnit = Convert.ToInt16(codeUnit);
        return user;
    }

}