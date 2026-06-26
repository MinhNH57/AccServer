using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Exceptions;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Identities;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Infrastructure.ConfigModel;
using SmartAccCloud.Infrastructure.Constants.ClaimType;
using SmartAccCloud.Infrastructure.Constants.Role;


namespace SmartAccCloud.Infrastructure.Authentication;

public class TokenService(IOptionsMonitor<JwtSettings> jwtSettings, IApplicationDbContext context, IDataServices dataServices,
    IMultiTenantContextAccessor<TenantInfoCustomize> multiTenantContextAccessor) : ITokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.CurrentValue;
    private readonly TenantInfoCustomize? _tenant = multiTenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;

    /// <summary>
    /// Tạo access token
    /// </summary>
    /// <param name="user"></param>
    /// <param name="token"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    public async Task<string> GenerateAccessToken(Users user, CancellationToken token, bool isNew = true)
    {
        ArgumentNullException.ThrowIfNull(user);
        var serectKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signCredentials = new SigningCredentials(serectKey, SecurityAlgorithms.HmacSha256);
        var claims = await GetClaims(user, isNew);

        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.TokenDurationInMinutes),
            signingCredentials: signCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return tokenString;
    }


    /// <summary>
    /// Tạo refresh token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public RefreshTokenModel GenerateRefreshToken(CancellationToken token)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return new()
        {
            Content = Convert.ToBase64String(randomNumber),
            ExpiredTime = DateTime.Now.AddDays(_jwtSettings.RefreshTokenDurationInDay)
        };
    }

    public ClaimsPrincipal GetClaimPrincipalFromExpiredToken(string accessToken, CancellationToken token)
    {
        var tokenValidationParam = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParam, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    private async Task<IEnumerable<Claim>> GetClaims(Users user, bool isNew = true)
    {
        ArgumentNullException.ThrowIfNull(user);
        CatalogUnit? unit = null;
        if (!isNew)
        {
            unit = context.CatalogUnit.FirstOrDefault(c => c.CodeUnit == user.CodeUnit);
            if (unit == null)
                throw new CustomException("Can't find Unit");
        }
        else
        {
            var param = new
            {
                Parameter = "Catalog",
                TableName = "CatalogUnit",
                TypeDocument = "",
                FirstOrLast = "",
                CodeUser = "",
                CodeUnit = 100,
                Condition = "",
            };
            unit = await dataServices.GetSingleObject<CatalogUnit>("WebSmartGetData", _tenant.ConnectionString(), param);

        }
        return new List<Claim>(20)
        {
            new(ClaimTypeCustom.CodeUser, user.CodeUser),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypeCustom.CodeUnit, user.CodeUnit.ToString()!),
            new(ClaimTypes.Name, user.NameUser!),
            new(ClaimTypeCustom.UnitNane, unit.NameUnit!),
            new(ClaimTypes.Role, user.IsAdmin ? Roles.Admin : Roles.User),
            new(ClaimTypeCustom.TenantId, multiTenantContextAccessor.MultiTenantContext.TenantInfo!.Identifier),
            new(ClaimTypeCustom.Decription, multiTenantContextAccessor.MultiTenantContext.TenantInfo?.Notes ?? string.Empty)
        };
    }

}