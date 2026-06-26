using System.Security.Claims;
using SmartAccCloud.Application.Models.Identities;

namespace SmartAccCloud.Application.Interfaces.Identities;

public interface ITokenService
{
    Task<string> GenerateAccessToken(Users user, CancellationToken token, bool isNew = true);
    RefreshTokenModel GenerateRefreshToken(CancellationToken token);
    ClaimsPrincipal GetClaimPrincipalFromExpiredToken(string accessToken, CancellationToken token);
}