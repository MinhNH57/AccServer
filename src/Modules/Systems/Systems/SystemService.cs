using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Systems.Infrastructure;

namespace Systems;

public class SystemService(
    SystemDbcontext dbContext,
    [FromServices] IDistributedCache cache,
    [FromServices] IMultiTenantContextAccessor tenantContextAccessor,
    [FromServices] SmartDataServices smartDataServices,
    [FromServices] IMultiTenantStore<TenantInfoCustomize> tenantStore,
    [FromServices] ICurrentUser currentUser,
    [FromServices]IMapper mapper,
    [FromServices] IMediator mediator,
    RedisCacheService cacheService
)
{
    public SystemDbcontext DbContext => dbContext;
    public IDistributedCache Cache => cache;
    public IMultiTenantContextAccessor TenantContextAccessor => tenantContextAccessor;
    public ICurrentUser CurrentUser => currentUser;
    public RedisCacheService CacheService => cacheService;
    public SmartDataServices SmartDataServices => smartDataServices;
    public IMapper Mapper => mapper;
    public IMediator Mediator => mediator;
    public IMultiTenantStore<TenantInfoCustomize> TenantStore => tenantStore;
}