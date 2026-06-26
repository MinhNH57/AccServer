using Finbuckle.MultiTenant.Abstractions;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Infrastructure.Caching;

namespace SmartAccCloud.Infrastructure.Persistence.Multitenant;

public  class MultitenantStoreCustomize(MasterDbContext context, IDistributedCache cache) : IMultiTenantStore<TenantInfoCustomize>
{
    public async Task<bool> TryAddAsync(TenantInfoCustomize tenantInfo)
    {
        ArgumentNullException.ThrowIfNull(tenantInfo);

        if (string.IsNullOrEmpty(tenantInfo.Identifier))
            throw new ArgumentNullException(nameof(tenantInfo.Identifier));

        var customer = await TryGetByIdentifierAsync(tenantInfo.Identifier);
        if (customer is not null)
        {
            await context.InfoCustomer.AddAsync(tenantInfo);
            var isSuccess = await context.SaveChangesAsync() > 0;
            if (isSuccess)
            {
                await cache.RemoveCacheAsync(GetTenantCacheKey("all"));
                return true;
            }
        }

        return false;
    }

    public async Task<bool> TryUpdateAsync(TenantInfoCustomize tenantInfo)
    {
        ArgumentNullException.ThrowIfNull(tenantInfo);
        ArgumentNullException.ThrowIfNull(tenantInfo.Identifier);

        var customer = await TryGetByIdentifierAsync(tenantInfo.Identifier);
        if (customer is null) return false;

        customer.StringConnection = tenantInfo.StringConnection;
        customer.ShortName = tenantInfo.ShortName;
        customer.ServerName = tenantInfo.ServerName;
        customer.DatabaseName = tenantInfo.DatabaseName;
        customer.YearWork = tenantInfo.YearWork;
        customer.IsActive = tenantInfo.IsActive;
        customer.CompanyAddress = tenantInfo.CompanyAddress;
        customer.CompanyName = tenantInfo.CompanyName;

        context.InfoCustomer.Update(tenantInfo);
        var isSuccess = await context.SaveChangesAsync() > 0;
        if (isSuccess)
        {
            await cache.RemoveCacheAsync(GetTenantCacheKey(tenantInfo.Identifier));
            await cache.RemoveCacheAsync(GetTenantCacheKey("all"));
            return true;
        }
        return false;
    }

    public async Task<bool> TryRemoveAsync(string identifier)
    {
        ArgumentNullException.ThrowIfNull(identifier);

        var customer = await TryGetByIdentifierAsync(identifier);
        if (customer is null) return false;
        context.InfoCustomer.Remove(customer);
        var isSuccess = await context.SaveChangesAsync() > 0;
        if (isSuccess)
        {
            await cache.RemoveCacheAsync(GetTenantCacheKey(identifier));
            await cache.RemoveCacheAsync(GetTenantCacheKey("all"));
            return true;
        }
        return false;
    }

    public async Task<TenantInfoCustomize?> TryGetByIdentifierAsync(string identifier)
    {
        ArgumentNullException.ThrowIfNull(identifier);
        var infoCustomer = await cache.GetOrCreateAsync(GetTenantCacheKey(identifier), async () =>
        {
            return await context.InfoCustomer.FirstOrDefaultAsync(c => c.Identifier == identifier);
        }, new DistributedCacheEntryOptions()
        {
            SlidingExpiration = null, 
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        });

        return infoCustomer;
    }

    public Task<TenantInfoCustomize?> TryGetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TenantInfoCustomize>> GetAllAsync()
    {
        var listInfoCustomer = await cache.GetOrCreateAsync(GetTenantCacheKey("all"),
            async () => await context.InfoCustomer.ToListAsync(),
            new DistributedCacheEntryOptions() { SlidingExpiration = null, AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)});

        return listInfoCustomer;
    }
    
    private string GetTenantCacheKey(string id)
    {
        return $"KT:tenant:{id}:info";
    }
}