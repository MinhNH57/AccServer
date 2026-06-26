using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Identities;

namespace SmartAccCloud.API.Controllers.Identites;
[ApiVersion(1)]
[Route("api/auths")]
[Route("api/v{v:apiVersion}/auths")]
[ApiController]
public class AuthsController(IAuthService authService) : ResultControllerBase
{
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel model, CancellationToken token)
    {
        var result = await authService.Login(model, token);
        return Ok(result);
    }

    [HttpPost]
    [MapToApiVersion(1)]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshAccessTokenRequest model, CancellationToken token)
    {
        var result = await authService.RefreshToken(model, token);
        return Ok(result);
    }

    [HttpPost]
    [Route("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequsest request, CancellationToken token)
    {
        var result = await authService.ChangePassword(request, token);

        return Ok(result);
    }

    [HttpPost]
    [MapToApiVersion(1)]
    [Route("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request, CancellationToken token)
    {
        await authService.Logout(request, token);
        return Ok();
    }

    
}
