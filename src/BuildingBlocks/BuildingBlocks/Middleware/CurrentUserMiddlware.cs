using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Middleware;

public class CurrentUserMiddlware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ICurrentUser currentUser)
    {
        currentUser.SetCurrentUser(context.User);
        //var tenant = context.GetMultiTenantContext<TenantInfoCustomize>().TenantInfo;
        await next(context);
    }
}
