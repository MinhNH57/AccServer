using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace Identity.Identity;

public class CustomeProfileService : IProfileService
{
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        throw new NotImplementedException();
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        throw new NotImplementedException();
    }
}