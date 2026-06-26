using BuildingBlocks.Caching;
using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using Catalog.SGas.Infrastructure;
using Finbuckle.MultiTenant.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Catalog.SGas;

public class CatalogSGasService(
    CatalogSGasDbContext dbContext,
    //CatalogFundContext dbContextFund,
    [FromServices] SmartDataServices smartDataServices,
    [FromServices] IDistributedCache cache,
    [FromServices] IMultiTenantContextAccessor tenantContextAccessor,
    [FromServices] ICurrentUser currentUser,
    RedisCacheService cacheService,
    [FromServices] IMediator mediator,
    ILogger<CatalogSGasService> logger)
{
    public CatalogSGasDbContext DbContext => dbContext;
    //public CatalogFundContext DbContextFund => dbContextFund;
    public SmartDataServices SmartDataServices => smartDataServices;
    public IDistributedCache Cache => cache;
    public IMultiTenantContextAccessor TenantContextAccessor => tenantContextAccessor;
    public ICurrentUser CurrentUser => currentUser;
    public RedisCacheService CacheService => cacheService;
    public ILogger<CatalogSGasService> Logger => logger;
    public IMediator Mediator => mediator;
}