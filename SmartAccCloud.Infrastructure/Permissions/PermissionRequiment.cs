using Microsoft.AspNetCore.Authorization;

namespace SmartAccCloud.Infrastructure.Permissions;

public class PermissionRequiment(string permission) : IAuthorizationRequirement
{
    public string Permission { get;  } = permission;
}