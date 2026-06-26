using System.Security.Cryptography;
using Carter;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Authentication.Endpoints;

public class JwkApi:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Identity");

        var api = vApi.MapGroup("");

        api.MapGet("/.well-known/jwks.json", GetJwk);
    }

    private IResult GetJwk(HttpContext context)
    {
        if (!File.Exists("key/public_key.xml"))
        {
            throw new SmartException("public key not provider");
        }
        string publicXmlKey = File.ReadAllText("key/public_key.xml");
        RSA rsa = RSA.Create();
        rsa.FromXmlString(publicXmlKey);
        var parameters = rsa.ExportParameters(false);
        var jwk = new JsonWebKey()
        {
            Kty = "RSA",
            E = Base64UrlEncoder.Encode(parameters.Exponent),
            N = Base64UrlEncoder.Encode(parameters.Modulus),
            Use = "sig",
            Kid = "Smart-Software",
            Alg = SecurityAlgorithms.RsaSha256
        };

        return Results.Json(new { keys = new[] { jwk } });
    }
}