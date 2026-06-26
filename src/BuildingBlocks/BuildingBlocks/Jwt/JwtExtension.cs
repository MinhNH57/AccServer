using System.Security.Cryptography;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BuildingBlocks.Jwt;

public static class JwtExtension
{
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        var jwtOption = services.GetOptions<JwtSettings>("JwtSettings");
        if (!File.Exists("key/public_key.xml"))
        {
            throw new SmartException("public key not provider");
        }
        string publicXmlKey = File.ReadAllText("key/public_key.xml");
        RSA rsa = RSA.Create();
        rsa.FromXmlString(publicXmlKey);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                //options.Authority = jwtOption?.Authority;
                // options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Key)),
                    IssuerSigningKey = new RsaSecurityKey(rsa),
                    ValidIssuer = jwtOption.Authority,
                    //ValidAudience = jwtOption.Audience,
                    //IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    //{
                    //    //var cache = services.BuildServiceProvider().GetRequiredService<IMemoryCache>();
                    //    var httpClient = new HttpClient();
                    //    var response = httpClient
                    //        .GetStringAsync("https://localhost:5001/.well-known/jwks.json").Result;
                    //    var keys = new JsonWebKeySet(response).GetSigningKeys();
                    //    var machingKey = keys.Where(c => c.KeyId == "Smart-Software").ToList();
                    //    if (machingKey.Count == 0)
                    //    {
                    //        throw new SecurityTokenException("No matching key found");
                    //    }

                    //    return machingKey;
                       
                    //}
                };
            });

        services.AddAuthorization();
        return services;
    }
}