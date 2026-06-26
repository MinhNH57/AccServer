using BuildingBlocks.Web;
using MassTransit;

public class FixedAssetServices(
    IMapper mapper,
    IMediator mediator,
    IFixedAssetQueries queries,
    IIdentityService identityService,
    ICurrentUser currentUser,
    IPublishEndpoint publishEndpoint,
    ILogger<FixedAssetServices> logger)
{
    public IMapper Mapper { get; set; } = mapper;
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<FixedAssetServices> Logger { get; } = logger;
    public IFixedAssetQueries Queries { get; } = queries;
    public IIdentityService IdentityService { get; } = identityService;
    public ICurrentUser CurrentUser { get; } = currentUser;
    public IPublishEndpoint PublishEndpoint { get; } = publishEndpoint;
}
