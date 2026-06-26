using Microsoft.AspNetCore.Authorization;

namespace BuildingBlocks.Permission;

public class PermissionRequiment(string permission) : IAuthorizationRequirement
{
    public string Permission { get;  } = permission;
}