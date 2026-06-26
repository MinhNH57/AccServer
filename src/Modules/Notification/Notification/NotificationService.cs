using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Notification.Data;

namespace Notification;

public class NotificationService(
    NotificationDbContext dbContext,
    //[FromServices] SmartDataServices smartDataServices,
    [FromServices] IDistributedCache cache,
    //[FromServices] IMultiTenantContextAccessor tenantContextAccessor,
    //[FromServices] ICurrentUser currentUser,
    [FromServices] IMediator mediator,
    ILogger<NotificationService> logger)
{
    //public CatalogFundContext DbContextFund => dbContextFund;
    public NotificationDbContext DbContext => dbContext;
    public IDistributedCache Cache => cache;
    //public IMultiTenantContextAccessor TenantContextAccessor => tenantContextAccessor;
    //public ICurrentUser CurrentUser => currentUser;
    public ILogger<NotificationService> Logger => logger;
    public IMediator Mediator => mediator;
}