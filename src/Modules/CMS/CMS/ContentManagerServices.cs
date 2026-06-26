using BuildingBlocks.Caching;
using BuildingBlocks.Web;
using CMS.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CMS;

public class CmsServices(
    [FromServices] CmsDbContext context,
    [FromServices] IMediator mediator,
    RedisCacheService cacheService,
    [FromServices]  ICurrentUser currentUser)
{
    public CmsDbContext Context { get; } = context;
    public IMediator Mediator { get; } = mediator;
    public RedisCacheService CacheService { get; } = cacheService;
    public ICurrentUser CurrentUser { get; } = currentUser;
    //public IOptions<CmsOptions> Options { get; } = options;
    //public ILogger<CmsServices> Logger { get; } = logger;
};