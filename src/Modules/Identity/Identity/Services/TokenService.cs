using BuildingBlocks.Jwt;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;

namespace Identity.Services;

public interface ITokenService
{
    Task<string> GenerateAccessToken([AsParameters] IdentityService service, Users user, CancellationToken token, int? codeUnit, bool isNew = true);
    Task<RefreshTokenModel> GenerateRefreshToken(string userCode, string clientType, CancellationToken clt);
    ClaimsPrincipal GetClaimPrincipalFromExpiredToken(string accessToken, CancellationToken token);
}

public class TokenService(IConfiguration configuration, IdentityDbContext dbContext,
    //  IDataServices dataServices,
    IMultiTenantContextAccessor<TenantInfoCustomize> multiTenantContextAccessor
    ) : ITokenService
{
    private readonly JwtSettings _jwtSettings = configuration.GetOptions<JwtSettings>("JwtSettings");
    private readonly TenantInfoCustomize? _tenant = multiTenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;

    /// <summary>
    /// Tạo access token
    /// </summary>
    /// <param name="service"></param>
    /// <param name="user"></param>
    /// <param name="token"></param>
    /// <param name="codeUnit"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    public async Task<string> GenerateAccessToken([AsParameters] IdentityService service, Users user, CancellationToken token, int? codeUnit, bool isNew = true)
    {
        var authori = configuration["JwtSettings:Authority"];
        ArgumentNullException.ThrowIfNull(user);
        //var serectKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        string privateKeyXml = await File.ReadAllTextAsync("key/private_key.xml", token);
        RSA rsa = RSA.Create();
        rsa.FromXmlString(privateKeyXml);
        var signCredentials = new SigningCredentials(key: new RsaSecurityKey(rsa), algorithm: SecurityAlgorithms.RsaSha256);

        var claims = await GetClaims(service, user, codeUnit, isNew);

        var tokenOptions = new JwtSecurityToken(
            issuer: authori,
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
    /// <param name="userCode"></param>
    /// <param name="clientType"></param>
    /// <param name="clt"></param>
    /// <returns></returns>
    public async Task<RefreshTokenModel> GenerateRefreshToken(string userCode, string clientType, CancellationToken clt)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var token = Convert.ToBase64String(randomNumber);
        var expireTime = DateTime.Now.AddDays(_jwtSettings.RefreshTokenDurationInDay);

        var refreshToken = new UsersRefreshToken()
        {
            UserCode = userCode,
            ClientType = clientType,
            RefreshToken = token,
            ExpiryDate = expireTime
        };
        await CreateOrUpdateUserRefreshToken(refreshToken, clt);

        return new()
        {
            Content = Convert.ToBase64String(randomNumber),
            ExpiredTime = expireTime
        };
    }
    private async Task CreateOrUpdateUserRefreshToken(UsersRefreshToken refreshToken, CancellationToken token)
    {
        var data = await dbContext.UsersRefreshToken.FirstOrDefaultAsync(c =>
            c.ClientType == refreshToken.ClientType && c.UserCode == refreshToken.UserCode, cancellationToken: token);

        if (data is null)
        {
            await dbContext.UsersRefreshToken.AddAsync(refreshToken, token);
        }
        else
        {
            data.RefreshToken = refreshToken.RefreshToken;
            data.ExpiryDate = refreshToken.ExpiryDate;
            dbContext.UsersRefreshToken.Update(data);
        }

        await dbContext.SaveChangesAsync(token);
    }

    public ClaimsPrincipal GetClaimPrincipalFromExpiredToken(string accessToken, CancellationToken token)
    {
        if (!File.Exists("key/public_key.xml"))
        {
            throw new SmartException("public key not provider");
        }
        string publicXmlKey = File.ReadAllText("key/public_key.xml");
        RSA rsa = RSA.Create();
        rsa.FromXmlString(publicXmlKey);
        var tokenValidationParam = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(rsa)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParam, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    private async Task<IEnumerable<Claim>> GetClaims([AsParameters] IdentityService service, Users user, int? codeUnit, bool isNew = true)
    {
        ArgumentNullException.ThrowIfNull(user);
        dynamic? unit = null;
        dynamic? obj = null;
        if (!isNew)
        {
            var query = $"select * from CatalogUnit where CodeUnit = '{user.CodeUnit}'";
            unit = await service.SmartDataService
                .GetSingleObjectQuery(query, service.DbContext.Database.GetConnectionString()!);

            //if (unit == null)
            //    throw new CustomException("Can't find Unit");
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
            unit = await service.SmartDataService.GetSingleObjectStore("WebSmartGetData", _tenant.ConnectionString(), param);

            var paramObj = new
            {
                Parameter = "Catalog",
                TableName = "CatalogObject",
                TypeDocument = "",
                FirstOrLast = "",
                CodeUser = "",
                CodeUnit = 888,
                Condition = $"ObjCode = '{user.CodeUser}'",
            };
            obj = await service.SmartDataService.GetSingleObjectStore("WebSmartGetData", _tenant.ConnectionString(), paramObj);
        }

        string role = string.Empty;
        if (user.IsAdmin)
            role = Roles.Admin;
        else
        {
            role = obj?.DataType ?? Roles.User;
        }
        if (!codeUnit.HasValue || codeUnit == 0) codeUnit = user.CodeUnit;

        return new List<Claim>(20)
        {
            //new Claim(JwtRegisteredClaimNames.Iss, "https://localhost:5001"),
            new(ClaimTypeCustom.CodeUser, user.CodeUser),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypeCustom.CodeUnit, codeUnit.ToString()!),
            new(ClaimTypes.Name, user.NameUser!),
            new(ClaimTypeCustom.UnitNane, unit?.NameUnit ?? ""),
            new(ClaimTypeCustom.WarehoseCode, user?.WarehoseCode ?? ""),
            new(ClaimTypes.Role, role),
            new(ClaimTypeCustom.TenantId, multiTenantContextAccessor.MultiTenantContext.TenantInfo!.Identifier),
            new(ClaimTypeCustom.StationId, multiTenantContextAccessor.MultiTenantContext.TenantInfo!.StationId ?? ""),
            new(ClaimTypeCustom.Decription, multiTenantContextAccessor.MultiTenantContext.TenantInfo?.Notes ?? string.Empty)
        };
    }
}