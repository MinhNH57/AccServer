using BuildingBlocks.MultiTenancy;
using Catalog.Fund.Infrastructure;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.EventHandlers;

public class CheckCitizenIDNumberConsumer(
    CatalogFundContext dbContext,
    IMultiTenantStore<TenantInfoCustomize> tenantStore,
    IMultiTenantContextSetter multiTenantContextSetter) : IConsumer<List<string>>
{
    public async Task Consume(ConsumeContext<List<string>> context)
    {
        var tenantId = context.Headers.Get<string>("X-Tenant-Id");
        var tenantInfo = await tenantStore.TryGetByIdentifierAsync(tenantId);
        multiTenantContextSetter.MultiTenantContext = new MultiTenantContext<TenantInfoCustomize>()
        { TenantInfo = tenantInfo };

        var listExist = await dbContext.CatalogObject
            .Where(c => context.Message.Contains(c.CitizenIDNumber))
            .ToListAsync();

        await context.RespondAsync<List<string>>(listExist);
    }

}