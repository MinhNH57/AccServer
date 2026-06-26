using System.Security.Claims;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Infrastructure.Constants.ClaimType;

namespace SmartAccCloud.Infrastructure.Authentication;

public class CurrentUser(IApplicationDbContext context) : ICurrentUser
{
    private ClaimsPrincipal? _user;

    public string? TenantId => IsAuthenticated ? _user?.FindFirstValue(ClaimTypeCustom.TenantId) : string.Empty;

    public string? TenantName { get; }

    public string? Role => IsAuthenticated ? _user?.FindFirstValue(ClaimTypes.Role) : string.Empty;
    public string? CodeUser => IsAuthenticated ? _user?.FindFirstValue(ClaimTypeCustom.CodeUser) : string.Empty;

    public int CodeUnit => IsAuthenticated ? int.Parse(_user?.FindFirstValue(ClaimTypeCustom.CodeUnit) ?? "0") : 0;
    public string? UnitName => (context.CatalogUnit.FirstOrDefault(c => c.CodeUnit == CodeUnit))?.NameUnit ?? string.Empty;
    public string? Description => IsAuthenticated ? _user?.FindFirstValue(ClaimTypeCustom.Decription) : string.Empty;

    public void SetCurrentUser(ClaimsPrincipal user)
    {
        ArgumentNullException.ThrowIfNull(user);
        _user = user;
    }

    private bool IsAuthenticated => _user?.Identity?.IsAuthenticated is true;

}