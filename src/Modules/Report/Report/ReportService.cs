using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Report.Infrastructure;

namespace Report;

public class ReportService( 
    [FromServices] ReportDbContext dbContext,
    [FromServices] SmartDataServices smartDataServices,
    [FromServices] IDistributedCache cache,
    [FromServices] IMultiTenantContextAccessor tenantContextAccessor,
    [FromServices] ICurrentUser currentUser,
    [FromServices] IMediator mediator)
{
    public ReportDbContext DbContext => dbContext;
    public SmartDataServices SmartDataServices => smartDataServices;
    public IDistributedCache Cache => cache;
    public IMultiTenantContextAccessor TenantContextAccessor => tenantContextAccessor;
    public ICurrentUser CurrentUser => currentUser;
    public IMediator Mediator => mediator;
}