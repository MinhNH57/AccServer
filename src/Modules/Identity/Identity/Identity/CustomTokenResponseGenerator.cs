using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Validation;

namespace Identity.Identity;

public class CustomTokenResponseGenerator : ITokenResponseGenerator
{
    public Task<TokenResponse> ProcessAsync(TokenRequestValidationResult validationResult)
    {
        return Task.FromResult(new TokenResponse());
    }
}