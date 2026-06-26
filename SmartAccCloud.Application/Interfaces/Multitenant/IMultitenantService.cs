using SmartAccCloud.Application.Models.Tenant;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Application.Interfaces.Multitenant;

public interface IMultitenantService
{
    Task<Result<List<TenantInfoCustomize>>> GetAllTenantActive();
    Task<Result<TenantDto>> GetTenantInfoById(string id);
    Result<bool> AddTenant();
    Task<Result<bool>> DeactivateTenant(string idTenant);
}