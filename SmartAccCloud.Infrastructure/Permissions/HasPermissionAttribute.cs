using Microsoft.AspNetCore.Authorization;
using SmartAccCloud.Infrastructure.Constants.Permissions;

namespace SmartAccCloud.Infrastructure.Permissions;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string action, string function)
    {
        Policy = CustomPermission.NameFor(action, function);
    }

}