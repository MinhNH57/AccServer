using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using MassTransit;
using RefNo.API.Infrastructure;
using RefNo.API.Model;
using System.Threading.Tasks;

namespace RefNo.API.IntegrationEvents.EventHandling;

public class FixedAssetCreatedSuccessfullyIntegrationEventHandler(
    RefNoDbContext dbContext,
    IMultiTenantStore<TenantInfoCustomize> tenantStore,
    IMultiTenantContextSetter multiTenantContextSetter)
    : IConsumer<ICreatedSuccessfully>
{
    public async Task Consume(ConsumeContext<ICreatedSuccessfully> context)
    {
        string tenantId = context.Headers.Get<string>(TenantConstant.TenantIdHeader)!;

        var tenantInfo = await tenantStore.TryGetByIdentifierAsync(tenantId);
        multiTenantContextSetter.MultiTenantContext = new MultiTenantContext<TenantInfoCustomize> { TenantInfo = tenantInfo };

        var message = context.Message;

        var numberingRule = await dbContext.NumberingRules.FirstOrDefaultAsync(x => x.RefTypeCategory == message.RefType);
        if(numberingRule != null)
        {
            numberingRule.Value = numberingRule.Value + 1;
        }

        await dbContext.SaveChangesAsync();
    }
}