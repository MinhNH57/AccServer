using Microsoft.AspNetCore.Authorization;

namespace BuildingBlocks.Permission;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string action, string function)
    {
        Policy = CustomPermission.NameFor(action, function);
    }

}