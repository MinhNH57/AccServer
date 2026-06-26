using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Permission;

public class PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory) : AuthorizationHandler<PermissionRequiment>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequiment requirement)
    {
        var endpoint = context.Resource switch
        {
            HttpContext httpContext => httpContext.GetEndpoint(),
            Endpoint ep => ep,
            _ => null
        };

        string? codeUser = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypeCustom.CodeUser)?.Value;
        string? role = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (!string.IsNullOrWhiteSpace(codeUser) && role == Roles.Admin)
        {
            context.Succeed(requirement);
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        //var permissionnService = scope.ServiceProvider.GetRequiredService<IPermissionnService>();

        //if (await permissionnService.HasPermission(codeUser, requirement.Permission))
        //{
        //    //context.User.Identity.
        //    context.Succeed(requirement);
        //}

    }
}