using Finbuckle.MultiTenant.Abstractions;
using SmartAccCloud.Application.Interfaces.Multitenant;
using SmartAccCloud.Application.Models.Tenant;
using SmartAccCloud.Application.Models.Users;


namespace SmartAccCloud.Infrastructure.Tenants;

public class TenantService(IMultiTenantStore<TenantInfoCustomize> tenantStore, IDistributedCache cache) : IMultitenantService
{
    public async Task<Result<List<TenantInfoCustomize>>> GetAllTenantActive()
    {
        var listTenant = await tenantStore.GetAllAsync();
        var listActive = listTenant.Where(c => c.IsActive).ToList();

        return Result<List<TenantInfoCustomize>>.Success(listActive);
    }

    public async Task<Result<TenantDto>> GetTenantInfoById(string id)
    {
        var tenant = await tenantStore.TryGetByIdentifierAsync(id);
        if (tenant is null)
            return Result<TenantDto>.Failure(new Error("404", "Không tìm thấy đơn vị"));

        var dto = new TenantDto()
        {
            CompanyId = tenant.CompanyId,
            CompanyAddress = tenant.CompanyAddress,
            ShortName = tenant.ShortName
        };

        return Result<TenantDto>.Success(dto);
    }

    public Result<bool> AddTenant()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeactivateTenant(string idTenant)
    {
        var tenant = await tenantStore.TryGetByIdentifierAsync(idTenant);
        if (tenant is null)
            return Result<bool>.Failure(new Error("404", "Không tìm thấy đơn vị"));

        tenant.IsActive = false;
        var isSuccess = await tenantStore.TryUpdateAsync(tenant);

        return isSuccess
            ? Result<bool>.Success(isSuccess)
            : Result<bool>.Failure(new Error("400", "Cập nhật thất bại"));
    }
}