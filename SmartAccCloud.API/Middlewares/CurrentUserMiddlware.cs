using SmartAccCloud.Application.Interfaces.Identities;

namespace SmartAccCloud.API.Middlewares;

public class CurrentUserMiddlware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ICurrentUser currentUser)
    {
        currentUser.SetCurrentUser(context.User);
        //var tenant = context.GetMultiTenantContext<TenantInfoCustommize>().TenantInfo;
        await next(context);
    }
}