using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using MediatR;

namespace Catalog.Base;

public class CatalogService(
    CatalogDbContext dbContext,
    //CatalogFundContext dbContextFund,
    [FromServices] SmartDataServices smartDataServices,
    [FromServices] IDistributedCache cache,
    [FromServices] IMultiTenantContextAccessor tenantContextAccessor,
    [FromServices] ICurrentUser currentUser,
    RedisCacheService cacheService,
    [FromServices] IMediator mediator,
    ILogger<CatalogService> logger)
{
    public CatalogDbContext DbContext => dbContext;
    //public CatalogFundContext DbContextFund => dbContextFund;
    public SmartDataServices SmartDataServices => smartDataServices;
    public IDistributedCache Cache => cache;
    public IMultiTenantContextAccessor TenantContextAccessor => tenantContextAccessor;
    public ICurrentUser CurrentUser => currentUser;
    public  RedisCacheService CacheService => cacheService;
    public ILogger<CatalogService> Logger => logger;
    public IMediator Mediator => mediator;
}