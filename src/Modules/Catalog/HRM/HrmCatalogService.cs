using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using MediatR;

namespace Catalog.HRM;

public class HrmCatalogService(
    HrmDbContext dbContext,
    [FromServices] SmartDataServices smartDataServices,
    [FromServices] IDistributedCache cache,
    [FromServices] IMultiTenantContextAccessor tenantContextAccessor,
    [FromServices] ICurrentUser currentUser,
    RedisCacheService cacheService,
    [FromServices] IMediator mediator,
    ILogger<HrmCatalogService> logger)
{
    public HrmDbContext DbContext => dbContext;
    //public CatalogFundContext DbContextFund => dbContextFund;
    public SmartDataServices SmartDataServices => smartDataServices;
    public IDistributedCache Cache => cache;
    public IMultiTenantContextAccessor TenantContextAccessor => tenantContextAccessor;
    public ICurrentUser CurrentUser => currentUser;
    public RedisCacheService CacheService => cacheService;
    public ILogger<HrmCatalogService> Logger => logger;
    public IMediator Mediator => mediator;
}