using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;

namespace Identity.Identity;

public class TestResponseGenerator : AuthorizeInteractionResponseGenerator
{
    public TestResponseGenerator(IdentityServerOptions options, IClock clock, ILogger<AuthorizeInteractionResponseGenerator> logger, IConsentService consent, IProfileService profile) : base(options, clock, logger, consent, profile)
    {
    }

    protected override async Task<InteractionResponse> ProcessLoginAsync(ValidatedAuthorizeRequest request)
    {
        Console.WriteLine("Triggered!");
        var result = await base.ProcessLoginAsync(request);
        if (!result.IsLogin && !result.IsError)
        {
            // check EULA database
            //var mustShowEulaPage = !HasUserAcceptedEula(request.Subject);
            //if (mustShowEulaPage)
            //{
            //}
        }
        result = new InteractionResponse
        {
            RedirectUrl = "https://acctest.ssoftware.vn/login"
        };

        return result;
    }
}